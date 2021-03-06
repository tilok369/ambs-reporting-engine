using Ambs.Reporting.Utility.Enum;
using Ambs.Reporting.ViewModel.Reponse;
using Ambs.Reporting.ViewModel.Reponse.Dashboard;
using Ambs.Reporting.ViewModel.Request.Dashboard;
using AutoMapper;
using Ambs.Reporting.DAL.CalculativeModels;
using static Ambs.Reporting.Utility.Enum.ReportEnum;
using FilterType = Ambs.Reporting.DAL.Entities.FilterType;
using Ambs.Reporting.Utility.Globals;

namespace Ambs.Reporting.Logic.Implementations;

public class DashboardLogic : IDashboardLogic
{
    private readonly IDashboardService _dashboardService;
    private readonly IWidgetService _widgetService;
    private readonly IReportService _reportService;
    private readonly IReportFilterService _reportFilterService;
    private readonly IFilterService _filterService;
    private readonly ITablularFeatureService _tablularFeatureService;
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    private readonly IMapper _mapper;
    private readonly IMetaDataLogic _metaDataLogic;

    public DashboardLogic(IDashboardService dashboardService
        , IWidgetService widgetService
        , IReportService reportService
        , IReportFilterService reportFilterService
        ,IFilterService filterService
        ,ITablularFeatureService tablularFeatureService
        , IGraphicalFeatureService graphicalFeatureService
        , IMapper mapper
        ,IMetaDataLogic metaDataLogic)
    {
        _dashboardService = dashboardService;
        _widgetService = widgetService;
        _reportService = reportService;
        _reportFilterService= reportFilterService;
        _filterService= filterService;
        _graphicalFeatureService= graphicalFeatureService;
        _tablularFeatureService= tablularFeatureService;
        _mapper= mapper;
        _metaDataLogic= metaDataLogic;
    }

    public DashboardResponseDTO Get(long id)
    {
        var dashboard = _dashboardService.Get(id);
        if(dashboard == null) return null;

        return new DashboardResponseDTO(dashboard.Id)
        {
            Name = dashboard.Name,
            IframeUrl = dashboard.IframeUrl,
            Status = dashboard.Status,
            CreatedBy = dashboard.CreatedBy,
            CreatedOn = dashboard.CreatedOn,
            UpdatedBy = dashboard.UpdatedBy,
            UpdatedOn = dashboard.UpdatedOn,
        };
    }

    public IList<DashboardResponseDTO> GetAll(int page, int size)
    {
        var dashboadList = _dashboardService.GetAll();
        var dashboards = new List<DashboardResponseDTO>();
        foreach (var dashboard in dashboadList.Take((page - 1)..size))
        {
            dashboards.Add(new DashboardResponseDTO(dashboard.Id)
            {
                Name = dashboard.Name,
                IframeUrl = dashboard.IframeUrl,
                Status = dashboard.Status,
                CreatedBy = dashboard.CreatedBy,
                CreatedOn = dashboard.CreatedOn,
                UpdatedBy = dashboard.UpdatedBy,
                UpdatedOn = dashboard.UpdatedOn,
            });
        }

        return dashboards;
    }  

    public DashboardPostResponseDTO Save(DashboardPostRequestDTO dashboard)
    {
        try
        {
            var db = new Dashboard
            {
                Id = dashboard.Id,
                CreatedBy = dashboard.Id == 0 ? "admin" : dashboard.CreatedBy,
                CreatedOn = dashboard.Id == 0 ? DateTime.Now : dashboard.CreatedOn,
                IframeUrl = dashboard.IframeUrl,
                Name = dashboard.Name,
                Status = dashboard.Status,
                UpdatedBy = "admin",
                UpdatedOn = DateTime.Now,
            };
            var result = _dashboardService.Save(db);

            return new DashboardPostResponseDTO
            {
                Id = result?.Id ?? 0,
                Success = result != null,
                Message = result != null ? "Dashboard saved successfully" : "Error while saving"
            };
        }
        catch (Exception ex)
        {
            return new DashboardPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving: " + ex.Message
            };
        }
    }
    public DashboardWidgetReportResponseDTO GetDashboard(long dashboardId)
    {
        var dbDashboard = _dashboardService.Get(dashboardId);
        var dashboard = new DashboardWidgetReportResponseDTO(dashboardId) { Name=dbDashboard.Name,IframeUrl=dbDashboard.IframeUrl,Status=dbDashboard.Status};
        var widgets = _widgetService.GetByDashboardId(dashboardId);
        var metaData=_metaDataLogic.GetMetadataByDashboard(dashboardId);
        dashboard.Widgets=new List<WidgetDTO>();
        foreach (var widget in widgets)
        {
            var widgetDto = new WidgetDTO(widget.Id) { DashboardId = dashboard.Id, Name = widget.Name, Status = widget.Status, Reports = new List<ReportDTO>() };
            
            var reports = _reportService.GetByWidgetId(widget.Id);
            foreach(var report in reports)
            {
                var reportDto = new ReportDTO(report.Id) 
                { 
                    Name = report.Name,
                    Status = (bool)report.Status,
                    Type = (ReportType)report.Type,
                    WidgetId = widget.Id,
                    Data = null,
                    Filters = new List<FilterDTO>() 
                };
                var reportFilters=_reportFilterService.GetReportFiltersByReportId(report.Id).OrderBy(rf=>rf.SortOrder).ToList();
                foreach(var reportFilter in reportFilters)
                {
                    var filter = _filterService.Get(reportFilter.FilterId);
                    var filterDto = new FilterDTO(reportFilter.FilterId)
                    {
                        Label = filter.Label,
                        Name = filter.Name,
                        Status = (bool)filter.Status,
                        Parameter = filter.Parameter,
                        DependentParameters = filter.DependentParameters,
                        ReportId = report.Id,
                        Type=filter.Type,
                        DropdownFilters = filter.Type == (int)FilterType.Dropdown && reportFilter.SortOrder==1 
                            ? _mapper.Map<IEnumerable<DropdownFilterCM>, IEnumerable<DropdownFilter>>(_filterService.GetDrowpdownFilterValues(Helper.ConstructScriptForDropdown(filter.Script), metaData?.DataSource)) 
                            : null
                    };
                    reportDto.Filters.Add(filterDto);
                }
                widgetDto.Reports.Add(reportDto);
            }
            dashboard.Widgets.Add(widgetDto);
        }
        return dashboard;
    }
}