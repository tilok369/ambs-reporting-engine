
using Ambs.Reporting.Engine.GraphModels;

namespace Ambs.Reporting.Logic.Factories;

public class GraphFactory
{
    public IGraph GetGraph(int type) => type switch
    {
        1 => new PieGraph(),
        2 => new ColumnGraph(),
        _ => new BarGraph()
    };
}
