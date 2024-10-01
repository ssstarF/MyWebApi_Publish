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
            //���U AppDbContext(�]�w ��Ʈw�s�u)
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=products.db"));

            //���U ��Ʀs���h(���U IProductRepository �P ProductRepository)
            services.AddScoped<IProductRepository, ProductRepository>(); 

            //���U Razor Pages
            services.AddRazorPages();

            //���U ProductService
            //AddScoped ��k��� ProductService ���ͩR�g���O�d��ʪ��A�Y�b�C�@�ӽШD�����|�Ыؤ@�ӷs�� ProductService ��ҡC
            services.AddScoped<ProductService>(); 

            services.AddControllers();

            //�s�W Swagger�A��
            services.AddSwaggerGen(c =>
            {
                //v1�GAPI�������W��(�۩w�q)�C
                //Title�GSwagger UI�����D(�۩w�q)�C
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�T�{��e���ҬO�_��"�}�o����"�C
            if (env.IsDevelopment())
            {
                //�ҥζ}�o�̲��`�����C
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //���������|�z�S�ԲӪ����`�H���A�קK���|�ӷP�T���C
                app.UseExceptionHandler("/Error");

                //�j��ϥΪ̪��s�����u��ϥ�"HTTPS"�ӳX�ݡA���C���������v�C
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //��ϥΪ̨ϥ�HTTP�X�ݮɡA�۰ʭ��s�ɦVHTTPS�C
            app.UseHttpsRedirection();
            //�ҥ� �R�A���(�p:CSS�BJavaScript�B�Ϥ���)�A�ȡC
            app.UseStaticFiles();
            //�ҥ� Routing
            app.UseRouting();

            //�ҥ� Swagger
            app.UseSwagger();
            //�]�w Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My WebApi V1");
                c.RoutePrefix = string.Empty; // �]�w���ڸ��|
            });

            //�]�w ���v
            //app.UseAuthorization() �����b app.UseRouting() �M app.UseEndpoints() ��������C
            //��]�GRouting�������T�w�ШD�����|�A�M����v�����n��|�ھ�Routing�����G�i����v�ˬd�C
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  //�Ω�API�A�����z�LRouting�X��API Controlloers
                endpoints.MapRazorPages();   //�Ω�ǲ�Web���ε{��
            });
        }
    }
}
