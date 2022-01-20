using Ambs.Reporting.ViewModel.Reponse.Dashboard;
using Ambs.Reporting.ViewModel.Request.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        [HttpGet("dashboard/{id}")]
        public IActionResult GetDashboard(long id)
        {
            return Ok(_dashboardLogic.GetDashboard(id));
        }


        [HttpPost("photo")]
        public ActionResult UploadPhoto()
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("Resources", "Dashboard");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }

        }
    }
}
