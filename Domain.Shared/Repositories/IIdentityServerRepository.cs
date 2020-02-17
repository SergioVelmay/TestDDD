using System.Threading.Tasks;

namespace Domain.Shared.Repositories
{
    public interface IIdentityServerRepository
    {
        Task<string> GetAccessTokenAsync();
    }
}
