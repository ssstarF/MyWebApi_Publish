using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Data
{
    public class AppDbContext : DbContext
    {
        //====================================================
        //建立資料庫上下文(DbContext)
        //----------------------------------------------------
        //優點：使得資料庫操作變得簡單、高效，不需要直接編寫SQL語句。
        //====================================================
        //AppDbContext繼承自DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //DbSet：表示資料庫中的一個資料表
        //定義資料表，讓Entity Framework能夠通過這個DbSet來進行Product的CRUD操作。
        public DbSet<Product> Products { get; set; }
    }
}
