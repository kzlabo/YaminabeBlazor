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
using YaminabeBlazor.Core.Enums;

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 得意先メンテナンスモデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.ICustomerMaintenanceQueryModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerMaintenanceQueryModel : ICustomerMaintenanceQueryModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public string CustomerId { get; set; }

        /// <inheritdoc/>
        public string CustomerName { get; set; }

        /// <inheritdoc/>
        public string CustomerKanaName { get; set; }

        /// <inheritdoc/>
        public string CustomerShortName { get; set; }

        /// <inheritdoc/>
        public DateTime? EstablishmentDate { get; set; }

        /// <inheritdoc/>
        public string Ceo { get; set; }

        /// <inheritdoc/>
        public string PostalCode { get; set; }

        /// <inheritdoc/>
        public string Address { get; set; }

        /// <inheritdoc/>
        public string Tel { get; set; }

        /// <inheritdoc/>
        public string Fax { get; set; }

        /// <inheritdoc/>
        public string Mail { get; set; }

        /// <inheritdoc/>
        public string Hp { get; set; }

        /// <inheritdoc/>
        public CutoffDateTypeOptions CutoffDateType { get; set; }

        /// <inheritdoc/>
        public string CutoffDateTypeName { get; set; }

        /// <inheritdoc/>
        public int CutoffDate { get; set; }

        /// <inheritdoc/>
        public CollectionDateTypeOptions CollectionDateType { get; set; }

        /// <inheritdoc/>
        public string CollectionDateTypeName { get; set; }

        /// <inheritdoc/>
        public int CollectionDate { get; set; }

        /// <inheritdoc/>
        public TaxTypeOptions TaxType { get; set; }

        /// <inheritdoc/>
        public string TaxTypeName { get; set; }

        /// <inheritdoc/>
        public TaxCalcTypeOptions TaxCalcType { get; set; }

        /// <inheritdoc/>
        public string TaxCalcTypeName { get; set; }

        /// <inheritdoc/>
        public TaxRoundTypeOptions TaxRoundType { get; set; }

        /// <inheritdoc/>
        public string TaxRoundTypeName { get; set; }

        /// <inheritdoc/>
        public string Note { get; set; }

        #endregion
    }
}
