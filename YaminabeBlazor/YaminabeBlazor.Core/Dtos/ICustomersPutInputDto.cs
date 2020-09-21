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

namespace YaminabeBlazor.Core.Dtos
{
    /// <summary>
    /// <see cref="YaminabeBlazor.Core.Services.ICustomersPutService"/> の入力情報を表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomersPutInputDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 追加分の得意先リストを取得または設定します。
        /// </summary>
        IEnumerable<ICustomerInputDto> AddedDtos { get; set; }

        /// <summary>
        /// 更新分の得意先リストを取得または設定します。
        /// </summary>
        IEnumerable<ICustomerInputDto> ChangedDtos { get; set; }

        /// <summary>
        /// 削除分の得意先リストを取得または設定します。
        /// </summary>
        IEnumerable<ICustomerInputDto> DeletedDtos { get; set; }

        #endregion
    }
}
