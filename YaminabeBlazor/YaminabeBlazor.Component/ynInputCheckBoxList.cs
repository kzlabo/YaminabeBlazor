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
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 選択チェックボックスリストコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TList">リストの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynInputCheckBoxList<TValue, TList> : ynInputSelectBase<TValue, TList>
        where TList : IListItemModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// チェックボックスのグループ名を取得または設定します。（既定値：Guid）
        /// </summary>
        [Parameter]
        public string GroupName { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 表示用文字列の区切り文字を取得または設定します。
        /// </summary>
        [Parameter]
        public string Separetor { get; set; } = "\n";

        protected override TValue CurrentValue
        {
            get
            {
                return this.Options.GetValue<TValue>(this.Item, this.ValueName);
            }
            set
            {
                var currentValue = this.Options.GetValue<TValue>(this.Item, this.ValueName);
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, currentValue);
                if (hasChanged == true)
                {
                    this.Options.SetValue(this.Item, this.ValueName, value);

                    var first = true;
                    var currentText = string.Empty;
                    var checkedItems = GetCheckedItems();
                    foreach (var checkedItem in checkedItems)
                    {
                        if (first == false)
                        {
                            currentText += this.Separetor;
                        }
                        currentText += checkedItem.Value;
                        first = false;
                    }

                    this.Options.SetValue(this.Item, this.TextName, currentText);

                    if (this.EditableContext != null)
                    {
                        this.EditableContext.EditState |= EditStateOptions.Changed;
                    }
                }
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
            if (this.EditableContext == null || this.EditableContext.EditModeOption == EditModeOptions.Edit)
            {
                builder.OpenElement(0, "div");
                builder.AddMultipleAttributes(1, AdditionalAttributes);

                if (this.ListItems != null)
                {
                    var seq = 0;
                    var flagsAttribute = Attribute.GetCustomAttribute(typeof(TValue), typeof(FlagsAttribute));

                    foreach (var listItem in this.ListItems)
                    {
                        // 列挙型(ビットタイプ)の 0 は未入力扱いなのでコントロールは作成しない
                        if (flagsAttribute != null &&
                            listItem.Key.Equals("0") == true)
                        {
                            continue;
                        }

                        builder.OpenElement(seq++, "span");

                        // ラベルに紐づける為にID指定
                        var id = $"{this.GroupName}-{listItem.Key}";

                        // チェックボックス
                        builder.OpenElement(seq++, "input");
                        builder.AddAttribute(seq++, "type", "checkbox");
                        builder.AddAttribute(seq++, "id", id);
                        builder.AddAttribute(seq++, "name", this.GroupName);
                        if ((int.Parse(this.CurrentValueAsString) & int.Parse(listItem.Key)) > 0)
                        {
                            builder.AddAttribute(seq++, "checked", true);
                        }
                        builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<bool>(
                            this,
                            __value =>
                            {
                                if (__value == true)
                                {
                                    this.CurrentValueAsString = (int.Parse(this.CurrentValueAsString) | int.Parse(listItem.Key)).ToString();
                                }
                                else
                                {
                                    this.CurrentValueAsString = (int.Parse(this.CurrentValueAsString) & ~int.Parse(listItem.Key)).ToString();
                                }
                            },
                            ((int.Parse(this.CurrentValueAsString) & int.Parse(listItem.Key)) > 0)
                            ));
                        builder.CloseElement();

                        // ラベル
                        builder.OpenElement(seq++, "label");
                        builder.AddAttribute(seq++, "for", id);
                        builder.AddContent(seq++, listItem.Value);
                        builder.CloseElement();

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

        /// <summary>
        /// 選択したリストアイテムを取得します。
        /// </summary>
        /// <returns>
        /// 選択したリストアイテムを返却します。
        /// </returns>
        public TList[] GetCheckedItems()
        {
            if (this.ListItems == null || this.CurrentValue == null)
            {
                return default;
            }

            // 選択値を文字列に変換
            int selectedValue = int.Parse(this.CurrentValueAsString);

            // 選択アイテムを取得して返却
            return this.ListItems?.Where(listItem => (int.Parse(listItem.Key) & selectedValue) > 0).ToArray();
        }

        #endregion
    }
}
