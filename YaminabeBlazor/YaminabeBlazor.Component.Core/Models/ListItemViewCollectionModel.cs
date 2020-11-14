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

using System.Collections.Generic;
using YaminabeBlazor.Component.Core.Extensions;

namespace YaminabeBlazor.Component.Core.Models
{
    /// <summary>
    /// リストアイテムコレクションモデルを提供します。
    /// </summary>
    /// <typeparam name="TItem">データアイテムの型。</typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ListItemViewCollectionModel<TItem> : List<TItem>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタ条件リストを取得または設定します。
        /// </summary>
        public Dictionary<string, (string FilterText, bool PartialMatch)> FilterConditionValues { get; set; }

        /// <summary>
        /// フィルタされたデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> FilterItems
        {
            get
            {
                return this.FilterItems<TItem>(this.FilterConditionValues);
            }
        }

        /// <summary>
        /// 表示対象のデータアイテムリストを取得または設定します。
        /// </summary>
        public virtual IEnumerable<TItem> ViewItems
        {
            get
            {
                if (this.FilterItems == null)
                {
                    return null;
                }

                return this.FilterItems;
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
        }

        #endregion
    }
}
