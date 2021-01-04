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
using System;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TList">リストの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynInputSelect<TValue, TList> : ynInputSelectBase<TValue, TList>
        where TList : IListItemModel
    {
        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTreeComponent(RenderTreeBuilder builder)
        {
            if (this.EditableContext == null || this.EditableContext.EditModeOption == EditModeOptions.Edit)
            {
                builder.OpenElement(0, "select");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "value", BindConverter.FormatValue(this.CurrentValueAsString));
                builder.AddAttribute(3, "onchange", EventCallback.Factory.CreateBinder<string>(
                    this,
                    __value =>
                    {
                        this.CurrentValueAsString = __value;
                    },
                    this.CurrentValueAsString
                    ));

                if (this.ListItems != null)
                {
                    var seq = 4;

                    // NULL許容型の場合には空選択リストを追加
                    var type = typeof(TValue);
                    if (typeof(string) == typeof(TValue) || Nullable.GetUnderlyingType(typeof(TValue)) != null)
                    {
                        builder.OpenElement(seq++, "option");
                        builder.AddAttribute(seq++, "value", string.Empty);
                        builder.AddContent(seq++, string.Empty);
                        builder.CloseElement();
                    }

                    // リスト作成
                    foreach (var item in this.ListItems)
                    {
                        builder.OpenElement(seq++, "option");
                        builder.AddAttribute(seq++, "value", item.Key);
                        builder.AddContent(seq++, item.Value);
                        builder.CloseElement();
                    }
                }

                builder.CloseElement();
            }
            else
            {
                builder.AddContent(1000, this.DisplayValue);
            }
        }

        #endregion
    }
}
