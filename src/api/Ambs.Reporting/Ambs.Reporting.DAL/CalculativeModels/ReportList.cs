using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.DAL.CalculativeModels
{
    public record ReportList(long Id, string Name, bool Status, Utility.Enum.ReportEnum.ReportType Type, string WidgetName, string DashboardName);
}
