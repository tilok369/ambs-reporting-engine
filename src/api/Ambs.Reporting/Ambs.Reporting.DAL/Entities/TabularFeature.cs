using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class TabularFeature
    {
        public long Id { get; set; }
        public long ReportId { get; set; }
        public string Script { get; set; } = null!;
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public bool? ShowFilterInfo { get; set; }
        public string? Template { get; set; }
        public bool AsOnDate { get; set; }
        public bool? Exportable { get; set; }
        public bool HasTotalColumn { get; set; }
        public bool HasTotalRow { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual Report Report { get; set; } = null!;
    }
}
