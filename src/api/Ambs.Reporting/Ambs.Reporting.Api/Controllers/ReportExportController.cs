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
    //public async Task<IActionResult> ExportExcel()
    //{
    //    var data = await _reportLogic.GetReportDataForExport(ExportType.Excel, _hostingEnvironment.ContentRootPath);
       
    //    var stream = new MemoryStream(data)
    //    {
    //        Position = 0
    //    };
    //    return File(stream, "application/ms-excel", "Test.xlsx");

    //}
    //[HttpGet("pdf")]
    //public async Task<IActionResult> ExportPdf()
    //{
    //    var data = await _reportLogic.GetReportDataForExport(ExportType.Pdf, _hostingEnvironment.ContentRootPath);
        
    //    var stream = new MemoryStream(data)
    //    {
    //        Position = 0
    //    };
    //    return File(stream, "application/pdf", "Test.pdf");

    //}
    [HttpGet("export/{dasboardId}/{reportId}/{paramVals}/{exportType}/{reportName}")]
    public async Task<IActionResult> ExportReport(long dasboardId,long reportId,string paramVals,ExportType exportType,string reportName)
    {
        var data = await _reportLogic.GetReportDataForExport(dasboardId,reportId, paramVals, exportType, _hostingEnvironment.ContentRootPath);
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return exportType==ExportType.Excel? File(stream, "application/ms-excel", reportName+ ".xlsx") : File(stream, "application/pdf", reportName+".pdf");
    }
    [HttpGet("data/{dasboardId}/{reportId}/{paraVals}")]
    public async Task<IActionResult> GetData(long dasboardId, long reportId,string paraVals)
    {
        return Ok(await _reportLogic.GetReportData(dasboardId,reportId, paraVals));
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
