using Ambs.Reporting.ViewModel.Reponse.MetaData;
using Ambs.Reporting.ViewModel.Request.MetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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

        [HttpPost("photo/{id}")]
        public ActionResult UploadPhoto(long id)
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("Resources", "Dashboard");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fileName = "Dashboard-" + id.ToString() + "-BrandImage.jpg";
                        var fullPath = Path.Combine(pathToSave, fileName);
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }

        }
    }
}
