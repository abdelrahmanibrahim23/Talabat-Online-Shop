using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.DTO;
using Talabat.Errors;
using Talabat.Helper;

namespace Talabat.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _product;
        private readonly IGenericRepository<ProductBreand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> product,
            IGenericRepository<ProductBreand> BrandRepo, 
            IGenericRepository<ProductType> TypeRepo,
            IMapper mapper)
        {
            _product = product;
            _brandRepo = BrandRepo;
            _typeRepo = TypeRepo;
            _mapper = mapper;
        }
        [CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnDTO>>> GetAllProduct([FromQuery]ProductSpecPrams productPrams)
        {
            var spec =new ProductWithBrandAndType(productPrams);
            var products =await _product.GetAllSpecAsync(spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDTO>>(products);
            var countSpec = new ProductWithFillteringSpecification(productPrams);
            var count= await _product.GetCountAsync(countSpec);
            return Ok(new Pagination<ProductReturnDTO>(productPrams.PageIndex,productPrams.PageSize,Data,count));
        }
        [CachedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnDTO>> GetProductById(int id)

        {
            var spec =new ProductWithBrandAndType(id);
            var products =await _product.GetAllSpecByIdAsync(spec);
            if(products == null) 
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductReturnDTO>(products));    
        }
        [CachedAttribute(600)]
        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<ProductBreand>>> GetProductBrand()
        {
            var brand = await _brandRepo.GetAllAsync();
            return Ok(brand);
        }
        [CachedAttribute(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductType()
        {
            var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }
       

    }
}
