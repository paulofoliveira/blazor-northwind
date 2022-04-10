using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDemo.Api.Infrastructure.Data;
using NorthwindDemo.Api.Infrastructure.EntityFrameworkCore.Pagination;
using NorthwindDemo.Infrastructure.Shared.Pagination;
using NorthwindDemo.Models;

namespace NorthwindDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly NorthwindDbContext _context;

        public OrdersController(NorthwindDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] PagedQueryModel pagedQueryModel)
        {
            var pagedQuery = pagedQueryModel.ToPagedQuery();

            var dto = await (from o in _context.Orders
                             from c in _context.Customers.Where(x => x.CustomerID == EF.Property<string>(o, "CustomerID")).DefaultIfEmpty()
                             from e in _context.Employees.Where(x => x.EmployeeID == EF.Property<int>(o, "EmployeeID")).DefaultIfEmpty()
                             select new OrderDto()
                             {
                                 Id = o.OrderID,
                                 Date = o.OrderDate,
                                 Customer = c.ContactName,
                                 Employee = e.FirstName,
                                 Ship = new ShipInformationDto()
                                 {
                                     Date = o.Ship.Date,
                                     Name = o.Ship.Name,
                                     Address = o.Ship.Address,
                                     City = o.Ship.City,
                                     PostalCode = o.Ship.PostalCode,
                                     Country = o.Ship.Country
                                 }
                             }).GetPaged(pagedQuery);

            return Ok(dto);
        }
    }
}
