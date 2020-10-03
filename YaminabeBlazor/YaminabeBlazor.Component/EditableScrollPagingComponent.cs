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
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 無限スクロールタイプのページングコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データ行の型。</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableScrollPagingComponent<TItem> : ComponentBase
        where TItem : IEditableViewModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public EditableViewCollectionModel<TItem> Items { get; set; }

        /// <summary>
        /// 1ページ内のデータ数を取得または設定します。
        /// </summary>
        [Parameter]
        public int PageRowSize { get; set; } = 10;

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

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// ページングの使用不可状態を取得または設定します。
        /// </summary>
        /// <returns>
        /// ページングが使用不可な場合は <c>true</c> 。使用可の場合は <c>false</c> を返却します。
        /// </returns>
        /// <remarks>
        /// 一時入力モード状態の場合はページングを提供せずに全件表示。
        /// </remarks>
        public bool IsDisabled
        {
            get
            {
                return this.Items.IsTemporaryMode;
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定を行います。
        /// </summary>
        /// <param name="parameters">パラメータリスト。</param>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            if (this.Items != null)
            {
                this.Items.PagingType = PagingTypeOptions.Scroll;
                this.Items.PageRowSize = this.PageRowSize;
                this.Items.StateHasChanged += this.StateHasChanged;
            }

            return base.SetParametersAsync(parameters);
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.Items == null)
            {
                return;
            }

            if (this.Items.IsLastPageIndex == true)
            {
                return;
            }

            builder.OpenElement(0, "button");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.AddAttribute(2, "disabled", this.IsDisabled);
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(
                this, 
                () =>
                {
                    this.Items.MoveNextPage();
                    this.Items.StateHasChanged();
                }));
            builder.AddContent(4, this.ChildContent);
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        #endregion
    }
}
