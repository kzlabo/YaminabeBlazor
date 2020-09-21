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
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Repositories;

namespace YaminabeBlazor.Core.Services
{
    /// <summary>
    /// 得意先マスタメンテナンスリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.ICustomersMaintenanceGetService" />
    public class CustomersMaintenanceGetService : ICustomersMaintenanceGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先マスタリストの取得リポジトリを取得します。
        /// </summary>
        private ICustomersGetRepository _customersGetRepository;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomersMaintenanceGetService> _logger;

        /// <summary>
        /// 得意先マスタメンテナンスリストの出力情報を取得します。
        /// </summary>
        private ICustomersMaintenanceGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersMaintenanceGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="customersGetRepository">得意先マスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public CustomersMaintenanceGetService(
            ICustomersGetRepository customersGetRepository,
            ILogger<CustomersMaintenanceGetService> logger
            )
        {
            this._customersGetRepository = customersGetRepository;
            this._logger = logger;
            this._outputDto = new CustomersMaintenanceGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public ICustomersMaintenanceGetOutputDto Get()
        {
            var customers = this._customersGetRepository.Get();
            var cutoffDateTypes = CutoffDateTypeFactory.CreateInitialData();
            var collectionDateTypes = CollectionDateTypeFactory.CreateInitialData();
            var taxTypes = TaxTypeFactory.CreateInitialData();
            var taxCalcTypes = TaxCalcTypeFactory.CreateInitialData();
            var taxRoundTypes = TaxRoundTypeFactory.CreateInitialData();

            var customerMaintenances = new List<CustomerMaintenanceQueryModel>();
            foreach (var customer in customers)
            {
                customerMaintenances.Add(new CustomerMaintenanceQueryModel()
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    CustomerKanaName = customer.CustomerKanaName,
                    CustomerShortName = customer.CustomerShortName,
                    EstablishmentDate = customer.EstablishmentDate,
                    Ceo = customer.Ceo,
                    PostalCode = customer.PostalCode,
                    Address = customer.Address,
                    Tel = customer.Tel,
                    Fax = customer.Fax,
                    Mail = customer.Mail,
                    Hp = customer.Hp,
                    CutoffDateType = customer.CutoffDateType,
                    CutoffDateTypeName = cutoffDateTypes.FirstOrDefault(c => c.CutoffDateType == customer.CutoffDateType)?.CutoffDateTypeName,
                    CutoffDate = customer.CutoffDate,
                    CollectionDateType = customer.CollectionDateType,
                    CollectionDateTypeName = collectionDateTypes.FirstOrDefault(c => c.CollectionDateType == customer.CollectionDateType)?.CollectionDateTypeName,
                    CollectionDate = customer.CollectionDate,
                    TaxType = customer.TaxType,
                    TaxTypeName = taxTypes.FirstOrDefault(t => t.TaxType == customer.TaxType)?.TaxTypeName,
                    TaxCalcType = customer.TaxCalcType,
                    TaxCalcTypeName = taxCalcTypes.FirstOrDefault(t => t.TaxCalcType == customer.TaxCalcType)?.TaxCalcTypeName,
                    TaxRoundType = customer.TaxRoundType,
                    TaxRoundTypeName = taxRoundTypes.FirstOrDefault(t => t.TaxRoundType == customer.TaxRoundType)?.TaxRoundTypeName,
                    Note = customer.Note
                });
            }
            this._outputDto.Customers = customerMaintenances;

            return this._outputDto;
        }

        #endregion

    }
}
