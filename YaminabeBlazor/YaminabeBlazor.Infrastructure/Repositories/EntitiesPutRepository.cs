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
using System.Data.SqlClient;
using System.Linq;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Infrastructure.Dtos;
using YaminabeBlazor.Infrastructure.Extensions;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// エンティティリストの更新リポジトリを提供します。
    /// </summary>
    /// <typeparam name="TEntity">エンティティの型。</typeparam>
    /// <typeparam name="TDto">転送オブジェクトの型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.RepositoryBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class EntitiesPutRepository<TEntity, TDto> : RepositoryBase 
        where TEntity : IEntityModel
        where TDto : IEntityConverter<TEntity>, new()
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 更新対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public abstract string MeergeTempTableName { get; }

        /// <summary>
        /// 削除対象データを格納する一時テーブル名を取得または設定します。
        /// </summary>
        public abstract string DeleteTempTableName { get; }

        /// <summary>
        /// 作成SQLを取得または設定します。
        /// </summary>
        public abstract string InsertSql { get; }

        /// <summary>
        /// 更新SQLを取得または設定します。
        /// </summary>
        public abstract string UpdateSql { get; }

        /// <summary>
        /// 削除SQLを取得または設定します。
        /// </summary>
        public abstract string DeleteSql { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="EntitiesPutRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public EntitiesPutRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// エンティティリストを更新します。
        /// </summary>
        /// <param name="entities">エンティティリスト。</param>
        public void Put(IEnumerable<TEntity> entities)
        {
            using var dataAccessContext = this.GetDataAccessContext();

            dataAccessContext.Open();

            // 更新対象一時テーブル作成
            var modifiedEntities = entities.Where(e => (e.EntityState.HasFlag(EntityStateOptions.Added) == true || e.EntityState.HasFlag(EntityStateOptions.Modified) == true) && e.EntityState.HasFlag(EntityStateOptions.Deleted) == false);
            var modifiedDtos = modifiedEntities.Select<TEntity, TDto>(
                e =>
                {
                    var dto = new TDto();
                    dto.ConvertFromEntity(e);
                    return dto;
                });

            ((SqlConnection)(dataAccessContext.Connection)).BulkCopyToTemporaryTable<TDto>(
                temporaryTableName: this.MeergeTempTableName,
                data: modifiedDtos,
                truncate: true,
                copyOptions: SqlBulkCopyOptions.Default,
                externalTransaction: (SqlTransaction)dataAccessContext.Transaction
                );

            // 削除対象一時テーブル作成
            var deletedEntities = entities.Where(e => e.EntityState.HasFlag(EntityStateOptions.Deleted));
            var deletedDtos = deletedEntities.Select<TEntity, TDto>(
                e =>
                {
                    var dto = new TDto();
                    dto.ConvertFromEntity(e);
                    return dto;
                });
            ((SqlConnection)(dataAccessContext.Connection)).BulkCopyToTemporaryTable<TDto>(
                temporaryTableName: this.DeleteTempTableName,
                data: deletedDtos,
                truncate: true,
                copyOptions: SqlBulkCopyOptions.Default,
                externalTransaction: (SqlTransaction)dataAccessContext.Transaction
                );

            // 更新実行
            dataAccessContext.Connection.Execute(
                sql: this.UpdateSql,
                transaction: dataAccessContext.Transaction
                );

            // 作成実行
            dataAccessContext.Connection.Execute(
                sql: this.InsertSql,
                transaction: dataAccessContext.Transaction
                );

            // 削除実行
            dataAccessContext.Connection.Execute(
                sql: this.DeleteSql,
                transaction: dataAccessContext.Transaction
                );
        }

        #endregion
    }
}
