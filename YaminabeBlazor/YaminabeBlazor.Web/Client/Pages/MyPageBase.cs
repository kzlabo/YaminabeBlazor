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

using System.Threading.Tasks;
using YaminabeBlazor.Component;
using YaminabeBlazor.Component.Pages;

namespace YaminabeBlazor.Web.Client.Pages
{
    /// <summary>
    /// アプリケーション固有のページコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Pages.PageBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class MyPageBase : PageBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// レイヤーコンポーネント管理を取得または設定します。
        /// </summary>
        protected LayerManagerComponent LayerManager { get; set; }

        /// <summary>
        /// サイドバーの非展開状態のメインコンテンツのクラス名を取得します。
        /// </summary>
        protected string MainFullClassName
        {
            get
            {
                if (this.LayerManager == null)
                {
                    return "full";
                }
                return this.LayerManager.IsActive ? null : "full";
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// コンポーネントのレンダリング後処理を行います。
        /// </summary>
        /// <param name="firstRender">初回フラグ。</param>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender == true)
            {
                var initializeSuccess = await this.InitializeSetting();
                if (initializeSuccess == false)
                {
                    this.Navigation.NavigateTo("Error");
                }
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 初期設定を行います。
        /// </summary>
        /// <returns>
        /// 初期設定に成功した場合は <c>true</c> 。初期設定に失敗した場合は <c>false</c>。
        /// </returns>
        protected async virtual Task<bool> InitializeSetting()
        {
            return await Task.Run(() => { return true; });
        }

        #endregion
    }
}
