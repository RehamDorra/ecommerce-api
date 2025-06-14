using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.RepositoriesContaract;
using Talabat.Core.Specifications.ProductsSpecifications;

namespace Talabat.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;

        public ProductsController(IGenericRepository<Product> productsRepository , IMapper mapper , IGenericRepository<ProductBrand> brandRepository , IGenericRepository<ProductCategory> categoryRepository)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductsWithBrandsAndCategoriesSpecifications(specParams);
            var AllProducts = await _productsRepository.GetAllWithSpecAsync(spec);
            var result = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(AllProducts);
            var countSpec = new ProductsWithCountSpecifications(specParams);
            var count = await _productsRepository.GetCountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize , specParams.PageIndex , count , result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithBrandsAndCategoriesSpecifications(id);
            var productById = await _productsRepository.GetByIdWithSpecAsync(spec);
            if (productById == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product , ProductToReturnDto>(productById));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }
    }
}
