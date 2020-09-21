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
using YaminabeBlazor.Core.Repositories;

namespace YaminabeBlazor.Core.Services
{
    /// <summary>
    /// 商品カテゴリマスタリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IProductCategoriesGetService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoriesGetService : IProductCategoriesGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品カテゴリマスタリストの取得リポジトリを取得します。
        /// </summary>
        private IProductCategoriesGetRepository _productCategoriesGetRepository;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductCategoriesGetService> _logger;

        /// <summary>
        /// 商品カテゴリマスタリストの出力情報を取得します。
        /// </summary>
        private IProductCategoriesGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductCategoriesGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="productCategoriesGetRepository">商品カテゴリマスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public ProductCategoriesGetService(
            IProductCategoriesGetRepository productCategoriesGetRepository,
            ILogger<ProductCategoriesGetService> logger
            )
        {
            this._productCategoriesGetRepository = productCategoriesGetRepository;
            this._logger = logger;
            this._outputDto = new ProductCategoriesGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IProductCategoriesGetOutputDto Get()
        {
            var productCategories = this._productCategoriesGetRepository.Get();

            this._outputDto.ProductCategories = productCategories;

            return this._outputDto;
        }

        #endregion
    }
}
