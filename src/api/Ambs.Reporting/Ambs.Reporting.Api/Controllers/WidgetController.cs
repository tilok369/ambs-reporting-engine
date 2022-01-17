using Ambs.Reporting.ViewModel.Reponse.Widget;
using Ambs.Reporting.ViewModel.Request.Widget;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/widget")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetLogic _widgetLogic;

        public WidgetController(IWidgetLogic widgetLogic)
        {
            this._widgetLogic = widgetLogic;
        }

        [HttpGet("{id}")]
        public WidgetResponseDTO Get(long id)
        {
            return _widgetLogic.Get(id);
        }

        [HttpGet()]
        public IList<WidgetResponseDTO> GetAll(long dashboardId, int page, int size)
        {
            return _widgetLogic.GetAll(dashboardId, page, size);
        }

        [HttpPost()]
        public WidgetPostResponseDTO Save(WidgetPostRequestDTO dashboard)
        {
            return _widgetLogic.Save(dashboard);
        }
    }
}
