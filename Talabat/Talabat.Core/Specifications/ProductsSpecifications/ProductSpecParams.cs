﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductsSpecifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? SearchByName { get; set; }
    }
}
