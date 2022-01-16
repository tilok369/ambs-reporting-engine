using Ambs.Reporting.ViewModel.Reponse.Dashboard;
using Ambs.Reporting.ViewModel.Request.Dashboard;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IDashboardLogic
{
    DashboardResponseDTO Get(long id);
    IList<DashboardResponseDTO> GetAll(int page, int size);
    DashboardPostResponseDTO Save(DashboardPostRequestDTO dashboard);
}
