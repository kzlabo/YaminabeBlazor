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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// 商品カテゴリマスタリストのApiサービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.IProductCategoriesApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoriesApiService : IProductCategoriesApiService
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoriesApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ProductCategoriesApiService()
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品カテゴリマスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 商品カテゴリマスタリストを返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, List<ProductCategoryInputModel> ProductCategories)> Get()
        {
            return await Task<List<ProductCategoryInputModel>>.Run(() =>
            {
                var producCategories = new List<ProductCategoryInputModel>();

                foreach (var productCategory in DataBase.ProductCategories)
                {
                    producCategories.Add(new ProductCategoryInputModel()
                    {
                        CategoryId = productCategory.CategoryId,
                        CategoryName = productCategory.CategoryName
                    });
                }

                return (HttpStatusCode.OK, producCategories);
            });

        }

        /// <summary>
        /// 商品カテゴリマスタリストを更新します。
        /// </summary>
        /// <param name="input">商品カテゴリマスタリストの更新対象。</param>
        public Task<HttpStatusCode> Put(IEnumerable<ProductCategoryInputModel> input)
        {
            return Task.Run(() =>
            {
                var addedProductCategories = input.GetAdded();
                var modifiedProductCategories = input.GetModified();
                var deletedProductCategories = input.GetDeleted();
                var updateDateTime = DateTime.Now;
                var updateUserId = "Stub";

                // 追加
                foreach (var productCategory in addedProductCategories)
                {
                    var addedProductCategory = DataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (addedProductCategory == null)
                    {
                        DataBase.ProductCategories.Add(ProductCategoryFactory.Create(
                            categoryId: productCategory.CategoryId,
                            categoryName: productCategory.CategoryName,
                            updateDateTime: updateDateTime,
                            updateUserId: updateUserId
                            ));
                    }
                }

                // 更新
                foreach (var productCategory in modifiedProductCategories)
                {
                    var modifiedProductCategory = DataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (modifiedProductCategory == null)
                    {
                        continue;
                    }
                    modifiedProductCategory.CategoryName = productCategory.CategoryName;
                    modifiedProductCategory.UpdateDateTime = updateDateTime;
                    modifiedProductCategory.UpdateUserId = updateUserId;
                }

                // 削除
                foreach (var productCategory in deletedProductCategories)
                {
                    var deletedProductCategory = DataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (deletedProductCategory == null)
                    {
                        continue;
                    }
                    DataBase.ProductCategories.Remove(deletedProductCategory);
                }

                return HttpStatusCode.OK;
            });
        }

        #endregion
    }
}
