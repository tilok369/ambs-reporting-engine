
namespace Ambs.Reporting.Service.Interfaces;

public interface IWidgetService
{
    IEnumerable<Widget> GetAll();
    Widget Get(long id);
    Widget Save(Widget widget);
    IEnumerable<Widget> GetByDashboardId(long id);
}
