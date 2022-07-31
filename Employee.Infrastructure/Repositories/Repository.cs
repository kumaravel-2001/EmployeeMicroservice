using Employee.Domain.Entities;
using Employee.Domain.Interfaces;
using Employee.Infrastructure.Data.contexts;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly EmployeeContext context;
        private readonly HttpClient Client;
       // private static readonly HttpContext contextHttp;
        public Repository(EmployeeContext context)
        {
            this.context = context;
        }
        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

        public IReadOnlyCollection<T> Get()
        {
            var Data = context.Set<T>().ToList();
            return Data.AsReadOnly();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }


        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }


        public T Update(T item)
        {
            return context.Update(item).Entity;
        }


    }
}
