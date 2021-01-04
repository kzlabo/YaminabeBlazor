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
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// フィルタ条件入力コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynFilterInputText : ComponentBase
    {
        #region -------------------- field --------------------

        private string _filterText;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// フィルタコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeFilterContext")]
        public FilterContext FilterContext { get; set; }

        /// <summary>
        /// プロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string PropertyName { get; set; }

        /// <summary>
        /// フィルタテキストを部分一致で判定要否を取得または設定します。
        /// </summary>
        [Parameter]
        public bool PartialMatch { get; set; } = true;

        /// <summary>
        /// フィルタテキストを取得または設定します。
        /// </summary>
        [Parameter]
        public string FilterText
        {
            get
            {
                return this._filterText;
            }
            set
            {
                this._filterText = value;

                this.FilterContext.Set(this.PropertyName, this._filterText, this.PartialMatch);
            }
        }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "value", this.FilterText);
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(
                this,
                __value =>
                {
                    this.FilterText = __value;
                },
                this.FilterText
                ));
            builder.CloseElement();
        }

        #endregion
    }
}
