namespace BlazorNorthwind.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Employee { get; set; }
        public DateTime? Date { get; set; }
        public ShipInformationDto Ship { get; set; }
    }
    public class ShipInformationDto
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
