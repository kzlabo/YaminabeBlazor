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
    /// 商品マスタメンテ画面設定のコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductMaintenanceSettingController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductMaintenanceSettingController> _logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductMaintenanceSettingController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public ProductMaintenanceSettingController(
            ILogger<ProductMaintenanceSettingController> logger
            )
        {
            this._logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品マスタメンテ画面設定を取得します。
        /// </summary>
        /// <param name="service">商品マスタメンテ画面設定取得サービス。</param>
        /// <returns>
        /// 商品マスタメンテ画面設定を返却します。
        /// </returns>
        [HttpGet]
        public ProductMaintenanceSettingDto Get(
            [FromServices] IProductMaintenanceSettingGetService service
            )
        {
            var output = service.Get();

            var response = new ProductMaintenanceSettingDto();

            // ブランドリスト
            response.BrandList = output.Brands.Select<IBrandModel, BrandListItem>(
                model => new BrandListItem()
                {
                    BrandId = model.BrandId,
                    BrandName = model.BrandName,
                    Note = model.Note
                }).ToList();

            // 商品カテゴリリスト
            response.ProductCategoryList = output.ProductCategories.Select<IProductCategoryModel, ProductCategoryListItem>(
                model => new ProductCategoryListItem()
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                }).ToList();

            // 商品タグリスト
            response.ProductTagTypeList = output.ProductTagTypes.Select<IProductTagTypeModel, ProductTagTypeListItem>(
                model => new ProductTagTypeListItem()
                {
                    ProductTagType = model.ProductTagType,
                    ProductTagTypeName = model.ProductTagTypeName
                }).ToList();

            return response;
        }

        #endregion
    }
}