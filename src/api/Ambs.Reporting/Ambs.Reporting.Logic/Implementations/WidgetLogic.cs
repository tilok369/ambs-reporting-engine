
using Ambs.Reporting.ViewModel.Reponse.Widget;
using Ambs.Reporting.ViewModel.Request.Widget;

namespace Ambs.Reporting.Logic.Implementations;

public class WidgetLogic : IWidgetLogic
{
    private readonly IWidgetService _widgetService;
    private readonly IDashboardService _dashboardService;

    public WidgetLogic(IWidgetService widgetService, IDashboardService dashboardService)
    {
        this._widgetService = widgetService;
        this._dashboardService = dashboardService;
    }

    public WidgetResponseDTO Get(long id)
    {
        var widget = _widgetService.Get(id);
        if (widget == null) return null;

        var dashboard = _dashboardService.Get(id);
        if(dashboard == null) return null;

        return new WidgetResponseDTO(widget.Id)
        {
            Name = widget.Name,
            DashboardId = widget.DashboardId,
            DashboardName = dashboard.Name,
            Status = widget.Status,
            Type = widget.Type,
            CreatedBy = widget.CreatedBy,
            CreatedOn = widget.CreatedOn,
            UpdatedBy = widget.UpdatedBy,
            UpdatedOn = widget.UpdatedOn,
        };
    }

    public IList<WidgetResponseDTO> GetAll(long dashboardId, int page, int size)
    {
        var widgetList = _widgetService.GetAll();
        var widgets = new List<WidgetResponseDTO>();
        foreach (var widget in widgetList.Where(w => w.DashboardId == dashboardId).Take((page - 1)..size))
        {
            var dashboard = _dashboardService.Get(widget.DashboardId);
            if (dashboard == null) continue;

            widgets.Add(new WidgetResponseDTO(widget.Id)
            {
                Name = widget.Name,
                DashboardId = widget.DashboardId,
                DashboardName= dashboard.Name,
                Status = widget.Status,
                Type = widget.Type,
                CreatedBy = widget.CreatedBy,
                CreatedOn = widget.CreatedOn,
                UpdatedBy = widget.UpdatedBy,
                UpdatedOn = widget.UpdatedOn,
            });
        }

        return widgets;
    }

    public WidgetPostResponseDTO Save(WidgetPostRequestDTO widget)
    {
        try
        {
            var wt = new Widget
            {
                Id = widget.Id,
                CreatedBy = widget.Id == 0 ? "admin" : widget.CreatedBy,
                CreatedOn = widget.Id == 0 ? DateTime.Now : widget.CreatedOn,
                DashboardId = widget.DashboardId,
                Type = widget.Type,
                Name = widget.Name,
                Status = widget.Status,
                UpdatedBy = "admin",
                UpdatedOn = DateTime.Now,
            };
            var result = _widgetService.Save(wt);

            return new WidgetPostResponseDTO
            {
                Id = result?.Id ?? 0,
                Success = result != null,
                Message = result != null ? "Widget saved successfully" : "Error while saving"
            };
        }
        catch (Exception ex)
        {
            return new WidgetPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving: " + ex.Message
            };
        }
    }
}
