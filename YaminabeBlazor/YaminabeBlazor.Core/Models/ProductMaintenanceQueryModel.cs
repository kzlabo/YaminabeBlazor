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

using YaminabeBlazor.Core.Enums;

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 商品メンテナンスモデルを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.IProductMaintenanceQueryModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductMaintenanceQueryModel : IProductMaintenanceQueryModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public string ProductId { get; set; }

        /// <inheritdoc/>
        public string ProductName { get; set; }

        /// <inheritdoc/>
        public string BrandId { get; set; }

        /// <inheritdoc/>
        public string BrandName { get; set; }

        /// <inheritdoc/>
        public string CatetoryId { get; set; }

        /// <inheritdoc/>
        public string CatetoryName { get; set; }

        /// <inheritdoc/>
        public decimal ListPrice { get; set; }

        /// <inheritdoc/>
        public ProductTagTypeOptions ProductTagType { get; set; }

        /// <inheritdoc/>
        public string ProductTagTypeName { get; set; }

        #endregion
    }
}
