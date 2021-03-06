namespace Ambs.Reporting.Engine.Model;
public class ReportData
{
    public List<string> Columns { get; set; }
    public List<List<string>> Rows { get; set; }
    public string ReportName { get; set; }
    public DateTime ReportCacheTime { get; set; }
}