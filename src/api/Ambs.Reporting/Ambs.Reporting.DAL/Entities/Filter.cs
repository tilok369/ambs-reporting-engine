using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class Filter
    {
        public Filter()
        {
            ReportFilters = new HashSet<ReportFilter>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Script { get; set; } = null!;
        public string Parameter { get; set; } = null!;
        public string DependentParameters { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<ReportFilter> ReportFilters { get; set; }
    }
}
