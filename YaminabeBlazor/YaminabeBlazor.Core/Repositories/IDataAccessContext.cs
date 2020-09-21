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

using System;
using System.Data;

namespace YaminabeBlazor.Core.Repositories
{
    /// <summary>
    /// データアクセスのコンテキストを表します。
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IDataAccessContext : IDisposable
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 接続を取得または設定します。
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// トランザクションを取得または設定します。
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// データベース接続をオープンします。
        /// </summary>
        void Open();

        /// <summary>
        /// データベース接続をクローズします。
        /// </summary>
        void Close();

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// トランザクションをコミットします。
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// トランザクションをロールバックします。
        /// </summary>
        void RollBackTransaction();

        #endregion
    }
}
