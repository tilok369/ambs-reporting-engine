using Ambs.Reporting.DAL.CalculativeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Service.Implementations
{
    public class  FilterService : IFilterService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFilterRepository _filterRepository;

        public FilterService(IGenericRepository genericRepository
            , IFilterRepository filterRepository)
        {
            _genericRepository = genericRepository;
            _filterRepository = filterRepository;
        }

        public Filter Get(long id)
        {
            return _genericRepository.Get<Filter>(id);
        }

        public IEnumerable<Filter> GetAll()
        {
            return _genericRepository.GetAll<Filter>();
        }

        public IEnumerable<Filter> GetByReportId(long reportId)
        {
            return _filterRepository.GetByReportId(reportId);
        }

        public IEnumerable<GraphType> GetGraphType()
        {
            return _genericRepository.Find<GraphType>(gt => gt.Status == true);
        }

        public Filter Save(Filter filter)
        {
            if (filter.Id == 0)
                return _genericRepository.Add(filter);
            return _genericRepository.Edit(filter);
        }

        IEnumerable<DropdownFilterCM> IFilterService.GetDrowpdownFilterValues(string script)
        {
            return _filterRepository.GetDrowpdownFilterValues(script);
        }
    }
}
