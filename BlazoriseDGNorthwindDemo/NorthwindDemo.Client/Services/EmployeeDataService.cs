using NorthwindDemo.Models;

namespace NorthwindDemo.Client.Services
{
    public class EmployeeDataService : DataService<EmployeeDto>, IEmployeeDataService
    {
        public EmployeeDataService(HttpClient client) : base(client)
        {
        }

        public override string Endpoint => "employees";
    }

    public interface IEmployeeDataService : IDataService<EmployeeDto> { }
}
