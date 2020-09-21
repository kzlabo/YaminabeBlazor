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

using System.Data;
using System.Data.SqlClient;
using YaminabeBlazor.Core.Repositories;

namespace YaminabeBlazor.Infrastructure.Repositories
{
    /// <summary>
    /// データアクセスのコンテキストを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IDataAccessContext" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class DataAccessContext : IDataAccessContext
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 解放時の接続クローズ有無を取得または設定します。
        /// </summary>
        public bool AutoClose { get; set; }

        /// <inheritdoc/>
        public IDbConnection Connection { get; set; }

        /// <inheritdoc/>
        public IDbTransaction Transaction { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="DataAccessContext"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">データベース設定。</param>
        public DataAccessContext(IDbConfig config)
        {
            this.AutoClose = true;
            this.Connection = new SqlConnection(config.ConnectionString);
        }

        /// <summary>
        /// <see cref="DataAccessContext"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="connection">既存の接続。</param>
        /// <param name="transaction">トランザクション。</param>
        public DataAccessContext(
            SqlConnection connection,
            SqlTransaction transaction
            )
        {
            this.AutoClose = false;
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public void Open()
        {
            // 既にオープンしていれば無効
            if(this.Connection.State == System.Data.ConnectionState.Open)
            {
                return;
            }
            this.Connection.Open();
        }

        /// <inheritdoc/>
        public void Close()
        {
            // 自動クローズ時は無効
            if(this.AutoClose == true)
            {
                return;
            }
            this.Connection.Close();
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            if (this.Connection.State == ConnectionState.Closed)
            {
                this.Open();
            }
            this.Transaction = this.Connection.BeginTransaction();
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            this.Transaction.Commit();
            this.Transaction = null;
        }

        /// <inheritdoc/>
        public void RollBackTransaction()
        {
            this.Transaction.Rollback();
            this.Transaction = null;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.AutoClose == true)
                    {
                        this.Transaction?.Rollback();
                        this.Connection?.Dispose();
                    }
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
