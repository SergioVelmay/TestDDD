using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Interfaces
{
    public interface IDbContext
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Rollback();
        Task RollbackAsync();
        void Commit();
        Task CommitAsync();
    }
}
