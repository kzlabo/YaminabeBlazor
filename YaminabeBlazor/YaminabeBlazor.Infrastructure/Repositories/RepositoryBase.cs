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

using System.Data.SqlClient;
using YaminabeBlazor.Core.Repositories;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// 基底リポジトリを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IRepositoryBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class RepositoryBase : IRepositoryBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データベース設定を取得または設定します。。
        /// </summary>
        private IDbConfig Config { get; set; }

        /// <inheritdoc/>
        public IDataAccessContext DataAccessContext { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="RepositoryBase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public RepositoryBase(IDbConfig config)
        {
            this.Config = config;
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public IDataAccessContext CreateDataAccessContext()
        {
            return new DataAccessContext(
                config: this.Config
                );
        }

        /// <inheritdoc/>
        public IDataAccessContext GetDataAccessContext()
        {
            if(this.DataAccessContext == null)
            {
                return new DataAccessContext(
                    config: this.Config
                    );
            }
            else
            {
                return new DataAccessContext(
                    connection: (SqlConnection)this.DataAccessContext.Connection,
                    transaction: (SqlTransaction)this.DataAccessContext.Transaction
                    );
            }
        }

        #endregion
    }
}
