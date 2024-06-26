﻿using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DatBanDbContext _db;
        public RepositoryBase(DatBanDbContext db)
        {
            _db = db;
        }
        public async Task<List<T>> GetAll(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expression)
        {
            var query = _db.Set<T>().Where(expression).AsQueryable();
            var pageCount = query.Count();
                query =  query.Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value).AsNoTracking();
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAll(int? pageSize, int? pageIndex)
        {
            var query = _db.Set<T>().AsQueryable();
            var pageCount = query.Count();

            query = query.Skip(((pageIndex ?? 1) - 1) * pageSize ?? 10)
            .Take(pageSize.Value).AsNoTracking();
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            var a = await _db.Set<T>().Where(expression).AsNoTracking().ToListAsync();
            return a;
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();

        }

        public async Task CreateAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            var a = await _db.Set<T>().Where(expression).FirstOrDefaultAsync();
            return a;
        }
        public async Task<T> GetById(int id)
        {
            var a = await _db.Set<T>().FindAsync(id);
            return a;
        }

        public async Task<int> CountAsync()
        {
            var query = await _db.Set<T>().CountAsync();
            return query;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            var query = await _db.Set<T>().Where(expression).CountAsync();
            return query;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var query = _db.Set<T>().ToList();
            return query;
        }

        public Task<IEnumerable<T>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByName(Expression<Func<T, bool>> expression)
        {
            var query = _db.Set<T>().Where(expression).FirstOrDefault();
            return query;
        }

        public async Task<IQueryable<T>> GetAllAsQueryable()
        {
            var query = _db.Set<T>().AsQueryable();
            return query;
        }
    }
}
