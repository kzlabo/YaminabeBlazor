/*
 * Copyright 2020 kzlabo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.Extensions.DependencyInjection;
using YaminabeBlazor.Web.Client.Stub.Services;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Extensions
{
    /// <summary>
    /// サービス拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/11/14" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class ServiceCollectionExtensions
    {
        #region -------------------- method --------------------

        /// <summary>
        /// YaminabeBlazorコンポーネントの初期化を行います。
        /// </summary>
        /// <param name="services">サービス。</param>
        /// <returns>
        /// サービスを返却します。
        /// </returns>
        private static IServiceCollection UseYaminabeBlazorDefaultClientStub(this IServiceCollection services)
        {
            services.AddScoped<IBrandsApiService, BrandsApiService>();
            services.AddScoped<ICustomerMaintenanceSettingApiService, CustomerMaintenanceSettingApiService>();
            services.AddScoped<ICustomersApiService, CustomersApiService>();
            services.AddScoped<IJWTApiService, JWTApiService>();
            services.AddScoped<IProductCategoriesApiService, ProductCategoriesApiService>();
            services.AddScoped<IProductMaintenanceSettingApiService, ProductMaintenanceSettingApiService>();
            services.AddScoped<IProductsApiService, ProductsApiService>();

            return services;
        }

        /// <summary>
        /// YaminabeBlazorコンポーネントの初期化を行います。
        /// </summary>
        /// <param name="services">サービス。</param>
        /// <returns>
        /// サービスを返却します。
        /// </returns>
        public static IServiceCollection UseYaminabeBlazorClientStub(this IServiceCollection services)
        {
            services.AddSingleton<DataBase, DataBase>();

            UseYaminabeBlazorDefaultClientStub(services);

            return services;
        }

        /// <summary>
        /// YaminabeBlazorコンポーネントのレスポンステスト用の初期化を行います。
        /// </summary>
        /// <param name="services">サービス。</param>
        /// <returns>
        /// サービスを返却します。
        /// </returns>
        public static IServiceCollection UseYaminabeBlazorResponseTestClientStub(this IServiceCollection services)
        {
            services.AddSingleton<DataBase, ResponseTestDataBase>();

            UseYaminabeBlazorDefaultClientStub(services);

            return services;
        }

        #endregion
    }
}
