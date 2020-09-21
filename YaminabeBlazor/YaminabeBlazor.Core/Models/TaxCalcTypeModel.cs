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

using YaminabeBlazor.Core.Enums;

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 消費税計算区分モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.ITaxCalcTypeModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class TaxCalcTypeModel : ITaxCalcTypeModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public TaxCalcTypeOptions TaxCalcType { get; }

        /// <inheritdoc/>
        public string TaxCalcTypeName { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="TaxCalcTypeModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="taxCalcType">消費税計算区分。</param>
        /// <param name="taxCalcTypeName">消費税計算区分名。</param>
        public TaxCalcTypeModel(
            TaxCalcTypeOptions taxCalcType,
            string taxCalcTypeName
            )
        {
            this.TaxCalcType  = taxCalcType;
            this.TaxCalcTypeName = taxCalcTypeName;
        }

        #endregion
    }
}
