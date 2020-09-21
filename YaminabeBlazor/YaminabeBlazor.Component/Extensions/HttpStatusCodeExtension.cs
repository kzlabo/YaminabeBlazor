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

namespace YaminabeBlazor.Component.Extensions
{
    /// <summary>
    /// HTTPステータスコード拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class HttpStatusCodeExtension
    {
        #region -------------------- method --------------------

        /// <summary>
        /// HTTPステータスコードが成功を示しているかを判定します。
        /// </summary>
        /// <param name="statusCode">インスタンス。</param>
        /// <returns>
        /// HTTPステータスコードが 200 ～ 299 の範囲の場合は <c>true</c> 。他の場合は <c>false</c> 。
        /// </returns>
        public static bool IsSuccessStatusCode(
            this HttpStatusCode statusCode
            )
        {
            return (int)statusCode >= 200 && (int)statusCode <= 299;
        }

        #endregion
    }
}
