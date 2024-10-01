using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repositories
{
    //==================================
    //管理Product物件的資料存取邏輯。
    //----------------------------------
    //定義介面：IProductRepository.cs
    //實現介面：ProductRepository.cs
    //==================================
    public interface IProductRepository
    {
        //取得所有Product物件的集合。
        Task<IEnumerable<Product>> GetAllAsync();

        //根據指定ID取得單個Product物件。
        Task<Product> GetByIdAsync(int id);

        //將一新Product物件新增到DB。
        Task AddAsync(Product product);

        //更新現有的Product物件。
        Task UpdateAsync(Product product);

        //根據指定ID刪除單個Product物件。
        Task DeleteAsync(int id);
    }
}
