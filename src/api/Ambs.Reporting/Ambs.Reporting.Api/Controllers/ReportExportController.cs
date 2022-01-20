using Microsoft.AspNetCore.Mvc;
using static Ambs.Reporting.Utility.Enum.ExportEnum;

namespace Ambs.Reporting.Api.Controllers;

[Route("api/v{version:apiVersion}/report-export")]
[ApiController]
public class ReportExportController : ControllerBase
{
    private readonly IReportExportLogic _reportLogic;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

    public ReportExportController(IReportExportLogic reportLogic
        , Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        _reportLogic = reportLogic;
        _hostingEnvironment=hostingEnvironment;
    }
    [HttpGet("excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var data = await _reportLogic.GetReportDataForExport(ExportType.Excel, _hostingEnvironment.ContentRootPath);
       
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return File(stream, "application/ms-excel", "Test.xlsx");

    }
    [HttpGet("pdf")]
    public async Task<IActionResult> ExportPdf()
    {
        var data = await _reportLogic.GetReportDataForExport(ExportType.Pdf, _hostingEnvironment.ContentRootPath);
        
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return File(stream, "application/pdf", "Test.pdf");

    }
    [HttpGet("data/{reportId}/{paraVals}")]
    public async Task<IActionResult> GetData(long reportId,string paraVals)
    {
        //var data = await _reportLogic.GetReportData();
        return Ok(await _reportLogic.GetReportData(reportId,paraVals));
    }

    //[HttpGet("reportExport")]
    //public async Task<IActionResult> ReportExport(string fileName)
    //{
    //    var data = await _reportLogic.GetReportExport(fileName);
    //    var stream = new MemoryStream(data)
    //    {
    //        Position = 0
    //    };
    //    return File(stream, "application/ms-excel", fileName+ ".xlsx");

    //}
}
