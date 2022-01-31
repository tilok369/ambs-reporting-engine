using Ambs.Reporting.DAL.CalculativeModels;
using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;
using Ambs.Reporting.ViewModel.Reponse.Report;
using Ambs.Reporting.ViewModel.Reponse.ReportFilter;
using Ambs.Reporting.ViewModel.Reponse.TabularFeature;
using Ambs.Reporting.ViewModel.Request.GraphicalFeature;
using Ambs.Reporting.ViewModel.Request.Report;
using Ambs.Reporting.ViewModel.Request.ReportFilter;
using Ambs.Reporting.ViewModel.Request.TabularFeature;
using Ambs.Reporting.ViewModel.Reponse;
using AutoMapper;
using Ambs.Reporting.ViewModel.Reponse.MetaData;

namespace Ambs.Reporting.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Report, ReportResponseDTO>();
            CreateMap<ReportPostRequestDTO, Report>();
            CreateMap<ReportList, ReportListResponseDTO>();

            CreateMap<ReportFilterPostRequestDTO, ReportFilter>();
            CreateMap<ReportFilter, ReportFilterResponseDTO>();

            CreateMap<TabularFeaturePostRequestDTO, TabularFeature>();
            CreateMap<TabularFeature, TabularFeatureResponseDTO>();

            CreateMap<GraphicalFeaturePostRequestDTO, GraphicalFeature>();
            CreateMap<GraphicalFeature, GraphicalFeatureResponseDTO>();

            CreateMap<DropdownFilterCM, DropdownFilter>();

            CreateMap<MetaDatum, MetaDataResponseDTO>();
        }
    }
}
