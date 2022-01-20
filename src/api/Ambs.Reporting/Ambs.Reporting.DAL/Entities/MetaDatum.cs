using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class MetaDatum
    {
        public long Id { get; set; }
        public long DashboardId { get; set; }
        public string DataSource { get; set; } = null!;
        public string BrandImage { get; set; }
        public virtual Dashboard Dashboard { get; set; } = null!;
    }
}
