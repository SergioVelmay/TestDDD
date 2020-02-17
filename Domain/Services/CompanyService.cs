using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Domains;
using Domain.Shared.Models;
using ServiceContracts.Output;

namespace Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyDomain _domain;

        public CompanyService(ICompanyDomain domain, IMapper mapper)
        {
            //_mapper = new MapperConfiguration(config =>
            //{
            //    config.CreateMap<CompanyDTO, Company>();
            //    config.CreateMap<Company, CompanyDTO>();
            //    config.CreateMap<IEnumerable<Company>, IEnumerable<CompanyDTO>>();
            //}).CreateMapper();

            _mapper = mapper;

            _domain = domain;
        }

        public async Task<CompanyDTO> FindById(string id)
        {
            return _mapper.Map<Company, CompanyDTO> (await _domain.FindById(id));
        }

        public async Task<IEnumerable<CompanyDTO>> FindCompany(String name)
        {
            var x = await _domain.FindCompany(name);
            var y = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>(x);
            return y;
        }

        public async Task<IEnumerable<CompanyDTO>> FindCompanies()
        {
            var x = await _domain.FindCompanies();
            var y = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>(x);
            return y;
        }
    }
}
