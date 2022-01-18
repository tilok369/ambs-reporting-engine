
namespace Ambs.Reporting.Logic.GraphModels;

public interface IGraph
{
    public string Type { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public bool ShowLegend { get; set; }
    public string XaxisTitle { get; set; }
    public string YaxisTitle { get; set; }
    public string XaxisSuffix { get; set; }
    public string XaxisPrefix { get; set; }
    public string YaxisSuffix { get; set; }
    public string YaxisPrefix { get; set; }
    public IList<IDataPoint> DataPoints { get;}

    public void SetDataPoints(List<string> columns, List<List<string>> rows);

}
