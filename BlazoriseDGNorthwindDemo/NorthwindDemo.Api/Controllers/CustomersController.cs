using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDemo.Api.Infrastructure.Data;
using NorthwindDemo.Models;

namespace NorthwindDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindDbContext _context;

        public CustomersController(NorthwindDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var dto = (await _context.Customers.ToListAsync())
                .Select(x => new CustomerDto()
                {
                    CustomerId = x.CustomerId,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address
                });

            return Ok(dto);
        }
    }
}
