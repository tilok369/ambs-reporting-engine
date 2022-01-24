
using Ambs.Reporting.Engine.GraphModels;
using Ambs.Reporting.Engine.Manager;
using Ambs.Reporting.Logic.Factories;
using Ambs.Reporting.Utility.Extensions;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;
using Microsoft.Data.SqlClient;

namespace Ambs.Reporting.Logic.Implementations;

public class GraphicalFeatureLogic : IGraphicalFeatureLogic
{
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    private readonly IReportExportService _reportExportService;
    private readonly IExporter _exporter;

    public GraphicalFeatureLogic(IGraphicalFeatureService graphicalFeatureService,
        IReportExportService reportExportService,
        IExporter exporter)
    {
        this._graphicalFeatureService = graphicalFeatureService;
        this._reportExportService = reportExportService;
        this._exporter = exporter;
    }

    public IGraph GetByReport(long reportId, string parameterVals)
    {
        var gf = _graphicalFeatureService.GetByReportId(reportId);
        if (gf == null) return null;

        var (columns, rows) = _reportExportService.GetReportData(ConstructCommand(gf.Script, parameterVals.ToDictionary()), System.Data.CommandType.Text, new SqlParameter[] { }).GetAwaiter().GetResult();

        var graph = new GraphFactory().GetGraph(gf.GraphType);      
        graph.SetDataPoints(columns, rows);

        graph.Title = gf.Title??"";
        graph.SubTitle = gf.SubTitle ?? "";
        graph.ShowLegend = gf.ShowLegend ?? false;
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisSuffix = gf.YaxisSuffix ?? "";
        graph.XaxisPrefix = gf.XaxisPrefix ?? "";
        graph.YaxisPrefix = gf.YaxisPrefix ?? "";
        graph.XaxisTitle = gf.XaxisTitle ?? "";
        graph.YaxisTitle = gf.YaxisTitle ?? "";

        return graph;
    }

    private string ConstructCommand(string script, Dictionary<string, string> parameters)
    {
        foreach (var p in parameters)
        {
            script = script.Replace(p.Key, p.Value);
        }

        return script;
    }

    public async Task<byte[]> GetReportExport(string fileName, long reportId, string parameterVals)
    {
        var gf = _graphicalFeatureService.GetByReportId(reportId);
        if (gf == null) return null;

        var (columns, rows) = _reportExportService.GetReportData(ConstructCommand(gf.Script, parameterVals.ToDictionary()), System.Data.CommandType.Text, new SqlParameter[] { }).GetAwaiter().GetResult();

        var graph = new GraphFactory().GetGraph(gf.GraphType);
        graph.SetDataPoints(columns, rows);

        graph.Title = gf.Title ?? "";
        graph.SubTitle = gf.SubTitle ?? "";
        graph.ShowLegend = gf.ShowLegend ?? false;
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisSuffix = gf.YaxisSuffix ?? "";
        graph.YaxisPrefix = gf.YaxisPrefix ?? "";
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisTitle = gf.YaxisTitle ?? "";
        graph.XaxisTitle = gf.XaxisTitle ?? "";

        return await _exporter.ReportExport(fileName, graph);
    }
}