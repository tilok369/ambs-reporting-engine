
namespace Ambs.Reporting.Engine.GraphModels;

public interface IDataPoint
{
    public double Y { get; set; }
    public string Label { get; set; }
    public string Color { get; set; }
}
