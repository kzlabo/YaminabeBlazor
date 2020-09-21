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

namespace YaminabeBlazor.Core.Repositories
{
    /// <summary>
    /// 基底リポジトリを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IRepositoryBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアクセスのコンテキストを取得または設定します。
        /// </summary>
        IDataAccessContext DataAccessContext { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 新規のデータアクセスのコンテキストを作成します。
        /// </summary>
        /// <returns>
        /// 新規のデータアクセスのコンテキストを返却します。
        /// </returns>
        IDataAccessContext CreateDataAccessContext();

        /// <summary>
        /// データアクセスのコンテキストを取得します。
        /// </summary>
        /// <returns>
        /// データアクセスのコンテキストを返却します。
        /// </returns>
        IDataAccessContext GetDataAccessContext();

        #endregion
    }
}
