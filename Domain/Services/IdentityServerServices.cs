using Domain.Shared.Repositories;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class IdentityServerServices : IIdentityServerServices
    {
        private IIdentityServerRepository IdentityServerRepository { get; }
        public IdentityServerServices(IIdentityServerRepository identityServerRepository)
        {
            IdentityServerRepository = identityServerRepository;
        }

        public Task<string> GetAccessTokenAsync()
        {
            return IdentityServerRepository.GetAccessTokenAsync();
        }
    }
}
