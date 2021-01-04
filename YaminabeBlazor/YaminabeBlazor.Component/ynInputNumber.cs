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
using System.Globalization;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 数値テキスト入力コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">数値の型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynInputNumber<TValue> : ynInputComponentBase<TValue>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 書式を取得または設定します。（既定値：0）
        /// </summary>
        [Parameter]
        public string Format { get; set; } = "0";

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        private MarkupString DisplayValue
        {
            get
            {
                bool isMark = false;

                // フィルタ条件値が入力値の型に変換できるか判定
                if (BindConverter.TryConvertTo<TValue>(this.FilterConditionValue, CultureInfo.InvariantCulture, out var ret) == true)
                {
                    if (this.CurrentValue.Equals(ret) == true)
                    {
                        isMark = true;
                    }
                }

                // フィルタ条件値に一致する場合は全体をマーカー指定
                if (isMark == true)
                {
                    return $"<mark>{this.CurrentValueAsString}</mark>".ConvertToMarkupString();
                }
                else
                {
                    return this.CurrentValueAsString.ConvertToMarkupString();
                }
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTreeComponent(RenderTreeBuilder builder)
        {
            if (this.EditableContext == null || this.EditableContext.EditModeOption == EditModeOptions.Edit)
            {
                builder.OpenElement(0, "input");
                builder.AddMultipleAttributes(1, this.AdditionalAttributes);
                builder.AddAttribute(2, "type", "number");
                builder.AddAttribute(3, "value", BindConverter.FormatValue(this.CurrentValueAsString));
                builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(
                    this,
                    __value =>
                    {
                        this.CurrentValueAsString = __value;
                    },
                    this.CurrentValueAsString
                    ));
                builder.CloseElement();
            }
            else
            {
                builder.AddContent(1000, this.DisplayValue);
            }
        }

        /// <summary>
        /// 値を文字列に変換した取得します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>
        /// 値を文字列に変換して返却します。
        /// </returns>
        protected override string FormatValueAsString(TValue value)
        {
            if (this.Format == null || value == null)
            {
                return value?.ToString();
            }

            return string.Format(string.Format($"{{0:{this.Format}}}", value));
        }

        /// <summary>
        /// 入力値をバインド項目値の型に変換します。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">返却値。</param>
        /// <returns>
        /// 入力値をバインド項目値の型に変換し、変換に成功した場合は <c>true</c> 。変換に失敗した場合は <c>false</c> を返却します。
        /// </returns>
        protected override bool TryParseValueFromString(string value, out TValue result)
        {
            if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result) == true)
            {
            }
            else
            {
                result = default;
            }
            return true;
        }

        #endregion
    }
}
