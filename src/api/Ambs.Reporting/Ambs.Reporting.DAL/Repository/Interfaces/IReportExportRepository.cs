using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.DAL.Repository.Interfaces;

public interface IReportExportRepository
{
    Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, SqlParameter[] parameters);
}

