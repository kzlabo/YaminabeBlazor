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

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 商品カテゴリモデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.IProductCategoryEntityModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoryEntityModel : IProductCategoryEntityModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public EntityStateOptions EntityState { get; set; }

        /// <inheritdoc/>
        public string CategoryId { get; }

        /// <inheritdoc/>
        public string CategoryName { get; set; }

        /// <inheritdoc/>
        public DateTime UpdateDateTime { get; set; }

        /// <inheritdoc/>
        public string UpdateUserId { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoryEntityModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="categoryId">カテゴリコード。</param>
        /// <param name="categoryName">カテゴリ名。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <param name="entityState">エンティティ状態。</param>
        public ProductCategoryEntityModel(
            string categoryId,
            string categoryName,
            DateTime updateDateTime,
            string updateUserId,
            EntityStateOptions entityState = EntityStateOptions.None
            )
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.UpdateDateTime = updateDateTime;
            this.UpdateUserId = updateUserId;
            this.EntityState = entityState;
        }

        #endregion
    }
}
