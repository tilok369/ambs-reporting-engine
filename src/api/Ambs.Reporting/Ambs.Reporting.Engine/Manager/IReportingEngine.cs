using Ambs.Reporting.Engine.Model;
namespace Ambs.Reporting.Engine.Manager
{
    public interface IReportingEngine
    {
        AmbsExportData GetExportData(AmbsReportData data);
        List<AmbsExportData> GetExportData(List<AmbsReportData> datas);
        byte[] GetExcelData(List<AmbsExportData> datas,string fileName);
        byte[] GetPdflData(List<AmbsExportData> datas,string fileName);
    }
}
