using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class Report
    {
        public Report()
        {
            GraphicalFeatures = new HashSet<GraphicalFeature>();
            ReportFilters = new HashSet<ReportFilter>();
            TabularFeatures = new HashSet<TabularFeature>();
        }

        public long Id { get; set; }
        public long WidgetId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public int Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int? CacheAliveTime { get; set; }
        public bool IsCacheEnable { get; set; }

        public virtual Widget Widget { get; set; } = null!;
        public virtual ICollection<GraphicalFeature> GraphicalFeatures { get; set; }
        public virtual ICollection<ReportFilter> ReportFilters { get; set; }
        public virtual ICollection<TabularFeature> TabularFeatures { get; set; }
    }
}
