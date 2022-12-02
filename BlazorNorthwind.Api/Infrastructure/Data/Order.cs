namespace BlazorNorthwind.Api.Infrastructure.Data
{
    public class Order
    {
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public DateTime? OrderDate { get; set; }
        public Ship Ship { get; set; }
    }
}
