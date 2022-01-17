namespace Ambs.Reporting.ViewModel.Request.ReportFilter;
public record ReportFilterPostRequestDTO : BasePostRequestDTO
{
    public ReportFilterPostRequestDTO(long Id) : base(Id)
    {
    }
    public long ReportId { get; set; }
    public long FilterId { get; set; }
    public int SortOrder { get; set; }
}

