namespace Shapefile2Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using DotSpatial.Data;
    using DotSpatial.Projections;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    public class ShapefileProcessor
    {
        #region Constants and Fields

        private static int currentImportedShapeCount = 0;

        private DateTime importStartDateTime;

        private DataColumn[] columns;

        private List<ShapeHeader> shapeHeaders;

        private Shapefile shapefile;

        #endregion

        #region Constructors and Destructors

        public ShapefileProcessor()
        {
            this.CreateSpatialIndex = true;
            this.ParallelImport = true;
            this.ShapeDataColumnName = "Geom";
            this.SpatialDataType = SpatialDataType.Geography;
            this.Srid = 4326;
        }

        #endregion

        #region Public Properties

        public IEnumerable<DataColumn> Columns
        {
            get
            {
                return this.columns;
            }
        }

        public string ConnectionString { get; set; }

        public bool CreateSpatialIndex { get; set; }

        public List<AttributeMapping> Mapping { get; private set; }

        public bool ParallelImport { get; set; }

        public string ShapeDataColumnName { get; set; }

        public SpatialDataType SpatialDataType { get; set; }

        public int? Srid { get; set; }

        public string TableName { get; set; }

        public int ShapeCount
        {
            get
            {
                return this.shapeHeaders.Count;
            }
        }

        public IProgressHandler ProgressHandler { get; set; }

        #endregion

        #region Public Methods

        public bool CanImport()
        {
            return !string.IsNullOrWhiteSpace(this.ConnectionString) && !string.IsNullOrWhiteSpace(this.TableName)
                   && !string.IsNullOrWhiteSpace(this.ShapeDataColumnName)
                   && (this.SpatialDataType != SpatialDataType.Geography || this.Srid.HasValue);
        }

        public void Import()
        {
            this.importStartDateTime = DateTime.Now;

            this.CreateTable();

            string insertStatement = this.GenerateInsertStatement();

            currentImportedShapeCount = 0;

            if (this.ParallelImport)
            {
                Parallel.For(
                    0,
                    this.shapeHeaders.Count,
                    i =>
                        {
                            this.ImportShape(i, insertStatement);
                            Interlocked.Increment(ref currentImportedShapeCount);

                            if (this.ProgressHandler != null && currentImportedShapeCount % 100 == 0)
                            {
                                this.ReportProgress();
                            }
                        });
            }
            else
            {
                for (int i = 0; i < this.shapeHeaders.Count; i++)
                {
                    this.ImportShape(i, insertStatement);
                    currentImportedShapeCount++;

                    if (this.ProgressHandler != null && currentImportedShapeCount % 100 == 0)
                    {
                        this.ReportProgress();
                    }
                }
            }

            if (this.ProgressHandler != null)
            {
                this.ProgressHandler.Progress("Import", 0, string.Format("Done: imported {0} shapes.", currentImportedShapeCount));
            }
        }

        private void ReportProgress()
        {
            var percentComplete = (int)(this.CalculatePercentComplete() * 100.0);

            if (percentComplete > 1)
            {
                this.ProgressHandler.Progress(
                    "Import",
                    percentComplete,
                    string.Format("Estimated time remaining: {0:hh\\:mm\\:ss}", this.CalculateTimeRemaining()));
            }
        }

        private void ImportShape(int index, string insertStatement)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                var command = new SqlCommand(insertStatement, conn);

                Shape shape = this.shapefile.GetShape(index, true);

                if (this.SpatialDataType == SpatialDataType.Geography)
                {
                    // Automatically reproject to WGS84 if Geography was selected
                    ProjectionInfo sourceProjection = this.shapefile.Projection;
                    ProjectionInfo destProjection = KnownCoordinateSystems.Geographic.World.WGS1984;

                    if (!sourceProjection.ToString().Equals(destProjection.ToString()))
                    {
                        shape.Z = new double[shape.Vertices.Length];
                        Reproject.ReprojectPoints(shape.Vertices, shape.Z, sourceProjection, destProjection, 0, shape.Vertices.Length / 2);
                    }

                    if (this.shapefile is PolygonShapefile)
                    {
                        // Test for clockwise-ness (for the SQL geography type, shapes must be in a counter-clockwise orientation)
                        if (this.IsPolygonClockwise(shape))
                        {
                            shape.Vertices = this.ReversePolygonVertexOrientation(shape);
                        }
                    }
                }

                command.Parameters.AddWithValue(this.ShapeDataColumnName, shape.ToGeometry().ToString());

                foreach (var attr in this.Mapping.Where(a => a.IncludeInImport))
                {
                    command.Parameters.AddWithValue(attr.MappedColumnName, shape.Attributes[attr.ColumnIndex]);
                }

                command.ExecuteNonQuery();
            }
        }

        private double[] ReversePolygonVertexOrientation(Shape polygon)
        {
            var reversed = new double[polygon.Vertices.Length];
            for (int j = 0; j < reversed.Length; j += 2)
            {
                reversed[j] = polygon.Vertices[polygon.Vertices.Length - j - 2];
                reversed[j + 1] = polygon.Vertices[polygon.Vertices.Length - j - 1];
            }
            return reversed;
        }

        /// <summary>
        /// http://paulbourke.net/geometry/clockwise/index.html
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private bool IsPolygonClockwise(Shape polygon)
        {
            // Calculate the area; positive result -> counter-clockwise; otherwise clockwise
            double area = 0.0;
            for (int i = 0; i < polygon.Vertices.Length - 2; i += 2)
            {
                area += (polygon.Vertices[i] * polygon.Vertices[i + 3]) - (polygon.Vertices[i + 2] * polygon.Vertices[i + 1]);
            }

            // To calculate actual area, need to divide by two; here we just need to know the sign (+/-)
            // area /= 2.0;
            return area < 0.0;
        }

        public void LoadShapefile(string fileName, IProgressHandler progressHandler)
        {
            this.shapefile = Shapefile.OpenFile(fileName, progressHandler);

            progressHandler.Progress(string.Empty, 100, "Reading projection...");
            this.shapefile.ReadProjection();

            progressHandler.Progress(string.Empty, 100, "Reading shape headers...");
            this.shapeHeaders = this.shapefile.ReadIndexFile(Path.ChangeExtension(fileName, "shx"));

            progressHandler.Progress(string.Empty, 100, "Reading columns...");
            this.columns = this.shapefile.GetColumns();

            this.Mapping =
                this.columns.Select(
                    (column, index) =>
                    new AttributeMapping
                        {
                            ColumnIndex = index, 
                            ShapefileAttributeName = column.ColumnName, 
                            MappedColumnName = column.ColumnName,
                            DataType = column.DataType,
                            IsNullable = column.AllowDBNull,
                            IncludeInImport = true,
                            MaxLength = column.MaxLength
                        }).ToList();

            this.TableName = Path.GetFileNameWithoutExtension(fileName);
        }

        #endregion

        #region Methods

        private void CreateTable()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                var serverConnection = new ServerConnection(conn);
                var server = new Server(serverConnection);
                var db = server.Databases[serverConnection.DatabaseName];

                if (db.Tables.Contains(this.TableName))
                {
                    db.Tables[this.TableName].Drop();
                }

                var table = new Table(db, this.TableName);

                // Create the identity column
                var idColumn = new Column(table, "ID")
                    {
                        DataType = DataType.Int,
                        Nullable = false,
                        Identity = true,
                        IdentitySeed = 1,
                        IdentityIncrement = 1
                    };
                table.Columns.Add(idColumn);

                // Create the PK index on the identity column
                var primaryKeyIndex = new Index(table, "PK_" + this.TableName)
                    {
                        IndexKeyType = IndexKeyType.DriPrimaryKey
                    };
                primaryKeyIndex.IndexedColumns.Add(new IndexedColumn(primaryKeyIndex, "ID"));
                table.Indexes.Add(primaryKeyIndex);

                // Create the shape column
                var geomColumn = new Column(table, this.ShapeDataColumnName)
                    {
                        DataType =
                            this.SpatialDataType == SpatialDataType.Geography ? DataType.Geography : DataType.Geometry,
                        Nullable = false
                    };
                table.Columns.Add(geomColumn);

                // Create the spatial index
                if (this.CreateSpatialIndex)
                {
                    var spatialIndex = new Index(table, "IX_" + this.TableName + "_Spatial")
                        {
                            SpatialIndexType =
                                this.SpatialDataType == SpatialDataType.Geography
                                    ? SpatialIndexType.GeographyGrid
                                    : SpatialIndexType.GeometryGrid,
                            IndexKeyType = IndexKeyType.None
                        };

                    spatialIndex.IndexedColumns.Add(new IndexedColumn(spatialIndex, this.ShapeDataColumnName));
                    table.Indexes.Add(spatialIndex);
                }

                // Create all of the mapped columns
                foreach (var attr in this.Mapping.Where(a => a.IncludeInImport).OrderBy(a => a.ColumnIndex))
                {
                    var column = new Column(table, attr.MappedColumnName, this.MapDataColumnTypeToSqlDataType(attr.DataType, attr.MaxLength))
                        {
                            Nullable = attr.IsNullable
                        };

                    table.Columns.Add(column);
                }

                table.Create();
            }
        }

        private string GenerateInsertStatement()
        {
            if (this.Mapping.Count == 0)
            {
                return string.Format(
                    @"BEGIN TRY
    INSERT INTO [{0}]({1}) VALUES({2});
END TRY
BEGIN CATCH
    -- Retry by converting from a geometry instance
    INSERT INTO [{0}]({1}) VALUES({3});
END CATCH",
                    this.TableName,
                    string.Format("[{0}]", this.ShapeDataColumnName),
                    this.GenerateInsertGeographyClause(),
                    this.GenerateInsertGeographyFromGeometryClause());
            }
            
            string setClause = string.Format("[{0}]", this.ShapeDataColumnName) + ","
                               + string.Join(
                                   ",",
                                   this.Mapping.Where(a => a.IncludeInImport).OrderBy(a => a.ColumnIndex).Select(
                                       s => string.Format("[{0}]", s.MappedColumnName)));

            string valuesClause = this.GenerateInsertGeographyClause() + ","
                                  +
                                  string.Join(
                                      ",",
                                      this.Mapping.Where(a => a.IncludeInImport).OrderBy(a => a.ColumnIndex).Select(
                                          s => string.Format("@{0}", s.MappedColumnName)));

            string alternateValuesClause = this.GenerateInsertGeographyFromGeometryClause() + ","
                                  +
                                  string.Join(
                                      ",",
                                      this.Mapping.Where(a => a.IncludeInImport).OrderBy(a => a.ColumnIndex).Select(
                                          s => string.Format("@{0}", s.MappedColumnName)));
            return string.Format(
                @"BEGIN TRY
INSERT INTO [{0}]({1}) VALUES({2});
END TRY
BEGIN CATCH 
    -- Retry by converting from a geometry instance
    INSERT INTO [{0}]({1}) VALUES({3});
END CATCH",
                this.TableName,
                setClause,
                valuesClause,
                alternateValuesClause);
        }

        private string GenerateInsertGeographyClause()
        {
            return string.Format(
                "{0}::STGeomFromText(@{1}, {2})",
                this.SpatialDataType == SpatialDataType.Geography ? "geography" : "geometry",
                this.ShapeDataColumnName,
                this.Srid);
        }

        private string GenerateInsertGeographyFromGeometryClause()
        {
            return string.Format(
                "geography::STGeomFromWKB(geometry::STGeomFromText(@{0}, {1}).MakeValid().Reduce(0.0000001).STUnion(geometry::STGeomFromText(@{0}, {1}).MakeValid().Reduce(0.0000001).STStartPoint()).STAsBinary(),{1})",
                this.ShapeDataColumnName,
                this.Srid);
        }

        private DataType MapDataColumnTypeToSqlDataType(Type type, int maxLength)
        {
            if (type == typeof(short))
            {
                return DataType.SmallInt;
            }
            
            if (type == typeof(int))
            {
                return DataType.Int;
            }

            if (type == typeof(long))
            {
                return DataType.BigInt;
            }

            if (type == typeof(DateTime))
            {
                return DataType.DateTime;
            }

            if (type == typeof(bool))
            {
                return DataType.Bit;
            }

            // Default to nvarchar
            if (maxLength < 0)
            {
                return DataType.NVarCharMax;
            }

            return DataType.NVarChar(maxLength);
        }

        private double CalculatePercentComplete()
        {
            if (this.ShapeCount < 1)
            {
                return double.NegativeInfinity;
            }

            return currentImportedShapeCount / (double)this.ShapeCount;
        }

        private TimeSpan CalculateTimeRemaining()
        {
            var durationSoFar = DateTime.Now - this.importStartDateTime;
            var remainingTime = new TimeSpan((long)(durationSoFar.Ticks / this.CalculatePercentComplete())) - durationSoFar;
            return remainingTime;
        }
        #endregion
    }
}