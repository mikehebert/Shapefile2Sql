namespace Shapefile2Sql
{
    using System;

    public class AttributeMapping
    {
        #region Public Properties

        public int ColumnIndex { get; set; }

        public string MappedColumnName { get; set; }

        public string ShapefileAttributeName { get; set; }

        public Type DataType { get; set; }

        public bool IsNullable { get; set; }

        public bool IncludeInImport { get; set; }

        public int MaxLength { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0} -> {1}", this.ShapefileAttributeName, this.MappedColumnName);
        }

        #endregion
    }
}