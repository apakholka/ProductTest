using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductTestServer.BusinessLogic.Services.Interfaces;
using ProductTestServer.Common.Models;
using ProductTestServer.Web.ViewModels.Product;

namespace ProductTestServer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseWebApiController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper,IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string sortField = null, string sortBy = null)
        {
            var allProducts =  _productService.GetAll(sortField, sortBy);
         
            List<ProductVm> listRehabCategory = new List<ProductVm>();
            foreach (var product in allProducts)
            {
                var oneProduct = _mapper.Map<ProductVm>(product);
                listRehabCategory.Add(oneProduct);
            }
            return Success(null, allProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var findProduct = await _productService.FindAsync(id);

            if (findProduct == null)
            {
                return Error("Product not found");
            }
            var product = _mapper.Map<ProductVm>(findProduct);

            return Success( null, product);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductVm productVm)
        {
            var findProduct = await _productService.FindAsync(productVm.Id);
            if (findProduct == null)
            {
                return Error("Product not found");
            }
            var product = _mapper.Map<Product>(productVm);
            var updateProduct = await _productService.UpdateAsync(product);

            return Success("Product successfully edited", null);
        }
    }
}