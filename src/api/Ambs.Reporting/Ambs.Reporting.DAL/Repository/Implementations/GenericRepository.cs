
namespace Ambs.Reporting.DAL.Repository.Implementations;

public class GenericRepository : IGenericRepository
{
    private readonly DbContextOptionsBuilder<ReportEngineContext> _dbContextOptionBuilder;

    public GenericRepository(IApplicationConfigurationManager applicationConfigurationManager)
    {
        _dbContextOptionBuilder = new DbContextOptionsBuilder<ReportEngineContext>();
        _dbContextOptionBuilder.UseSqlServer(applicationConfigurationManager.GetConnectionString());
    }

    public T Add<T>(T entity) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        dbSet.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public T Delete<T>(T entity) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        dbSet.Remove(entity);
        context.SaveChanges();
        return entity;
    }

    public T Edit<T>(T entity) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
        return entity;
    }

    public T Delete<T>(long id) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entity = dbSet.Find(id);
        dbSet.Remove(entity);
        context.SaveChanges();
        return entity;
    }

    public bool AddAll<T>(List<T> entityList) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        try
        {
            DbSet<T> dbSet = context.Set<T>();
            foreach (var entity in entityList)
            {
                dbSet.Add(entity);
            }
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool EditAll<T>(List<T> entityList) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        try
        {
            foreach (var entity in entityList)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DeleteAll<T>(List<T> entityList) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        try
        {
            DbSet<T> dbSet = context.Set<T>();
            foreach (var entity in entityList)
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public T Get<T>(long id) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entity = dbSet.Find(id);
        return entity;
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entityList = dbSet.AsNoTracking().ToList();
        return entityList;
    }

    public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entityList = dbSet.Where(predicate).ToList();
        return entityList;
    }

    public T First<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entity = dbSet.FirstOrDefault(predicate);
        return entity;
    }
    public bool DeleteByProperty<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        DbSet<T> dbSet = context.Set<T>();
        var entityList = dbSet.Where(predicate).ToList();
        try
        {
            foreach (var entity in entityList)
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}