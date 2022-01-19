
namespace Ambs.Reporting.Engine.GraphModels;

public class PieDataPoint : IDataPoint
{
    public double Y { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
