using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using ServiceContracts.Output;
using WebAPI.Helpers;
using System.Net.Http;
using IdentityModel.Client;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private ICompanyService _service;
        private IIdentityServerServices _identityServerServices;

        public TestController(ICompanyService service, IIdentityServerServices identityServerServices)
        {
            _service = service;
            _identityServerServices = identityServerServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> Test()
        {
            try
            {
                // var x = await _service.FindCompany("Test");
                var x = await _service.FindCompanies();
                var y = new ApiResult<IEnumerable<CompanyDTO>>(x);
                return y;
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<CompanyDTO>> Test(string id)
        {
            try
            {
                id = Guid.NewGuid().ToString();
                return new ApiResult<CompanyDTO>(await _service.FindById(id));
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }

        [HttpGet("IdentityTest")]
        public async Task IdentityTest()
        {
            var accessToken = await _identityServerServices.GetAccessTokenAsync();

            // call api 
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);
            var response = await apiClient.GetAsync("http://localhost:57242/identity");
        }
    }
}
