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
using System.Linq;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// ソート用のレイヤーコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データ行の型。<</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.LayerComponentBase" />
    /// <seealso cref="YaminabeBlazor.Component.ISorterComponent" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class SortOptionBarComponent<TItem> : LayerComponentBase, ISorterComponent
        where TItem : EditableViewModelBase, new()
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public EditableViewCollectionModel<TItem> Items { get; set; }

        /// <summary>
        /// 子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// ソート条件リストを取得または設定します。
        /// </summary>
        public List<SortConditionComponent> SortConditions { get; set; } = new List<SortConditionComponent>();

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<List<SortConditionComponent>>>(0);
            builder.AddAttribute(1, "Value", this.SortConditions);
            builder.AddAttribute(2, "Name", "CascadeSortConditions");
            builder.AddAttribute(3, "ChildContent", this.ChildContent);
            builder.CloseComponent();

            if (this.IsVisible == true)
            {
                builder.OpenElement(4, "div");
                builder.AddMultipleAttributes(5, this.AdditionalAttributes);
                builder.AddAttribute(6, "class", $"layer-component layer-content {this.ClassName}");

                builder.OpenRegion(1000);
                this.BuildRenderTreeAction(builder);
                builder.CloseRegion();

                builder.OpenElement(7, "div");

                var seq = 0;
                builder.OpenRegion(2000);
                builder.OpenElement(seq++, "ul");
                builder.AddAttribute(seq++, "style", "display:flex; flex-direction:column; list-style:none; padding:0 0 0 0;");
                foreach (var sortCondition in this.SortConditions)
                {
                    builder.OpenElement(seq++, "li");
                    builder.AddAttribute(seq++, "style", "display:flex; margin-top:5px; margin-bottom:5px;");

                    // 項目名
                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "style", "flex-basis: 60%;");
                    builder.AddContent(seq++, sortCondition.DisplayName);
                    builder.CloseElement();

                    // 昇順・降順・優先順
                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "style", "flex-basis: 40%; border:1px solid black; border-bottom:2px solid black;");

                    // 昇順
                    builder.OpenElement(seq++, "button");
                    builder.AddAttribute(seq++, "type", "button");
                    builder.AddAttribute(seq++, "style", "width:35%; height:100%; border:none;");
                    builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(
                        this,
                        __value =>
                        {
                            // 既にソート種別が指定されている場合は解除
                            sortCondition.SetOption(sortCondition.SortOption == SortOptions.Asc ? SortOptions.None : SortOptions.Asc);

                            // 選択行のソート順を強制的に最大値にして後で変更
                            sortCondition.SetNumber(int.MaxValue);

                            // リスト内でソート順を振り直し
                            int? reNumber = 1;
                            foreach (var childSortCondition in this.SortConditions.Where(c => c.SortOption == SortOptions.None))
                            {
                                childSortCondition.SetNumber(null);
                            }
                            foreach (var childSortCondition in this.SortConditions.Where(c => c.SortOption != SortOptions.None).OrderBy(c => c.SortNumber))
                            {
                                childSortCondition.SetNumber(reNumber++);
                            }

                            this.StateHasChanged();
                        }
                        ));
                    builder.AddContent(seq++, sortCondition.SortOption == SortOptions.Asc ? "▲" : "△");
                    builder.CloseElement();

                    // 降順
                    builder.OpenElement(seq++, "button");
                    builder.AddAttribute(seq++, "type", "button");
                    builder.AddAttribute(seq++, "style", "width:35%; height:100%; border:none;");
                    builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(
                        this,
                        __value =>
                        {
                            // 既にソート種別が指定されている場合は解除
                            sortCondition.SetOption(sortCondition.SortOption == SortOptions.Desc ? SortOptions.None : SortOptions.Desc);

                            // 選択行のソート順を強制的に最大値にして後で変更
                            sortCondition.SetNumber(int.MaxValue);

                            // リスト内でソート順を振り直し
                            int? reNumber = 1;
                            foreach (var childSortCondition in this.SortConditions.Where(c => c.SortOption == SortOptions.None))
                            {
                                childSortCondition.SetNumber(null);
                            }
                            foreach (var childSortCondition in this.SortConditions.Where(c => c.SortOption != SortOptions.None).OrderBy(c => c.SortNumber))
                            {
                                childSortCondition.SetNumber(reNumber++);
                            }

                            this.StateHasChanged();
                        }
                        ));
                    builder.AddContent(seq++, sortCondition.SortOption == SortOptions.Desc ? "▼" : "▽");
                    builder.CloseElement();

                    // 優先順
                    builder.OpenElement(seq++, "input");
                    builder.AddAttribute(seq++, "type", "text");
                    builder.AddAttribute(seq++, "style", "width:30%; height:100%; border:none; padding:0 0 0 0; text-align:center;");
                    builder.AddAttribute(seq++, "value", BindConverter.FormatValue(sortCondition.SortNumber));
                    builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<int?>(
                        this,
                        __value =>
                        {
                            sortCondition.SetNumber(__value);
                            this.StateHasChanged();
                        }
                        ,
                        sortCondition.SortNumber
                        ));
                    builder.CloseElement();

                    builder.CloseElement();

                    builder.CloseElement();

                }
                builder.CloseElement();
                builder.CloseRegion();

                builder.CloseElement();

                builder.CloseElement();
            }
        }

        /// <summary>
        /// アクションコンテンツのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected virtual void BuildRenderTreeAction(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");

            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "type", "button");
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, this.OnSortClick));
            builder.AddContent(4, "実行");
            builder.CloseElement();

            builder.OpenElement(5, "button");
            builder.AddAttribute(6, "type", "button");
            builder.AddAttribute(7, "onclick", EventCallback.Factory.Create(this, this.OnSortCancel));
            builder.AddContent(8, "解除");
            builder.CloseElement();

            builder.OpenElement(9, "button");
            builder.AddAttribute(10, "type", "button");
            builder.AddAttribute(11, "onclick", EventCallback.Factory.Create(this, this.OnHideClick));
            builder.AddContent(12, "閉じる");
            builder.CloseElement();

            builder.CloseElement();
        }

        /// <summary>
        /// ソート処理を実行します。
        /// </summary>
        public void SortOn()
        {
            var sortConditionValues = new Dictionary<string, (string PropertyName, bool Desc)>();
            foreach (var sortCondition in this.SortConditions.Where(condition => condition.SortOption != SortOptions.None).OrderBy(condition => condition.SortNumber))
            {
                sortConditionValues[sortCondition.PropertyName] = (PropertyName: sortCondition.PropertyName, Desc: sortCondition.SortOption == SortOptions.Desc);
            }
            this.Items.SortConditionValues = sortConditionValues;
            this.Items.StateHasChanged();
        }

        /// <summary>
        /// ソートを無効に設定します。
        /// </summary>
        public void SortOff()
        {
            foreach (var sortCondition in this.SortConditions)
            {
                sortCondition.SetOption(SortOptions.None);
                sortCondition.SetNumber(null);
            }

            this.Items.SortConditionValues = null;
            this.Items.StateHasChanged();
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// ×ボタン押下時の処理を行います。
        /// </summary>
        protected void OnHideClick()
        {
            this.Hide();
        }

        /// <summary>
        /// ソート実行ボタン押下時の処理を行います。
        /// </summary>
        protected void OnSortClick()
        {
            this.SortOn();
        }

        /// <summary>
        /// ソート解除ボタン押下時の処理を行います。
        /// </summary>
        protected void OnSortCancel()
        {
            this.SortOff();
        }

        #endregion

    }
}
