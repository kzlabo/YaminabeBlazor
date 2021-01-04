using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Component.Services;
using YaminabeBlazor.Component.Settings;
using YaminabeBlazor.Web.Client.Extensions;
using YaminabeBlazor.Web.Client.Settings;
using YaminabeBlazor.Web.Client.Stub.Extensions;
using YaminabeBlazor.Web.Shared.Models;

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

            builder.Services.AddSingleton<NotifierService>();
            builder.Services.AddSingleton<ILoginAuthorizedModel>(new LoginAuthorizedModel(string.Empty, string.Empty));
            builder.Services.AddSingleton<IApplicationStateService, ApplicationStateService>();
            builder.Services.AddSingleton<IEditableItemEditSetting, MyEditableItemEditSetting>();
            builder.Services.AddSingleton<IEditableItemDeleteSetting, MyEditableItemDeleteSetting>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.UseYaminabeBlazorComponent();
            builder.Services.UseYaminabeBlazorApi();

            // -------------------- LocalOnly Stub --------------------
            // ローカル環境のみで実行する為のスタブ
            builder.Services.UseYaminabeBlazorClientStub();

            await builder.Build().RunAsync();
        }
    }
}
