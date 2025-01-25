using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_productService.TProductCount());
        }
        
        [HttpGet("TotalPriceByDrinkCategory")]
        public IActionResult TotalPriceByDrinkCategory()
        {
            return Ok(_productService.TotalPriceByDrinkCategory());
        }
        
        [HttpGet("TotalPriceBySaladCategory")]
        public IActionResult TotalPriceBySaladCategory()
        {
            return Ok(_productService.TotalPriceBySaladCategory());
        }
        
        [HttpGet("ProductNameByMaxPrice")]
        public IActionResult ProductNameByMaxPrice()
        {
            return Ok(_productService.ProductNameByMaxPrice());
        }
        
        [HttpGet("ProductNameByMinPrice")]
        public IActionResult ProductNameByMinPrice()
        {
            return Ok(_productService.ProductNameByMinPrice());
        }
        
        [HttpGet("AvgProductPriceByHamburger")]
        public IActionResult AvgProductPriceByHamburger()
        {
            return Ok(_productService.TAvgProductPriceByHamburger());
        }
        
        [HttpGet("ProductCountByCategoryHamburger")]
        public IActionResult ProductCountByCategoryHamburger()
        {
            return Ok(_productService.TProductCountByCategoryNameHamburger());
        }
        
        [HttpGet("ProductCountByCategoryDrink")]
        public IActionResult ProductCountByCategoryDrink()
        {
            return Ok(_productService.TProductCountByCategoryNameDrink());
        }
        
        [HttpGet("AvgProductPrice")]
        public IActionResult AvgProductPrice()
        {
            return Ok(_productService.TAvgProductPrice());
        }
        
        [HttpGet("ProductPriceBySteakBurger")]
        public IActionResult ProductPriceBySteakBurger()
        {
            return Ok(_productService.TProductPriceBySteakBurger());
        }

        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var context = new SignalRContext();

            var values = context.Products.Include(x => x.Category).Select(y => new ResultProductWithCategory
            {
                Description = y.Description,
                ImageUrl = y.ImageUrl,
                Price = y.Price,
                ProductName = y.ProductName,
                ProductStatus = y.ProductStatus,
                CategoryName = y.Category.CategoryName,
                ProductID = y.ProductID
            }).ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);

            _productService.TAdd(value);
            return Ok("Ürün bilgisi başarıyla oluşturuldu.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetById(id);
            _productService.TDelete(value);
            return Ok("Ürün bilgisi başarıyla silindi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetById(id);
            return Ok(_mapper.Map<GetProductDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);

            _productService.TUpdate(value);
            return Ok("Ürün bilgisi başarıyla güncellendi.");

        }

        [HttpGet("GetLast9Products")]
        public IActionResult GetLast9Products()
        {
            var values = _mapper.Map<List<ResultProductDto>>(_productService.TGetLast9Products());
            return Ok(values);
        }
    }
}
