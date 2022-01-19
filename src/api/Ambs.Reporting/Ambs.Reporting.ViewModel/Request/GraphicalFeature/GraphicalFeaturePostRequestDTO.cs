namespace Ambs.Reporting.ViewModel.Request.GraphicalFeature;
public record GraphicalFeaturePostRequestDTO : BasePostRequestDTO
{
    public GraphicalFeaturePostRequestDTO(long Id) : base(Id)
    {
    }
    public long ReportId { get; set; }
    public int GraphType { get; set; }
    public string Script { get; set; } = null!;
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public bool? ShowFilterInfo { get; set; }
    public bool? ShowLegend { get; set; }
    public string? XaxisTitle { get; set; }
    public string? YaxisTitle { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public string? XaxisSuffix { get; set; }
    public string? XaxisPrefix { get; set; }
    public string? YaxisSuffix { get; set; }
    public string? YaxisPrefix { get; set; }
    public bool? Exportable { get; set; }
}

