
namespace Ambs.Reporting.Logic.GraphModels;

public class PieGraph : IGraph
{
    public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string SubTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool ShowLegend { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string XaxisTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string YaxisTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string XaxisSuffix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string XaxisPrefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string YaxisSuffix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string YaxisPrefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IList<IDataPoint> DataPoints => throw new NotImplementedException();

    public void SetDataPoints(List<string> columns, List<List<string>> rows)
    {
        throw new NotImplementedException();
    }
}