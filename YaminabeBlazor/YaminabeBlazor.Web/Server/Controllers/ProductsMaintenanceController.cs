﻿/*
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
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Server.Controllers
{
    /// <summary>
    /// 商品マスタメンテナンスリストのコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsMaintenanceController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductsMaintenanceController> Logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsMaintenanceController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public ProductsMaintenanceController(
            ILogger<ProductsMaintenanceController> logger
            )
        {
            this.Logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品マスタメンテナンスリストを取得します。
        /// </summary>
        /// <param name="service">取得サービス。</param>
        /// <returns>
        /// 商品マスタメンテナンスリストを返却します。
        /// </returns>
        [HttpGet]
        public IReadOnlyList<ProductInputModel> Get(
            [FromServices] IProductsMaintenanceGetService service
            )
        {
            var output = service.Get();

            return output.Products.Select<IProductMaintenanceQueryModel, ProductInputModel>(
                model => new ProductInputModel()
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    BrandId = model.BrandId,
                    BrandName = model.BrandName,
                    CatetoryId = model.CatetoryId,
                    CategoryName = model.CatetoryName,
                    ListPrice = model.ListPrice,
                    ProductTagType = model.ProductTagType,
                    ProductTagTypeName = model.ProductTagTypeName
                }).ToList();
        }

        #endregion
    }
}
