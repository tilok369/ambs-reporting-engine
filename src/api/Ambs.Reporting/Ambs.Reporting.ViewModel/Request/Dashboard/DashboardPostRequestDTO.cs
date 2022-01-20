
namespace Ambs.Reporting.ViewModel.Request.Dashboard;

public record DashboardPostRequestDTO: BasePostRequestDTO
{
    public DashboardPostRequestDTO(long Id) : base(Id)
    {

    }

    public string Name { get; set; } = null!;
    public string? IframeUrl { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public string BrandImage { get; set; }
}
