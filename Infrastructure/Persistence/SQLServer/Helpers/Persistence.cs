using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.SQLServer.Contexts;
using Infrastructure.Shared.Exceptions;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.SQLServer.Helpers
{
    public class Persistence<TDAO, TID> : BaseDb<TDAO, TID>, IPersistence<TDAO, TID> where TDAO : class, IDAO<TID>
    {
        public Persistence(IDbContext context) : base(context)
        {
        }

        private void Remove(TDAO item)
        {
            GetDbSet().Remove(item);
        }

        private TDAO FindById(TID id)
        {
            TDAO dao = GetDbSet().Find(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        private async Task<TDAO> FindByIdAsync(TID id)
        {
            TDAO dao = await GetDbSet().FindAsync(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.BeginTransactionAsync();
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task CommitAsync()
        {
            await _context.CommitAsync();
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public async Task RollbackAsync()
        {
            await _context.RollbackAsync();
        }

        public void Delete(TDAO item)
        {
            TDAO dao = FindById(GetId(item));
            Remove(dao);
        }

        public async Task DeleteAsync(TDAO item)
        {
            TDAO dao = await FindByIdAsync(GetId(item));
            Remove(dao);
        }

        public void Delete(TID id)
        {
            TDAO dao = FindById(id);
            Remove(dao);
        }

        public async Task DeleteAsync(TID id)
        {
            TDAO dao = await FindByIdAsync(id);
            Remove(dao);
        }

        public void Delete(IEnumerable<TDAO> items)
        {
            GetDbSet().RemoveRange(items);
        }

        public async Task DeleteAsync(IEnumerable<TDAO> items)
        {
            Task task = Task.Run(() =>
            {
                Delete(items);
            });

            await task;
        }

        public TDAO Insert(TDAO item)
        {
            item = GetDbSet().Add(item).Entity;

            return item;
        }

        public async Task<TDAO> InsertAsync(TDAO item)
        {   
            item = (await GetDbSet().AddAsync(item)).Entity;

            return item;
        }

        public TDAO Update(TDAO item)
        {
            item = GetDbSet().Update(item).Entity;

            return item;
        }

        public async Task<TDAO> UpdateAsync(TDAO item)
        {
            Task<TDAO> task = Task.Run(() =>
            {
                return Update(item);
            });
                
            return await task;
        }
    }
}
