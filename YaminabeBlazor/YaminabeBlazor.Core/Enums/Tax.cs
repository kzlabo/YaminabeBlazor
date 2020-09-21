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
    /// 外税・内税を表します。
    /// </summary>
    public enum TaxTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 内税であることを示します。
        /// </summary>
        Included = 1,
        /// <summary>
        /// 外税であることを示します。
        /// </summary>
        Excluded = 2
    }

    /// <summary>
    /// 消費税の計算方法を表します。
    /// </summary>
    public enum TaxCalcTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 明細単位で計算することを示します。
        /// </summary>
        Detail = 1,
        /// <summary>
        /// 合算で計算することを示します。
        /// </summary>
        Total = 2
    }

    /// <summary>
    /// 消費税計算の端数処理方法を表します。
    /// </summary>
    public enum TaxRoundTypeOptions
    {
        /// <summary>
        /// 未設定であることを示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 四捨五入であることを示します。
        /// </summary>
        Round = 1,
        /// <summary>
        /// 切り捨てであることを示します。
        /// </summary>
        Down = 2,
        /// <summary>
        /// 切り上げであることを示します。
        /// </summary>
        Up = 3
    }
}
