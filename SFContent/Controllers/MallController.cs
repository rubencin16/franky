using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using Mall.storage.Models;

namespace SFContent.Controllers
{
    [RoutePrefix("store")]
    public class MallController : ApiController 
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Xbox 360", Category = "Consoles", Price = 1 },
            new Product { Id = 2, Name = "Xbox One", Category = "Consoles", Price = 23.95M },
            new Product { Id = 3, Name = "PSP4", Category = "Hardware", Price = 1.99M }
        };

        [Route("products")]
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [Route("catalog")]
        [HttpGet]
        public IHttpActionResult MallContentProducts()
        {            
            return Ok(products);
        }

        [Route("product/{id:int}")]
        [HttpGet]
        public IHttpActionResult MallProductById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }
    }
}
