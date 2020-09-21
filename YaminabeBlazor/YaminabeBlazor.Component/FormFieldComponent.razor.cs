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

using Microsoft.AspNetCore.Components;
using System;
using System.Linq.Expressions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// フォーム項目コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class FormFieldComponent<TValue> : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// 子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// タイトルの幅を取得または設定します。（既定値：35%）
        /// </summary>
        [Parameter]
        public string TitleWidth { get; set; } = "35%";

        /// <summary>
        /// 項目の幅を取得または設定します。（既定値：65%）
        /// </summary>
        [Parameter]
        public string ControlWidth { get; set; } = "65%";

        /// <summary>
        /// 検証メッセージを取得または設定します。
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>> ValidationMessageExpression { get; set; }

        #endregion
    }
}
