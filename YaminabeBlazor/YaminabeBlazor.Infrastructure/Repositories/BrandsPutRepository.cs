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
    /// ブランドマスタリストの更新リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.EntitiesPutRepository{YaminabeBlazor.Core.Models.IBrandModel, YaminabeBlazor.Infrastructure.Dtos.BrandDto}" />
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IBrandsPutRepository" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandsPutRepository : EntitiesPutRepository<IBrandEntityModel, BrandDto>, IBrandsPutRepository
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 更新対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string MeergeTempTableName { get; } = "#ブランド更新Temp";

        /// <summary>
        /// 削除対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string DeleteTempTableName { get; } = "#ブランド削除Temp";

        /// <summary>
        /// 作成SQLを取得または設定します。
        /// </summary>
        public override string InsertSql { get; } = @"
INSERT INTO [ブランドMaster](
    [ブランドコード],
    [ブランド名],
    [メモ],
    [更新日時],
    [更新者コード]
)
SELECT
    [TM].[ブランドコード],
    [TM].[ブランド名],
    [TM].[メモ],
    [TM].[更新日時],
    [TM].[更新者コード]
FROM [#ブランド更新Temp] AS [TM]
    LEFT JOIN [ブランドMaster] AS [M]
        ON [TM].[ブランドコード] = [M].[ブランドコード]
WHERE [M].[ブランドコード] IS NULL
";

        /// <summary>
        /// 更新SQLを取得または設定します。
        /// </summary>
        public override string UpdateSql { get; } = @"
UPDATE [M]
SET
    [M].[ブランド名] = [TM].[ブランド名],
    [M].[メモ] = [TM].[メモ],
    [M].[更新日時] = [TM].[更新日時],
    [M].[更新者コード] = [TM].[更新者コード]
FROM [ブランドMaster] AS [M]
    INNER JOIN [#ブランド更新Temp] AS [TM]
        ON [M].[ブランドコード] = [TM].[ブランドコード]
";

        /// <summary>
        /// 削除SQLを取得または設定します。
        /// </summary>
        public override string DeleteSql { get; } = @"
DELETE [M]
FROM [ブランドMaster] AS [M]
    INNER JOIN [#ブランド削除Temp] AS [TM]
        ON [M].[ブランドコード] = [TM].[ブランドコード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsPutRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public BrandsPutRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion
    }
}
