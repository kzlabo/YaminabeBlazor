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
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Factories;

namespace YaminabeBlazor.Core.Services
{
    /// <summary>
    /// 得意先マスタメンテ画面の設定取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.ICustomerMaintenanceSettingGetService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerMaintenanceSettingGetService : ICustomerMaintenanceSettingGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomerMaintenanceSettingGetService> _logger;

        /// <summary>
        /// 得意先マスタメンテ画面設定の出力情報を取得します。
        /// </summary>
        private ICustomersMaintenanceSettingGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomerMaintenanceSettingGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public CustomerMaintenanceSettingGetService(
            ILogger<CustomerMaintenanceSettingGetService> logger
            )
        {
            this._logger = logger;
            this._outputDto = new CustomerMaintenanceSettingGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public ICustomersMaintenanceSettingGetOutputDto Get()
        {
            this._outputDto.CutoffDateTypes = CutoffDateTypeFactory.CreateInitialData();
            this._outputDto.CollectionDateTypes = CollectionDateTypeFactory.CreateInitialData();
            this._outputDto.TaxTypes = TaxTypeFactory.CreateInitialData();
            this._outputDto.TaxCalcTypes = TaxCalcTypeFactory.CreateInitialData();
            this._outputDto.TaxRoundTypes = TaxRoundTypeFactory.CreateInitialData();

            return this._outputDto;
        }

        #endregion
    }
}
