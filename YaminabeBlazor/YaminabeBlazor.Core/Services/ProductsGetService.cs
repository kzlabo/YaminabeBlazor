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
    /// 商品マスタリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IProductsGetService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductsGetService : IProductsGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品マスタリストの取得リポジトリを取得します。
        /// </summary>
        private IProductsGetRepository _productsGetRepository;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductsGetService> _logger;

        /// <summary>
        /// 商品マスタリストの出力情報を取得します。
        /// </summary>
        private IProductsGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="productsGetRepository">商品マスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public ProductsGetService(
            IProductsGetRepository productsGetRepository,
            ILogger<ProductsGetService> logger
            )
        {
            this._productsGetRepository = productsGetRepository;
            this._logger = logger;
            this._outputDto = new ProductsGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IProductsGetOutputDto Get()
        {
            var products = this._productsGetRepository.Get();

            this._outputDto.Products = products;

            return this._outputDto;
        }

        #endregion
    }
}
