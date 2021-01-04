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
using System.ComponentModel.DataAnnotations;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// テーブルヘッダーコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynTh : ComponentBase
    {
        #region -------------------- field --------------------

        private SortCondition _sortCondition;
        private string _title;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// ソートコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeSortContext")]
        public SortContext SortContext { get; set; }

        /// <summary>
        /// データアイテムの型を取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeItemType")]
        public Type ItemType { get; set; }

        /// <summary>
        /// ソート対象のプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string PropertyName { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        /// <remarks>
        /// <see cref="Title"/> プロパティを設定していない場合、
        /// モデルのプロパティに <see cref="DisplayAttribute"/> を付与している場合は <see cref="DisplayAttribute.Name"/> を取得。
        /// </remarks>
        public string Title
        {
            get
            {
                var title = this._title;
                if (title == null &&
                    this.PropertyName != null &&
                    this.ItemType != null
                    )
                {
                    var property = this.ItemType.GetProperty(this.PropertyName);
                    var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayAttribute));
                    if (attribute != null)
                    {
                        title = attribute.Name;
                    }
                }
                return title;
            }
            set
            {
                this._title = value;
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定後初期化処理を行います。
        /// </summary>
        protected override void OnParametersSet()
        {
            _ = this.SortContext.SortConditions.TryGetValue(this.PropertyName, out this._sortCondition);
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "th");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "yn-th");

            builder.OpenElement(100, "div");
            builder.AddAttribute(101, "class", "yn-th-title");
            builder.AddAttribute(102, "onclick", EventCallback.Factory.Create(this, () =>
            {
                OnIncrementt();
            }));
            if (this.ChildContent == null)
            {
                builder.AddContent(103, this.Title);
            }
            else
            {
                builder.AddContent(103, this.ChildContent);
            }
            builder.CloseElement();

            if (this._sortCondition != null && this._sortCondition.SortOption != SortOptions.None)
            {
                builder.OpenElement(200, "div");
                builder.AddAttribute(201, "class", $"yn-th-sort-mark yn-arrow {(this._sortCondition.SortOption == SortOptions.Asc ? "asc": "desc")}");
                builder.CloseElement();

                builder.OpenElement(300, "div");
                builder.AddAttribute(301, "class", "yn-th-sort-no");
                builder.AddContent(302, this._sortCondition.SortNo);
                builder.CloseElement();
            }

            builder.CloseElement();
            builder.CloseElement();
        }

        /// <summary>
        /// ソート条件を未指定・昇順・降順の順にインクリメントを行います。
        /// </summary>
        private void OnIncrementt()
        {
            this.SortContext.Increment(this.PropertyName);
        }

        #endregion
    }
}
