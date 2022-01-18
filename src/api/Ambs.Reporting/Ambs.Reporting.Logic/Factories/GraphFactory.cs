
using Ambs.Reporting.Logic.GraphModels;

namespace Ambs.Reporting.Logic.Factories;

public class GraphFactory
{
    public IGraph GetGraph(int type) => type switch
    {
        1 => new PieGraph(),
        _ => new ColumnGraph(),
    };
}
