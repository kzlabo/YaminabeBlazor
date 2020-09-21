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
    /// 得意先マスタリストの取得サービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Services.ICustomersGetService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomersGetService : ICustomersGetService
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先マスタリストの取得リポジトリを取得します。
        /// </summary>
        private ICustomersGetRepository _customersGetRepository;

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<CustomersGetService> _logger;

        /// <summary>
        /// 得意先マスタリストの出力情報を取得します。
        /// </summary>
        private ICustomersGetOutputDto _outputDto;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="customersGetRepository">得意先マスタリストの取得リポジトリ。</param>
        /// <param name="logger">ロガー。</param>
        public CustomersGetService(
            ICustomersGetRepository customersGetRepository,
            ILogger<CustomersGetService> logger
            )
        {
            this._customersGetRepository = customersGetRepository;
            this._logger = logger;
            this._outputDto = new CustomersGetOutputDto();
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public ICustomersGetOutputDto Get()
        {
            var customers = this._customersGetRepository.Get();

            this._outputDto.Customers = customers;

            return this._outputDto;
        }

        #endregion
    }
}
