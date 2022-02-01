using Ambs.Reporting.DAL.CalculativeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Service.Interfaces
{
    public interface IFilterService
    {
        Filter Get(long id);
        IEnumerable<Filter> GetAll();

        Filter Save(Filter filter);
        IEnumerable<GraphType> GetGraphType();
        IEnumerable<DropdownFilterCM> GetDrowpdownFilterValues(string script,string connectionString);
        IEnumerable<Filter> GetByReportId(long reportId);
    }
}
