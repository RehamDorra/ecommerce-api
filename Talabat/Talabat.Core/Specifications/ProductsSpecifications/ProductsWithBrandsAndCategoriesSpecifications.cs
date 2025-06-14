using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductsSpecifications
{
    public class ProductsWithBrandsAndCategoriesSpecifications:BaseSpecification<Product>
    {
        //This ctor will be used to create object that will be used with Get All
        public ProductsWithBrandsAndCategoriesSpecifications(ProductSpecParams specParams):base
        (p =>

        (string.IsNullOrEmpty(specParams.SearchByName) || p.Name.ToLower().Contains(specParams.SearchByName))
        &&
        (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value)
        &&
        (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)

        )

        {
            Includes.Add(P =>  P.Brand);
            Includes.Add(P =>  P.Category);

            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                if (specParams.Sort == "priceAsc")
                {
                    AddOrderByAscending(p => p.Price);
                }
                else if (specParams.Sort == "priceDesc")
                {
                    AddOrderByDescending(p => p.Price);
                }
                else
                {
                    AddOrderByAscending(p => p.Name);
                }             
            }
            else
            {
                AddOrderByAscending(p => p.Name);

            }

            ApplyPagination((specParams.PageIndex-1) * specParams.PageSize , specParams.PageSize ); 
        }
        public ProductsWithBrandsAndCategoriesSpecifications(int id) : base( p => p.Id == id )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
