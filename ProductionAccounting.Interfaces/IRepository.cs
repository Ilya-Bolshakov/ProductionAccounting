using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        public IQueryable<T> Items { get; }

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(int id);

        Task DeleteAsync(int id);
    }
}
