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

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// ページングコンテキストを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class PagingContext
    {
        #region -------------------- delegate --------------------

        public Action StateHasChanged;
        public Action<PagingContext> OnPageMoved;
        public Action<PagingContext> OnPageGroupMoved;

        #endregion

        #region -------------------- property --------------------

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
        public int PageRowSize { get; set; } = 50;

        /// <summary>
        /// ページグループ位置を取得まはた設定します。
        /// </summary>
        public int PageGroupIndex { get; set; } = 1;

        /// <summary>
        /// ページグループのサイズを取得または設定します。
        /// </summary>
        public int PageGroupSize { get; set; } = 10;

        /// <summary>
        /// 最大データアイテム数を取得または設定します。
        /// </summary>
        public int MaxItemCount { get; set; }

        /// <summary>
        /// 表示アイテムの開始位置を取得します。
        /// </summary>
        public int StartItemIndex
        {
            get
            {
                switch (this.PagingType)
                {
                    case PagingTypeOptions.None:
                        return 1;
                    case PagingTypeOptions.Scroll:
                        return 1;
                    case PagingTypeOptions.Pagination:
                        return (this.PageIndex - 1) * this.PageRowSize + 1;
                    default:
                        throw new InvalidOperationException($"not support {this.PagingType.GetType()}");
                }
            }
        }

        /// <summary>
        /// 表示アイテムの終了位置を取得します。
        /// </summary>
        public int EndItemIndex
        {
            get
            {
                switch (this.PagingType)
                {
                    case PagingTypeOptions.None:
                        return this.MaxItemCount;
                    case PagingTypeOptions.Scroll:
                        return this.PageIndex * this.PageRowSize;
                    case PagingTypeOptions.Pagination:
                        return this.StartItemIndex + this.PageRowSize - 1 < this.MaxItemCount ? this.StartItemIndex + this.PageRowSize - 1 : this.MaxItemCount;
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
                // 端数が発生する場合は切り上げ
                var maxPageIndex = MaxItemCount / this.PageRowSize;
                if (MaxItemCount % this.PageRowSize > 0)
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
                var index = this.MaxPageIndex / this.PageGroupSize;
                if (this.MaxPageIndex % this.PageGroupSize > 0)
                {
                    index++;
                }
                return index;
            }
        }

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
        /// 次のページ位置に変更します。
        /// </summary>
        public void MoveNextPage()
        {
            if (this.PageIndex < this.MaxPageIndex)
            {
                this.PageIndex++;

                this.OnPageMoved?.Invoke(this);
            }
        }

        /// <summary>
        /// 指定のページ位置に変更します。
        /// </summary>
        /// <param name="index">ページ位置。</param>
        public void MovePage(int index)
        {
            if (index < 1 || index > this.MaxPageIndex)
            {
                index = 1;
            }
            if (this.PageIndex.Equals(index) == false)
            {
                this.PageIndex = index;

                this.OnPageMoved?.Invoke(this);
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

                this.OnPageGroupMoved?.Invoke(this);
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

                this.OnPageGroupMoved?.Invoke(this);
            }
        }

        /// <summary>
        /// データアイテムリストから現在の指定範囲のデータアイテムのみを抽出します。
        /// </summary>
        /// <typeparam name="T">データアイテムの型。</typeparam>
        /// <param name="items">データアイテムリスト。</param>
        /// <returns>
        /// データアイテムリストから現在の指定範囲のデータアイテムのみを返却します。
        /// </returns>
        public IEnumerable<T> GetPagedItems<T>(IEnumerable<T> items)
        {
            switch (this.PagingType)
            {
                case PagingTypeOptions.None:
                    return items;
                case PagingTypeOptions.Scroll:
                    return items.Take(this.PageIndex * this.PageRowSize);
                case PagingTypeOptions.Pagination:
                    return items.Skip((this.PageIndex - 1) * this.PageRowSize).Take(this.PageRowSize);
                default:
                    throw new InvalidOperationException($"not support {this.PagingType.GetType()}");
            }
        }

        #endregion
    }
}
