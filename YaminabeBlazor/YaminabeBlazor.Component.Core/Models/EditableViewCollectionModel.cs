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
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Extensions;

namespace YaminabeBlazor.Component.Core.Models
{
    /// <summary>
    /// 編集可能コレクションモデルを提供します。
    /// </summary>
    /// <typeparam name="TItem">データアイテムの型。</typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class EditableViewCollectionModel<TItem> : List<TItem>
        where TItem : IEditableViewModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 一時入力モードかどうかの判定を取得または設定します。
        /// </summary>
        public bool IsTemporaryMode { get; set; }

        /// <summary>
        /// 一時入力データリストを取得または設定します。
        /// </summary>
        private List<TItem> TemporaryItems { get; set; }

        /// <summary>
        /// ページング種別を取得または設定します。
        /// </summary>
        public PagingTypeOptions PagingType { get; set; }

        /// <summary>
        /// ページ位置を取得または設定します。
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 1ページ内のデータ数を取得または設定します。
        /// </summary>
        public int PageRowSize { get; set; } = 10;

        /// <summary>
        /// ページグループ位置を取得まはた設定します。
        /// </summary>
        public int PageGroupIndex { get; set; } = 1;

        /// <summary>
        /// ページグループのサイズを取得または設定します。
        /// </summary>
        public int PageGroupSize { get; set; } = 10;

        /// <summary>
        /// 一時入力アイテム数を取得または設定します。
        /// </summary>
        public int TemporarySize { get; set; } = 10;

        /// <summary>
        /// フィルタ条件リストを取得または設定します。
        /// </summary>
        public Dictionary<string, (string FilterText, bool PartialMatch)> FilterConditionValues { get; set; }

        /// <summary>
        /// ソート条件リストを取得または設定します。
        /// </summary>
        public Dictionary<string, (string PropertyName, bool Desc)> SortConditionValues { get; set; }

        /// <summary>
        /// 有効なデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> ActiveItems
        {
            get
            {
                return this?.Where(item => item.EditState.HasFlag(EditStateOptions.Deleted) == false);
            }
        }

        /// <summary>
        /// フィルタされたデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> FilterItems
        {
            get
            {
                return this.ActiveItems?.FilterItems<TItem>(this.FilterConditionValues);
            }
        }

        /// <summary>
        /// ソートされたデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> SortItems
        {
            get
            {
                return this.FilterItems?.SortItems<TItem>(this.SortConditionValues);
            }
        }

        /// <summary>
        /// 表示対象のデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> ViewItems
        {
            get
            {
                // 一時入力モードのデータアイテムに対してはページング・フィルタ・ソートは無効
                if (this.IsTemporaryMode == true)
                {
                    return this.TemporaryItems;
                }

                // ページング設定に応じた範囲を取得
                switch (this.PagingType)
                {
                    case PagingTypeOptions.None:
                        return this.SortItems;
                    case PagingTypeOptions.Scroll:
                        return this.SortItems?.Take(this.PageIndex * this.PageRowSize);
                    case PagingTypeOptions.Pagination:
                        return this.SortItems?.Skip((this.PageIndex - 1) * this.PageRowSize).Take(this.PageRowSize);
                    default:
                        throw new InvalidOperationException($"not support {this.PagingType.GetType()}");
                }
            }
        }

        /// <summary>
        /// 最大ページ位置を取得します。
        /// </summary>
        public int MaxPageIndex
        {
            get
            {
                if (this.FilterItems == null)
                {
                    return 1;
                }

                // 端数が発生する場合は切り上げ
                var maxPageIndex = this.FilterItems.Count() / this.PageRowSize;
                if (this.FilterItems.Count() % this.PageRowSize > 0)
                {
                    maxPageIndex++;
                }
                return maxPageIndex;
            }
        }

        /// <summary>
        /// ページグループ位置の最初のページ位置を取得します。
        /// </summary>
        public int PageGroupStartPageIndex
        {
            get
            {
                return (this.PageGroupIndex - 1) * this.PageGroupSize + 1;
            }
        }

        /// <summary>
        /// ページグループ位置の最大ページ位置を取得します。
        /// </summary>
        public int PageGroupMaxPageIndex
        {
            get
            {
                var index = this.PageGroupStartPageIndex + this.PageGroupSize - 1;

                return index < this.MaxPageIndex ? index : this.MaxPageIndex;
            }
        }

        /// <summary>
        /// 最大ページグループ位置を取得します。
        /// </summary>
        public int MaxPageGroupIndex
        {
            get
            {
                if (this.FilterItems == null)
                {
                    return 1;
                }
                var index = this.MaxPageIndex / this.PageGroupSize;
                if (this.MaxPageIndex % this.PageGroupSize > 0)
                {
                    index++;
                }
                return index;
            }
        }

        /// <summary>
        /// 状態変更アクションを取得または設定します。
        /// </summary>
        public Action StateHasChanged { get; set; }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// 現在のページ位置が開始ページか判定します。
        /// </summary>
        /// <returns>
        /// 開始ページの場合は <c>true</c> 。開始ページではない場合は <c>false</c>。
        /// </returns>
        public bool IsStartPageIndex
        {
            get
            {
                return this.PageIndex == 1;
            }
        }

        /// <summary>
        /// 現在のページ位置が最終ページか判定します。
        /// </summary>
        /// <returns>
        /// 最終ページの場合は <c>true</c> 。最終ページではない場合は <c>false</c>。
        /// </returns>
        public bool IsLastPageIndex
        {
            get
            {
                return this.PageIndex == this.MaxPageIndex;
            }
        }

        /// <summary>
        /// 現在のページグループ位置が開始ページグループ位置か判定します。
        /// </summary>
        /// <value>
        /// 開始ページグループの場合は <c>true</c> 。開始ページグループではない場合は <c>false</c>。
        /// </value>
        public bool IsStartPageGroupIndex
        {
            get
            {
                return this.PageGroupIndex == 1;
            }
        }

        /// <summary>
        /// 現在のページグループ位置が最終ページグループ位置か判定します。
        /// </summary>
        /// <value>
        /// 最終ページグループの場合は <c>true</c> 。最終ページグループではない場合は <c>false</c>。
        /// </value>
        public bool IsLastPageGroupIndex
        {
            get
            {
                return this.PageGroupIndex == this.MaxPageGroupIndex;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// データアイテムリストを読込みます。
        /// </summary>
        /// <param name="items">データアイテムリスト。</param>
        public void Load(IList<TItem> items)
        {
            this.Clear();
            this.AddRange(items);

            foreach (var item in items)
            {
                item.StateHasChanged += this.StateHasChanged;
                item.ItemRemoved += this.ItemRemoved;
            }
        }

        /// <summary>
        /// 新規データアイテムを追加します。
        /// </summary>
        public void AddNewItem()
        {
            var item = (TItem)Activator.CreateInstance(typeof(TItem));
            item.EditState = EditStateOptions.Added;
            item.StateHasChanged += this.StateHasChanged;
            item.ItemRemoved += this.ItemRemoved;

            this.Add(item);

            this.StateHasChanged?.Invoke();
        }

        /// <summary>
        /// データアイテムを削除します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        private void ItemRemoved(IEditableViewModel item)
        {
            item.StateHasChanged -= this.StateHasChanged;
            item.ItemRemoved -= this.ItemRemoved;

            this.Remove((TItem)item);

            this.StateHasChanged?.Invoke();
        }

        /// <summary>
        /// 一時入力データアイテムを削除します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        private void TemporaryItemRemoved(IEditableViewModel item)
        {
            item.StateHasChanged -= this.StateHasChanged;
            item.ItemRemoved -= this.TemporaryItemRemoved;

            this.TemporaryItems.Remove((TItem)item);

            this.StateHasChanged?.Invoke();
        }

        /// <summary>
        /// 次のページ位置に変更します。
        /// </summary>
        public void MoveNextPage()
        {
            if (this.PageIndex < this.MaxPageIndex)
            {
                this.PageIndex++;
            }
        }

        /// <summary>
        /// 指定のページ位置に変更します。
        /// </summary>
        /// <param name="index">ページ位置。</param>
        public void MovePage(int index)
        {
            if (index < 1 ||
                index > this.MaxPageIndex)
            {
                index = 1;
            }
            if (this.PageIndex.Equals(index) == false)
            {
                this.PageIndex = index;
            }
        }

        /// <summary>
        /// 前のページグループ位置に変更します。
        /// </summary>
        public void MovePreviousPageGroup()
        {
            if (this.PageGroupIndex > 1)
            {
                this.PageGroupIndex--;
            }
        }

        /// <summary>
        /// 次のページグループ位置に変更します。
        /// </summary>
        public void MoveNextPageGroup()
        {
            if (this.PageGroupIndex < this.MaxPageGroupIndex)
            {
                this.PageGroupIndex++;
            }
        }

        /// <summary>
        /// 一時入力モードに設定します。
        /// </summary>
        public void SetTemporary()
        {
            this.TemporaryItems = this.TemporaryItems ?? new List<TItem>();
            for (var i = this.TemporaryItems.Count; i < this.TemporarySize; i++)
            {
                var item = (TItem)Activator.CreateInstance(typeof(TItem));
                item.EditState = EditStateOptions.Added;
                item.StateHasChanged += this.StateHasChanged;
                item.ItemRemoved += this.TemporaryItemRemoved;

                this.TemporaryItems.Add(item);
            }

            this.IsTemporaryMode = true;
        }

        /// <summary>
        /// 一時入力モードで入力したアイテムを確定します。
        /// </summary>
        public void CommitTemporary()
        {
            // 確定対象のアイテムをデータアイテムリストに移動
            foreach (var item in this.TemporaryItems.Where(item => item.EditMode == EditModeOptions.Read && item.EditState.HasFlag(EditStateOptions.Changed) == true))
            {
                item.ItemRemoved -= this.TemporaryItemRemoved;
                item.ItemRemoved += this.ItemRemoved;
                this.Add(item);
            }
            // 確定したアイテムの削除
            this.TemporaryItems.RemoveAll(item => item.EditMode == EditModeOptions.Read && item.EditState.HasFlag(EditStateOptions.Changed) == true);

            this.IsTemporaryMode = false;
        }

        #endregion
    }
}
