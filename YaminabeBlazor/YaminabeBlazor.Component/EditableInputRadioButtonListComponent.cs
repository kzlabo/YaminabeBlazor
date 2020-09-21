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
    /// 編集可能コンポーネントのラジオボタン選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TRadioModel">ラジオボタンストの型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.EditableInputSelectComponentBase{TValue, TDropModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableInputRadioButtonListComponent<TValue, TRadioModel> : EditableInputSelectComponentBase<TValue, TRadioModel>
        where TRadioModel : IListItemModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ラジオボタンのグループ名を取得または設定します。（既定値：Guid）
        /// </summary>
        [Parameter]
        public string GroupName { get; set; } = Guid.NewGuid().ToString("N");

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

                    builder.OpenElement(0, "div");
                    builder.AddMultipleAttributes(1, AdditionalAttributes);
                    builder.AddAttribute(2, "class", CssClass);

                    if (this.DropItems != null)
                    {
                        var seq = 4;
                        foreach (var dropItem in this.DropItems)
                        {
                            // ラベルに紐づける為にID指定
                            var id = $"{this.GroupName}-{dropItem.Key}";

                            // ラジオ
                            builder.OpenElement(seq++, "input");
                            builder.AddAttribute(seq++, "type", "radio");
                            builder.AddAttribute(seq++, "id", id);
                            builder.AddAttribute(seq++, "name", this.GroupName);
                            builder.AddAttribute(seq++, "value", dropItem.Key);
                            if (dropItem.Key.Equals(this.CurrentValueAsString) == true)
                            {
                                builder.AddAttribute(seq++, "checked", true);
                            }
                            builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<string>(
                                this,
                                __value =>
                                {
                                    this.CurrentValueAsString = __value;
                                    this.SelectedCallBack?.Invoke(this.GetSelectedDropItem());
                                },
                                this.CurrentValueAsString
                                ));
                            builder.CloseElement();

                            // ラベル
                            builder.OpenElement(seq++, "label");
                            builder.AddAttribute(seq++, "for", id);
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
