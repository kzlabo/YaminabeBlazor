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
using YaminabeBlazor.Core.Enums;

namespace YaminabeBlazor.Core.Dtos
{
    /// <summary>
    /// 得意先マスタの入力情報を表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ICustomerInputDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先コードを取得します。
        /// </summary>
        string CustomerId { get; }

        /// <summary>
        /// 得意先名を取得または設定します。
        /// </summary>
        string CustomerName { get; set; }

        /// <summary>
        /// 得意先名カナを取得または設定します。
        /// </summary>
        string CustomerKanaName { get; set; }

        /// <summary>
        /// 得意先略名を取得または設定します。
        /// </summary>
        string CustomerShortName { get; set; }

        /// <summary>
        /// 設立年月日を取得または設定します。
        /// </summary>
        DateTime? EstablishmentDate { get; set; }

        /// <summary>
        /// 代表者を取得または設定します。
        /// </summary>
        string Ceo { get; set; }

        /// <summary>
        /// 郵便番号を取得または設定します。
        /// </summary>
        string PostalCode { get; set; }

        /// <summary>
        /// 住所を取得または設定します。
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// 電話番号を取得または設定します。
        /// </summary>
        string Tel { get; set; }

        /// <summary>
        /// FAX番号を取得または設定します。
        /// </summary>
        string Fax { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        string Mail { get; set; }

        /// <summary>
        /// ホームページを取得または設定します。
        /// </summary>
        string Hp { get; set; }

        /// <summary>
        /// 締日区分を取得または設定します。
        /// </summary>
        CutoffDateTypeOptions CutoffDateType { get; set; }

        /// <summary>
        /// 締日を取得または設定します。
        /// </summary>
        int CutoffDate { get; set; }

        /// <summary>
        /// 回収日区分を取得または設定します。
        /// </summary>
        CollectionDateTypeOptions CollectionDateType { get; set; }

        /// <summary>
        /// 回収日を取得または設定します。
        /// </summary>
        int CollectionDate { get; set; }

        /// <summary>
        /// 消費税区分を取得または設定します。
        /// </summary>
        TaxTypeOptions TaxType { get; set; }

        /// <summary>
        /// 消費税計算区分を取得または設定します。
        /// </summary>
        TaxCalcTypeOptions TaxCalcType { get; set; }

        /// <summary>
        /// 消費税端数処理区分を取得または設定します。
        /// </summary>
        TaxRoundTypeOptions TaxRoundType { get; set; }

        /// <summary>
        /// メモを取得または設定します。
        /// </summary>
        string Note { get; set; }

        #endregion
    }
}
