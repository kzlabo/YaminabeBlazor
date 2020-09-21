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

using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Shared.Services
{
    /// <summary>
    /// JWTのApiサービスを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IJWTApiService
    {
        #region -------------------- method --------------------

        /// <summary>
        /// トークンを発行します。
        /// </summary>
        /// <param name="input">ログイン入力情報。</param>
        /// <returns>
        /// ログイン認証時にはトークンを取得します、ログイン失敗時にはトークンにnullが返却されます。
        /// </returns>
        Task<(HttpStatusCode HttpStatusCode, LoginAuthorizedModel LoginAuthorized)> Post(LoginInputModel input);

        #endregion
    }
}
