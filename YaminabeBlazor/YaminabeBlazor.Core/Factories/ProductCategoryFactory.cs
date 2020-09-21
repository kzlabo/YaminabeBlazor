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
    /// <see cref="YaminabeBlazor.Core.Models.ProductCategoryEntityModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoryFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// インスタンスを商品カテゴリマスタからのロード状態で作成します。
        /// </summary>
        /// <param name="categoryId">カテゴリコード。</param>
        /// <param name="categoryName">カテゴリ名。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// 商品カテゴリマスタを返却します。
        /// </returns>
        public static ProductCategoryEntityModel Load(
            string categoryId,
            string categoryName,
            DateTime updateDateTime,
            string updateUserId
            )
        {
            return new ProductCategoryEntityModel(
                categoryId: categoryId,
                categoryName: categoryName,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.UnChanged
                );
        }

        /// <summary>
        /// インスタンスを商品カテゴリマスタからの新規状態で作成します。
        /// </summary>
        /// <param name="categoryId">カテゴリコード。</param>
        /// <param name="categoryName">カテゴリ名。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// 商品カテゴリマスタを返却します。
        /// </returns>
        public static ProductCategoryEntityModel Create(
            string categoryId,
            string categoryName = default,
            DateTime updateDateTime = default,
            string updateUserId = default
            )
        {
            return new ProductCategoryEntityModel(
                categoryId: categoryId,
                categoryName: categoryName,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.Added
                );
        }

        #endregion
    }
}
