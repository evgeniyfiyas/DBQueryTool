using System.Collections.Generic;

namespace DBQueryTool.DataAccess.Service.Interfaces.Common
{
    public interface ICRUD<T>
    {
        T Add(T entity);
        List<T> GetAll();
        T Update(T entity);
        void Remove(T entity);
    }
}