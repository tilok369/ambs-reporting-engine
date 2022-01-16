using Ambs.Reporting.Engine.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ambs.Reporting.Service.Interfaces
{
    public interface IReportService
    {
        Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, SqlParameter[] parameters);
    }
}
