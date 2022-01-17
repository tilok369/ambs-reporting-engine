using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.DAL.Repository.Interfaces;

public interface IGenericRepository
{
    T Add<T>(T entity) where T : class;
    T Delete<T>(T entity) where T : class;
    T Edit<T>(T entity) where T : class;
    T Delete<T>(long id) where T : class;
    bool AddAll<T>(List<T> entityList) where T : class;
    bool EditAll<T>(List<T> entityList) where T : class;
    bool DeleteAll<T>(List<T> entityList) where T : class;
    T Get<T>(long id) where T : class;
    IEnumerable<T> GetAll<T>() where T : class;
    IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class;
    T First<T>(Expression<Func<T, bool>> predicate) where T : class;
    bool DeleteByProperty<T>(Expression<Func<T, bool>> predicate) where T : class;
}

