
using Ambs.Reporting.Utility.Extensions;
using Ambs.Reporting.Utility.Globals;

namespace Ambs.Reporting.Engine.GraphModels;

public class PieGraph : IGraph
{
    public string Type { get; set; } = "pie";
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public bool ShowLegend { get; set; } = false;
    public string XaxisTitle { get; set; } = string.Empty;
    public string YaxisTitle { get; set; } = string.Empty;
    public string XaxisSuffix { get; set; } = string.Empty;
    public string XaxisPrefix { get; set; } = string.Empty;
    public string YaxisSuffix { get; set; } = string.Empty;
    public string YaxisPrefix { get; set; } = string.Empty;

    public IList<IDataPoint> DataPoints { get; set; }

    public void SetDataPoints(List<string> columns, List<List<string>> rows)
    {
        DataPoints = new List<IDataPoint>();
        var i = 0;
        foreach (var row in rows)
        {
            DataPoints.Add(new PieDataPoint
            {
                Label = row.Any() ? row.First() : string.Empty,
                Y = row.Count > 1 ? row[1].ToDouble() : 0,
                Color = i < ColorSchema.BrandColors.Count ? ColorSchema.BrandColors[i++] : ""
            });
        }
    }
}