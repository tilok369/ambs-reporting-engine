namespace Ambs.Reporting.DAL.Repository.Interfaces;
public interface IInvalidateCacheRepository
{
    void Invalidate(string cacheKey);
    void Invalidate();
}
