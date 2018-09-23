using ShopifyProducts.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Implementations
{
    public class User : Entity , IUser
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
