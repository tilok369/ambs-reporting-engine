using Ambs.Reporting.ViewModel.Reponse.MetaData;
using Ambs.Reporting.ViewModel.Request.MetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/v{version:apiVersion}/metadata")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetaDataLogic _metaDataLogic;

        public MetaDataController(IMetaDataLogic metaDataLogic)
        {
            this._metaDataLogic = metaDataLogic;
        }

        [HttpGet("{id}")]
        public MetaDataResponseDTO Get(long id)
        {
            return _metaDataLogic.Get(id);
        }

        [HttpGet()]
        public IList<MetaDataResponseDTO> GetAll(int page, int size)
        {
            return _metaDataLogic.GetAll(page, size);
        }

        [HttpPost()]
        public MetaDataPostResponseDTO Save(MetaDataPostRequestDTO dashboard)
        {
            return _metaDataLogic.Save(dashboard);
        }
    }
}
