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

namespace YaminabeBlazor.Core.Dtos
{
    /// <summary>
    /// <see cref="YaminabeBlazor.Core.Services.ICustomerMaintenanceSettingGetService"/> の出力情報を表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomersMaintenanceSettingGetOutputDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 締日リストを取得または設定します。
        /// </summary>
        IReadOnlyList<ICutoffDateTypeModel> CutoffDateTypes { get; set; }

        /// <summary>
        /// 回収日リストを取得または設定します。
        /// </summary>
        IReadOnlyList<ICollectionDateTypeModel> CollectionDateTypes { get; set; }

        /// <summary>
        /// 消費税区分リストを取得または設定します。
        /// </summary>
        IReadOnlyList<ITaxTypeModel> TaxTypes { get; set; }

        /// <summary>
        /// 消費税計算区分リストを取得または設定します。
        /// </summary>
        IReadOnlyList<ITaxCalcTypeModel> TaxCalcTypes { get; set; }

        /// <summary>
        /// 消費税端数処理区分リストを取得または設定します。
        /// </summary>
        IReadOnlyList<ITaxRoundType> TaxRoundTypes { get; set; }

        #endregion
    }
}
