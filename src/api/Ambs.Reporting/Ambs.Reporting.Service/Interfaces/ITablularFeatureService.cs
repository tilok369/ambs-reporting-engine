using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Service.Interfaces
{
    public interface ITablularFeatureService
    {
        TabularFeature Add(TabularFeature tabularFeature);
        TabularFeature Edit(TabularFeature tabularFeature);
        TabularFeature GetByReportId(long reportId);
        TabularFeature Delete(long id);
    }
}
