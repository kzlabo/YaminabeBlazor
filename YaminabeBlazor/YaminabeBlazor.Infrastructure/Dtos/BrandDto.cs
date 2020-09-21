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

using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Infrastructure.Attributes;
using YaminabeBlazor.Infrastructure.Extensions;

namespace YaminabeBlazor.Infrastructure.Dtos
{
    /// <summary>
    /// ブランドマスタの転送オブジェクトを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.EntityDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IDapperDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IEntityConverter{YaminabeBlazor.Core.Models.IBrandEntityModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandDto : EntityDto, IDapperDto, IEntityConverter<IBrandEntityModel>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ブランドコードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "ブランドコード")]
        public string BrandId { get; set; }

        /// <summary>
        /// ブランド名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "ブランド名")]
        public string BrandName { get; set; }

        /// <summary>
        /// メモを取得または設定します。
        /// </summary>
        [DapperMap(Name = "メモ")]
        public string Note { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// エンティティを転送オブジェクトに変換して取り込みます。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        public void ConvertFromEntity(IBrandEntityModel entity)
        {
            this.BrandId = entity.BrandId;
            this.BrandName = entity.BrandName;
            this.Note = entity.Note;
            this.UpdateDateTime = entity.UpdateDateTime.SqlServerValue();
            this.UpdateUserId = entity.UpdateUserId;
            this.EntityState = entity.EntityState;
        }

        /// <summary>
        /// 転送オブジェクトをエンティティに変換します。
        /// </summary>
        /// <returns>
        /// エンティティを返却します。
        /// </returns>
        public IBrandEntityModel ConvertToEntity()
        {
            return BrandFactory.Load(
                brandId: this.BrandId,
                brandName: this.BrandName,
                note: this.Note,
                updateDateTime: this.UpdateDateTime,
                updateUserId: this.UpdateUserId
                );
        }

        #endregion
    }
}
