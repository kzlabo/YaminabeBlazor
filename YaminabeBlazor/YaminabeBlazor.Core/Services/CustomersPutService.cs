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
    /// 得意先マスタリストの更新サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.ICustomersPutService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomersPutService : ICustomersPutService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先マスタリストの取得リポジトリを取得または設定します。
        /// </summary>
        private ICustomersGetRepository GetRepository { get; }

        /// <summary>
        /// 得意先マスタリストの更新リポジトリを取得または設定します。
        /// </summary>
        private ICustomersPutRepository PutRepository { get; }

        /// <summary>
        /// 実行者を取得します。
        /// </summary>
        private IExecutionModel Executor { get; }

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomersPutService> Logger { get; }

        /// <summary>
        /// 出力情報を取得または設定します。
        /// </summary>
        private ICustomersPutOutputDto Output { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersPutService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="getRepository">取得リポジトリ。</param>
        /// <param name="putRepository">更新リポジトリ。</param>
        /// <param name="executor">実行者。</param>
        /// <param name="logger">ロガー。</param>
        public CustomersPutService(
            ICustomersGetRepository getRepository,
            ICustomersPutRepository putRepository,
            IExecutionModel executor,
            ILogger<CustomersPutService> logger
            )
        {
            this.GetRepository = getRepository;
            this.PutRepository = putRepository;
            this.Executor = executor;
            this.Logger = logger;
            this.Output = new CustomersPutOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public ICustomersPutOutputDto Put(ICustomersPutInputDto input)
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
                var entity = entites.FirstOrDefault(e => e.CustomerId == changedInput.CustomerId);
                if (entity == null)
                {
                    continue;
                }

                entity.CustomerName = changedInput.CustomerName;
                entity.CustomerKanaName = changedInput.CustomerKanaName;
                entity.CustomerShortName = changedInput.CustomerShortName;
                entity.EstablishmentDate = changedInput.EstablishmentDate;
                entity.Ceo = changedInput.Ceo;
                entity.PostalCode = changedInput.PostalCode;
                entity.Address = changedInput.Address;
                entity.Tel = changedInput.Tel;
                entity.Fax = changedInput.Fax;
                entity.Mail = changedInput.Mail;
                entity.Hp = changedInput.Hp;
                entity.CutoffDateType = changedInput.CutoffDateType;
                entity.CutoffDate = changedInput.CutoffDate;
                entity.CollectionDateType = changedInput.CollectionDateType;
                entity.CollectionDate = changedInput.CollectionDate;
                entity.TaxType = changedInput.TaxType;
                entity.TaxCalcType = changedInput.TaxCalcType;
                entity.TaxRoundType = changedInput.TaxRoundType;
                entity.Note = changedInput.Note;
                entity.UpdateDateTime = this.Executor.ExecuteDateTime;
                entity.UpdateUserId = this.Executor.UserId;
                entity.EntityState |= EntityStateOptions.Modified;
            }

            // 削除
            foreach (var deletedInput in input.DeletedDtos)
            {
                var entity = entites.FirstOrDefault(e => e.CustomerId == deletedInput.CustomerId);
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
                var entity = CustomerFactory.Create(
                    customerId: addedInput.CustomerId,
                    customerName: addedInput.CustomerName,
                    customerKanaName: addedInput.CustomerKanaName,
                    customerShortName: addedInput.CustomerShortName,
                    establishmentDate: addedInput.EstablishmentDate,
                    ceo: addedInput.Ceo,
                    postalCode: addedInput.PostalCode,
                    address: addedInput.Address,
                    tel: addedInput.Tel,
                    fax: addedInput.Fax,
                    mail: addedInput.Mail,
                    hp: addedInput.Hp,
                    cutoffDateType: addedInput.CutoffDateType,
                    cutoffDate: addedInput.CutoffDate,
                    collectionDateType: addedInput.CollectionDateType,
                    collectionDate: addedInput.CollectionDate,
                    taxType: addedInput.TaxType,
                    taxCalcType: addedInput.TaxCalcType,
                    taxRoundType: addedInput.TaxRoundType,
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
