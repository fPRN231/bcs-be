using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Service : BaseEntity
{ 
    public string Description { get; set; }

    public decimal BookingPrice { get; set; }
}