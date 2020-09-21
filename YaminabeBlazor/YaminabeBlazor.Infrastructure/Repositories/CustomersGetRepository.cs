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

using Dapper;
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Core.Repositories;
using YaminabeBlazor.Infrastructure.Dtos;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// 得意先マスタリストの取得リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Repositories.RepositoryBase" />
    /// <seealso cref="YaminabeBlazor.Core.Repositories.ICustomersGetRepository" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomersGetRepository : RepositoryBase, ICustomersGetRepository
    {
        #region -------------------- const --------------------

        /// <summary>
        /// 取得SQL文。
        /// </summary>
        private const string _getSql = @"
SELECT
    [得意先コード],
    [得意先名],
    [得意先名カナ],
    [得意先略名],
    [設立年月日],
    [代表者],
    [郵便番号],
    [住所],
    [電話番号],
    [FAX番号],
    [メールアドレス],
    [ホームページアドレス],
    [締日区分],
    [締日],
    [回収日区分],
    [回収日],
    [消費税区分],
    [消費税計算区分],
    [消費税端数処理区分],
    [メモ],
    [更新日時],
    [更新者コード]
FROM [得意先Master]
ORDER BY
    [得意先コード]
";

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersGetRepository"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public CustomersGetRepository(IDbConfig config) : base(config: config)
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public List<ICustomerEntityModel> Get()
        {
            using var dataAccessContext = this.GetDataAccessContext();

            var dtos = dataAccessContext.Connection.Query<CustomerDto>(
                sql: CustomersGetRepository._getSql,
                transaction: dataAccessContext.Transaction
                );

            return dtos.Select<CustomerDto, ICustomerEntityModel>(dto => dto?.ConvertToEntity()).ToList();
        }

        #endregion
    }
}
