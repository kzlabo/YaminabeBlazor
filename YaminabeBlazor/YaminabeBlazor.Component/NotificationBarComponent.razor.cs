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
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Services;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 通知バーコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class NotificationBarComponent : ComponentBase, IDisposable
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="NotifierService"/> を注入します。
        /// </summary>
        [Inject]
        public NotifierService Notifier { get; set; }

        /// <summary>
        /// シングルトン <see cref="ApplicationStateService"/> を注入します。
        /// </summary>
        [Inject]
        public IApplicationStateService ApplicationState { get; set; }

        /// <summary>
        /// 通知バーコンポーネントの表示状態を取得します。
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.ApplicationState.NotificationMessages.Count > 0;
            }
        }

        /// <summary>
        /// 通知バーの非展開状態のクラス名を取得します。
        /// </summary>
        private string NotificationBarClosedClassName
        {
            get
            {
                return this.IsActive ? null : "close";
            }
        }

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

        /// <summary>
        /// 通知メッセージの通知種類に対応したクラス名を取得します。
        /// </summary>
        /// <param name="notificationMessage">通知メッセージ。</param>
        /// <returns></returns>
        public string GetNotificationMessageClassName(NotificationMessageModel notificationMessage)
        {
            switch (notificationMessage.NotiferType)
            {
                case NotificationMessageModel.NotiferTypeOptions.None:
                    return "";
                case NotificationMessageModel.NotiferTypeOptions.Info:
                    return "is-info";
                case NotificationMessageModel.NotiferTypeOptions.Caution:
                    return "is-warning";
                case NotificationMessageModel.NotiferTypeOptions.Error:
                    return "is-danger";
                default:
                    return "";
            }
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 通知メッセージのクローズボタン押下時処理を行います。
        /// </summary>
        /// <param name="e">イベント引数。</param>
        /// <param name="index">通知メッセージリストのインデックス。</param>
        private void OnCloseClick(MouseEventArgs e, int index)
        {
            this.ApplicationState.NotificationMessages.RemoveAt(index);
        }

        #endregion
    }
}
