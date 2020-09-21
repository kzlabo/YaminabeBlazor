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

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Component.Services;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Services
{
    /// <summary>
    /// 得意先マスタリストのApiサービスを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Services.WebApiServiceBase" />
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.ICustomersApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomersApiService : WebApiServiceBase, ICustomersApiService
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="http">シングルトン <see cref="HttpClient"/>。</param>
        /// <param name="applicationState">シングルトン <see cref="ApplicationStateService"/>。</param>
        public CustomersApiService(
            HttpClient http,
            IApplicationStateService applicationState
            ) : base(
                http: http,
                applicationState: applicationState
                )
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 得意先マスタリストを返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, List<CustomerInputModel> Customers)> Get()
        {
            return await this.GetMethod<List<CustomerInputModel>>("api/CustomersMaintenance");
        }

        /// <summary>
        /// 得意先マスタリストを更新します。
        /// </summary>
        /// <param name="input">得意先マスタリストの更新対象。</param>
        public async Task<HttpStatusCode> Put(IEnumerable<CustomerInputModel> input)
        {
            return await this.PutMethod("api/Customers", input.GetPutTarget());
        }

        #endregion
    }
}
