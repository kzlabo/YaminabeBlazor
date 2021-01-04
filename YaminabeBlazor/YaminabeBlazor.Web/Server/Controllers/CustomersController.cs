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
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Services;
using YaminabeBlazor.Web.Server.Dtos;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Server.Controllers
{
    /// <summary>
    /// 得意先マスタリストのコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomersController> Logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public CustomersController(
            ILogger<CustomersController> logger
            )
        {
            this.Logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタリストを取得します。
        /// </summary>
        /// <param name="service">得意先マスタリスト取得サービス。</param>
        /// <returns>
        /// 得意先マスタリストを返却します。
        /// </returns>
        [HttpGet]
        public IReadOnlyList<CustomerInputModel> Get(
            [FromServices] ICustomersGetService service
            )
        {
            var output = service.Get();

            return output.Customers.Select<ICustomerEntityModel, CustomerInputModel>(
                model => new CustomerInputModel()
                {
                    CustomerId = model.CustomerId,
                    CustomerName = model.CustomerName,
                    CustomerKanaName = model.CustomerKanaName,
                    CustomerShortName = model.CustomerShortName,
                    EstablishmentDate = model.EstablishmentDate,
                    Ceo = model.Ceo,
                    PostalCode = model.PostalCode,
                    Tel = model.Tel,
                    Fax = model.Fax,
                    Mail = model.Mail,
                    Hp = model.Hp,
                    CutoffDateType = model.CutoffDateType,
                    CutoffDate = model.CutoffDate,
                    CollectionDateType = model.CollectionDateType,
                    CollectionDate = model.CollectionDate,
                    TaxType = model.TaxType,
                    TaxCalcType = model.TaxCalcType,
                    TaxRoundType = model.TaxRoundType,
                    Note = model.Note
                }).ToList();
        }

        /// <summary>
        /// 得意先マスタを更新します。
        /// </summary>
        /// <param name="input">入力情報。</param>
        /// <param name="service">更新サービス。</param>
        public void Put(
            [FromBody] ItemsPutDto<CustomerInputModel> input,
            [FromServices] ICustomersPutService service
            )
        {
            var requestDto = new CustomersPutInputDto();
            requestDto.AddedDtos = input.AddedItems;
            requestDto.ChangedDtos = input.ChangedItems;
            requestDto.DeletedDtos = input.DeletedItems;

            service.Put(requestDto);
        }

        #endregion
    }
}
