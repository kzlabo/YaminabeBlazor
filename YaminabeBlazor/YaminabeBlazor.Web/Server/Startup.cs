using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace YaminabeBlazor.Web.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // -------------------- DI --------------------
            services.AddHttpContextAccessor();

            services.AddTransient<Infrastructure.Repositories.IDbConfig>(r => new Infrastructure.Repositories.DbConfig(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<Core.Models.IExecutionModel, Web.Server.Models.ExecutionModel>();

            // Services
            services.AddTransient<Core.Services.IProductsGetService, Core.Services.ProductsGetService>();
            services.AddTransient<Core.Services.IProductsMaintenanceGetService, Core.Services.ProductsMaintenanceGetService>();
            services.AddTransient<Core.Services.IProductsPutService, Core.Services.ProductsPutService>();
            services.AddTransient<Core.Services.IProductCategoriesGetService, Core.Services.ProductCategoriesGetService>();
            services.AddTransient<Core.Services.IProductCategoriesPutService, Core.Services.ProductCategoriesPutService>();
            services.AddTransient<Core.Services.IBrandsGetService, Core.Services.BrandsGetService>();
            services.AddTransient<Core.Services.IBrandsPutService, Core.Services.BrandsPutService>();
            services.AddTransient<Core.Services.ICustomersGetService, Core.Services.CustomersGetService>();
            services.AddTransient<Core.Services.ICustomersMaintenanceGetService, Core.Services.CustomersMaintenanceGetService>();
            services.AddTransient<Core.Services.ICustomersPutService, Core.Services.CustomersPutService>();

            services.AddTransient<Core.Services.IProductMaintenanceSettingGetService, Core.Services.ProductMaintenanceSettingGetService>();
            services.AddTransient<Core.Services.ICustomerMaintenanceSettingGetService, Core.Services.CustomerMaintenanceSettingGetService>();

            // Queries
            services.AddTransient<Core.Queries.IProductsMaintenanceGetQuery, Infrastructure.Queries.ProductsMaintenanceGetQuery>();

            // Repositories
            services.AddTransient<Core.Repositories.IProductsGetRepository, Infrastructure.Repositories.ProductsGetRepository>();
            services.AddTransient<Core.Repositories.IProductsPutRepository, Infrastructure.Repositories.ProductsPutRepository>();
            services.AddTransient<Core.Repositories.IProductCategoriesGetRepository, Infrastructure.Repositories.ProductCategoriesGetRepository>();
            services.AddTransient<Core.Repositories.IProductCategoriesPutRepository, Infrastructure.Repositories.ProductCategoriesPutRepository>();
            services.AddTransient<Core.Repositories.IBrandsGetRepository, Infrastructure.Repositories.BrandsGetRepository>();
            services.AddTransient<Core.Repositories.IBrandsPutRepository, Infrastructure.Repositories.BrandsPutRepository>();
            services.AddTransient<Core.Repositories.ICustomersGetRepository, Infrastructure.Repositories.CustomersGetRepository>();
            services.AddTransient<Core.Repositories.ICustomersPutRepository, Infrastructure.Repositories.CustomersPutRepository>();

            // -------------------- JWT --------------------
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Issuer"],
                    LifetimeValidator = (
                        DateTime? notBefore,
                        DateTime? expires,
                        SecurityToken securityToken,
                        TokenValidationParameters validationParameters
                        ) =>
                    {
                        return (expires != null && expires > DateTime.UtcNow);
                    },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]))
                };
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(option =>
                {
                    // Ž©“®“I‚È 400 ‰ž“š‚ð–³Œø
                    option.SuppressModelStateInvalidFilter = true;
                });
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new Attributes.ValidateActionFilterAttribute());
                });
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            Infrastructure.Config.Register();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
