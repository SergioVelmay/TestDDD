using System;
using AutoMapper;

namespace Infrastructure.Persistence.SQLServer.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DAO.Company, Domain.Shared.Models.Company>()
                .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id.Value.ToString()));
            CreateMap<Domain.Shared.Models.Company, DAO.Company>()
                .ForMember(destination => destination.Id, map => {
                    map.PreCondition(source => !string.IsNullOrEmpty(source.Id));
                    map.MapFrom(source => new Guid(source.Id));
                }
            );

            CreateMap<DAO.DeliveryPoint, Domain.Shared.Models.DeliveryPoint>()
                .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id.Value.ToString()));
            CreateMap<Domain.Shared.Models.DeliveryPoint, DAO.DeliveryPoint>()
                .ForMember(destination => destination.Id, map => {
                    map.PreCondition(source => !string.IsNullOrEmpty(source.Id));
                    map.MapFrom(source => new Guid(source.Id));
                }
            );
        }

    }
}
