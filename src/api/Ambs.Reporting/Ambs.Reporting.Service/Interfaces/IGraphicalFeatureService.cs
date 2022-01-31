using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Service.Interfaces
{
    public interface IGraphicalFeatureService
    {
        GraphicalFeature Add(GraphicalFeature graphicalFeature);
        GraphicalFeature Edit(GraphicalFeature graphicalFeature);
        GraphicalFeature GetByReportId(long reportId);
        List<GraphicalFeature> GetAllByReportId(long reportId);
        GraphicalFeature Delete(long id);
        bool DeleteByReportId(long reportId);
    }
}
