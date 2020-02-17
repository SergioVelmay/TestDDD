using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Interfaces
{
    public interface IQuery<TDAO, TID>
    {
        IEnumerable<TDAO> Find(Expression<Func<TDAO, bool>> func);
        Task<IEnumerable<TDAO>> FindAsync(Expression<Func<TDAO, bool>> func);
        TDAO Get(Expression<Func<TDAO, bool>> func);
        Task<TDAO> GetAsync(Expression<Func<TDAO, bool>> func);
        TDAO Get(TID id);
        Task<TDAO> GetAsync(TID id);
    }
}
