using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdWorksCore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdWorksCore.Web.ControllersView
{
    // api route names should always be plural nouns
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private static List<Product> productList = new List<Product>() {
            new Product() {Id=101, Name="Apple", ProductNumber="A127", Color="Red", ListPrice=Convert.ToDecimal(1.49) },
            new Product() {Id=102, Name="Chocolate", ProductNumber="A127", Color="Brown", ListPrice=Convert.ToDecimal(3.49) },
            new Product() {Id=103, Name="Doll", ProductNumber="A127", Color="Beige", ListPrice=Convert.ToDecimal(4.49) },
            new Product() {Id=104, Name="Banana", ProductNumber="A127", Color="Yello", ListPrice=Convert.ToDecimal(0.49) }
        };
        private readonly ILogger<ProductsController> logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(productList.OrderBy(p => p.Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(productList.First(p => p.Id == id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to find product: {ex}");
                return NotFound($"Could not find {id} product");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(vm.Id > 0)
                    {
                        throw new NotSupportedException("product id set to non-zero value");
                    }
                    vm.Id = productList.Max(p => p.Id) + 1;
                    productList.Add(vm.ToProduct());
                    return Created($"/api/products/{vm.Id}", vm);
                }
                catch(Exception e)
                {
                    logger.LogError($"Failed to create product: {e.Message}");
                    return BadRequest($"Failed to create product: {e.Message}");
                }
            }

            logger.LogError($"Bad model state: {ModelState}");
            return BadRequest(ModelState);
        }
    }
}