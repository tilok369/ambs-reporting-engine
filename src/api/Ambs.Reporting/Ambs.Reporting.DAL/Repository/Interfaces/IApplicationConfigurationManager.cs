using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.DAL.Repository.Interfaces
{
    public interface IApplicationConfigurationManager
    {
        string GetConnectionString();
    }
}
