using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class GraphicalFeature
    {
        public long Id { get; set; }
        public long ReportId { get; set; }
        public int GraphType { get; set; }
        public string Script { get; set; } = null!;
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public bool? ShowFilterInfo { get; set; }
        public bool? ShowLegend { get; set; }
        public string? XaxisTitle { get; set; }
        public string? YaxisTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual Report Report { get; set; } = null!;
    }
}
