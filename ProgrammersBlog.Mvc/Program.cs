using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Mvc.Areas.Admin.Controllers;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Concrete;
using ProgrammersBlog.Services.Extensions;
using Microsoft.Extensions.FileProviders;
using ProgrammersBlog.Data.Concrete.EntityFramework.Mappings;
using ProgrammersBlog.Mvc.AutoMapper.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using MyProject.Business.Concrete;

namespace ProgrammersBlog.Mvc
{

    public class Program
    {

     

        public void ConfigureServices(IServiceCollection services)
        {
            // Kullancaðýmýz servisin sisteme tanýtýlmasý
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(GroupProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(RoleProfile), typeof(EventProfile));
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }
        
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddSession();
            builder.Services.LoadMyServices();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ProgrammersBlogContext>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<IArticleService, ArticleManager>();
            builder.Services.AddScoped<IGroupService, GroupManager>();
            builder.Services.AddScoped<IEventService, EventManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            
            // Kullanacaðýmýz servisin kullanacaðý profillerin tanýtýlmasý
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Assembly.GetExecutingAssembly() bu kod typeof profile yazmak yerine assemly olarak execute edilecek olan sýnýfýn tüürnü direkt çeker 
            var mapperConfig = new MapperConfiguration(mc =>
            {
                 mc.AddProfile(new CategoryProfile()); //AutoMapperProfiles 
                 mc.AddProfile(new ArticleProfile()); //AutoMapperProfiles 
                mc.AddProfile(new ArticleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new EventProfile()); 
                mc.AddProfile(new RoleProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
            app.Run();
        }
    }
}
