
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IGraphicalFeatureLogic
{
    GraphicalFeatureDTO GetByReport(long reportId, string parameterVals);
}
