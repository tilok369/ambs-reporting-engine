
using Ambs.Reporting.Utility.Extensions;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;

namespace Ambs.Reporting.Logic.Implementations;

public class GraphicalFeatureLogic : IGraphicalFeatureLogic
{
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    private readonly IReportExportService _reportExportService;

    public GraphicalFeatureLogic(IGraphicalFeatureService graphicalFeatureService, IReportExportService reportExportService)
    {
        this._graphicalFeatureService = graphicalFeatureService;
        this._reportExportService = reportExportService;
    }

    public GraphicalFeatureDTO GetByReport(long reportId, string parameterVals)
    {
        var gf = _graphicalFeatureService.GetByReportId(reportId);
        if (gf == null) return null;

        var data = _reportExportService.GetReportData(gf.Script, System.Data.CommandType.Text, parameterVals.ToSqlParameterVals());

        var gfWithData = new GraphicalFeatureDTO(gf.Id)
        {
            ReportId = gf.ReportId,
            Script = gf.Script,
            GraphType = gf.GraphType,
            Title = gf.Title,
            SubTitle  = gf.SubTitle,
            ShowFilterInfo = gf.ShowFilterInfo,
            ShowLegend = gf.ShowLegend,
            XaxisPrefix = gf.XaxisPrefix,
            XaxisSuffix = gf.XaxisSuffix,
            XaxisTitle = gf.XaxisTitle,
            YaxisPrefix = gf.YaxisPrefix,
            YaxisSuffix = gf.YaxisSuffix,
            YaxisTitle = gf.YaxisTitle,
        };

        return gfWithData;
    }
}