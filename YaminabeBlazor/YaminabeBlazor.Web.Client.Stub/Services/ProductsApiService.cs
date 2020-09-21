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
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// 商品マスタリストのApiサービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.IProductsApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductsApiService : IProductsApiService
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ProductsApiService()
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品マスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 商品マスタリストを返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, List<ProductInputModel> Products)> Get()
        {
            return await Task<List<ProductInputModel>>.Run(() =>
            {
                var producs = new List<ProductInputModel>();

                foreach(var product in DataBase.Products)
                {
                    producs.Add(new ProductInputModel()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        BrandId = product.BrandId,
                        BrandName = DataBase.Brands.FirstOrDefault(b => b.BrandId.Equals(product.BrandId))?.BrandName,
                        CatetoryId = product.CatetoryId,
                        CategoryName = DataBase.ProductCategories.FirstOrDefault(p => p.CategoryId.Equals(product.CatetoryId))?.CategoryName,
                        ListPrice = product.ListPrice,
                        ProductTagType = product.ProductTagType,
                        ProductTagTypeName = string.Join("\r\n", DataBase.ProductTagTypes.Where(p => p.ProductTagType.Equals(ProductTagTypeOptions.None) == false).Where(p => product.ProductTagType.HasFlag(p.ProductTagType)).Select(p => p.ProductTagTypeName).ToArray())
                    });
                }

                return (HttpStatusCode.OK, producs);
            });
        }

        /// <summary>
        /// 商品マスタリストを更新します。
        /// </summary>
        /// <param name="input">商品マスタリストの更新対象。</param>
        public Task<HttpStatusCode> Put(IEnumerable<ProductInputModel> input)
        {
            return Task.Run(() =>
            {
                var addedProducts = input.GetAdded();
                var modifiedProducts = input.GetModified();
                var deletedProducts = input.GetDeleted();
                var updateDateTime = DateTime.Now;
                var updateUserId = "Stub";

                // 追加
                foreach (var product in addedProducts)
                {
                    var addedProduct = DataBase.Products.FirstOrDefault(p => p.ProductId.Equals(product.ProductId));
                    if (addedProduct == null)
                    {
                        DataBase.Products.Add(ProductFactory.Create(
                            productId: product.ProductId,
                            productName: product.ProductName,
                            brandId: product.BrandId,
                            catetoryId: product.CatetoryId,
                            listPrice: product.ListPrice,
                            productTagType: product.ProductTagType,
                            updateDateTime: updateDateTime,
                            updateUserId: updateUserId
                            ));
                    }
                }

                // 更新
                foreach (var product in modifiedProducts)
                {
                    var modifiedProduct = DataBase.Products.FirstOrDefault(p => p.ProductId.Equals(product.ProductId));
                    if (modifiedProduct == null)
                    {
                        continue;
                    }
                    modifiedProduct.ProductName = product.ProductName;
                    modifiedProduct.BrandId = product.BrandId;
                    modifiedProduct.CatetoryId = product.CatetoryId;
                    modifiedProduct.ListPrice = product.ListPrice;
                    modifiedProduct.ProductTagType = product.ProductTagType;
                    modifiedProduct.UpdateDateTime = updateDateTime;
                    modifiedProduct.UpdateUserId = updateUserId;
                }

                // 削除
                foreach (var product in deletedProducts)
                {
                    var deletedProduct = DataBase.Products.FirstOrDefault(p => p.ProductId.Equals(product.ProductId));
                    if (deletedProduct == null)
                    {
                        continue;
                    }
                    DataBase.Products.Remove(deletedProduct);
                }

                return HttpStatusCode.OK;
            });
        }

        #endregion
    }
}
