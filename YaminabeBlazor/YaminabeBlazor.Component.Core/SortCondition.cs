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

using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// ソート条件を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class SortCondition
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ソート対象のプロパティ名を取得または設定します。
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// ソート種別を取得または設定します。
        /// </summary>
        public SortOptions SortOption { get; set; }

        /// <summary>
        /// ソート番号を取得または設定します。
        /// </summary>
        public int SortNo { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="SortCondition"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyName">ソート対象のプロパティ名。</param>
        /// <param name="sortOption">ソート種別。</param>
        /// <param name="sortNo">ソート番号。</param>
        public SortCondition(
            string propertyName, 
            SortOptions sortOption = SortOptions.None
            , int sortNo = int.MinValue
            )
        {
            this.PropertyName = propertyName;
            this.SortOption = sortOption;
            this.SortNo = SortNo;
        }

        #endregion
    }
}
