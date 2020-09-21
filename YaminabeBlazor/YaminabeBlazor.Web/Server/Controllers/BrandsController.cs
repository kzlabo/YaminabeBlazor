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
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Services;
using YaminabeBlazor.Web.Server.Dtos;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Server.Controllers
{
    /// <summary>
    /// ブランドマスタリストのコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<BrandsController> Logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public BrandsController(
            IExecutionModel executor,
            ILogger<BrandsController> logger
            )
        {
            this.Logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// ブランドマスタリストを取得します。
        /// </summary>
        /// <param name="service">取得サービス。</param>
        /// <returns>
        /// ブランドマスタリストを返却します。
        /// </returns>
        [HttpGet]
        public IReadOnlyList<BrandInputModel> Get(
            [FromServices] IBrandsGetService service
            )
        {
            var output = service.Get();

            return output.Brands.Select<IBrandModel, BrandInputModel>(
                model => new BrandInputModel()
                {
                    BrandId = model.BrandId,
                    BrandName = model.BrandName,
                    Note = model.Note
                }).ToList();
        }

        /// <summary>
        /// ブランドマスタリストを更新します。
        /// </summary>
        /// <param name="input">入力情報。</param>
        /// <param name="service">更新サービス。</param>
        [HttpPut]
        public void Put(
            [FromBody] IEnumerable<BrandInputModel> input,
            [FromServices] IBrandsPutService service
            )
        {
            var requestDto = new BrandsPutInputDto();
            requestDto.AddedDtos = input.GetAdded();
            requestDto.ChangedDtos = input.GetModified();
            requestDto.DeletedDtos = input.GetDeleted();

            service.Put(requestDto);
        }

        #endregion
    }
}