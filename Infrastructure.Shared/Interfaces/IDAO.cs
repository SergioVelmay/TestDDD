using System;
namespace Infrastructure.Shared.Interfaces
{
    public interface IDAO<TID>
    {
        TID GetId();
        void SetNewId();
    }
}
