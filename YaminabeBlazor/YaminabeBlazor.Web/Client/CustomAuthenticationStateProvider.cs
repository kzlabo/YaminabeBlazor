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

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Services;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Client
{
    /// <summary>
    /// 独自認証プロバイダーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="NotifierService"/> を注入します。
        /// </summary>
        private NotifierService _notifier;

        /// <summary>
        /// シングルトン <see cref="HttpClient"/> を注入します。
        /// </summary>
        private HttpClient _http;

        /// <summary>
        /// シングルトン <see cref="JSRuntime"/> を注入します。
        /// </summary>
        private IJSRuntime _jSRuntime;

        /// <summary>
        /// シングルトン <see cref="LoginAuthorizedModel"/> を注入します。
        /// </summary>
        private ILoginAuthorizedModel _loginAuthorized;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomAuthenticationStateProvider"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="notifierService">シングルトン <see cref="NotifierService"/>。</param>
        /// <param name="http">シングルトン <see cref="HttpClient"/>。</param>
        /// <param name="jSRuntime">シングルトン <see cref="JSRuntime"/>。</param>
        /// <param name="loginAuthorized">シングルトン <see cref="LoginAuthorizedModel"/>。</param>
        public CustomAuthenticationStateProvider(
            NotifierService notifierService,
            HttpClient http,
            IJSRuntime jSRuntime,
            ILoginAuthorizedModel loginAuthorized
            )
        {
            this._notifier = notifierService;
            this._http = http;
            this._jSRuntime = jSRuntime;
            this._loginAuthorized = loginAuthorized;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 認証状態を取得します。
        /// </summary>
        /// <returns>
        /// 認証状態を返却します。
        /// </returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            if (this._loginAuthorized.IsAuthorized() == true)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, this._loginAuthorized.UserId), }, "webapi");
                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }

        /// <summary>
        /// 認証状態を認証済みに設定し、認証状態の変更を通知します。
        /// </summary>
        /// <param name="userId">ユーザーID。</param>
        /// <param name="token">トークン。</param>
        public async void Authroize(
            string userId,
            string token
            )
        {
            this._loginAuthorized.SetAuthorizedUser(
                userId: userId,
                token: token
                );

            this._http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            await this._notifier.Update();

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// 認証状態を解除し、認証状態の変更を通知します。
        /// </summary>
        public async void UnAuthroze()
        {
            this._loginAuthorized.SetAuthorizedUser(
                userId: null,
                token: null
                );

            this._http.DefaultRequestHeaders.Authorization = null;

            await this._notifier.Update();

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        #endregion
    }
}
