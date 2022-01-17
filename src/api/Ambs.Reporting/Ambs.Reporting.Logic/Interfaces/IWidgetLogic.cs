
using Ambs.Reporting.ViewModel.Reponse.Widget;
using Ambs.Reporting.ViewModel.Request.Widget;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IWidgetLogic
{
    WidgetResponseDTO Get(long id);
    IList<WidgetResponseDTO> GetAll(long dashboardId, int page, int size);
    WidgetPostResponseDTO Save(WidgetPostRequestDTO dashboard);
}