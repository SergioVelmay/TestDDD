using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Interfaces
{
    public interface IPersistence<TDAO, TID>
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
        void Delete(TDAO item);
        Task DeleteAsync(TDAO item);
        void Delete(TID id);
        Task DeleteAsync(TID id);
        void Delete(IEnumerable<TDAO> items);
        Task DeleteAsync(IEnumerable<TDAO> items);
        TDAO Insert(TDAO item);
        Task<TDAO> InsertAsync(TDAO item);
        TDAO Update(TDAO item);
        Task<TDAO> UpdateAsync(TDAO item);
    }
}
