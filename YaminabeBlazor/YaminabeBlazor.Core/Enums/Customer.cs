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

namespace YaminabeBlazor.Core.Enums
{
    /// <summary>
    /// 締日を表します。
    /// </summary>
    public enum CutoffDateTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 当月締めであることを示します。
        /// </summary>
        CurrentMonth = 1,
        /// <summary>
        /// 翌月締めであることを示します。
        /// </summary>
        NextMonth = 2,
        /// <summary>
        /// 翌々月締めであることを示します。
        /// </summary>
        AfterNextMonth = 3
    }

    /// <summary>
    /// 回収日を表します。
    /// </summary>
    public enum CollectionDateTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 当月回収であることを示します。
        /// </summary>
        CurrentMonth = 1,
        /// <summary>
        /// 翌月回収であることを示します。
        /// </summary>
        NextMonth = 2,
        /// <summary>
        /// 翌々月回収であることを示します。
        /// </summary>
        AfterNextMonth = 3
    }
}
