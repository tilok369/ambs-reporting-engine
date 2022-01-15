
namespace Ambs.Reporting.ViewModel.Reponse.Dashboard;

public record DashboardResponseDTO: BaseGetResponseDTO
{
    public DashboardResponseDTO(long Id): base(Id)
    {

    }

    public string Name { get; set; } = null!;
    public string? IframeUrl { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
}