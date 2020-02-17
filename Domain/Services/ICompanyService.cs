using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceContracts.Output;

namespace Domain.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> FindCompany(string name);

        Task<IEnumerable<CompanyDTO>> FindCompanies();

        Task<CompanyDTO> FindById(string id);
    }
}