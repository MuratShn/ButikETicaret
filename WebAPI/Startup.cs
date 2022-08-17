using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.Identitiy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            services.AddCors();

            services.AddSingleton<IProductDal, EfProductDal>();
            services.AddSingleton<IProductManager, ProductService>();

            services.AddSingleton<IProductFeatureDal, EfProductFeatureDal>();
            services.AddSingleton<IProductFeatureManager, ProductFeatureService>();

            services.AddSingleton<ICategoryDal, EfCategoryDal>();
            services.AddSingleton<ICategoryManager, CategoryService>();

            services.AddSingleton<IProductImageDal, EfProductImageDal>();
            services.AddSingleton<IProductImageManager, ProductImageService>();

            services.AddSingleton<ILikedProductDal, EfLikedProductDal>();
            services.AddSingleton<ILikedProductManager, LikedProductService>();

            services.AddSingleton<IAddressDal, EfAddressDal>();
            services.AddSingleton<IAddressManager, AddressService>();

            services.AddSingleton<IOrderDal, EfCoreOrderDal>();
            services.AddSingleton<IOrderManager, OrderService>();

            services.AddSingleton<ISoldProductDal, EfCoreSoldProductDal>();
            services.AddSingleton<ISoldProductManager, SoldProductService>();

            services.AddSingleton<IProductCommentDal, EfProductCommentDal>();
            services.AddSingleton<IProductCommentManager, ProductCommentService>();


            services.AddScoped<ITokenManager, TokenService>();

            services.AddScoped<IIdentityManager, IdentityService>();

            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>(x=> {
                x.Password.RequiredLength = 3;
                x.Password.RequireDigit = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<Context>();

            services.AddAuthentication(x=> {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience=Configuration["Token:Audience"],
                        ValidIssuer= Configuration["Token:Issuer"],
                        IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                        ClockSkew = TimeSpan.Zero
                        
                    };
                });

            services.AddHttpClient();

            services.AddSingleton<IConfiguration>(provider => Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
