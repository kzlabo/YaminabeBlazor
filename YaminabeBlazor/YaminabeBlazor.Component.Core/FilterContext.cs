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

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// フィルタコンテキストを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class FilterContext
    {
        #region -------------------- delegate --------------------

        public Action<FilterContext> OnFilter;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// フィルタ条件リストを取得します。
        /// </summary>
        public Dictionary<string, FilterCondition> FilterConditions { get; } = new Dictionary<string, FilterCondition>();

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// フィルタ条件を設定します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="filterText">フィルタテキスト。</param>
        /// <param name="partialMatch">部分一致判定の要否。</param>
        public void Set(
            string propertyName, 
            string filterText, 
            bool partialMatch
            )
        {

            if (this.FilterConditions.TryGetValue(propertyName, out var filterCondition) == false)
            {
                filterCondition = this.FilterConditions[propertyName] = new FilterCondition(propertyName, filterText, partialMatch);
            }
            filterCondition.PropertyName = propertyName;
            filterCondition.FilterText = filterText;
            filterCondition.PartialMatch = partialMatch;

            this.OnFilter?.Invoke(this);
        }

        #endregion
    }
}
