using System;
namespace Domain.Shared.Models
{
    public class DeliveryPoint
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CP { get; set; }

        public Company Company { get; set; }
    }
}
