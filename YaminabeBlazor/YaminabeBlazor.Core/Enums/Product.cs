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

namespace YaminabeBlazor.Core.Enums
{
    /// <summary>
    /// 商品タグを示します。
    /// </summary>
    [Flags]
    public enum ProductTagTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 限定品であることを示します。
        /// </summary>
        LimitedEdition = 1,
        /// <summary>
        /// 販売終了品であることを示します。
        /// </summary>
        EndOfSale = 1 << 1,
        /// <summary>
        /// 受注生産品であることを示します。
        /// </summary>
        MadeToOrder = 1 << 2
    }
}
