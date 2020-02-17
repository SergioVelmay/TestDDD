using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Shared.Interfaces;

namespace Infrastructure.Persistence.SQLServer.DAO
{
    public class Company : IDAO<Guid?>
    {
        [Key]
        public Guid? Id { get; set; }
        public string Name { get; set; }

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
