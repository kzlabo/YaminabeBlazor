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
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Services;

namespace YaminabeBlazor.Component.Pages
{
    /// <summary>
    /// 基底のページコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class PageBase : ComponentBase, IDisposable
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="ApplicationStateService"/> を注入します。
        /// </summary>
        [Inject]
        protected IApplicationStateService ApplicationState { get; set; }

        /// <summary>
        /// シングルトン <see cref="NotifierService"/> を注入します。
        /// </summary>
        [Inject]
        protected NotifierService Notifier { get; set; }

        /// <summary>
        /// シングルトン <see cref="JSRuntime"/> を注入します。
        /// </summary>
        [Inject]
        protected IJSRuntime JavascriptRuntime { get; set; }

        /// <summary>
        /// シングルトン <see cref="NavigationManager"/> を注入します。
        /// </summary>
        [Inject]
        protected NavigationManager Navigation { get; set; }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        protected override void OnInitialized()
        {
            this.Notifier.Notify += OnNotify;
        }

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public void Dispose()
        {
            this.Notifier.Notify -= OnNotify;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 通知イベント。
        /// </summary>
        public async Task OnNotify()
        {
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        #endregion
    }
}

