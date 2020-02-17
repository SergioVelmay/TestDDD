using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shared.Models;

namespace Domain.Shared.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(string id);
        Task<IEnumerable<Company>> GetCompaniesWithComplexQuery();
        Task<IEnumerable<Company>> FindByName(string name);
        Task<IEnumerable<Company>> FindAll();
        Task<Company> Insert(Company company);
        Task<Company> Update(Company company);
        Task Delete(string id);
    }
}
