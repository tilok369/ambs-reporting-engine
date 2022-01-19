
using Ambs.Reporting.Utility.Extensions;

namespace Ambs.Reporting.Engine.GraphModels;

public class ColumnGraph : IGraph
{
    public string Type { get; set; } = "column";
    public IList<IDataPoint> DataPoints { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public bool ShowLegend { get; set; } = true;
    public string XaxisTitle { get; set; } = string.Empty;
    public string YaxisTitle { get; set; } = string.Empty;
    public string XaxisSuffix { get; set; } = string.Empty;
    public string XaxisPrefix { get; set; } = string.Empty;
    public string YaxisSuffix { get; set; } = string.Empty;
    public string YaxisPrefix { get; set; } = string.Empty;

    public void SetDataPoints(List<string> columns, List<List<string>> rows)
    {
        DataPoints = new List<IDataPoint>();
        foreach (var row in rows)
        {
            DataPoints.Add(new ColumnDataPoint
            {
                Label = row.Any() ? row.First() : string.Empty,
                Y = row.Count > 1 ? row[1].ToDouble() : 0,
            });
        }
    }
}
