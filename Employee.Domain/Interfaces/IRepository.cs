using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Employee.Domain.Interfaces
{
    public interface  IRepository<T> where T : EntityBase //entitites and aggregrates are accessible
    {
        T Add(T item);
        T Update(T item);
        IReadOnlyCollection<T> Get();
        Task<T> GetByIdAsync(int id);
        Task<int> SaveAsync();

       

    }
}
