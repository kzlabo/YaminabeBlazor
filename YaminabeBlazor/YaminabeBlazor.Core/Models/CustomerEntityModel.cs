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

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 得意先モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.ICustomerEntityModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerEntityModel : ICustomerEntityModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public EntityStateOptions EntityState { get; set; }

        /// <inheritdoc/>
        public string CustomerId { get; set; }

        /// <inheritdoc/>
        public string CustomerName { get; set; }

        /// <inheritdoc/>
        public string CustomerKanaName { get; set; }

        /// <inheritdoc/>
        public string CustomerShortName { get; set; }

        /// <inheritdoc/>
        public DateTime? EstablishmentDate { get; set; }

        /// <inheritdoc/>
        public string Ceo { get; set; }

        /// <inheritdoc/>
        public string PostalCode { get; set; }

        /// <inheritdoc/>
        public string Address { get; set; }

        /// <inheritdoc/>
        public string Tel { get; set; }

        /// <inheritdoc/>
        public string Fax { get; set; }

        /// <inheritdoc/>
        public string Mail { get; set; }

        /// <inheritdoc/>
        public string Hp { get; set; }

        /// <inheritdoc/>
        public CutoffDateTypeOptions CutoffDateType { get; set; }

        /// <inheritdoc/>
        public int CutoffDate { get; set; }

        /// <inheritdoc/>
        public CollectionDateTypeOptions CollectionDateType { get; set; }

        /// <inheritdoc/>
        public int CollectionDate { get; set; }

        /// <inheritdoc/>
        public TaxTypeOptions TaxType { get; set; }

        /// <inheritdoc/>
        public TaxCalcTypeOptions TaxCalcType { get; set; }

        /// <inheritdoc/>
        public TaxRoundTypeOptions TaxRoundType { get; set; }

        /// <inheritdoc/>
        public string Note { get; set; }

        /// <inheritdoc/>
        public DateTime UpdateDateTime { get; set; }

        /// <inheritdoc/>
        public string UpdateUserId { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomerEntityModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="customerId">得意先コード。</param>
        /// <param name="customerName">得意先名。</param>
        /// <param name="customerKanaName">得意先名カナ。</param>
        /// <param name="customerShortName">得意先略名。</param>
        /// <param name="establishmentDate">設立年月日。</param>
        /// <param name="ceo">代表者。</param>
        /// <param name="postalCode">郵便番号。</param>
        /// <param name="address">住所。</param>
        /// <param name="tel">電話番号。</param>
        /// <param name="fax">FAX番号。</param>
        /// <param name="mail">メールアドレス。</param>
        /// <param name="hp">ホームページ。</param>
        /// <param name="cutoffDateType">締日区分。</param>
        /// <param name="cutoffDate">締日。</param>
        /// <param name="collectionDateType">支払日区分。</param>
        /// <param name="collectionDate">支払日。</param>
        /// <param name="taxType">消費税区分。</param>
        /// <param name="taxCalcType">消費税計算区分。</param>
        /// <param name="taxRoundType">消費税端数処理区分。</param>
        /// <param name="note">メモ。</param>
        /// <param name="updateDateTime">更新日時。</param>
        /// <param name="updateUserId">更新者コード。</param>
        /// <param name="entityState">エンティティ状態。</param>
        public CustomerEntityModel(
            string customerId,
            string customerName,
            string customerKanaName,
            string customerShortName,
            DateTime? establishmentDate,
            string ceo,
            string postalCode,
            string address,
            string tel,
            string fax,
            string mail,
            string hp,
            CutoffDateTypeOptions cutoffDateType,
            int cutoffDate,
            CollectionDateTypeOptions collectionDateType,
            int collectionDate,
            TaxTypeOptions taxType,
            TaxCalcTypeOptions taxCalcType,
            TaxRoundTypeOptions taxRoundType,
            string note,
            DateTime updateDateTime,
            string updateUserId,
            EntityStateOptions entityState = EntityStateOptions.None
            )
        {
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.CustomerKanaName = customerKanaName;
            this.CustomerShortName = customerShortName;
            this.EstablishmentDate = establishmentDate;
            this.Ceo = ceo;
            this.PostalCode = postalCode;
            this.Address = address;
            this.Tel = tel;
            this.Fax = fax;
            this.Mail = mail;
            this.Hp = hp;
            this.CutoffDateType = cutoffDateType;
            this.CutoffDate = cutoffDate;
            this.CollectionDateType = collectionDateType;
            this.CollectionDate = collectionDate;
            this.TaxType = taxType;
            this.TaxCalcType = taxCalcType;
            this.TaxRoundType = taxRoundType;
            this.Note = note;
            this.UpdateDateTime = updateDateTime;
            this.UpdateUserId = updateUserId;
            this.EntityState = entityState;
        }

        #endregion
    }
}
