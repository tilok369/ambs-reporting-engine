using Ambs.Reporting.DAL.CalculativeModels;
using System.Data;
using System.Data.SqlClient;

namespace Ambs.Reporting.DAL.Repository.Implementations;

public class FilterRepository:IFilterRepository
{
    private readonly DbContextOptionsBuilder<ReportEngineContext> _dbContextOptionBuilder;
    public FilterRepository(IApplicationConfigurationManager applicationConfigurationManager)
    {
        _dbContextOptionBuilder = new DbContextOptionsBuilder<ReportEngineContext>();
        _dbContextOptionBuilder.UseSqlServer(applicationConfigurationManager.GetConnectionString());
    }

    public IEnumerable<Filter> GetByReportId(long reportId)
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        return (from filter in context.Filters
                join reportFilter in context.ReportFilters on filter.Id equals reportFilter.FilterId
                where reportFilter.ReportId== reportId
                select filter).ToList();
    }

    public IEnumerable<DropdownFilterCM> GetDrowpdownFilterValues(string script)
    {
        var data=new List<DropdownFilterCM>();
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        var metaData = context.MetaData.FirstOrDefault(d => d.Id == 1);
        using var sqlConnection = new SqlConnection(metaData.DataSource);
        using var sqlCommand = new SqlCommand(script, sqlConnection);
        sqlConnection.Open();
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.CommandTimeout = 5000;
        using var sqlReader = sqlCommand.ExecuteReader();
        while (sqlReader.Read())
        {
            data.Add(new DropdownFilterCM(long.Parse(sqlReader["Value"].ToString()), sqlReader["Name"].ToString(), 0));
        }
        sqlConnection.Close();
        return data;
    }
}

