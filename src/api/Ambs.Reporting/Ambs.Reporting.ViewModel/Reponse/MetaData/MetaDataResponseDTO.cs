
namespace Ambs.Reporting.ViewModel.Reponse.MetaData;

public record MetaDataResponseDTO: BaseGetResponseDTO
{
    public MetaDataResponseDTO(long Id): base(Id) {}

    public long DashboardId { get; set; }
    public string DashboardName { get; set; }
    public string DataSource { get; set; } = null!;
}

