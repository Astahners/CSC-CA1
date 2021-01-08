using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class ProductsV2Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        // GET ALL ITEMS
        [HttpGet]
        [Route("api/v2/products")]
        public IEnumerable<Product> getAllProducts()
        {
            return repository.GetAll();
        }

        // GET ITEM BY ID
        [HttpGet]
        [Route("api/v2/products/{id:int}", Name ="getProduct")]
        public Product getProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return item;
        }

        // GET ITEM(S) BY CATEGORY
        [HttpGet]
        [Route("api/v2/products")]
        public IEnumerable<Product> getProductsByCategory(string category)
        {
            return repository.GetAll().Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        // ADD
        [HttpPost]
        [Route("api/v2/products")]
        public HttpResponseMessage postProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("getProduct", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // UPDATE AN ITEM
        [HttpPut]
        [Route("api/v2/products/{id:int}")]
        public void putProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE AN ITEM
        [HttpDelete]
        [Route("api/v2/products/{id:int}")]
        public void deleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
