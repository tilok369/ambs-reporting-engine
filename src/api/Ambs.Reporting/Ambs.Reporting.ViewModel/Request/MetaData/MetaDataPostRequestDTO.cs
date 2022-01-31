
namespace Ambs.Reporting.ViewModel.Request.MetaData;

public record MetaDataPostRequestDTO : BasePostRequestDTO
{
    public MetaDataPostRequestDTO(long Id): base(Id) { }

    public long DashboardId { get; set; }
    public string DataSource { get; set; } = null!;
    public string BrandImage { get; set; }
}
