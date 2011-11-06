namespace Shapefile2Sql
{
    public class AttributeMapping
    {
        #region Public Properties

        public int ColumnIndex { get; set; }

        public string MappedColumnName { get; set; }

        public string ShapefileAttributeName { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0} -> {1}", this.ShapefileAttributeName, this.MappedColumnName);
        }

        #endregion
    }
}