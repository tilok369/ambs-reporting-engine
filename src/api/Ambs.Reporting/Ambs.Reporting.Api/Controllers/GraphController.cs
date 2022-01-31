using Ambs.Reporting.Engine.GraphModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/graph")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IGraphicalFeatureLogic _graphicalFeatureLogic;

        public GraphController(IGraphicalFeatureLogic graphicalFeatureLogic)
        {
            this._graphicalFeatureLogic = graphicalFeatureLogic;
        }

        [HttpGet()]
        public IGraph Get(long dashboardId, long reportId, string parameterVals)
        {
            return _graphicalFeatureLogic.GetByReport(dashboardId,reportId, parameterVals);
        }

        [HttpGet("reportExport/{dashboardId}/{fileName}/{reportId}/{parameterVals}")]
        public async Task<IActionResult> ReportExport(long dashboardId,string fileName, long reportId, string parameterVals)
        {
            var data = await _graphicalFeatureLogic.GetReportExport(dashboardId,fileName, reportId,parameterVals);
            var stream = new MemoryStream(data)
            {
                Position = 0
            };
            return File(stream, "application/ms-excel", fileName + ".xlsx");

        }
    }
}
