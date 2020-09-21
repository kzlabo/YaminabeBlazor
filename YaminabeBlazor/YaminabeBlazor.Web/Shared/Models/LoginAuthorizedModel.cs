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

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// ログイン認証済みのログインモデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Models.ViewModelBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class LoginAuthorizedModel : ILoginAuthorizedModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ユーザーIDを取得します。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// トークンを取得します。
        /// </summary>
        public string Token { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="LoginAuthorizedModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public LoginAuthorizedModel()
        {
        }

        /// <summary>
        /// <see cref="LoginAuthorizedModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="userId">ユーザーID。</param>
        /// <param name="token">トークン。</param>
        public LoginAuthorizedModel(
            string userId,
            string token
            )
        {
            this.UserId = userId;
            this.Token = token;
        }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// ログイン認証済みかどうかを判定します。
        /// </summary>
        /// <returns>
        /// ログイン認証済みの場合は <c>true</c> 。ログイン認証がされていない場合は <c>false</c> を返却します。
        /// </returns>
        public bool IsAuthorized()
        {
            return !string.IsNullOrEmpty(this.Token);
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 認証情報を設定します。
        /// </summary>
        /// <param name="userId">ユーザーID。</param>
        /// <param name="token">トークン。</param>
        public void SetAuthorizedUser(
            string userId,
            string token
            )
        {
            this.UserId = userId;
            this.Token = token;
        }

        #endregion
    }
}
