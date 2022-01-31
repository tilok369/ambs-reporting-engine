using Ambs.Reporting.ViewModel.Reponse.GraphicalFeature;
using Ambs.Reporting.ViewModel.Reponse.ReportFilter;
using Ambs.Reporting.ViewModel.Reponse.TabularFeature;
using static Ambs.Reporting.Utility.Enum.ReportEnum;

namespace Ambs.Reporting.ViewModel.Reponse.Report;

public record ReportResponseDTO: BaseGetResponseDTO
{
    public ReportResponseDTO(long Id) : base(Id)
    {
    }
    public long WidgetId { get; set; }
    public string Name { get; set; } = null!;
    public bool? Status { get; set; }
    public ReportType Type { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }

    public IEnumerable<ReportFilterResponseDTO> ReportFilterList { get; set; }
    public TabularFeatureResponseDTO? TabularFeature { get; set; }
    public GraphicalFeatureResponseDTO? GraphicalFeature { get; set; }
}

