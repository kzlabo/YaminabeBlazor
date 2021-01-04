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

using System;
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Component.Core.Extensions;

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// コレクション操作コンテキストを提供します。
    /// </summary>
    /// <typeparam name="TItem">データアイテムの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CollectionNavigateContext<TItem> : IDisposable
    {
        #region -------------------- field --------------------

        private List<TItem> _items;
        private IReadOnlyList<TItem> _filteredItems;

        #endregion

        #region -------------------- delegate --------------------

        public Action<CollectionNavigateEventArgs> OnNavigate;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        public List<TItem> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
                this.FilteredItems = this._items;
                this.PagingContext.PageIndex = 1;
                this.PagingContext.PageGroupIndex = 1;
                this.PagingContext.MaxItemCount = this._items?.Count ?? 1;
            }
        }

        public IReadOnlyList<TItem> FilteredItems
        {
            get
            {
                return this._filteredItems;
            }
            set
            {
                this._filteredItems = value;
            }
        }

        /// <summary>
        /// 操作済みデータアイテムリストを取得または設定します。
        /// </summary>
        public IReadOnlyList<TItem> NavigatedItems
        {
            get
            {
                if (this.Items == null)
                {
                    return default;
                }

                return this.PagingContext.GetPagedItems<TItem>(this._filteredItems ?? this.Items).ToList();
            }
        }

        /// <summary>
        /// フィルタコンテキストを取得します。
        /// </summary>
        public FilterContext FilterContext { get; } = new FilterContext();

        /// <summary>
        /// ソートコンテキストを取得します。
        /// </summary>
        public SortContext SortContext { get; } = new SortContext();

        /// <summary>
        /// ページングコンテキストを取得します。
        /// </summary>
        public PagingContext PagingContext { get; } = new PagingContext();

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CollectionNavigateContext"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public CollectionNavigateContext()
        {
            this.FilterContext.OnFilter += OnFilter;
            this.SortContext.OnSort += OnSort;
            this.PagingContext.OnPageMoved += OnPageMoved;
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public void Dispose()
        {
            this.FilterContext.OnFilter -= OnFilter;
            this.SortContext.OnSort -= OnSort;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// データアイテムを追加します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        public void Add(TItem item)
        {
            this._items.Add(item);

            this.FilteredItems = this.Items.FilterItems(this.FilterContext.FilterConditions).SortItems(this.SortContext.SortConditions).ToList();

            // ページングカウント再設定
            this.PagingContext.MaxItemCount = this.FilteredItems.Count;
        }

        /// <summary>
        /// データアイテムを削除します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        public void Remove(TItem item)
        {
            this._items.Remove(item);

            this.FilteredItems = this.Items.FilterItems(this.FilterContext.FilterConditions).SortItems(this.SortContext.SortConditions).ToList();

            // ページングカウント再設定
            this.PagingContext.MaxItemCount = this.FilteredItems.Count;
        }

        /// <summary>
        /// データアイテムリストのフィルタを行います。
        /// </summary>
        /// <param name="filterContext">フィルタコンテキスト。</param>
        /// <remarks>
        /// フィルタ時にはページングを初期化します。
        /// </remarks>
        private void OnFilter(FilterContext filterContext)
        {
            this.FilteredItems = this.Items.FilterItems(this.FilterContext.FilterConditions).SortItems(this.SortContext.SortConditions).ToList();

            // ページング初期化
            this.PagingContext.PageIndex = 1;
            this.PagingContext.PageGroupIndex = 1;
            this.PagingContext.MaxItemCount = this.FilteredItems.Count;

            this.OnNavigate?.Invoke(new CollectionNavigateEventArgs() { Filtered = true });
        }

        /// <summary>
        /// データアイテムリストのソートを行います。
        /// </summary>
        /// <param name="sortContext">ソートコンテキスト。</param>
        private void OnSort(SortContext sortContext)
        {
            this.FilteredItems = this.FilteredItems.SortItems(this.SortContext.SortConditions).ToList();

            this.OnNavigate?.Invoke(new CollectionNavigateEventArgs() { Sorted = true });
        }

        /// <summary>
        /// ページ移動を行います。
        /// </summary>
        /// <param name="pagingContext">ページングコンテキスト。</param>
        private void OnPageMoved(PagingContext pagingContext)
        {
            this.OnNavigate?.Invoke(new CollectionNavigateEventArgs() { PageMoved = true });
        }

        #endregion
    }
}
