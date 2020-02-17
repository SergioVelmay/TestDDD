using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shared.Models;

namespace Domain.Domains
{
    public interface ICompanyDomain
    {
        Task<IEnumerable<Company>> FindCompany(string name);
        Task<IEnumerable<Company>> FindCompanies();
        Task<Company> FindById(string id);
        Task<IEnumerable<Company>> FindCompanyWithQueryRepository();
        Task<Company> InsertCompany(Company item);
    }
}