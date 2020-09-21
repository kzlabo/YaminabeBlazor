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
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントのテキストエリアコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.EditableInputComponentBase{System.String}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableInputTextAreaComponent : EditableInputComponentBase<string>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        private MarkupString DisplayValue
        {
            get
            {
                return this.Value.HtmlBreak().HtmlMark(this.FilterConditionValue).HtmlSafeDecode().ConvertToMarkupString();
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
            if (this.Item.EditMode == EditModeOptions.Edit)
            {
                if (this.EditableState == EditStateOptions.None ||
                    (this.Item.EditState & this.EditableState) > 0)
                {
                    builder.OpenElement(0, "textarea");
                    builder.AddMultipleAttributes(1, AdditionalAttributes);
                    builder.AddAttribute(2, "class", CssClass);
                    builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
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
                    builder.AddContent(100, this.DisplayValue);
                }
            }
            else
            {
                builder.AddContent(200, this.DisplayValue);
            }

            base.BuildRenderTree(builder);
        }

        /// <summary>
        /// 入力値の文字列型への変換を行います。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">戻り値</param>
        /// <param name="validationErrorMessage">変換エラーメッセージ。</param>
        /// <returns>
        /// 入力値を文字列型に変換した値を返却します。
        /// </returns>
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }

        #endregion
    }
}
