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
    /// 商品モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.IProductEntityModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductEntityModel : IProductEntityModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public EntityStateOptions EntityState { get; set; }

        /// <inheritdoc/>
        public string ProductId { get; }

        /// <inheritdoc/>
        public string ProductName { get; set; }

        /// <inheritdoc/>
        public string BrandId { get; set; }

        /// <inheritdoc/>
        public string CatetoryId { get; set; }

        /// <inheritdoc/>
        public decimal ListPrice { get; set; }

        /// <inheritdoc/>
        public ProductTagTypeOptions ProductTagType { get; set; }

        /// <inheritdoc/>
        public DateTime UpdateDateTime { get; set; }

        /// <inheritdoc/>
        public string UpdateUserId { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductEntityModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="productId">商品コード。</param>
        /// <param name="productName">商品名。.</param>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="catetoryId">カテゴリコード。</param>
        /// <param name="listPrice">定価。</param>
        /// <param name="productTagType">商品タグ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <param name="entityState">エンティティ状態。</param>
        public ProductEntityModel(
            string productId,
            string productName,
            string brandId,
            string catetoryId,
            decimal listPrice,
            ProductTagTypeOptions productTagType,
            DateTime updateDateTime,
            string updateUserId,
            EntityStateOptions entityState = EntityStateOptions.None
            )
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.BrandId = brandId;
            this.CatetoryId = catetoryId;
            this.ListPrice = listPrice;
            this.ProductTagType = productTagType;
            this.UpdateDateTime = updateDateTime;
            this.UpdateUserId = updateUserId;
            this.EntityState = entityState;
        }

        #endregion
    }
}
