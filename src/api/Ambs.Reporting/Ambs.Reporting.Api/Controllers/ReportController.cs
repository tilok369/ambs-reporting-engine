using Ambs.Reporting.ViewModel.Request.Report;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public ReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }
        [HttpGet("id")]
        public IActionResult Get(long id)
        {
            var data = _reportLogic.Get(id);
            if(data!=null)
                return Ok(data);
            return NotFound();
        }
        [HttpGet]
        public IActionResult GetAll(int page, int size)
        {
            var data = _reportLogic.GetAll(page, size);
            if (data != null)
                return Ok(data);
            return NotFound();
        }
        [HttpPost]
        public IActionResult Add(ReportPostRequestDTO report)
        {
            var result= _reportLogic.Add(report);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPut]
        public IActionResult Edit(ReportPostRequestDTO report)
        {
            var result = _reportLogic.Edit(report);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpDelete("id")]
        public IActionResult Delete(long id)
        {
            var result = _reportLogic.Delete(id);
            return result.Success ? Ok(result) : BadRequest();
        }
    }
}
