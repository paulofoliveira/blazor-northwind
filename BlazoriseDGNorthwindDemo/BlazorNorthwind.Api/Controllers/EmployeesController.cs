using Microsoft.AspNetCore.Mvc;
using BlazorNorthwind.Api.Infrastructure.Data;
using BlazorNorthwind.Api.Infrastructure.EntityFrameworkCore.Pagination;
using BlazorNorthwind.Infrastructure.Shared.Pagination;
using BlazorNorthwind.Models;

namespace BlazorNorthwind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindDbContext _context;

        public EmployeesController(NorthwindDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] PagedQueryModel pagedQueryModel)
        {
            var pagedQuery = pagedQueryModel.ToPagedQuery();

            var dto = await _context.Employees
                .Select(x => new EmployeeDto()
                {
                    Id = x.EmployeeID,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).GetPaged(pagedQuery);

            return Ok(dto);
        }
    }
}
