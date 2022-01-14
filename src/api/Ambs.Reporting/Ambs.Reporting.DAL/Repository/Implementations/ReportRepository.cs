using Microsoft.Data.SqlClient;
using System.Data;
using Ambs.Reporting.Utility.Extensions;

namespace Ambs.Reporting.DAL.Repository.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly IApplicationConfigurationManager _applicationConfigurationManager;
        public ReportRepository(IApplicationConfigurationManager applicationConfigurationManager)
        {
            _applicationConfigurationManager = applicationConfigurationManager;
        }
        public async Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            var columns = new List<string>();
            var rows = new List<List<string>>();
            using var sqlConnection = new SqlConnection(_applicationConfigurationManager.GetConnectionString());
            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandTimeout = 5000;
            if (parameters.Any())
                sqlCommand.Parameters.AddRange(parameters);
            if(sqlConnection.State==ConnectionState.Closed)
                sqlConnection.Open();
            using var sqlReader =await sqlCommand.ExecuteReaderAsync();
            columns = Enumerable.Range(0, sqlReader.FieldCount).Select(sqlReader.GetName).ToList();
            while (sqlReader.Read())
            {
                rows.Add(columns.Select(columnName => sqlReader.GetStringValue(columnName)).ToList());
            }
            sqlCommand.Parameters.Clear();
            return (columns, rows);
        }
    }
}
