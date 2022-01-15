namespace Ambs.Reporting.Engine.Model
{
    public class Column
    {
        public string ColumnName { get; set; }
        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int Order { get; set; }
        public string ParentColumn { get; set; }
    }
}