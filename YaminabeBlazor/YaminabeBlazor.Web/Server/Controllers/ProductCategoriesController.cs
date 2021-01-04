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
    /// 商品カテゴリマスタリストのコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategoriesController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductCategoriesController> Logger { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoriesController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="logger">ロガー。</param>
        public ProductCategoriesController(
            ILogger<ProductCategoriesController> logger
            )
        {
            this.Logger = logger;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 商品カテゴリマスタリストを取得します。
        /// </summary>
        /// <param name="service">取得サービス。</param>
        /// <returns>
        /// 商品カテゴリマスタリストを返却します。
        /// </returns>
        [HttpGet]
        public IReadOnlyList<ProductCategoryInputModel> Get(
            [FromServices] IProductCategoriesGetService service
            )
        {
            var output = service.Get();

            return output.ProductCategories.Select<IProductCategoryModel, ProductCategoryInputModel>(
                model => new ProductCategoryInputModel()
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                }).ToList();
        }

        /// <summary>
        /// 商品カテゴリマスタを更新します。
        /// </summary>
        /// <param name="input">入力情報。</param>
        /// <param name="service">更新サービス。</param>
        public void Put(
            [FromBody] ItemsPutDto<ProductCategoryInputModel> input,
            [FromServices] IProductCategoriesPutService service
            )
        {
            var requestDto = new ProductCategoriesPutInputDto();
            requestDto.AddedDtos = input.AddedItems;
            requestDto.ChangedDtos = input.ChangedItems;
            requestDto.DeletedDtos = input.DeletedItems;

            service.Put(requestDto);
        }

        #endregion
    }
}