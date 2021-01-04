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

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Shared.Services
{
    /// <summary>
    /// 得意先マスタリストのApiサービスを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomersApiService
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 得意先マスタリストを返却します。
        /// </returns>
        Task<(HttpStatusCode HttpStatusCode, List<CustomerInputModel> Customers)> Get();

        /// <summary>
        /// 得意先マスタリストを更新します。
        /// </summary>
        /// <param name="addedItems">得意先マスタリストの追加対象。</param>
        /// <param name="changedItems">得意先マスタリストの更新対象。</param>
        /// <param name="deletedItems">得意先マスタリストの削除対象。</param>
        Task<HttpStatusCode> Put(
            IEnumerable<CustomerInputModel> addedItems,
            IEnumerable<CustomerInputModel> changedItems,
            IEnumerable<CustomerInputModel> deletedItems
            );

        #endregion
    }
}
