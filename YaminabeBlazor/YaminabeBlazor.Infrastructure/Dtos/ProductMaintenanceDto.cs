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
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Infrastructure.Attributes;

namespace YaminabeBlazor.Infrastructure.Dtos
{
    /// <summary>
    /// 商品マスタメンテナンスの転送オブジェクトを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.EntityDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IEntityConverter{YaminabeBlazor.Core.Models.IProductMaintenanceQueryModel}" />
    public class ProductMaintenanceDto : EntityDto, IEntityConverter<IProductMaintenanceQueryModel>
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
        /// ブランド名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "ブランド名")]
        public string BrandName { get; set; }

        /// <summary>
        /// カテゴリコードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "カテゴリコード")]
        public string CatetoryId { get; set; }

        /// <summary>
        /// カテゴリ名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "カテゴリ名")]
        public string CatetoryName { get; set; }

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

        /// <summary>
        /// 商品タグ名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "商品タグ名")]
        public string ProductTagTypeName { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// エンティティを転送オブジェクトに変換して取り込みます。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        public void ConvertFromEntity(IProductMaintenanceQueryModel entity)
        {
            this.ProductId = entity.ProductId;
            this.ProductName = entity.ProductName;
            this.BrandId = entity.BrandId;
            this.BrandName = entity.BrandName;
            this.CatetoryId = entity.CatetoryId;
            this.CatetoryName = entity.CatetoryName;
            this.ListPrice = entity.ListPrice;
            this.ProductTagType = entity.ProductTagType;
            this.ProductTagTypeName = entity.ProductTagTypeName;
        }


        /// <summary>
        /// 転送オブジェクトをエンティティに変換します。
        /// </summary>
        /// <returns>
        /// エンティティを返却します。
        /// </returns>
        public IProductMaintenanceQueryModel ConvertToEntity()
        {
            var entity = new ProductMaintenanceQueryModel();

            entity.ProductId = this.ProductId;
            entity.ProductName = this.ProductName;
            entity.BrandId = this.BrandId;
            entity.BrandName = this.BrandName;
            entity.CatetoryId = this.CatetoryId;
            entity.CatetoryName = this.CatetoryName;
            entity.ListPrice = this.ListPrice;
            entity.ProductTagType = this.ProductTagType;
            entity.ProductTagTypeName = this.ProductTagTypeName;

            return entity;
        }

        #endregion
    }
}
