
namespace Ambs.Reporting.ViewModel.Reponse;

public abstract record BasePostReponseDTO
{
    public long Id { get; init; } = 0;
    public bool Success { get; init; } = true;
    public string Message { get; init; } = string.Empty;
}
