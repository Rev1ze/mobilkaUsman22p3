using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rmpAuthorization;
using WebApplicationAuthorization.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("GetItems")]
        public ActionResult getAllItem()
        {
            List<Product> json = Getter.getProducts();
            if (json.Count == 0)
            {
                return NotFound("Товары не найдены");
            }
            return Ok(JsonConvert.SerializeObject(json));
        }



        // PUT api/<ProductsController>/5
        [HttpPost("addProduct")]
        public ActionResult AddProduct(Product product)
        {
            List<Product> products = Getter.getProducts();
            product.setUniqueId();
            products.Add(product);
            System.IO.File.WriteAllText(Getter.pathProducts, JsonConvert.SerializeObject(products));
            return Ok("Продукт добавлен");
        }
        [HttpPut("editProduct")]
        public ActionResult UpdateProduct(int id_old_product,Product newProduct)
        {
            List<Product> products = Getter.getProducts();
            Product old_product = products.FirstOrDefault(u=> u.id == id_old_product);

            if (old_product == null)
            {
                return NotFound("Продукт не найден");
            }
            newProduct.id = id_old_product;
            products.Remove(old_product);
            products.Add(newProduct);
            System.IO.File.WriteAllText(Getter.pathProducts, JsonConvert.SerializeObject(products));
            return Ok("Продукт изменен");

        }


        // DELETE api/<ProductsController>/5
        [HttpDelete("DeleteProduct")]
        public ActionResult Delete(int id)
        {
            List<Product> products = Getter.getProducts();
            Product newProduct = products.FirstOrDefault(u => u.id == id);
            if (newProduct == null)
            {
                return NotFound("Продукт не найден");
            }
            products.Remove(newProduct);
            System.IO.File.WriteAllText(Getter.pathProducts, JsonConvert.SerializeObject(products));
            return Ok("Товар удален");
        }
    }
}
