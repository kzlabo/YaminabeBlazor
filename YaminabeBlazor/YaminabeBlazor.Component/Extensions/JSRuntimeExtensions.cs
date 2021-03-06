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

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace YaminabeBlazor.Component.Extensions
{
    /// <summary>
    /// javascriptランタイム拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class JSRuntimeExtensions
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 確認メッセージを表示します。
        /// </summary>
        /// <param name="jSRuntime">ランタイム。</param>
        /// <param name="message">確認メッセージ。</param>
        /// <returns>
        /// OKが選択された場合は <c>true</c> 。Cancelが選択された場合は <c>false</c> 。
        /// </returns>
        public async static Task<bool> Confirm(
            this IJSRuntime jSRuntime,
            string message
            )
        {
            return await jSRuntime.InvokeAsync<bool>("confirm", message);
        }

        /// <summary>
        /// 警告メッセージを表示します。
        /// </summary>
        /// <param name="jSRuntime">ランタイム。</param>
        /// <param name="message">警告メッセージ。</param>
        public async static Task Alert(
            this IJSRuntime jSRuntime,
            string message
            )
        {
            await jSRuntime.InvokeVoidAsync("alert", message);
        }

        #endregion
    }
}
