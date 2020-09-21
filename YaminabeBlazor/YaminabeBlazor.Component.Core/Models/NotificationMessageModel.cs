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

namespace YaminabeBlazor.Component.Core.Models
{
    /// <summary>
    /// 通知メッセージを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class NotificationMessageModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 通知種類を表します。
        /// </summary>
        public enum NotiferTypeOptions
        {
            /// <summary>
            /// 通常通知を示します。
            /// </summary>
            None = 0,
            /// <summary>
            /// 情報通知を示します。
            /// </summary>
            Info = 1,
            /// <summary>
            /// 警告通知を示します。
            /// </summary>
            Caution = 2,
            /// <summary>
            /// エラー通知を示します。
            /// </summary>
            Error = 3
        }

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 通知タイトルを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 通知種類を取得または設定します。
        /// </summary>
        public NotiferTypeOptions NotiferType { get; set; }

        /// <summary>
        /// 通知日時を取得または設定します。
        /// </summary>
        public DateTime NotiferDateTime { get; set; }

        #endregion
    }
}
