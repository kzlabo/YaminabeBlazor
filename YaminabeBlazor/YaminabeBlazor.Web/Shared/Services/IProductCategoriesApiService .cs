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

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Shared.Services
{
    /// <summary>
    /// 商品カテゴリマスタリストのApiサービスを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IProductCategoriesApiService
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 商品カテゴリマスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 商品カテゴリマスタリストを返却します。
        /// </returns>
        Task<(HttpStatusCode HttpStatusCode, List<ProductCategoryInputModel> ProductCategories)> Get();

        /// <summary>
        /// 商品カテゴリマスタリストを更新します。
        /// </summary>
        /// <param name="addedItems">商品カテゴリマスタリストの追加対象。</param>
        /// <param name="changedItems">商品カテゴリマスタリストの更新対象。</param>
        /// <param name="deletedItems">商品カテゴリマスタリストの削除対象。</param>
        Task<HttpStatusCode> Put(
            IEnumerable<ProductCategoryInputModel> addedItems,
            IEnumerable<ProductCategoryInputModel> changedItems,
            IEnumerable<ProductCategoryInputModel> deletedItems
            );

        #endregion
    }
}
