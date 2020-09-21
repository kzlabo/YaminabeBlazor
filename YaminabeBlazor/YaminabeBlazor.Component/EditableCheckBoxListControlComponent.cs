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
    /// 編集可能コンポーネントのラジオボタン選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TCheckModel">チェックボックスリストの型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.EditableSelectControlComponentBase{TValue, TDropModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableCheckBoxListControlComponent<TValue, TCheckModel> : EditableSelectControlComponentBase<TValue, TCheckModel>
        where TValue : Enum
        where TCheckModel : IListItemModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// チェックボックスのグループ名を取得または設定します。（既定値：Guid）
        /// </summary>
        [Parameter]
        public string GroupName { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// バインド項目値の変更時コールバック処理を取得または設定します。
        /// </summary>
        [Parameter]
        public Action<IReadOnlyList<TCheckModel>> CheckedCallBack { get; set; }

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

                    if (this.DropItems != null)
                    {
                        var seq = 0;
                        var flagsAttribute = Attribute.GetCustomAttribute(typeof(TValue), typeof(FlagsAttribute));

                        foreach (var dropItem in this.DropItems)
                        {
                            // 列挙型(ビットタイプ)の 0 は未入力扱いなのでコントロールは作成しない
                            if (flagsAttribute != null &&
                                dropItem.Key.Equals("0") == true)
                            {
                                continue;
                            }

                            builder.OpenElement(seq++, "span");

                            // ラベルに紐づける為にID指定
                            var id = $"{this.GroupName}-{dropItem.Key}";

                            // チェックボックス
                            builder.OpenElement(seq++, "input");
                            builder.AddAttribute(seq++, "type", "checkbox");
                            builder.AddAttribute(seq++, "id", id);
                            builder.AddAttribute(seq++, "name", this.GroupName);
                            if ((int.Parse(this.CurrentValueAsString) & int.Parse(dropItem.Key)) > 0)
                            {
                                builder.AddAttribute(seq++, "checked", true);
                            }
                            builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<bool>(
                                this,
                                __value =>
                                {
                                    if (__value == true)
                                    {
                                        this.CurrentValueAsString = (int.Parse(this.CurrentValueAsString) | int.Parse(dropItem.Key)).ToString();
                                    }
                                    else
                                    {
                                        this.CurrentValueAsString = (int.Parse(this.CurrentValueAsString) & ~int.Parse(dropItem.Key)).ToString();
                                    }
                                    this.CheckedCallBack?.Invoke(this.GetCheckedItems());
                                },
                                ((int.Parse(this.CurrentValueAsString) & int.Parse(dropItem.Key)) > 0)
                                ));
                            builder.CloseElement();

                            // ラベル
                            builder.OpenElement(seq++, "label");
                            builder.AddAttribute(seq++, "for", id);
                            builder.AddContent(seq++, dropItem.Value);
                            builder.CloseElement();

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

        /// <summary>
        /// 選択したリストアイテムを取得します。
        /// </summary>
        /// <returns>
        /// 選択したリストアイテムを返却します。
        /// </returns>
        public TCheckModel[] GetCheckedItems()
        {
            if (this.DropItems == null || this.Value == null)
            {
                return default;
            }

            // 選択値を文字列に変換
            int selectedValue = int.Parse(this.CurrentValueAsString);

            // 選択アイテムを取得して返却
            return this.DropItems?.Where(dropItem => (int.Parse(dropItem.Key) & selectedValue) > 0).ToArray();
        }

        #endregion
    }
}
