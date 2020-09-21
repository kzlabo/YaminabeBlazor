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
    /// <see cref="YaminabeBlazor.Core.Models.BrandEntityModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// インスタンスをブランドマスタからのロード状態で作成します。
        /// </summary>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="brandName">ブランド名。</param>
        /// <param name="note">メモ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// ブランドマスタを返却します。
        /// </returns>
        public static BrandEntityModel Load(
            string brandId,
            string brandName,
            string note,
            DateTime updateDateTime,
            string updateUserId
            )
        {
            return new BrandEntityModel(
                brandId: brandId,
                brandName: brandName,
                note: note,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.UnChanged
                );
        }

        /// <summary>
        /// インスタンスをブランドマスタの新規状態で作成します。
        /// </summary>
        /// <param name="brandId">ブランドコード。</param>
        /// <param name="brandName">ブランド名。</param>
        /// <param name="note">メモ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <returns>
        /// ブランドマスタを返却します。
        /// </returns>
        public static BrandEntityModel Create(
            string brandId,
            string brandName = default,
            string note = default,
            DateTime updateDateTime = default,
            string updateUserId = default
            )
        {
            return new BrandEntityModel(
                brandId: brandId,
                brandName: brandName,
                note: note,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.Added
                );
        }

        #endregion
    }
}
