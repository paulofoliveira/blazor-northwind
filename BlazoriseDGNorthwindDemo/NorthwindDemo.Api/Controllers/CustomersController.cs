using Microsoft.AspNetCore.Mvc;
using NorthwindDemo.Api.Infrastructure.Data;
using NorthwindDemo.Api.Infrastructure.EntityFrameworkCore.Pagination;
using NorthwindDemo.Infrastructure.Shared.Pagination;
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
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] PagedQueryModel pagedQueryModel)
        {
            var pagedQuery = pagedQueryModel.ToPagedQuery();

            var dto = await _context.Customers
                .Select(x => new CustomerDto()
                {
                    CustomerId = x.CustomerId,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address
                })
                .GetPaged(pagedQuery);

            return Ok(dto);
        }
    }
}
