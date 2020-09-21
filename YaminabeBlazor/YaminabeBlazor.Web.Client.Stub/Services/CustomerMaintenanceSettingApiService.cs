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

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Web.Client.Stub.Extensions;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// 得意先マスタメンテナンス設定のApiサービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.ICustomerMaintenanceSettingApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerMaintenanceSettingApiService : ICustomerMaintenanceSettingApiService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// オンメモリ上での得意先マスタメンテナンス設定を取得または設定します。
        /// </summary>
        private CustomerMaintenanceSettingDto CustomerMaintenanceSetting { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomerMaintenanceSettingApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public CustomerMaintenanceSettingApiService(
            )
        {
            var customerMaintenanceSetting = new CustomerMaintenanceSettingDto();

            // 締日区分
            customerMaintenanceSetting.CutoffDateTypeList = DataBase.CutoffDateTypes.Select<CutoffDateTypeModel, CutoffDateTypeListItem>(
                c =>
                new CutoffDateTypeListItem()
                {
                    CutoffDateType = c.CutoffDateType,
                    CutoffDateTypeName = c.CutoffDateTypeName
                }).ToList();

            // 回収日区分
            customerMaintenanceSetting.CollectionDateTypeList = DataBase.CollectionDateTypes.Select<CollectionDateTypeModel, CollectionDateTypeListItem>(
                c =>
                new CollectionDateTypeListItem()
                {
                    CollectionDateType = c.CollectionDateType,
                    CollectionDateTypeName = c.CollectionDateTypeName
                }).ToList();

            // 消費税区分
            customerMaintenanceSetting.TaxTypeList = DataBase.TaxTypes.Select<TaxTypeModel, TaxTypeListItem>(
                t =>
                new TaxTypeListItem()
                {
                    TaxType = t.TaxType,
                    TaxTypeName = t.TaxTypeName
                }).ToList();

            // 消費税計算区分
            customerMaintenanceSetting.TaxCalcTypeList = DataBase.TaxCalcTypes.Select<TaxCalcTypeModel, TaxCalcTypeListItem>(
                t =>
                new TaxCalcTypeListItem()
                {
                    TaxCalcType = t.TaxCalcType,
                    TaxCalcTypeName = t.TaxCalcTypeName
                }).ToList();

            // 消費税端数処理区分
            customerMaintenanceSetting.TaxRoundTypeList = DataBase.TaxRoundTypes.Select<TaxRoundTypeModel, TaxRoundTypeListItem>(
                t =>
                new TaxRoundTypeListItem()
                {
                    TaxRoundType = t.TaxRoundType,
                    TaxRoundTypeName = t.TaxRoundTypeName
                }).ToList();

            this.CustomerMaintenanceSetting = customerMaintenanceSetting;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタメンテナンス設定を取得します。
        /// </summary>
        /// <returns>
        /// 得意先マスタメンテナンス設定を返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, CustomerMaintenanceSettingDto CustomerMaintenanceSetting)> Get()
        {
            return await Task<CustomerMaintenanceSettingDto>.Run(() =>
            {
                return (HttpStatusCode.OK, this.CustomerMaintenanceSetting.DeepCopy<CustomerMaintenanceSettingDto>());
            });
        }

        #endregion
    }
}
