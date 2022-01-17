
namespace Ambs.Reporting.ViewModel.Reponse.Widget;

public record WidgetResponseDTO: BaseGetResponseDTO
{
    public WidgetResponseDTO(long Id): base(Id) { }

    public long DashboardId { get; set; }
    public string DashboardName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool Status { get; set; }
    public int Type { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
}
