using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace YaminabeBlazor.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.RootComponents.Add<App>("app");

            // -------------------- DI --------------------
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<YaminabeBlazor.Component.Services.IApplicationStateService, YaminabeBlazor.Component.Services.ApplicationStateService>();
            builder.Services.AddSingleton<YaminabeBlazor.Web.Shared.Models.ILoginAuthorizedModel>(new YaminabeBlazor.Web.Shared.Models.LoginAuthorizedModel(string.Empty, string.Empty));
            builder.Services.AddSingleton<YaminabeBlazor.Component.Services.NotifierService>();
            builder.Services.AddSingleton<YaminabeBlazor.Component.Settings.IEditableItemEditSetting, Settings.MyEditableItemEditSetting>();
            builder.Services.AddSingleton<YaminabeBlazor.Component.Settings.IEditableItemDeleteSetting, Settings.MyEditableItemDeleteSetting>();

            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IJWTApiService, YaminabeBlazor.Web.Client.Services.JWTApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductsApiService, YaminabeBlazor.Web.Client.Services.ProductsApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductCategoriesApiService, YaminabeBlazor.Web.Client.Services.ProductCategoriesApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IBrandsApiService, YaminabeBlazor.Web.Client.Services.BrandsApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.ICustomersApiService, YaminabeBlazor.Web.Client.Services.CustomersApiService>();

            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductMaintenanceSettingApiService, YaminabeBlazor.Web.Client.Services.ProductMaintenanceSettingApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.ICustomerMaintenanceSettingApiService, YaminabeBlazor.Web.Client.Services.CustomerMaintenanceSettingApiService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            // -------------------- LocalOnly Stub --------------------
            // ローカル環境のみで実行する為のスタブ
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IBrandsApiService, YaminabeBlazor.Web.Client.Stub.Services.BrandsApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.ICustomerMaintenanceSettingApiService, YaminabeBlazor.Web.Client.Stub.Services.CustomerMaintenanceSettingApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.ICustomersApiService, YaminabeBlazor.Web.Client.Stub.Services.CustomersApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IJWTApiService, YaminabeBlazor.Web.Client.Stub.Services.JWTApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductCategoriesApiService, YaminabeBlazor.Web.Client.Stub.Services.ProductCategoriesApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductMaintenanceSettingApiService, YaminabeBlazor.Web.Client.Stub.Services.ProductMaintenanceSettingApiService>();
            builder.Services.AddScoped<YaminabeBlazor.Web.Shared.Services.IProductsApiService, YaminabeBlazor.Web.Client.Stub.Services.ProductsApiService>();

            await builder.Build().RunAsync();
        }
    }
}
