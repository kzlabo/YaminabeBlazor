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
    /// ブランドモデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.IBrandEntityModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandEntityModel : IBrandEntityModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public EntityStateOptions EntityState { get; set; }

        /// <inheritdoc/>
        public string BrandId { get; set; }

        /// <inheritdoc/>
        public string BrandName { get; set; }

        /// <inheritdoc/>
        public string Note { get; set; }

        /// <inheritdoc/>
        public DateTime UpdateDateTime { get; set; }

        /// <inheritdoc/>
        public string UpdateUserId { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandEntityModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="brandName">ブランド名。</param>
        /// <param name="note">メモ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <param name="entityState">エンティティ状態。</param>
        public BrandEntityModel(
            string brandId,
            string brandName,
            string note,
            DateTime updateDateTime,
            string updateUserId,
            EntityStateOptions entityState = EntityStateOptions.None
            )
        {
            this.BrandId = brandId;
            this.BrandName = brandName;
            this.Note = note;
            this.UpdateDateTime = updateDateTime;
            this.UpdateUserId = updateUserId;
            this.EntityState = entityState;
        }

        #endregion
    }
}
