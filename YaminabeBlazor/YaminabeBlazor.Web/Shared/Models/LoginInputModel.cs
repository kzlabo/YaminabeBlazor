﻿/*
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

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// ログイン画面入力モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Models.InputViewModelBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class LoginInputModel : InputViewModelBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ユーザーIDを取得または設定します。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// パスワードを取得または設定します。
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}
