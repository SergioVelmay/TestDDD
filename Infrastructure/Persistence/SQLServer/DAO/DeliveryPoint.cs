using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.SQLServer.DAO
{
    public class DeliveryPoint : IDAO<Guid?>
    {
        [Key]
        public Guid? Id { get; set; }
        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CP { get; set; }

        public Company Company { get; set; }

        public Guid? GetId()
        {
            return Id;
        }

        public void SetNewId()
        {
            Id = Guid.NewGuid();
        }
    }
}
