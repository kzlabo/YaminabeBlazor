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

using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Repositories;
using YaminabeBlazor.Infrastructure.Dtos;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// 商品マスタリストの更新リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.EntitiesPutRepository{YaminabeBlazor.Core.Models.IProductCategoryModel, YaminabeBlazor.Infrastructure.Dtos.ProductCategoryDto}" />
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IProductCategoriesPutRepository" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductsPutRepository : EntitiesPutRepository<IProductEntityModel, ProductDto>, IProductsPutRepository
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 更新対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string MeergeTempTableName { get; } = "#商品更新Temp";

        /// <summary>
        /// 削除対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string DeleteTempTableName { get; } = "#商品削除Temp";

        /// <summary>
        /// 作成SQLを取得または設定します。
        /// </summary>
        public override string InsertSql { get; } = @"
INSERT INTO [商品Master](
    [商品コード],
    [商品名],
    [ブランドコード],
    [カテゴリコード],
    [定価],
    [商品タグ],
    [更新日時],
    [更新者コード]
)
SELECT
    [TM].[商品コード],
    [TM].[商品名],
    [TM].[ブランドコード],
    [TM].[カテゴリコード],
    [TM].[定価],
    [TM].[商品タグ],
    [TM].[更新日時],
    [TM].[更新者コード]
FROM [#商品更新Temp] AS [TM]
    LEFT JOIN [商品Master] AS [M]
        ON [TM].[商品コード] = [M].[商品コード]
WHERE [M].[商品コード] IS NULL
";

        /// <summary>
        /// 更新SQLを取得または設定します。
        /// </summary>
        public override string UpdateSql { get; } = @"
UPDATE [M]
SET
    [M].[商品名] = [TM].[商品名],
    [M].[ブランドコード] = [TM].[ブランドコード],
    [M].[カテゴリコード] = [TM].[カテゴリコード],
    [M].[定価] = [TM].[定価],
    [M].[商品タグ] = [TM].[商品タグ],
    [M].[更新日時] = [TM].[更新日時],
    [M].[更新者コード] = [TM].[更新者コード]
FROM [商品Master] AS [M]
    INNER JOIN [#商品更新Temp] AS [TM]
        ON [M].[商品コード] = [TM].[商品コード]
";

        /// <summary>
        /// 削除SQLを取得または設定します。
        /// </summary>
        public override string DeleteSql { get; } = @"
DELETE [M]
FROM [商品Master] AS [M]
    INNER JOIN [#商品削除Temp] AS [TM]
        ON [M].[商品コード] = [TM].[商品コード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsPutRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public ProductsPutRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion
    }
}
