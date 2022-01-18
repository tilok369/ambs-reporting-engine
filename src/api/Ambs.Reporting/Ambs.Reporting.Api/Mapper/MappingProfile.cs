using Ambs.Reporting.DAL.CalculativeModels;
using Ambs.Reporting.ViewModel.Reponse.Report;
using Ambs.Reporting.ViewModel.Request.GraphicalFeature;
using Ambs.Reporting.ViewModel.Request.Report;
using Ambs.Reporting.ViewModel.Request.ReportFilter;
using Ambs.Reporting.ViewModel.Request.TabularFeature;
using AutoMapper;

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
            CreateMap<ReportFilter, ReportFilterPostRequestDTO>();

            CreateMap<TabularFeaturePostRequestDTO, TabularFeature>();

            CreateMap<GraphicalFeaturePostRequestDTO, GraphicalFeature>();
        }
    }
}
