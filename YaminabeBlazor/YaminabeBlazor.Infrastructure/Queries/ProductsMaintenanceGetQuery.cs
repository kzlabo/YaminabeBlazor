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
using YaminabeBlazor.Core.Queries;
using YaminabeBlazor.Infrastructure.Dtos;
using YaminabeBlazor.Infrastructure.Repositories;

namespace YaminabeBlazor.Infrastructure.Queries
{
    /// <summary>
    /// 商品マスタメンテナンスリストの取得クエリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.RepositoryBase" />
    /// <seealso cref="YaminabeBlazor.Core.Queries.IProductsMaintenanceGetQuery" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductsMaintenanceGetQuery : RepositoryBase, IProductsMaintenanceGetQuery
    {
        #region -------------------- const --------------------

        /// <summary>
        /// 取得SQL文。
        /// </summary>
        private const string _getSql = @"
SELECT
    [M1].[商品コード],
    [M1].[商品名],
    [M1].[ブランドコード],
    [M2].[ブランド名],
    [M1].[カテゴリコード],
    [M3].[カテゴリ名],
    [M1].[定価],
    [M1].[商品タグ],
    [M1].[更新日時],
    [M1].[更新者コード]
FROM [商品Master] AS [M1]
    INNER JOIN [ブランドMaster] AS [M2]
        ON [M1].[ブランドコード] = [M2].[ブランドコード]
    INNER JOIN [商品カテゴリMaster] AS [M3]
        ON [M1].[カテゴリコード] = [M3].[カテゴリコード]
ORDER BY
    [M1].[商品コード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsMaintenanceGetQuery"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public ProductsMaintenanceGetQuery(IDbConfig config) : base(config: config)
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public List<IProductMaintenanceQueryModel> Get()
        {
            using var dataAccessContext = this.GetDataAccessContext();

            var dtos = dataAccessContext.Connection.Query<ProductMaintenanceDto>(
                sql: ProductsMaintenanceGetQuery._getSql,
                transaction: dataAccessContext.Transaction
                );

            return dtos.Select<ProductMaintenanceDto, IProductMaintenanceQueryModel>(dto => dto?.ConvertToEntity()).ToList();
        }

        #endregion
    }
}
