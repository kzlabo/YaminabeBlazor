﻿/*
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

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// フィルタコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IFilterComponent
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタ条件リストを取得または設定します。
        /// </summary>
        public List<FilterConditionComponent> FilterConditions { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// フィルタ処理を実行します。
        /// </summary>
        public void FilterOn();

        /// <summary>
        /// フィルタを無効に設定します。
        /// </summary>
        public void FilterOff();

        #endregion
    }
}
