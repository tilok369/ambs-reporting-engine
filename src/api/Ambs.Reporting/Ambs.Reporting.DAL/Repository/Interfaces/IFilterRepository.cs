using Ambs.Reporting.DAL.CalculativeModels;

namespace Ambs.Reporting.DAL.Repository.Interfaces;

public interface IFilterRepository
{
    IEnumerable<DropDownFilter> GetDrowpdownFilterValues(string script);
}

