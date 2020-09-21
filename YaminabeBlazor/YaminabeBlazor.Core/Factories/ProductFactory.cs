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
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Core.Factories
{
    /// <summary>
    /// <see cref="YaminabeBlazor.Core.Models.ProductEntityModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// インスタンスを商品マスタからのロード状態で作成します。
        /// </summary>
        /// <param name="productId">商品コード。</param>
        /// <param name="productName">商品名。</param>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="catetoryId">カテゴリコード。</param>
        /// <param name="listPrice">定価。</param>
        /// <param name="productTagType">商品タグ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// 商品マスタを返却します。
        /// </returns>
        public static ProductEntityModel Load(
            string productId,
            string productName,
            string brandId,
            string catetoryId,
            decimal listPrice,
            ProductTagTypeOptions productTagType,
            DateTime updateDateTime,
            string updateUserId
            )
        {
            return new ProductEntityModel(
                productId: productId,
                productName: productName,
                brandId: brandId,
                catetoryId: catetoryId,
                listPrice: listPrice,
                productTagType: productTagType,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId
                );
        }

        /// <summary>
        /// インスタンスを商品マスタからの新規状態で作成します。
        /// </summary>
        /// <param name="productId">商品コード。</param>
        /// <param name="productName">商品名。</param>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="catetoryId">カテゴリコード。</param>
        /// <param name="listPrice">定価。</param>
        /// <param name="productTagType">商品タグ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// 商品マスタを返却します。
        /// </returns>
        public static ProductEntityModel Create(
            string productId,
            string productName = default,
            string brandId = default,
            string catetoryId = default,
            decimal listPrice = default,
            ProductTagTypeOptions productTagType = default,
            DateTime updateDateTime = default,
            string updateUserId = default
            )
        {
            return new ProductEntityModel(
                productId: productId,
                productName: productName,
                brandId: brandId,
                catetoryId: catetoryId,
                listPrice: listPrice,
                productTagType: productTagType,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.Added
                );
        }

        #endregion
    }
}
