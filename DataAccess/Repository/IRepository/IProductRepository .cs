﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IReository
{
  public  interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

    }
}
