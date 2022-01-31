using Ambs.Reporting.ViewModel.Reponse.MetaData;
using Ambs.Reporting.ViewModel.Request.MetaData;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IMetaDataLogic
{
    MetaDataResponseDTO Get(long id);
    IList<MetaDataResponseDTO> GetAll(int page, int size);
    MetaDataPostResponseDTO Save(MetaDataPostRequestDTO metaData);
    MetaDataResponseDTO GetMetadataByReportId(long reportId);
}
