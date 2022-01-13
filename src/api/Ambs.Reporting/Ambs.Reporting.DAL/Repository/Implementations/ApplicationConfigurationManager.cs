using Ambs.Reporting.DAL.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.DAL.Repository.Implementations
{
    public class ApplicationConfigurationManager: IApplicationConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return this._configuration.GetConnectionString("ReportDb");
        }
    }
}
