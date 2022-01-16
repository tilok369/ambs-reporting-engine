using Microsoft.AspNetCore.Mvc;
using static Ambs.Reporting.Utility.Enum.ExportEnum;

namespace Ambs.Reporting.Api.Controllers;

[Route("api/v{version:apiVersion}/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportLogic _reportLogic;
    public ReportController(IReportLogic reportLogic)
    {
        _reportLogic = reportLogic;
    }
    [HttpGet("excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var data = await _reportLogic.GetReportData(ExportType.Excel);
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return File(stream, "application/ms-excel", "Test.xlsx");

    }
    [HttpGet("pdf")]
    public async Task<IActionResult> ExportPdf()
    {
        var data = await _reportLogic.GetReportData(ExportType.Pdf);
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return File(stream, "application/pdf", "Test.pdf");

    }

    [HttpGet("reportExport")]
    public async Task<IActionResult> ReportExport(string fileName)
    {
        var data = await _reportLogic.GetReportExport(fileName);
        var stream = new MemoryStream(data)
        {
            Position = 0
        };
        return File(stream, "application/ms-excel", fileName+ ".xlsx");

    }
}
