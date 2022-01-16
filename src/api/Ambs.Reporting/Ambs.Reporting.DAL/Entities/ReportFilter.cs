using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class ReportFilter
    {
        public long Id { get; set; }
        public long ReportId { get; set; }
        public long FilterId { get; set; }
        public int SortOrder { get; set; }

        public virtual Filter Filter { get; set; } = null!;
        public virtual Report Report { get; set; } = null!;
    }
}
