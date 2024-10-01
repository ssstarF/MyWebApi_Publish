using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyWebApi.Data;
using MyWebApi.Repositories;
using MyWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //註冊 AppDbContext(設定 資料庫連線)
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=products.db"));

            //註冊 資料存取層(註冊 IProductRepository 與 ProductRepository)
            services.AddScoped<IProductRepository, ProductRepository>(); 

            //註冊 Razor Pages
            services.AddRazorPages();

            //註冊 ProductService
            //AddScoped 方法表示 ProductService 的生命週期是範圍性的，即在每一個請求中都會創建一個新的 ProductService 實例。
            services.AddScoped<ProductService>(); 

            services.AddControllers();

            //新增 Swagger服務
            services.AddSwaggerGen(c =>
            {
                //v1：API的版本名稱(自定義)。
                //Title：Swagger UI的標題(自定義)。
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //確認當前環境是否為"開發環境"。
            if (env.IsDevelopment())
            {
                //啟用開發者異常頁面。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //此頁面不會透露詳細的異常信息，避免洩漏敏感訊息。
                app.UseExceptionHandler("/Error");

                //強制使用者的瀏覽器只能使用"HTTPS"來訪問，降低受攻擊機率。
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //當使用者使用HTTP訪問時，自動重新導向HTTPS。
            app.UseHttpsRedirection();
            //啟用 靜態文件(如:CSS、JavaScript、圖片等)服務。
            app.UseStaticFiles();
            //啟用 Routing
            app.UseRouting();

            //啟用 Swagger
            app.UseSwagger();
            //設定 Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My WebApi V1");
                c.RoutePrefix = string.Empty; // 設定為根路徑
            });

            //設定 授權
            //app.UseAuthorization() 必須在 app.UseRouting() 和 app.UseEndpoints() 之間執行。
            //原因：Routing必須先確定請求的路徑，然後授權中介軟體會根據Routing的結果進行授權檢查。
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  //用於API，直接透過Routing訪問API Controlloers
                endpoints.MapRazorPages();   //用於傳統Web應用程式
            });
        }
    }
}
