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
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component.Services
{
    /// <summary>
    /// アプリケーションの状態サービスを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IApplicationStateService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 通知メッセージリストを取得します。
        /// </summary>
        List<NotificationMessageModel> NotificationMessages { get; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 通知メッセージを追加します。
        /// </summary>
        /// <param name="title">タイトル。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="notiferType">通知種類。（既定値：通常）</param>
        void AddNotificationMessage(
            string title,
            string message,
            NotificationMessageModel.NotiferTypeOptions notiferType = NotificationMessageModel.NotiferTypeOptions.None
            );

        #endregion
    }
}
