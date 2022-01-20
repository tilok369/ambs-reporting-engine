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

        public FilterService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;

        }

        public Filter Get(long id)
        {
            return _genericRepository.Get<Filter>(id);
        }

        public IEnumerable<Filter> GetAll()
        {
            return _genericRepository.GetAll<Filter>();
        }

        public IEnumerable<GraphType> GetDrowpdownFilterValues(string script)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetDrowpdownFilterValues<T>(string script) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GraphType> GetGraphType()
        {
            return _genericRepository.Find<GraphType>(gt => gt.Status == true);
        }

        public Filter Save(Filter filter)
        {
            if (filter.Id == 0)
                return _genericRepository.Add<Filter>(filter);

            return _genericRepository.Edit<Filter>(filter);
        }
    }
}
