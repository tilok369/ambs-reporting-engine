
using Ambs.Reporting.ViewModel.Reponse.MetaData;
using Ambs.Reporting.ViewModel.Request.MetaData;
using System.Net.Http.Headers;
using AutoMapper;

namespace Ambs.Reporting.Logic.Implementations;

public class MetaDataLogic : IMetaDataLogic
{
    private readonly IMetaDataService _metaDataService;
    private readonly IDashboardService _dashboardService;
    private readonly IMapper _mapper;

    public MetaDataLogic(IMetaDataService metaDataService
        , IDashboardService dashboardService
        , IMapper mapper)
    {
        this._metaDataService = metaDataService;
        this._dashboardService = dashboardService;
        _mapper=mapper;
    }

    public MetaDataResponseDTO Get(long id)
    {
        


        var metaData = _metaDataService.Get(id);
        if (metaData == null) return null;

        var dashboard = _dashboardService.Get(metaData.DashboardId);
        if (dashboard == null) return null;

        var folderName = Path.Combine("Resources", "Dashboard");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        var fileName = metaData.BrandImage;
        var fullPath = Path.Combine(pathToSave, fileName);
        

        return new MetaDataResponseDTO(metaData.Id)
        {
            DataSource = metaData.DataSource,
            DashboardId = metaData.DashboardId,
            DashboardName = dashboard.Name,
            BrandImage = metaData?.BrandImage ?? "",
            ImageData = fullPath
        };
    }

    public IList<MetaDataResponseDTO> GetAll(int page, int size)
    {
        var metaDataList = _metaDataService.GetAll();
        var metaDatas = new List<MetaDataResponseDTO>();
        foreach (var metaData in metaDataList.Take((page - 1)..size))
        {
            var dashboard = _dashboardService.Get(metaData.DashboardId);
            if (dashboard == null) continue;

            metaDatas.Add(new MetaDataResponseDTO(metaData.Id)
            {
                DataSource = metaData.DataSource,
                DashboardId = metaData.DashboardId,
                DashboardName = dashboard.Name,
                BrandImage = metaData?.BrandImage ?? ""
            });
        }

        return metaDatas;
    }

    public MetaDataResponseDTO GetMetadataByReportId(long reportId)
    {
        return _mapper.Map<MetaDatum, MetaDataResponseDTO>(_metaDataService.GetMetaDatumByReport(reportId));
    }

    public MetaDataPostResponseDTO Save(MetaDataPostRequestDTO metaData)
    {
        try
        {
            var md = new MetaDatum
            {
                Id = metaData.Id,
                DataSource = metaData.DataSource,
                DashboardId = metaData.DashboardId,
                BrandImage = "Dashboard-" + metaData.DashboardId.ToString() + "-" + metaData.BrandImage
            };
            var result = _metaDataService.Save(md);

            return new MetaDataPostResponseDTO
            {
                Id = result?.Id ?? 0,
                Success = result != null,
                Message = result != null ? "MetaData saved successfully" : "Error while saving"
            };
        }
        catch (Exception ex)
        {
            return new MetaDataPostResponseDTO
            {
                Id = 0,
                Success = false,
                Message = "Error while saving: " + ex.Message
            };
        }
    }
}
