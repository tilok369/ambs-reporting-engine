
namespace Ambs.Reporting.Logic.GraphModels;

public class ColumnDataPoint : IDataPoint
{
    public double Y { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Color { get; set; } = "#22A1C7";
}
