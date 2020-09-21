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
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Core.Repositories
{
    /// <summary>
    /// 得意先マスタリストの更新リポジトリを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Repositories.IRepositoryBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomersPutRepository : IRepositoryBase
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 指定した得意先リストを得意先マスタに更新します。
        /// </summary>
        /// <param name="customers">得意先リスト。</param>
        void Put(IEnumerable<ICustomerEntityModel> customers);

        #endregion
    }
}
