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

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// フィルタ条件を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class FilterCondition
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタ対象のプロパティ名を取得または設定します。
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// フィルタテキストを取得または設定します。
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        /// フィルタテキストを部分一致判定の要否を取得または設定します。
        /// </summary>
        public bool PartialMatch { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="FilterCondition"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="filterText">フィルタテキスト。</param>
        /// <param name="partialMatch">部分一致判定の要否。</param>
        public FilterCondition(
            string propertyName,
            string filterText, 
            bool partialMatch
            )
        {
            this.PropertyName = propertyName;
            this.FilterText = filterText;
            this.PartialMatch = partialMatch;
        }

        #endregion
    }
}
