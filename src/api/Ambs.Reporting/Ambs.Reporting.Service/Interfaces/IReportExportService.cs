using Microsoft.Data.SqlClient;
using System.Data;

namespace Ambs.Reporting.Service.Interfaces;
public interface IReportExportService
{
    Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, SqlParameter[] parameters);
}