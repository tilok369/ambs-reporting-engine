namespace Ambs.Reporting.Engine.Model;
public class ExportData
{
    public List<string> Columns { get; set; }
    public List<List<string>> Rows { get; set; }
    public List<DataLayer> Layers { get; set; }
    public string SheetName { get; set; }
}

