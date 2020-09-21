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

using Dapper;
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Repositories;
using YaminabeBlazor.Infrastructure.Dtos;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// ブランドマスタリストの取得リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.RepositoryBase" />
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IBrandsGetRepository" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandsGetRepository : RepositoryBase, IBrandsGetRepository
    {
        #region -------------------- const --------------------

        /// <summary>
        /// 取得SQL文。
        /// </summary>
        private const string _getSql = @"
SELECT
    [ブランドコード],
    [ブランド名],
    [メモ],
    [更新日時],
    [更新者コード]
FROM [ブランドMaster]
ORDER BY
    [ブランドコード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsGetRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public BrandsGetRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public List<IBrandEntityModel> Get()
        {
            using var dataAccessContext = this.GetDataAccessContext();

            var dtos = dataAccessContext.Connection.Query<BrandDto>(
                sql: BrandsGetRepository._getSql,
                transaction: dataAccessContext.Transaction
                );

            return dtos?.Select<BrandDto, IBrandEntityModel>(dto => dto?.ConvertToEntity()).ToList();
        }

        #endregion
    }
}
