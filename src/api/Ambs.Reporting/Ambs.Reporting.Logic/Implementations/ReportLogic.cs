using Ambs.Reporting.DAL.CalculativeModels;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;
using Ambs.Reporting.ViewModel.Reponse.Report;
using Ambs.Reporting.ViewModel.Reponse.ReportFilter;
using Ambs.Reporting.ViewModel.Reponse.TabularFeature;
using Ambs.Reporting.ViewModel.Request;
using Ambs.Reporting.ViewModel.Request.GraphicalFeature;
using Ambs.Reporting.ViewModel.Request.Report;
using Ambs.Reporting.ViewModel.Request.ReportFilter;
using Ambs.Reporting.ViewModel.Request.TabularFeature;
using AutoMapper;
using static Ambs.Reporting.Utility.Enum.ReportEnum;

namespace Ambs.Reporting.Logic.Implementations;
public class ReportLogic : IReportLogic
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    private readonly IReportFilterService _reportFilterService;
    private readonly ITablularFeatureService _tablularFeatureService;
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    public ReportLogic(IReportService reportService
        , IMapper mapper
        , IReportFilterService reportFilterService
        , ITablularFeatureService tablularFeatureService
        , IGraphicalFeatureService graphicalFeatureService)
    {
        _reportService = reportService;
        _mapper = mapper;
        _reportFilterService = reportFilterService;
        _tablularFeatureService = tablularFeatureService;
        _graphicalFeatureService = graphicalFeatureService;
    }

    public ReportResponseDTO Get(long id)
    {
        var report = _mapper.Map<Report, ReportResponseDTO>(_reportService.Get(id));
        report.GraphicalFeature = report.Type == ReportType.Graphical ? _mapper.Map<GraphicalFeature, GraphicalFeatureResponseDTO>(_graphicalFeatureService.GetByReportId(report.Id)) : null;
        report.TabularFeature = report.Type == ReportType.Tabular ? _mapper.Map<TabularFeature, TabularFeatureResponseDTO>(_tablularFeatureService.GetByReportId(report.Id)) : null;
        report.ReportFilterList = _mapper.Map<IEnumerable<ReportFilter>, IEnumerable<ReportFilterResponseDTO>>(_reportFilterService.GetReportFiltersByReportId(report.Id));
        return report;
    }

    public IEnumerable<ReportListResponseDTO> GetAll(int page, int size)
    {
        return _mapper.Map<IEnumerable<ReportList>, IEnumerable<ReportListResponseDTO>>(_reportService.GetAll(page, size));
    }

    public ReportPostResponseDTO Add(ReportPostRequestDTO report)
    {
        var result = _reportService.Add(_mapper.Map<ReportPostRequestDTO, Report>(report));
        if (result.Id == 0)
            return new ReportPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving report"
            };
        //Report Filter Add
        report.ReportFilterList.ForEach(reportFilter => reportFilter.ReportId = result.Id);
        if (!_reportFilterService.AddAll(_mapper.Map<List<ReportFilterPostRequestDTO>, List<ReportFilter>>(report.ReportFilterList)))
        {
            _reportService.Delete(report.Id);
            return new ReportPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving report filters"
            };
        }
        long tabularFeatuareAddedId = 0;
        if (report.Type == ReportType.Tabular)
        {
            report.TabularFeature.ReportId = result.Id;
            tabularFeatuareAddedId = _tablularFeatureService.Add(_mapper.Map<TabularFeaturePostRequestDTO, TabularFeature>(report.TabularFeature)).Id;
        }
        else
        {
            report.GraphicalFeature.ReportId = result.Id;
            tabularFeatuareAddedId = _graphicalFeatureService.Add(_mapper.Map<GraphicalFeaturePostRequestDTO, GraphicalFeature>(report.GraphicalFeature)).Id;
        }
        if (tabularFeatuareAddedId == 0)
        {
            _reportService.Delete(report.Id);
            _reportFilterService.DeleteByReportId(report.Id);
            return new ReportPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving tabular feature"
            };
        }
        return new ReportPostResponseDTO
        {
            Id = result?.Id ?? 0,
            Success = result != null,
            Message = result != null ? "Report saved successfully" : "Error while saving"
        };
    }

    public ReportPostResponseDTO Edit(ReportPostRequestDTO report)
    {
        var dbReport = _reportService.Get(report.Id);
        if (_reportFilterService.DeleteByReportId(report.Id) && _reportFilterService.AddAll(_mapper.Map<List<ReportFilterPostRequestDTO>, List<ReportFilter>>(report.ReportFilterList)))
        {
            if ((ReportType)dbReport.Type == report.Type)
            {
                if (report.Type == ReportType.Tabular)
                    _tablularFeatureService.Edit(_mapper.Map<TabularFeaturePostRequestDTO, TabularFeature>(report.TabularFeature));
                else
                    _graphicalFeatureService.Edit(_mapper.Map<GraphicalFeaturePostRequestDTO, GraphicalFeature>(report.GraphicalFeature));
            }
            else
            {
                if (report.Type == ReportType.Tabular)
                {
                    _graphicalFeatureService.DeleteByReportId(report.Id);
                    report.TabularFeature.ReportId = report.Id;
                    _tablularFeatureService.Add(_mapper.Map<TabularFeaturePostRequestDTO, TabularFeature>(report.TabularFeature));
                }
                else
                {
                    _tablularFeatureService.DeleteByReportId(report.Id);
                    report.GraphicalFeature.ReportId = report.Id;
                    _graphicalFeatureService.Add(_mapper.Map<GraphicalFeaturePostRequestDTO, GraphicalFeature>(report.GraphicalFeature));
                }
            }
            var result = _reportService.Edit(_mapper.Map<ReportPostRequestDTO, Report>(report));
            return new ReportPostResponseDTO
            {
                Id = result?.Id ?? 0,
                Success = result != null,
                Message = result != null ? "Report saved successfully" : "Error while saving"
            };
        }
        return new ReportPostResponseDTO
        {
            Id = 0,
            Success = false,
            Message = "Error while saving report filters"
        };

    }

    public ReportPostResponseDTO Delete(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ReportListResponseDTO> GetByWidget(long widgetId, int page, int size)
    {
        return _mapper.Map<IEnumerable<ReportList>, IEnumerable<ReportListResponseDTO>>(_reportService.GetByWidget(widgetId,page, size));
    }
}
