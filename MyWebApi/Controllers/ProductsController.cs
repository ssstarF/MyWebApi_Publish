using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //====================================================================
        //角色：表示層(Presentation Layer)
        //--------------------------------------------------------------------
        //責任：1.負責處理 HTTP 請求和響應。
        //     2.調用商業邏輯層（例如 ProductService）來處理業務邏輯。
        //     3.將請求轉發給合適的服務，然後將結果返回給客戶端。
        //     4.通常不直接與資料存取層交互，而是依賴服務層來處理。
        //====================================================================


        //====================================================================
        //更新 ProductsController，使其依賴於 ProductService 而不是 AppDbContext
        //====================================================================
        #region "依賴 AppDbContext 寫法"
        //private readonly AppDbContext _context;

        //public ProductsController(AppDbContext context)
        //{
        //    _context = context;
        //}
        #endregion

        //依賴 ProductsController 寫法
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products         (取得所有項目)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            #region "依賴 AppDbContext 寫法"
            //return await _context.Products.ToListAsync();
            #endregion

            return Ok(await _productService.GetAllProductsAsync());
        }

        // GET: api/products/{id}    (根據ID取得特定項目)
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            #region "依賴 AppDbContext 寫法"
            //var product = await _context.Products.FindAsync(id);
            #endregion

            //依賴 ProductsController 寫法
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/products        (建立新項目)
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            #region "依賴 AppDbContext 寫法"
            //_context.Products.Add(product);
            //await _context.SaveChangesAsync();
            #endregion

            //依賴 ProductsController 寫法
            await _productService.AddProductAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/products/{id}    (根據ID更新特定項目)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            #region "依賴 AppDbContext 寫法"
            //_context.Entry(product).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProductExists(id))
            //    {
            //        return NotFound();
            //    }
            //    throw;
            //}
            #endregion

            //依賴 ProductsController 寫法
            await _productService.UpdateProductAsync(product);

            return NoContent();
        }

        // DELETE: api/products/{id} (根據ID刪除特定項目)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            #region "依賴 AppDbContext 寫法"
            //var product = await _context.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();
            #endregion           

            //依賴 ProductsController 寫法
            await _productService.DeleteProductAsync(id);

            return NoContent();
        }

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}