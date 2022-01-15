using Ambs.Reporting.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public ReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }
        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            var data=await _reportLogic.GetReportData();
            var stream = new MemoryStream(data)
            {
                //var response = new HttpResponseMessage { Content = new StreamContent(stream) };
                //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = "Test.xlsx"
                //};
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                //response.Content.Headers.ContentLength = stream.Length;
                //return response;
                Position = 0
            };
            return File(stream, "application/ms-excel", "Test.xlsx");

        }
    }
}
