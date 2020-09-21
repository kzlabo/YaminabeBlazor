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
    /// 商品カテゴリマスタリストの更新サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IProductCategoriesPutService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoriesPutService : IProductCategoriesPutService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品カテゴリマスタリストの取得リポジトリを取得または設定します。
        /// </summary>
        private IProductCategoriesGetRepository GetRepository { get; }

        /// <summary>
        /// 商品カテゴリマスタリストの更新リポジトリを取得または設定します。
        /// </summary>
        private IProductCategoriesPutRepository PutRepository { get; }

        /// <summary>
        /// 実行者を取得します。
        /// </summary>
        private IExecutionModel Executor { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductCategoriesPutService> Logger { get; }

        /// <summary>
        /// 出力情報を取得または設定します。
        /// </summary>
        private IProductCategoriesPutOutputDto Output { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoriesPutService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="getRepository">取得リポジトリ。</param>
        /// <param name="putRepository">更新リポジトリ。</param>
        /// <param name="executor">実行者。</param>
        /// <param name="logger">ロガー。</param>
        public ProductCategoriesPutService(
            IProductCategoriesGetRepository getRepository,
            IProductCategoriesPutRepository putRepository,
            IExecutionModel executor,
            ILogger<ProductCategoriesPutService> logger
            )
        {
            this.GetRepository = getRepository;
            this.PutRepository = putRepository;
            this.Executor = executor;
            this.Logger = logger;
            this.Output = new ProductCategoriesPutOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IProductCategoriesPutOutputDto Put(IProductCategoriesPutInputDto input)
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
                var entity = entites.FirstOrDefault(e => e.CategoryId == changedInput.CategoryId);
                if (entity == null)
                {
                    continue;
                }

                entity.CategoryName = changedInput.CategoryName;
                entity.UpdateDateTime = this.Executor.ExecuteDateTime;
                entity.UpdateUserId = this.Executor.UserId;
                entity.EntityState |= EntityStateOptions.Modified;
            }

            // 削除
            foreach (var deletedInput in input.DeletedDtos)
            {
                var entity = entites.FirstOrDefault(e => e.CategoryId == deletedInput.CategoryId);
                if (entity == null)
                {
                    continue;
                }

                entity.UpdateDateTime = this.Executor.ExecuteDateTime;
                entity.UpdateUserId = this.Executor.UserId;
                entity.EntityState |= EntityStateOptions.Deleted;
            }

            // 作成
            foreach (var addedInput in input.AddedDtos)
            {
                var entity = ProductCategoryFactory.Create(
                    categoryId: addedInput.CategoryId,
                    categoryName: addedInput.CategoryName,
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
