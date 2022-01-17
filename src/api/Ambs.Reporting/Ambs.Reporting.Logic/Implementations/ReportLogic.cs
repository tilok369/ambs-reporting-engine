using Ambs.Reporting.ViewModel.Reponse.Report;
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
        _reportService=reportService;
        _mapper = mapper;
        _reportFilterService=reportFilterService;
        _tablularFeatureService=tablularFeatureService;
        _graphicalFeatureService=graphicalFeatureService;
    }

    public ReportResponseDTO Get(long id)
    {
        return _mapper.Map<Report,ReportResponseDTO>(_reportService.Get(id));
    }

    public IEnumerable<ReportResponseDTO> GetAll(int page, int size)
    {
        return _mapper.Map<IEnumerable<Report>,IEnumerable<ReportResponseDTO>>(_reportService.GetAll().Take((page - 1)..size));
    }

    public ReportPostResponseDTO Add(ReportPostRequestDTO report)
    {
        var result=_reportService.Add(_mapper.Map<ReportPostRequestDTO, Report>(report));
        if(result.Id==0)
        return new ReportPostResponseDTO
        {
            Id = 0,
            Success = false,
            Message = "Error while saving report"
        };
        //Report Filter Add
        report.ReportFilterList.ForEach(reportFilter=>reportFilter.ReportId=result.Id);
        if(!_reportFilterService.AddAll(_mapper.Map<List<ReportFilterPostRequestDTO>, List<ReportFilter>>(report.ReportFilterList)))
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
        if(report.Type == ReportType.Tabular)
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
        throw new NotImplementedException();
    }

    public ReportPostResponseDTO Delete(long id)
    {
        throw new NotImplementedException();
    }
}
