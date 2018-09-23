﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyProducts.Core.Contracts
{
    public interface IUser : IEntity
    {
         string Username { get; set; }
         string PasswordHash { get; set; }
    }
}
