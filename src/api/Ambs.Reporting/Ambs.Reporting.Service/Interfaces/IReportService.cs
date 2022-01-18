using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Service.Interfaces;
public interface IReportService
{
    Report Get(long id);
    IEnumerable<Report> GetAll();
    Report Add(Report report);
    Report Edit(Report report);
    Report Delete(long id);
}

