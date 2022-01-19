
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/filter")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterLogic _filterLogic;

        public FilterController(IFilterLogic filterLogic)
        {
            this._filterLogic = filterLogic;
        }

        [HttpGet("{id}")]
        public FilterResponseDTO Get(long id)
        {
            return _filterLogic.Get(id);
        }

        [HttpGet()]
        public IList<FilterResponseDTO> GetAll(int page, int size)
        {
            return _filterLogic.GetAll(page, size);

        }

        [HttpPost]
        public FilterPostResponseDTO Save(FilterPostRequestDTO filter)
        {
            return _filterLogic.Save(filter);
        }
        [HttpGet("graphType")]
        public IActionResult GetGraphType()
        {
            return Ok(_filterLogic.GetGraphType());
        }
    }
}
