namespace Shapefile2Sql
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using DotSpatial.Data;

    public class ShapefileProcessor
    {
        #region Constants and Fields

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
            foreach (AttributeMapping mapItem in this.Mapping)
            {
                MessageBox.Show(mapItem.MappedColumnName);
            }
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
                            MappedColumnName = column.ColumnName
                        }).ToList();

            this.TableName = Path.GetFileNameWithoutExtension(fileName);
        }

        #endregion
    }
}