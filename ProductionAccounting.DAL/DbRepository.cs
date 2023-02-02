using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Context;
using ProductionAccounting.DAL.Entities.Base;
using ProductionAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly ProductionAccountingContext _context;
        public bool AutoSaveChanges { get; set; }
        public DbRepository(ProductionAccountingContext productionAccounting)
        {
            _context = productionAccounting;
        }
        public virtual IQueryable<T> Items => _context.Set<T>();

        public T GetById(int id)
        {
            return Items.SingleOrDefault(x => x.Id == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Items.SingleOrDefaultAsync(x => x.Id == id);
        }

        public T Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Added;
            if (AutoSaveChanges)
                _context.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Added;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync();
            return entity;
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
            if (AutoSaveChanges)
                _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            _context.Remove(new T { Id = id });
            if (AutoSaveChanges)
                _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _context.SaveChangesAsync();
        }
    }
}
