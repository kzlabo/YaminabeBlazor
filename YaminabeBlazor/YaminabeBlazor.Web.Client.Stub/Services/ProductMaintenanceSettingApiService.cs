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

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// 商品マスタメンテナンス設定のApiサービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.IProductMaintenanceSettingApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductMaintenanceSettingApiService : IProductMaintenanceSettingApiService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// オンメモリ上での商品マスタメンテナンス設定を取得または設定します。
        /// </summary>
        private ProductMaintenanceSettingDto ProductMaintenanceSetting { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductMaintenanceSettingApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ProductMaintenanceSettingApiService(
            )
        {
            var productMaintenanceSetting = new ProductMaintenanceSettingDto();

            // ブランド
            productMaintenanceSetting.BrandList = DataBase.Brands.Select<BrandEntityModel, BrandListItem>(
                b => 
                new BrandListItem()
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    Note = b.Note
                }).ToList();

            // 商品カテゴリ
            productMaintenanceSetting.ProductCategoryList = DataBase.ProductCategories.Select<ProductCategoryEntityModel, ProductCategoryListItem>(
                p => 
                new ProductCategoryListItem()
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName
                }).ToList();

            // 商品タグ
            productMaintenanceSetting.ProductTagTypeList = DataBase.ProductTagTypes.Select<ProductTagTypeModel, ProductTagTypeListItem>(
                p =>
                new ProductTagTypeListItem()
                {
                    ProductTagType = p.ProductTagType,
                    ProductTagTypeName = p.ProductTagTypeName
                }).ToList();

            this.ProductMaintenanceSetting = productMaintenanceSetting;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品マスタメンテナンス設定を取得します。
        /// </summary>
        /// <returns>
        /// 商品マスタメンテナンス設定を返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, ProductMaintenanceSettingDto ProductMaintenanceSetting)> Get()
        {
            return await Task<ProductMaintenanceSettingDto>.Run(() =>
            {
                return (
                HttpStatusCode.OK,
                new ProductMaintenanceSettingDto()
                {
                    BrandList = this.ProductMaintenanceSetting.BrandList,
                    ProductCategoryList = this.ProductMaintenanceSetting.ProductCategoryList,
                    ProductTagTypeList = this.ProductMaintenanceSetting.ProductTagTypeList
                }
                );
            });
        }

        #endregion
    }
}
