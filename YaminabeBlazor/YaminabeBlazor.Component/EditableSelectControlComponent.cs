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
    /// 編集可能コンポーネントの選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TDropModel">ドロップダウンリストの型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.EditableSelectControlComponentBase{TValue, TDropModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableSelectControlComponent<TValue, TDropModel> : EditableSelectControlComponentBase<TValue, TDropModel>
        where TDropModel : IListItemModel
    {
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

                    builder.OpenElement(0, "select");
                    builder.AddMultipleAttributes(1, AdditionalAttributes);
                    builder.AddAttribute(2, "value", BindConverter.FormatValue(this.CurrentValueAsString));
                    builder.AddAttribute(3, "onchange", EventCallback.Factory.CreateBinder<string>(
                        this,
                        __value =>
                        {
                            this.CurrentValueAsString = __value;
                            this.SelectedCallBack?.Invoke(this.GetSelectedDropItem());
                        },
                        this.CurrentValueAsString
                        ));

                    if (this.DropItems != null)
                    {
                        var seq = 4;
                        var type = typeof(TValue);
                        if (typeof(string) == typeof(TValue) || Nullable.GetUnderlyingType(typeof(TValue)) != null)
                        {
                            builder.OpenElement(seq++, "option");
                            builder.AddAttribute(seq++, "value", string.Empty);
                            builder.AddContent(seq++, string.Empty);
                            builder.CloseElement();
                        }
                        foreach (var dropItem in this.DropItems)
                        {
                            builder.OpenElement(seq++, "option");
                            builder.AddAttribute(seq++, "value", dropItem.Key);
                            builder.AddContent(seq++, dropItem.Value);
                            builder.CloseElement();
                        }
                    }
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

        #endregion
    }
}
