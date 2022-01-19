namespace Ambs.Reporting.ViewModel.Reponse.ReportFilter;

public record ReportFilterResponseDTO : BaseGetResponseDTO
{
    public ReportFilterResponseDTO(long Id) : base(Id)
    {
    }
    public long ReportId { get; set; }
    public long FilterId { get; set; }
    public int SortOrder { get; set; }
}

