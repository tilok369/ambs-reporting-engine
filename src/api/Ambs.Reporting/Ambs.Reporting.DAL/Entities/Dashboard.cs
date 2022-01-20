using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class Dashboard
    {
        public Dashboard()
        {
            MetaData = new HashSet<MetaDatum>();
            Widgets = new HashSet<Widget>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? IframeUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string BrandImage { get; set; }

        public virtual ICollection<MetaDatum> MetaData { get; set; }
        public virtual ICollection<Widget> Widgets { get; set; }
    }
}
