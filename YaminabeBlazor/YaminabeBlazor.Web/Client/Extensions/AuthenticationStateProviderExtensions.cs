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

using Microsoft.AspNetCore.Components.Authorization;

namespace YaminabeBlazor.Web.Client.Extensions
{
    /// <summary>
    /// 認証プロバイダー拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class AuthenticationStateProviderExtensions
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 認証状態を認証済み設定します。
        /// </summary>
        /// <param name="provider">プロバイダ。</param>
        /// <param name="userId">ユーザーID。</param>
        /// <param name="token">トークン。</param>
        /// <remarks>
        /// <see cref="AuthenticationStateProvider.NotifyAuthenticationStateChanged"/> を呼び出すためのラップメソッド。
        /// </remarks>
        public static void Authroize(
            this AuthenticationStateProvider provider,
            string userId,
            string token
            )
        {
            ((CustomAuthenticationStateProvider)provider).Authroize(
                userId: userId,
                token: token
                );
        }

        /// <summary>
        /// 認証状態を解除に設定します。
        /// </summary>
        /// <param name="provider">プロバイダ。</param>
        /// <remarks>
        /// <see cref="AuthenticationStateProvider.NotifyAuthenticationStateChanged"/> を呼び出すためのラップメソッド。
        /// </remarks>
        public static void UnAuthroze(
            this AuthenticationStateProvider provider
            )
        {
            ((CustomAuthenticationStateProvider)provider).UnAuthroze();

        }

        #endregion
    }
}
