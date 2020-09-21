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

namespace YaminabeBlazor.Component.Core.Enums
{
    /// <summary>
    /// ページング種別を表します。
    /// </summary>
    public enum PagingTypeOptions
    {
        /// <summary>
        /// ページングなしを表します。
        /// </summary>
        None = 0,
        /// <summary>
        /// スクロールによるページングを表します。
        /// </summary>
        Scroll = 1,
        /// <summary>
        /// ページネーションによるページングを表します。
        /// </summary>
        Pagination = 2
    }

    /// <summary>
    /// ソート種別を表します。
    /// </summary>
    public enum SortOptions
    {
        /// <summary>
        /// ソート指定なしを表します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 昇順ソートを表します。
        /// </summary>
        Asc = 1,
        /// <summary>
        /// 降順ソートを表します。
        /// </summary>
        Desc = 2
    }
}
