using Ambs.Reporting.ViewModel.Reponse.Dashboard;
using Ambs.Reporting.ViewModel.Request.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardLogic _dashboardLogic;

        public DashboardController(IDashboardLogic dashboardLogic)
        {
            this._dashboardLogic = dashboardLogic;
        }

        [HttpGet("{id}")]
        public DashboardResponseDTO Get(long id)
        {
            return _dashboardLogic.Get(id);
        }

        [HttpGet()]
        public IList<DashboardResponseDTO> GetAll(int page, int size)
        {
            return _dashboardLogic.GetAll(page, size);
        }

        [HttpPost()]
        public DashboardPostResponseDTO Save(DashboardPostRequestDTO dashboard)
        {
            return _dashboardLogic.Save(dashboard);
        }
    }
}
