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

using Microsoft.Extensions.Logging;
using System.Linq;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Repositories;

namespace YaminabeBlazor.Core.Services
{
    /// <summary>
    /// ブランドマスタリストの更新サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IBrandsPutService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandsPutService : IBrandsPutService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ブランドマスタリストの取得リポジトリを取得または設定します。
        /// </summary>
        private IBrandsGetRepository GetRepository { get; }

        /// <summary>
        /// ブランドマスタリストの更新リポジトリを取得または設定します。
        /// </summary>
        private IBrandsPutRepository PutRepository { get; }

        /// <summary>
        /// 実行者を取得します。
        /// </summary>
        private IExecutionModel Executor { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<BrandsPutService> Logger { get; }

        /// <summary>
        /// 出力情報を取得または設定します。
        /// </summary>
        private IBrandsPutOutputDto Output { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsPutService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="getRepository">取得リポジトリ。</param>
        /// <param name="putRepository">更新リポジトリ。</param>
        /// <param name="executor">実行者。</param>
        /// <param name="logger">ロガー。</param>
        public BrandsPutService(
            IBrandsGetRepository getRepository,
            IBrandsPutRepository putRepository,
            IExecutionModel executor,
            ILogger<BrandsPutService> logger
            )
        {
            this.GetRepository = getRepository;
            this.PutRepository = putRepository;
            this.Executor = executor;
            this.Logger = logger;
            this.Output = new BrandsPutOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IBrandsPutOutputDto Put(IBrandsPutInputDto input)
        {
            using var dataAccessContext = this.GetRepository.CreateDataAccessContext();
            this.GetRepository.DataAccessContext = dataAccessContext;
            this.PutRepository.DataAccessContext = dataAccessContext;

            // トランザクション開始
            dataAccessContext.BeginTransaction();

            // 取得
            var entites = this.GetRepository.Get();

            // 更新
            foreach (var changedInput in input.ChangedDtos)
            {
                var entity = entites.FirstOrDefault(e => e.BrandId == changedInput.BrandId);
                if (entity == null)
                {
                    continue;
                }

                entity.BrandName = changedInput.BrandName;
                entity.Note = changedInput.Note;
                entity.UpdateDateTime = this.Executor.ExecuteDateTime;
                entity.UpdateUserId = this.Executor.UserId;
                entity.EntityState |= EntityStateOptions.Modified;
            }

            // 削除
            foreach (var deletedInput in input.DeletedDtos)
            {
                var entity = entites.FirstOrDefault(e => e.BrandId == deletedInput.BrandId);
                if (entity == null)
                {
                    continue;
                }

                entity.UpdateDateTime = this.Executor.ExecuteDateTime;
                entity.UpdateUserId = this.Executor.UserId;
                entity.EntityState |= Enums.EntityStateOptions.Deleted;
            }

            // 作成
            foreach (var addedInput in input.AddedDtos)
            {
                var entity = BrandFactory.Create(
                    brandId: addedInput.BrandId,
                    brandName: addedInput.BrandName,
                    note: addedInput.Note,
                    updateDateTime: this.Executor.ExecuteDateTime,
                    updateUserId: this.Executor.UserId
                    );

                entites.Add(entity);
            }

            // マスタ更新
            this.PutRepository.Put(entites);

            // コミット
            dataAccessContext.CommitTransaction();

            return this.Output;
        }

        #endregion
    }
}
