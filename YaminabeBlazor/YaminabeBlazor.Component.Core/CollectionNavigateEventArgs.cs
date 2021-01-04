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
    /// コレクション操作イベント引数を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CollectionNavigateEventArgs
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタイベントの発生有無を判定します。
        /// </summary>
        /// <remarks>
        /// フィルタイベントが発生した場合は <c>true</c> 。フィルタイベントが未発生の場合は <c>false</c> 。
        /// </remarks>
        public bool Filtered { get; set; } = false;

        /// <summary>
        /// ソートイベントの発生有無を判定します。
        /// </summary>
        /// <remarks>
        /// ソートイベントが発生した場合は <c>true</c> 。ソートイベントが未発生の場合は <c>false</c> 。
        /// </remarks>
        public bool Sorted { get; set; } = false;

        /// <summary>
        /// ページ移動イベントの発生有無を判定します。
        /// </summary>
        /// <remarks>
        /// ページ移動イベントが発生した場合は <c>true</c> 。ページ移動イベントが未発生の場合は <c>false</c> 。
        /// </remarks>
        public bool PageMoved { get; set; } = false;

        #endregion
    }
}
