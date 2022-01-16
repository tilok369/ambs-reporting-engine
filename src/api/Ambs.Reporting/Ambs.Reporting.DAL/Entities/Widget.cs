using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class Widget
    {
        public Widget()
        {
            Reports = new HashSet<Report>();
        }

        public long Id { get; set; }
        public long DashboardId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }
        public int Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual Dashboard Dashboard { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}
