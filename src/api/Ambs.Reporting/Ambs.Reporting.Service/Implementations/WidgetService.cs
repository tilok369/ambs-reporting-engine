
namespace Ambs.Reporting.Service.Implementations;

public class WidgetService : IWidgetService
{
    private readonly IGenericRepository _genericRepository;

    public WidgetService(IGenericRepository genericRepository)
    {
        this._genericRepository = genericRepository;
    }

    public Widget Get(long id)
    {
        return _genericRepository.Get<Widget>(id);
    }

    public IEnumerable<Widget> GetAll()
    {
        return _genericRepository.GetAll<Widget>();
    }

    public Widget Save(Widget widget)
    {
        if (widget.Id == 0)
            return _genericRepository.Add<Widget>(widget);

        return _genericRepository.Edit<Widget>(widget);
    }
}
