using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shared.Models;
using Domain.Shared.Repositories;
using Infrastructure.Shared.Interfaces;

namespace Domain.Domains
{
    public class CompanyDomain : ICompanyDomain
    {
        private readonly ICompanyRepository _repository;

        public CompanyDomain(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> FindCompany(String name)
        {
            var x = await _repository.FindByName(name);
            return x;
        }

        public async Task<IEnumerable<Company>> FindCompanies()
        {
            var x = await _repository.FindAll();
            return x;
        }

        public async Task<Company> InsertCompany(Company item)
        {
            return await _repository.Insert(item);
        }

        public async Task<IEnumerable<Company>> FindCompanyWithQueryRepository()
        {
            return await _repository.GetCompaniesWithComplexQuery();
        }

        public async Task<Company> FindById(string id)
        {
            return await _repository.GetById(id);
        }
    }
}
