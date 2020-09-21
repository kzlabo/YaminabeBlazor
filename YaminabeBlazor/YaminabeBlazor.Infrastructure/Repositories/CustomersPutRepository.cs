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
    /// 得意先マスタリストの更新リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.EntitiesPutRepository{YaminabeBlazor.Core.Models.ICustomerEntityModel, YaminabeBlazor.Infrastructure.Dtos.CustomerDto}" />
    /// <seealso cref="YaminabeBlazor.Core.Repositories.ICustomersPutRepository" />
    public class CustomersPutRepository : EntitiesPutRepository<ICustomerEntityModel, CustomerDto>, ICustomersPutRepository
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 更新対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string MeergeTempTableName { get; } = "#得意先更新Temp";

        /// <summary>
        /// 削除対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public override string DeleteTempTableName { get; } = "#得意先削除Temp";

        /// <summary>
        /// 作成SQLを取得または設定します。
        /// </summary>
        public override string InsertSql { get; } = @"
INSERT INTO [得意先Master](
    [得意先コード],
    [得意先名],
    [得意先名カナ],
    [得意先略名],
    [設立年月日],
    [代表者],
    [郵便番号],
    [住所],
    [電話番号],
    [FAX番号],
    [メールアドレス],
    [ホームページアドレス],
    [締日区分],
    [締日],
    [回収日区分],
    [回収日],
    [消費税区分],
    [消費税計算区分],
    [消費税端数処理区分],
    [メモ],
    [更新日時],
    [更新者コード]
)
SELECT
    [TM].[得意先コード],
    [TM].[得意先名],
    [TM].[得意先名カナ],
    [TM].[得意先略名],
    [TM].[設立年月日],
    [TM].[代表者],
    [TM].[郵便番号],
    [TM].[住所],
    [TM].[電話番号],
    [TM].[FAX番号],
    [TM].[メールアドレス],
    [TM].[ホームページアドレス],
    [TM].[締日区分],
    [TM].[締日],
    [TM].[回収日区分],
    [TM].[回収日],
    [TM].[消費税区分],
    [TM].[消費税計算区分],
    [TM].[消費税端数処理区分],
    [TM].[メモ],
    [TM].[更新日時],
    [TM].[更新者コード]
FROM [#得意先更新Temp] AS [TM]
    LEFT JOIN [得意先Master] AS [M]
        ON [TM].[得意先コード] = [M].[得意先コード]
WHERE [M].[得意先コード] IS NULL
";

        /// <summary>
        /// 更新SQLを取得または設定します。
        /// </summary>
        public override string UpdateSql { get; } = @"
UPDATE [M]
SET
    [M].[得意先名] = [TM].[得意先名],
    [M].[得意先名カナ] = [TM].[得意先名カナ],
    [M].[得意先略名] = [TM].[得意先略名],
    [M].[設立年月日] = [TM].[設立年月日],
    [M].[代表者] = [TM].[代表者],
    [M].[郵便番号] = [TM].[郵便番号],
    [M].[住所] = [TM].[住所],
    [M].[電話番号] = [TM].[電話番号],
    [M].[FAX番号] = [TM].[FAX番号],
    [M].[メールアドレス] = [TM].[メールアドレス],
    [M].[ホームページアドレス] = [TM].[ホームページアドレス],
    [M].[締日区分] = [TM].[締日区分],
    [M].[締日] = [TM].[締日],
    [M].[回収日区分] = [TM].[回収日区分],
    [M].[回収日] = [TM].[回収日],
    [M].[消費税区分] = [TM].[消費税区分],
    [M].[消費税計算区分] = [TM].[消費税計算区分],
    [M].[消費税端数処理区分] = [TM].[消費税端数処理区分],
    [M].[メモ] = [TM].[メモ],
    [M].[更新日時] = [TM].[更新日時],
    [M].[更新者コード] = [TM].[更新者コード]
FROM [得意先Master] AS [M]
    INNER JOIN [#得意先更新Temp] AS [TM]
        ON [M].[得意先コード] = [TM].[得意先コード]
";

        /// <summary>
        /// 削除SQLを取得または設定します。
        /// </summary>
        public override string DeleteSql { get; } = @"
DELETE [M]
FROM [得意先Master] AS [M]
    INNER JOIN [#得意先削除Temp] AS [TM]
        ON [M].[得意先コード] = [TM].[得意先コード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersPutRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public CustomersPutRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion
    }
}
