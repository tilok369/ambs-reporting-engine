using Ambs.Reporting.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Logic.Interfaces
{
    public interface IReportLogic
    {
        Task<byte[]> GetReportData();
    }
}
