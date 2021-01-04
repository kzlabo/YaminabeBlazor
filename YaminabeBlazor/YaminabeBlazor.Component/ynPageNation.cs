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
using System;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// ページネーションコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynPageNation : ComponentBase, IDisposable
    {
        #region -------------------- field --------------------

        private PagingContext _pagingContext;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        protected IOptions<YaminabeBlazorOptions> OptionsAccessor { get; set; }

        /// <summary>
        /// コンポーネントの設定アクセサを取得します。
        /// </summary>
        /// <remarks>
        /// <c>OptionsAccessor.Value</c> でのアクセスを省略。
        /// </remarks>
        protected YaminabeBlazorOptions Options
        {
            get
            {
                return this.OptionsAccessor.Value;
            }
        }

        /// <summary>
        /// ページングコンテキストを取得または設定します。
        /// </summary>
        [Parameter]
        public PagingContext Context
        {
            get
            {
                return this._pagingContext;
            }
            set
            {
                if (this._pagingContext != null)
                {
                    this._pagingContext.StateHasChanged -= this.StateHasChanged;
                }
                this._pagingContext = value;
                this._pagingContext.StateHasChanged += this.StateHasChanged;
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
                return false;
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
                return this.IsDisabled || this.Context.IsStartPageGroupIndex;
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
                return this.IsDisabled || this.Context.IsLastPageGroupIndex;
            }
        }

        /// <summary>
        /// 前ページングテキストを取得します。
        /// </summary>
        private string PreviousText
        {
            get
            {
                return this.Options.GetWordResouce(nameof(WordResource.PagenationPrevious));
            }
        }

        /// <summary>
        /// 次ページングテキストを取得します。
        /// </summary>
        public string NextText
        {
            get
            {
                return this.Options.GetWordResouce(nameof(WordResource.PagenationNext));
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public virtual void Dispose()
        {
            this._pagingContext.StateHasChanged -= this.StateHasChanged;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.Context.MaxPageIndex <= 1 || this.Context.PagingType.Equals(PagingTypeOptions.Pagination) == false)
            {
                return;
            }

            builder.OpenElement(0, "ul");
            builder.AddAttribute(1, "class", "yn-page-nation");

            // 前へ
            builder.OpenElement(2, "li");
            builder.OpenElement(3, "button");
            builder.AddAttribute(4, "class", "yn-page-previous");
            builder.AddAttribute(5, "disabled", this.IsPreviousPagingDisabled);
            builder.AddAttribute(6, "onclick", EventCallback.Factory.Create(
                this,
                () =>
                {
                    this.Context.MovePreviousPageGroup();
                }));
            builder.AddContent(7, this.PreviousText);
            builder.CloseElement();
            builder.CloseElement();

            // ページ
            builder.OpenRegion(1000);
            var seq = 0;
            var maxIndex = this.Context.PageGroupMaxPageIndex;
            for (var i = this.Context.PageGroupStartPageIndex; i <= maxIndex; i++)
            {
                var index = i;
                builder.OpenElement(seq++, "li");
                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "class", "yn-page-index");
                builder.AddAttribute(seq++, "disabled", this.IsDisabled);
                builder.AddAttribute(seq++, "data-yn-paged", this.Context.PageIndex == index);
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(
                    this,
                    () =>
                    {
                        this.Context.MovePage(index);
                    }));
                builder.AddContent(seq++, index);
                builder.CloseElement();
                builder.CloseElement();
            }
            builder.CloseRegion();

            // 次へ
            builder.OpenElement(8, "li");
            builder.OpenElement(9, "button");
            builder.AddAttribute(10, "class", "yn-page-next");
            builder.AddAttribute(11, "disabled", this.IsNextPagingDisabled);
            builder.AddAttribute(12, "onclick", EventCallback.Factory.Create(
                this,
                () =>
                {
                    this.Context.MoveNextPageGroup();
                }));
            builder.AddContent(13, this.NextText);
            builder.CloseElement();
            builder.CloseElement();

            builder.CloseElement();
        }

        #endregion
    }
}
