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
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Infrastructure.Attributes;
using YaminabeBlazor.Infrastructure.Extensions;

namespace YaminabeBlazor.Infrastructure.Dtos
{
    /// <summary>
    /// 商品マスタの転送オブジェクトを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.EntityDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IDapperDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IEntityConverter{YaminabeBlazor.Core.Models.IProductEntityModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductDto : EntityDto, IDapperDto, IEntityConverter<IProductEntityModel>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品コードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "商品コード")]
        public string ProductId { get; set; }

        /// <summary>
        /// 商品名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "商品名")]
        public string ProductName { get; set; }

        /// <summary>
        /// ブランドコードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "ブランドコード")]
        public string BrandId { get; set; }

        /// <summary>
        /// カテゴリコードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "カテゴリコード")]
        public string CatetoryId { get; set; }

        /// <summary>
        /// 定価を取得または設定します。
        /// </summary>
        [DapperMap(Name = "定価")]
        public decimal ListPrice { get; set; }

        /// <summary>
        /// 商品タグを取得または設定します。
        /// </summary>
        [DapperMap(Name = "商品タグ")]
        public ProductTagTypeOptions ProductTagType { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// エンティティを転送オブジェクトに変換して取り込みます。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        public void ConvertFromEntity(IProductEntityModel entity)
        {
            this.ProductId = entity.ProductId;
            this.ProductName = entity.ProductName;
            this.BrandId = entity.BrandId;
            this.CatetoryId = entity.CatetoryId;
            this.ListPrice = entity.ListPrice;
            this.ProductTagType = entity.ProductTagType;
            this.UpdateDateTime = entity.UpdateDateTime.SqlServerValue();
            this.UpdateUserId = entity.UpdateUserId;
            this.EntityState = entity.EntityState;
        }

        /// <summary>
        /// 転送オブジェクトをエンティティに変換します。
        /// </summary>
        /// <returns>
        /// エンティティを返却します。
        /// </returns>
        public IProductEntityModel ConvertToEntity()
        {
            return ProductFactory.Load(
                productId: this.ProductId,
                productName: this.ProductName,
                brandId: this.BrandId,
                catetoryId: this.CatetoryId,
                listPrice: this.ListPrice,
                productTagType: this.ProductTagType,
                updateDateTime: this.UpdateDateTime,
                updateUserId: this.UpdateUserId
                );
        }

        #endregion
    }
}
