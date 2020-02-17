using Domain.Shared.Repositories;
using Microsoft.Extensions.Configuration;
using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Authorization
{
    public class IdentityServerRepository : IIdentityServerRepository
    {
        public IConfiguration Configuration { get; }

        public IdentityServerRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Configuration.GetSection("IdentityServer:ISUrl").Value);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return string.Empty;
            }

            var clientToken = new PasswordTokenRequest
            {
                UserName = Configuration.GetSection("IdentityServer:ISName").Value,
                Password = Configuration.GetSection("IdentityServer:ISPassword").Value,
                Address = disco.TokenEndpoint,
                ClientId = Configuration.GetSection("IdentityServer:ISClientId").Value,
                ClientSecret = Configuration.GetSection("IdentityServer:ISClientSecret").Value,
                Scope = Configuration.GetSection("IdentityServer:ISClientScope").Value,
            };

            // request token
            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(clientToken);
            return tokenResponse.AccessToken;
        }
    }
}
