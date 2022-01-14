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
        [HttpPost]
        public async Task<HttpResponseMessage> ExportExcel()
        {
            var data=await _reportLogic.GetReportData();
            var stream = new MemoryStream(data);
            var response = new HttpResponseMessage { Content = new StreamContent(stream) };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Test.xlsx"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = stream.Length;
            return response;

        }
    }
}
