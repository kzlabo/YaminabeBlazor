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
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// テキスト表示コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynText : ComponentBase
    {
        #region -------------------- field --------------------

        private object _item;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        protected IOptions<YaminabeBlazorOptions> OptionsAccessor { get; set; }

        /// <summary>
        /// コンポーネントの設定アクセサを取得します。
        /// </summary>
        /// <remarks>
        /// <c>OptionsAccessor.Value</c> でのアクセスを省略。
        /// </remarks>
        protected YaminabeBlazorOptions Options
        {
            get
            {
                return this.OptionsAccessor.Value;
            }
        }

        /// <summary>
        /// データコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeEditableContext")]
        public EditableContext EditableContext { get; set; }

        /// <summary>
        /// フィルタコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeFilterContext")]
        public FilterContext FilterContext { get; set; }

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        [Parameter]
        public object Item
        {
            get
            {
                if (this.EditableContext == null)
                {
                    return this._item;
                }
                return this.EditableContext.Item;
            }
            set
            {
                this._item = value;
            }
        }

        /// <summary>
        /// バインドプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public virtual string PropertyName { get; set; }

        /// <summary>
        /// 書式を取得または設定します。
        /// </summary>
        [Parameter]
        public string Format { get; set; }

        /// <summary>
        /// カレント値を取得または設定します。
        /// </summary>
        protected object CurrentValue
        {
            get
            {
                return this.Options.GetValue<object>(this.Item, this.PropertyName);
            }
        }

        /// <summary>
        /// カレント値を文字列で取得または設定します。
        /// </summary>
        protected string CurrentValueAsString
        {
            get
            {
                return this.FormatValueAsString(this.CurrentValue);
            }
        }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// フィルタ条件文字列を取得します。
        /// </summary>
        public string FilterConditionValue
        {
            get
            {
                if (this.FilterContext == null)
                {
                    return null;
                }
                if (this.FilterContext.FilterConditions.TryGetValue(this.PropertyName, out var @filterCondition) == false)
                {
                    return null;
                }
                return @filterCondition.FilterText;
            }
        }

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        private MarkupString DisplayValue
        {
            get
            {
                return this.CurrentValueAsString.HtmlBreak().HtmlMark(this.FilterConditionValue).HtmlSafeDecode().ConvertToMarkupString();
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
            builder.AddContent(0, this.DisplayValue);
        }

        /// <summary>
        /// 値を文字列に変換した取得します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>
        /// 値を文字列に変換して返却します。
        /// </returns>
        protected string FormatValueAsString(object value)
        {
            if (this.Format == null || value == null)
            {
                return value?.ToString();
            }

            return string.Format(string.Format($"{{0:{this.Format}}}", value));
        }

        #endregion
    }
}
