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
    /// ソートコンテキストを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class SortContext
    {
        #region -------------------- delegate --------------------

        public Action<SortContext> OnSort;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// ソート条件リストを取得します。
        /// </summary>
        public Dictionary<string, SortCondition> SortConditions { get; } = new Dictionary<string, SortCondition>();

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// ソート条件を未指定・昇順・降順の順にインクリメントを行います。
        /// </summary>
        /// <param name="propertyName">ソート対象のプロパティ名。</param>
        public void Increment(string propertyName)
        {
            if (this.SortConditions.TryGetValue(propertyName, out var sortCondition) == false)
            {
                sortCondition = this.SortConditions[propertyName] = new SortCondition(propertyName);
            }
            switch (sortCondition.SortOption)
            {
                case SortOptions.None:
                    sortCondition.SortOption = SortOptions.Asc;
                    sortCondition.SortNo = int.MaxValue;
                    break;
                case SortOptions.Asc:
                    sortCondition.SortOption = SortOptions.Desc;
                    break;
                case SortOptions.Desc:
                    sortCondition.SortOption = SortOptions.None;
                    sortCondition.SortNo = int.MinValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
            var sortNo = 1;
            foreach (var reSortCondition in this.SortConditions.OrderBy(s => s.Value.SortNo))
            {
                if (reSortCondition.Value.SortNo < 0)
                {
                    continue;
                }
                reSortCondition.Value.SortNo = sortNo++;
            }

            this.OnSort?.Invoke(this);
        }

        #endregion
    }
}
