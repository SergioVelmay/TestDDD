using System;
using AutoMapper;

namespace Infrastructure.Persistence.MongoDB.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DAO.Company, Domain.Shared.Models.Company>()
                .ForMember(destination => destination.date, map => map.MapFrom(source => DateTime.Now));
            CreateMap<Domain.Shared.Models.Company, DAO.Company>()
                .ForMember(destination => destination.TableName, map => map.MapFrom(source => DbCollectionCatalog.Company));

            CreateMap<Domain.Shared.Models.Company, ServiceContracts.Output.CompanyDTO>()
                .ForMember(destination => destination.Date, map => map.MapFrom(source => source.date.ToBinary()));
            CreateMap<ServiceContracts.Output.CompanyDTO, Domain.Shared.Models.Company>()
                .ForMember(destination => destination.date, map => map.MapFrom(source => DateTime.FromBinary(source.Date)));

            CreateMap<DAO.DeliveryPoint, Domain.Shared.Models.DeliveryPoint>();
            CreateMap<Domain.Shared.Models.DeliveryPoint, DAO.DeliveryPoint>()
                .ForMember(destination => destination.TableName, map => map.MapFrom(source => DbCollectionCatalog.DeliveryPoint));
        }

    }
}
