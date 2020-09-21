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
using YaminabeBlazor.Infrastructure.Attributes;

namespace YaminabeBlazor.Infrastructure.Dtos
{
    /// <summary>
    /// エンティティの転送オブジェクトを提供します。
    /// </summary>
    /// <seealso cref="YaminabeFramework.Models.IEntityModel" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IDapperDto" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class EntityDto : IEntityModel, IDapperDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// エンティティの状態を取得または設定します。
        /// </summary>
        [DapperMap(Ignore = true)]
        public EntityStateOptions EntityState { get; set; } = EntityStateOptions.UnChanged;

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        [DapperMap(Name = "更新日時")]
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 更新者コードを取得または設定します。
        /// </summary>
        [DapperMap(Name = "更新者コード")]
        public string UpdateUserId { get; set; }

        #endregion
    }
}
