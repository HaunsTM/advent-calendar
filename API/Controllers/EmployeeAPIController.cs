using System.Collections.Generic;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    public class EmployeeAPIController : ApiController
    {
        public List<Employee> Get()
        {
            return new EmployeeDatabase();
        }
    }
}
