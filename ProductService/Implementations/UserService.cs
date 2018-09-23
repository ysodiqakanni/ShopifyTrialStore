using ProductService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Implementations
{
    public class UserService : IUserService
    {
        public bool IsAuthenticated(string username, string password)
        {
            // Todo: assume thet username and password are correct
            return true;
        }
    }
}
