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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Web.Client.Extensions;
using YaminabeBlazor.Web.Client.Services;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Shared
{
    /// <summary>
    /// 基本レイアウトコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class MainLayout
    {
        #region -------------------- property --------------------

        /// <summary>
        /// <see cref="JWTApiService"/> を注入します。
        /// </summary>
        [Inject]
        private IJWTApiService JWTWebApi { get; set; }

        /// <summary>
        /// シングルトン <see cref="AuthenticationStateProvider"/> を注入します。
        /// </summary>
        [Inject]
        private AuthenticationStateProvider AuthProvider { get; set; }

        /// <summary>
        /// ログイン認証情報を取得または設定します。
        /// </summary>
        [Inject]
        private ILoginAuthorizedModel LoginAuthorized { get; set; }

        /// <summary>
        /// ログイン入力情報を取得または設定します。
        /// </summary>
        public LoginInputModel LoginInput { get; set; } = new LoginInputModel() { UserId = "kz", Password = "labo" };

        /// <summary>
        /// サイドバーの展開状態を取得または設定します。
        /// </summary>
        private bool IsSidebarOpend { get; set; } = true;

        /// <summary>
        /// サイドバーの非展開状態のサイドコンテンツのクラス名を取得します。
        /// </summary>
        private string SidebarCloseClassName
        {
            get
            {
                return this.IsSidebarOpend ? null : "close";
            }
        }

        /// <summary>
        /// サイドバーの非展開状態のメインコンテンツのクラス名を取得します。
        /// </summary>
        private string MainFullClassName
        {
            get
            {
                return this.IsSidebarOpend ? null : "full";
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// ログイン認証を行います。
        /// </summary>
        public async Task Login()
        {
            var result = await this.JWTWebApi.Post(this.LoginInput);
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return;
            }
            var loginAuthorized = result.LoginAuthorized;
            if (loginAuthorized.IsAuthorized())
            {
                this.AuthProvider.Authroize(loginAuthorized.UserId, loginAuthorized.Token);
                this.LoginInput.Password = null;
            }
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// ログインボタン押下時の処理を行います。
        /// </summary>
        private async void OnLoginClick()
        {
             await this.Login();
        }

        /// <summary>
        /// メニュー選択時の処理を行います。
        /// </summary>
        public void OnMenuClick()
        {
            this.IsSidebarOpend = false;
        }

        /// <summary>
        /// サイドメニュー選択時の処理を行います。
        /// </summary>
        public void OnSideMenuClick()
        {
            this.IsSidebarOpend = !this.IsSidebarOpend;
        }

        #endregion
    }
}
