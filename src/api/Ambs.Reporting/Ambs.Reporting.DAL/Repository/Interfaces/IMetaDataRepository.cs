namespace Ambs.Reporting.DAL.Repository.Interfaces;

public interface IMetaDataRepository
{
    MetaDatum GetMetaDatumByReport(long reportId);
}

