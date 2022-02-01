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
    [HttpGet("export/{dasboardId}/{reportId}/{paramVals}/{exportType}/{reportName}/{filterValues?}")]
    public async Task<IActionResult> ExportReport(long dasboardId,long reportId,string paramVals,ExportType exportType,string reportName,string filterValues="")
    {
        var data = await _reportLogic.GetReportDataForExport(dasboardId,reportId, paramVals, exportType, filterValues, _hostingEnvironment.ContentRootPath);
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
}
