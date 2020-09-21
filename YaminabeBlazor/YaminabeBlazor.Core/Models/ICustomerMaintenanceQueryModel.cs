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

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 得意先メンテナンスモデルを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.ICustomerModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomerMaintenanceQueryModel : ICustomerModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 締日区分名を取得または設定します。
        /// </summary>
        string CutoffDateTypeName { get; set; }

        /// <summary>
        /// 回収日区分名を取得または設定します。
        /// </summary>
        string CollectionDateTypeName { get; set; }

        /// <summary>
        /// 消費税区分名を取得または設定します。
        /// </summary>
        string TaxTypeName { get; set; }

        /// <summary>
        /// 消費税計算区分名を取得または設定します。
        /// </summary>
        string TaxCalcTypeName { get; set; }

        /// <summary>
        /// 消費税端数処理区分名を取得または設定します。
        /// </summary>
        string TaxRoundTypeName { get; set; }

        #endregion
    }
}
