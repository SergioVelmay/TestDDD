using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Shared.Models;
using Domain.Shared.Repositories;
using Infrastructure.Persistence.SQLServer.Helpers;
using Infrastructure.Shared.Interfaces;
using System.Linq.Expressions;
using Infrastructure.Shared.Exceptions;

namespace Infrastructure.Persistence.SQLServer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Persistence<DAO.Company, Guid?> _persistence;
        private readonly Query<DAO.Company, Guid?> _query;
        private readonly IMapper _mapper;

        public CompanyRepository(IPersistence<DAO.Company, Guid?> persistence, IQuery<DAO.Company, Guid?> query, IMapper mapper)
        {
            _persistence = (Persistence<DAO.Company, Guid?>)persistence;
            _query = (Query<DAO.Company, Guid?>)query;
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

        public async Task<Company> GetById(string id)
        {
            //return _mapper.Map<DAO.Company, Company>((await _query.Find(item => item.Id == new Guid(id))).FirstOrDefault());
            return _mapper.Map<DAO.Company, Company>(await _query.GetAsync(new Guid(id)));
        }

        public async Task<IEnumerable<Company>> FindByName(string name)
        {
            Expression<Func<DAO.Company, bool>> expr = a => a.Name == name;

            return _mapper.Map<IEnumerable<DAO.Company>, IEnumerable<Company>>(await _query.FindAsync(expr));
        }

        public async Task<IEnumerable<Company>> GetCompaniesWithComplexQuery()
        {
            return _mapper.Map<IEnumerable<DAO.Company>, IEnumerable<Company>>(await _query.FindAsync("select * from company"));
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

        public async Task Delete(string id)
        {
            DAO.Company company = await _query.GetAsync(new Guid(id));

            DeleteDAOValidations();

            await _persistence.BeginTransactionAsync();
            await _persistence.DeleteAsync(company);
            await _persistence.CommitAsync();
        }

        public Task<IEnumerable<Company>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
