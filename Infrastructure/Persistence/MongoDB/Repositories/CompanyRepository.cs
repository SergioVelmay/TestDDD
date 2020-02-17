using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Domain.Shared.Models;
using Domain.Shared.Repositories;
using Infrastructure.Persistence.MongoDB.Helpers;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.MongoDB.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Persistence<DAO.Company, string> _persistence;
        private readonly Query<DAO.Company, string> _query;
        private readonly IMapper _mapper;

        public CompanyRepository(IPersistence<DAO.Company, string> persistence, IQuery<DAO.Company, string> query, IMapper mapper)
        {
            _persistence = (Persistence<DAO.Company, string>)persistence;
            _query = (Query<DAO.Company, string>)query;
            _mapper = mapper;
        }

        private void InsertDAOValidations(DAO.Company company)
        {

        }

        private void UpdateDAOValidations(DAO.Company company)
        {

        }

        private void DeleteDAOValidations()
        {

        }

        public async Task Delete(string id)
        {
            DAO.Company company = await _query.GetAsync(id);

            DeleteDAOValidations();

            await _persistence.BeginTransactionAsync();
            await _persistence.DeleteAsync(company);
            await _persistence.CommitAsync();
        }

        public async Task<IEnumerable<Company>> FindByName(string name)
        {
            var x = await _query.FindAsync(item => item.Name == name);
            var y = _mapper.Map<IEnumerable<DAO.Company>, IEnumerable<Company>>(x);
            return y;
        }

        public async Task<IEnumerable<Company>> FindAll()
        {
            var x = await _query.FindAllAsync();
            var y = _mapper.Map<IEnumerable<DAO.Company>, IEnumerable<Company>>(x);
            return y;
        }

        public async Task<Company> GetById(string id)
        {
            return _mapper.Map<DAO.Company, Company>(await _query.GetAsync(id));
        }

        public async Task<IEnumerable<Company>> GetCompaniesWithComplexQuery()
        {
            return _mapper.Map<IEnumerable<DAO.Company>, IEnumerable<Company>>(await _query.FindAsync(_persistence.GetFilter().Text("N")));
        }

        public async Task<Company> Insert(Company company)
        {
            DAO.Company dao = _mapper.Map<Company, DAO.Company>(company);
            dao.SetNewId();

            InsertDAOValidations(dao);

            await _persistence.BeginTransactionAsync();
            await _persistence.InsertAsync(dao);
            await _persistence.CommitAsync();

            return _mapper.Map<DAO.Company, Company>(dao);
        }

        public async Task<Company> Update(Company company)
        {
            DAO.Company dao = _mapper.Map<Company, DAO.Company>(company);

            UpdateDAOValidations(dao);

            await _persistence.BeginTransactionAsync();
            await _persistence.UpdateAsync(dao);
            await _persistence.CommitAsync();

            return _mapper.Map<DAO.Company, Company>(dao);
        }
    }
}
