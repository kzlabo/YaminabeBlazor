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

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// フィルタ条件コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class FilterConditionComponent : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタコンポーネントを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeFilter")]
        private IFilterComponent Filter { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// フィルタ条件にマッピングするプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string PropertyName { get; set; }

        /// <summary>
        /// フィルタ条件を部分一致で判定するかどうかを取得または設定します。
        /// </summary>
        [Parameter]
        public bool PartialMatch { get; set; } = true;

        /// <summary>
        /// フィルタ条件値を取得または設定します。
        /// </summary>
        public string FilterText { get; set; }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// コンポーネントのレンダリング後に処理を行います。
        /// </summary>
        /// <param name="firstRender">初回。</param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            // フィルタコンポーネントに自身を追加。
            if (this.Filter.FilterConditions.Contains(this) == false)
            {
                this.Filter.FilterConditions.Add(this);
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "value", BindConverter.FormatValue(this.FilterText));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(
                this,
                __value =>
                {
                    this.FilterText = __value;
                    this.Filter.FilterOn();
                },
                this.FilterText
                ));
            builder.CloseElement();
        }

        #endregion
    }
}
