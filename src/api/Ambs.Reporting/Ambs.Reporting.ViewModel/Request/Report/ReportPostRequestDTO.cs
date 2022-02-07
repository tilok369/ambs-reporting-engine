using Ambs.Reporting.ViewModel.Request.GraphicalFeature;
using Ambs.Reporting.ViewModel.Request.ReportFilter;
using Ambs.Reporting.ViewModel.Request.TabularFeature;
using static Ambs.Reporting.Utility.Enum.ReportEnum;

namespace Ambs.Reporting.ViewModel.Request.Report;
public record ReportPostRequestDTO : BasePostRequestDTO
{
    public ReportPostRequestDTO(long Id) : base(Id)
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
    public int? CacheAliveTime { get; set; }
    public bool IsCacheEnable { get; set; }

    public List<ReportFilterPostRequestDTO> ReportFilterList { get; set; }
    public TabularFeaturePostRequestDTO? TabularFeature { get; set; }
    public GraphicalFeaturePostRequestDTO? GraphicalFeature { get; set; }
}

