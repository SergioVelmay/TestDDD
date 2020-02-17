using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IIdentityServerServices
    {
        Task<string> GetAccessTokenAsync();
    }
}
