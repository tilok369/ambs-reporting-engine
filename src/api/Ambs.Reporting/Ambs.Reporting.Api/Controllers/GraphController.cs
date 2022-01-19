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
        public IGraph Get(long reportId, string parameterVals)
        {
            return _graphicalFeatureLogic.GetByReport(reportId, parameterVals);
        }
    }
}
