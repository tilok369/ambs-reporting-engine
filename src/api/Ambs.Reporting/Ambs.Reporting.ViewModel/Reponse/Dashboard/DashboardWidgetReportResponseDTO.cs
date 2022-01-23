using Ambs.Reporting.ViewModel.Reponse.Widget;
using static Ambs.Reporting.Utility.Enum.ReportEnum;

namespace Ambs.Reporting.ViewModel.Reponse.Dashboard;
public record DashboardWidgetReportResponseDTO : BaseGetResponseDTO
{
    public DashboardWidgetReportResponseDTO(long Id) : base(Id)
    {
    }
    public string Name { get; set; } = null!;
    public string? IframeUrl { get; set; }
    public bool Status { get; set; }
    public List<WidgetDTO>? Widgets { get; set; }
}
public record WidgetDTO : BaseGetResponseDTO
{
    public WidgetDTO(long Id) : base(Id)
    {
    }
    public long DashboardId { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public List<ReportDTO>? Reports { get; set; }

}
public record ReportDTO : BaseGetResponseDTO
{
    public ReportDTO(long Id) : base(Id)
    {
    }
    public long WidgetId { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public ReportType Type { get; set; }
    public object? Data { get; set; }
    public List<FilterDTO>? Filters { get; set; }
}
public record FilterDTO : BaseGetResponseDTO
{
    public FilterDTO(long Id) : base(Id)
    {
    }
    public long ReportId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public string Parameter { get; set; }
    public string DependentParameters { get; set; }
    public bool Status { get; set; }
    public int? Type { get; set; }
    public IEnumerable<DropdownFilter>? DropdownFilters { get; set; }
}

