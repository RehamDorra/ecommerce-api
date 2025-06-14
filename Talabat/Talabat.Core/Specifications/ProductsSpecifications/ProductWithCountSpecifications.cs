using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductsSpecifications
{
    public class ProductsWithCountSpecifications:BaseSpecification<Product>
    {
        public ProductsWithCountSpecifications(ProductSpecParams specParams) : base
        (p =>

        (string.IsNullOrEmpty(specParams.SearchByName) || p.Name.ToLower().Contains(specParams.SearchByName))
        &&
        (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value)
        &&
        (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)

        )
        {


        }

    }
}
