
using Ambs.Reporting.Logic.GraphModels;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IGraphicalFeatureLogic
{
    IGraph GetByReport(long reportId, string parameterVals);
}
