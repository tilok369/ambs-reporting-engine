
using Ambs.Reporting.Engine.GraphModels;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IGraphicalFeatureLogic
{
    IGraph GetByReport(long reportId, string parameterVals);
    Task<byte[]> GetReportExport(string fileName, long reportId, string parameterVals);
}
