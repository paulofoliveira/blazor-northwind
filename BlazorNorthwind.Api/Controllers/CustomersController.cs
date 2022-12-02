using Microsoft.AspNetCore.Mvc;
using BlazorNorthwind.Api.Infrastructure.Data;
using BlazorNorthwind.Api.Infrastructure.EntityFrameworkCore.Pagination;
using BlazorNorthwind.Infrastructure.Shared.Pagination;
using BlazorNorthwind.Models;

namespace BlazorNorthwind.Api.Controllers
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
                    CustomerId = x.CustomerID,
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
