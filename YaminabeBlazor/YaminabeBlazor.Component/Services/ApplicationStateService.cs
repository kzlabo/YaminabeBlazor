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

using System;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component.Services
{
    /// <summary>
    /// アプリケーションの状態サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.Services.IApplicationStateService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ApplicationStateService : IApplicationStateService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="NotifierService"/> を注入します。
        /// </summary>
        private NotifierService Notifier { get; set; }

        /// <inheritdoc/>
        public List<NotificationMessageModel> NotificationMessages { get; } = new List<NotificationMessageModel>();

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ApplicationStateService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="notifier">シングルトン <see cref="NotifierService"/>。</param>
        public ApplicationStateService(
            NotifierService notifier
            )
        {
            this.Notifier = notifier;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 通知メッセージを追加します。
        /// </summary>
        /// <param name="title">タイトル。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="notiferType">通知種類。（既定値：通常）</param>
        public void AddNotificationMessage(
            string title,
            string message,
            NotificationMessageModel.NotiferTypeOptions notiferType = NotificationMessageModel.NotiferTypeOptions.None
            )
        {
            this.NotificationMessages.Add(new NotificationMessageModel()
            {
                Title = title,
                Message = message,
                NotiferType = notiferType,
                NotiferDateTime = DateTime.Now
            });
            _ = this.Notifier.Update();
        }

        #endregion
    }
}
