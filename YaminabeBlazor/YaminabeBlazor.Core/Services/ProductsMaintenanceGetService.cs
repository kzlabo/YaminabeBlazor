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
using System.Linq;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Queries;

namespace YaminabeBlazor.Core.Services
{
    /// <summary>
    /// 商品マスタメンテナンスリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IProductsMaintenanceGetService" />
    public class ProductsMaintenanceGetService : IProductsMaintenanceGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品マスタメンテナンスリストの取得クエリを取得します。
        /// </summary>
        private IProductsMaintenanceGetQuery _productsMaintenanceGetQuery;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<ProductsMaintenanceGetService> _logger;

        /// <summary>
        /// 商品マスタメンテナンスリストの出力情報を取得します。
        /// </summary>
        private IProductsMaintenanceGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ProductsMaintenanceGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="productsMaintenanceGetQuery">商品マスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public ProductsMaintenanceGetService(
            IProductsMaintenanceGetQuery productsMaintenanceGetQuery,
            ILogger<ProductsMaintenanceGetService> logger
            )
        {
            this._productsMaintenanceGetQuery = productsMaintenanceGetQuery;
            this._logger = logger;
            this._outputDto = new ProductsMaintenanceGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IProductsMaintenanceGetOutputDto Get()
        {
            var products = this._productsMaintenanceGetQuery.Get();
            var productTagTypes = ProductTagTypeFactory.CreateInitialData();
            foreach (var product in products)
            {
                product.ProductTagTypeName = string.Join("\r\n", productTagTypes.Where(p => p.ProductTagType.Equals(ProductTagTypeOptions.None) == false).Where(p => product.ProductTagType.HasFlag(p.ProductTagType)).Select(p => p.ProductTagTypeName).ToArray());
            }

            this._outputDto.Products = products;

            return this._outputDto;
        }

        #endregion
    }
}
