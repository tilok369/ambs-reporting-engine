using Ambs.Reporting.ViewModel.Reponse.Dashboard;
using Ambs.Reporting.ViewModel.Request.Dashboard;

namespace Ambs.Reporting.Logic.Implementations;

public class DashboardLogic : IDashboardLogic
{
    private readonly IDashboardService _dashboardService;

    public DashboardLogic(IDashboardService dashboardService)
    {
        this._dashboardService = dashboardService;
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
                BrandImage = dashboard.BrandImage
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
}