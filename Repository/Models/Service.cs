namespace Repository.Models
{
    public partial class Service : BaseEntity
    {
        public String Description { get; set; }
        public decimal BookingPrice { get; set; }
    }
}