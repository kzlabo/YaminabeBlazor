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
        #region -------------------- field --------------------

        private DataBase _dataBase;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoriesApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="database">データベース。</param>
        public ProductCategoriesApiService(DataBase database)
        {
            this._dataBase = database;
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public async Task<(HttpStatusCode HttpStatusCode, List<ProductCategoryInputModel> ProductCategories)> Get()
        {
            return await Task<List<ProductCategoryInputModel>>.Run(() =>
            {
                var producCategories = new List<ProductCategoryInputModel>();

                foreach (var productCategory in this._dataBase.ProductCategories)
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

        /// <inheritdoc/>
        public Task<HttpStatusCode> Put(
            IEnumerable<ProductCategoryInputModel> addedItems,
            IEnumerable<ProductCategoryInputModel> changedItems,
            IEnumerable<ProductCategoryInputModel> deletedItems
            )
        {
            return Task.Run(() =>
            {
                var updateDateTime = DateTime.Now;
                var updateUserId = "Stub";

                // 追加
                foreach (var productCategory in addedItems)
                {
                    var addedProductCategory = this._dataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (addedProductCategory == null)
                    {
                        this._dataBase.ProductCategories.Add(ProductCategoryFactory.Create(
                            categoryId: productCategory.CategoryId,
                            categoryName: productCategory.CategoryName,
                            updateDateTime: updateDateTime,
                            updateUserId: updateUserId
                            ));
                    }
                }

                // 更新
                foreach (var productCategory in changedItems)
                {
                    var modifiedProductCategory = this._dataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (modifiedProductCategory == null)
                    {
                        continue;
                    }
                    modifiedProductCategory.CategoryName = productCategory.CategoryName;
                    modifiedProductCategory.UpdateDateTime = updateDateTime;
                    modifiedProductCategory.UpdateUserId = updateUserId;
                }

                // 削除
                foreach (var productCategory in deletedItems)
                {
                    var deletedProductCategory = this._dataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(productCategory.CategoryId));
                    if (deletedProductCategory == null)
                    {
                        continue;
                    }
                    this._dataBase.ProductCategories.Remove(deletedProductCategory);
                }

                return HttpStatusCode.OK;
            });
        }

        #endregion
    }
}
