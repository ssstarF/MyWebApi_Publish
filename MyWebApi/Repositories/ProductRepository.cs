using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repositories
{
    //==================================
    //定義介面：IProductRepository.cs
    //實現介面：ProductRepository.cs
    //==================================
    public class ProductRepository : IProductRepository
    {
        //====================================================================
        //角色：資料存取層(Data Access Layer)
        //--------------------------------------------------------------------
        //責任：1.負責直接與資料庫進行交互，執行 CRUD 操作。
        //     2.封裝所有與資料存取相關的邏輯，例如查詢、新增、更新和刪除資料。
        //     3.提供一個清晰的介面來進行資料操作，供服務層或控制器調用。
        //====================================================================

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
