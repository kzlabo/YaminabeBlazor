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

using Microsoft.JSInterop;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 検証結果の通知方法を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ValidationNotifier : IValidationNotifier
    {
        #region -------------------- field --------------------

        private IJSRuntime _javascript;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ValidationNotifier"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="javascript">シングルトン <see cref="JSRuntime"/> 。</param>
        public ValidationNotifier(IJSRuntime javascript)
        {
            this._javascript = javascript;
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public async void ValidationErrorNotice(string message)
        {
            await this._javascript.Alert(message);
        }

        #endregion
    }
}
