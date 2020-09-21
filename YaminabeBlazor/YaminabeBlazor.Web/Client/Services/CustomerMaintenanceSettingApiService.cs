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

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Services;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Services
{
    /// <summary>
    /// 得意先マスタメンテナンス画面設定のApiサービスを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Services.WebApiServiceBase" />
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.ICustomerMaintenanceSettingApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerMaintenanceSettingApiService : WebApiServiceBase, ICustomerMaintenanceSettingApiService
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomerMaintenanceSettingApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="http">シングルトン <see cref="HttpClient"/>。</param>
        /// <param name="applicationState">シングルトン <see cref="ApplicationStateService"/>。</param>
        public CustomerMaintenanceSettingApiService(
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
        /// 得意先マスタメンテナンス画面設定を取得します。
        /// </summary>
        /// <returns>
        /// 得意先マスタメンテナンス画面設定を返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, CustomerMaintenanceSettingDto CustomerMaintenanceSetting)> Get()
        {
            return await this.GetMethod<CustomerMaintenanceSettingDto>("api/CustomerMaintenanceSetting");
        }

        #endregion
    }
}
