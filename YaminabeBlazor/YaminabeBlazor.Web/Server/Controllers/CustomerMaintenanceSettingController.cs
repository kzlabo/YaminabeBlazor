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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Services;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.ListItems;

namespace YaminabeBlazor.Web.Server.Controllers
{
    /// <summary>
    /// 得意先マスタメンテ画面設定のコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerMaintenanceSettingController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomerMaintenanceSettingController> _logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomerMaintenanceSettingController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public CustomerMaintenanceSettingController(
            ILogger<CustomerMaintenanceSettingController> logger
            )
        {
            this._logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタメンテ画面設定を取得します。
        /// </summary>
        /// <param name="service">得意先マスタメンテ画面設定取得サービス。</param>
        /// <returns>
        /// 得意先マスタメンテ画面設定を返却します。
        /// </returns>
        [HttpGet]
        public CustomerMaintenanceSettingDto Get(
            [FromServices] ICustomerMaintenanceSettingGetService service
            )
        {
            var output = service.Get();

            var response = new CustomerMaintenanceSettingDto();

            // 締日区分リスト
            response.CutoffDateTypeList = output.CutoffDateTypes.Select<ICutoffDateTypeModel, CutoffDateTypeListItem>(
                model => new CutoffDateTypeListItem()
                {
                    CutoffDateType = model.CutoffDateType,
                    CutoffDateTypeName = model.CutoffDateTypeName
                }).ToList();

            // 回収日区分リスト
            response.CollectionDateTypeList = output.CollectionDateTypes.Select<ICollectionDateTypeModel, CollectionDateTypeListItem>(
                model => new CollectionDateTypeListItem()
                {
                    CollectionDateType = model.CollectionDateType,
                    CollectionDateTypeName = model.CollectionDateTypeName
                }).ToList();

            // 消費税区分リスト
            response.TaxTypeList = output.TaxTypes.Select<ITaxTypeModel, TaxTypeListItem>(
                model => new TaxTypeListItem()
                {
                    TaxType = model.TaxType,
                    TaxTypeName = model.TaxTypeName
                }).ToList();

            // 消費税計算区分リスト
            response.TaxCalcTypeList = output.TaxCalcTypes.Select<ITaxCalcTypeModel, TaxCalcTypeListItem>(
                model => new TaxCalcTypeListItem()
                {
                    TaxCalcType = model.TaxCalcType,
                    TaxCalcTypeName = model.TaxCalcTypeName
                }).ToList();

            // 消費税端数処理区分リスト
            response.TaxRoundTypeList = output.TaxRoundTypes.Select<ITaxRoundType, TaxRoundTypeListItem>(
                model => new TaxRoundTypeListItem()
                {
                    TaxRoundType = model.TaxRoundType,
                    TaxRoundTypeName = model.TaxRoundTypeName
                }).ToList();

            return response;
        }

        #endregion
    }
}
