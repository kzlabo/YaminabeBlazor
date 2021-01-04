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
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// ページ指定タイプのページングコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データ行の型。</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditablePageNationComponent<TItem> : ComponentBase
        where TItem : IEditableViewModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        private IOptions<YaminabeBlazorOptions> OptionsAccessor { get; set; }

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
        /// ページグループのサイズを取得または設定します。
        /// </summary>
        [Parameter]
        public int PageGroupSize { get; set; } = 10;

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// コンポーネントのクラス名を取得します。
        /// </summary>
        public virtual string ClassName
        {
            get
            {
                var className = string.Empty;
                if (this.AdditionalAttributes != null)
                {
                    className = this.AdditionalAttributes.ContainsKey("class") ? this.AdditionalAttributes["class"].ToString() : string.Empty;
                }
                className += " pagenation";
                return className;
            }
        }

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

        /// <summary>
        /// 前ページングの使用不可状態を取得または設定します。
        /// </summary>
        /// <returns>
        /// 前ページングが使用不可な場合は <c>true</c> 。使用可の場合は <c>false</c> を返却します。
        /// </returns>
        /// <remarks>
        /// 一時入力モード状態の場合または開始ページグループの場合は使用不可。
        /// </remarks>
        public bool IsPreviousPagingDisabled
        {
            get
            {
                return this.IsDisabled || this.Items.IsStartPageGroupIndex;
            }
        }

        /// <summary>
        /// 次ページングの使用不可状態を取得または設定します。
        /// </summary>
        /// <returns>
        /// 次ページングが使用不可な場合は <c>true</c> 。使用可の場合は <c>false</c> を返却します。
        /// </returns>
        /// <remarks>
        /// 一時入力モード状態の場合または終了ページグループの場合は使用不可。
        /// </remarks>
        public bool IsNextPagingDisabled
        {
            get
            {
                return this.IsDisabled || this.Items.IsLastPageGroupIndex;
            }
        }

        /// <summary>
        /// 前ページングテキストを取得します。
        /// </summary>
        private string PreviousText
        {
            get
            {
                return this.OptionsAccessor.Value.GetWordResouce(nameof(WordResource.PagenationPrevious));
            }
        }

        /// <summary>
        /// 次ページングテキストを取得します。
        /// </summary>
        public string NextText
        {
            get
            {
                return this.OptionsAccessor.Value.GetWordResouce(nameof(WordResource.PagenationNext));
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
                this.Items.PagingType = PagingTypeOptions.Pagination;
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

            if (this.Items.PageGroupMaxPageIndex <= 1 && this.Items.PageGroupMaxPageIndex <= 1)
            {
                return;
            }

            builder.OpenElement(0, "ul");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.AddAttribute(2, "class", this.ClassName);
            builder.AddAttribute(3, "style", "display:flex; list-style:none; padding:0 0 0 0;");

            // 前へ
            builder.OpenElement(3, "li");
            builder.OpenElement(4, "button");
            builder.AddAttribute(5, "disabled", this.IsPreviousPagingDisabled);
            builder.AddAttribute(6, "style", "width:4.0rem; height:1.5rem;");
            builder.AddAttribute(7, "onclick", EventCallback.Factory.Create(
                this,
                () =>
                {
                    this.Items.MovePreviousPageGroup();
                    this.Items.StateHasChanged();
                }));
            builder.AddContent(8, this.PreviousText);
            builder.CloseElement();
            builder.CloseElement();

            // ページ
            builder.OpenRegion(1000);
            var seq = 0;
            var maxIndex = this.Items.PageGroupMaxPageIndex;
            for (var i = this.Items.PageGroupStartPageIndex; i <= maxIndex; i++)
            {
                var index = i;
                builder.OpenElement(seq++, "li");
                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "disabled", this.IsDisabled || index == this.Items.PageIndex);
                builder.AddAttribute(seq++, "style", "width:3.0rem; height:1.5rem;");
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(
                    this,
                    () =>
                    {
                        this.Items.MovePage(index);
                        this.Items.StateHasChanged();
                    }));
                builder.AddContent(seq++, index);
                builder.CloseElement();
                builder.CloseElement();
            }
            builder.CloseRegion();

            // 次へ
            builder.OpenElement(9, "li");
            builder.OpenElement(10, "button");
            builder.AddAttribute(11, "disabled", this.IsNextPagingDisabled);
            builder.AddAttribute(12, "style", "width:4.0rem; height:1.5rem;");
            builder.AddAttribute(13, "onclick", EventCallback.Factory.Create(
                this,
                () =>
                {
                    this.Items.MoveNextPageGroup();
                    this.Items.StateHasChanged();
                }));
            builder.AddContent(14, this.NextText);
            builder.CloseElement();
            builder.CloseElement();

            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        #endregion
    }
}
