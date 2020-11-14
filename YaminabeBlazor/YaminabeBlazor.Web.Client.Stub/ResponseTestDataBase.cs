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
using YaminabeBlazor.Core.Factories;

namespace YaminabeBlazor.Web.Client.Stub
{
    /// <summary>
    /// クライアントのみ実行用のレスポンステスト用のオンメモリデータベースを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/11/14" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ResponseTestDataBase : DataBase
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ResponseTestDataBase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ResponseTestDataBase()
        {
            this.Products.Clear();
            this.ProductCategories.Clear();
            this.Brands.Clear();
            this.Customers.Clear();

            // 1000件越えテスト用
            for (var i = 0; i < 1000; i++)
            {
                this.Products.Add(
                    ProductFactory.Load(
                        productId: $"99999999{i:D4}",
                        productName: $"テスト商品 {i}",
                        brandId: "99999",
                        catetoryId: "999",
                        listPrice: 9999,
                        productTagType: ProductTagTypeOptions.None,
                        updateDateTime: DateTime.Now,
                        updateUserId: "Stub"
                        )); ;
            }
            this.ProductCategories.Add(
                ProductCategoryFactory.Load(
                    categoryId: "999",
                    categoryName: "テストお菓子",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ));
            this.Brands.Add(
                BrandFactory.Load(
                    brandId: "99999",
                    brandName: "テストメーカー",
                    note: "1000件テスト用",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ));
        }

        #endregion
    }
}
