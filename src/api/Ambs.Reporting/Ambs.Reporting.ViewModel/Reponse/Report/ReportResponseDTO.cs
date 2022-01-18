using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.ViewModel.Reponse.Report;

public record ReportResponseDTO: BaseGetResponseDTO
{
    public ReportResponseDTO(long Id) : base(Id)
    {
    }
    public long WidgetId { get; set; }
    public string WidgetName { get; set; }
    public string Name { get; set; } = null!;
    public bool? Status { get; set; }
    public int Type { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
}

