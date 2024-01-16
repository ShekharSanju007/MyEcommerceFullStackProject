using APICODEBASE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICODEBASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDBContext _context;

        public ProductController(MyDBContext context)
        {

            _context=context;
        }

        [HttpGet]

        public IActionResult Get()
        {

            try
            {
                var ProductData = _context.Product.ToList();

                if (ProductData.Count==0)
                {

                    return NotFound("Product Data not found");
                }

                return Ok(ProductData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var ProductIdData = _context.Product.Find(id);

                if (ProductIdData==null)
                {

                    return NotFound($"Product Details not found with id {id}");
                }

                return Ok(ProductIdData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult Post(Product product)
        {

            try
            {
                _context.Add(product);
                _context.SaveChanges();

                return Ok("Product Created");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(Product product)
        {


            if (product == null || product.ProductId == 0)

            {

                if (product == null)
                {
                    return BadRequest("Product data is Invalid");
                }

                if (product.ProductId == 0)
                {
                    return BadRequest($"product id {product.ProductId} is Invalid");
                }
            }

            try
            {
                var ProductDataInfo = _context.Product.Find(product.ProductId);
                if (ProductDataInfo==null)
                {
                    return NotFound($"Product id not found {product.ProductId}");
                }

                ProductDataInfo.name=product.name;
                ProductDataInfo.description=product.description;
                ProductDataInfo.image=product.image;

                _context.SaveChanges();
                return Ok("Product Details Updated");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {


            try
            {
                var productDataDelete = _context.Product.Find(id);
                if (productDataDelete==null)
                {
                    return NotFound($"Product id not found with {id}");
                }

                _context.Product.Remove(productDataDelete);
                _context.SaveChanges();
                return Ok("Product Details Deleted");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }










    }
}
