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
    /// ブランドマスタリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.IBrandsGetService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandsGetService : IBrandsGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ブランドマスタリストの取得リポジトリを取得します。
        /// </summary>
        private IBrandsGetRepository _brandsGetRepository;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<BrandsGetService> _logger;

        /// <summary>
        /// ブランドマスタリストの出力情報を取得します。
        /// </summary>
        private IBrandsGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="brandsGetRepository">ブランドマスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public BrandsGetService(
            IBrandsGetRepository brandsGetRepository,
            ILogger<BrandsGetService> logger
            )
        {
            this._brandsGetRepository = brandsGetRepository;
            this._logger = logger;
            this._outputDto = new BrandsGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IBrandsGetOutputDto Get()
        {
            var brands = this._brandsGetRepository.Get();

            this._outputDto.Brands = brands;

            return this._outputDto;
        }

        #endregion
    }
}
