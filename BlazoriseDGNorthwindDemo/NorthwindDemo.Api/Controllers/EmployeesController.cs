using Microsoft.AspNetCore.Mvc;
using NorthwindDemo.Api.Infrastructure.Data;
using NorthwindDemo.Api.Infrastructure.EntityFrameworkCore.Pagination;
using NorthwindDemo.Infrastructure.Shared.Pagination;
using NorthwindDemo.Models;

namespace NorthwindDemo.Api.Controllers
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
