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
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Core.Factories
{
    /// <summary>
    /// <see cref="YaminabeBlazor.Core.Models.CustomerEntityModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// インスタンスを得意先マスタからのロード状態で作成します。
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
        /// <returns>
        /// 得意先マスタを返却します。
        /// </returns>
        public static CustomerEntityModel Load(
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
            string updateUserId
            )
        {
            return new CustomerEntityModel(
                customerId: customerId,
                customerName: customerName,
                customerKanaName: customerKanaName,
                customerShortName: customerShortName,
                establishmentDate: establishmentDate,
                ceo: ceo,
                postalCode: postalCode,
                address: address,
                tel: tel,
                fax: fax,
                mail: mail,
                hp: hp,
                cutoffDateType: cutoffDateType,
                cutoffDate: cutoffDate,
                collectionDateType: collectionDateType,
                collectionDate: collectionDate,
                taxType: taxType,
                taxCalcType: taxCalcType,
                taxRoundType: taxRoundType,
                note: note,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.UnChanged
                );
        }

        /// <summary>
        /// インスタンスを得意先マスタからの新規状態で作成します。
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
        /// <returns>
        /// 得意先マスタを返却します。
        /// </returns>
        public static CustomerEntityModel Create(
            string customerId,
            string customerName = default,
            string customerKanaName = default,
            string customerShortName = default,
            DateTime? establishmentDate = default,
            string ceo = default,
            string postalCode = default,
            string address = default,
            string tel = default,
            string fax = default,
            string mail = default,
            string hp = default,
            CutoffDateTypeOptions cutoffDateType = CutoffDateTypeOptions.None,
            int cutoffDate = default,
            CollectionDateTypeOptions collectionDateType = CollectionDateTypeOptions.None,
            int collectionDate = default,
            TaxTypeOptions taxType = TaxTypeOptions.None,
            TaxCalcTypeOptions taxCalcType = TaxCalcTypeOptions.None,
            TaxRoundTypeOptions taxRoundType = TaxRoundTypeOptions.None,
            string note = default,
            DateTime updateDateTime = default,
            string updateUserId = default
            )
        {
            return new CustomerEntityModel(
                customerId: customerId,
                customerName: customerName,
                customerKanaName: customerKanaName,
                customerShortName: customerShortName,
                establishmentDate: establishmentDate,
                ceo: ceo,
                postalCode: postalCode,
                address: address,
                tel: tel,
                fax: fax,
                mail: mail,
                hp: hp,
                cutoffDateType: cutoffDateType,
                cutoffDate: cutoffDate,
                collectionDateType: collectionDateType,
                collectionDate: collectionDate,
                taxType: taxType,
                taxCalcType: taxCalcType,
                taxRoundType: taxRoundType,
                note: note,
                updateDateTime: updateDateTime,
                updateUserId: updateUserId,
                entityState: EntityStateOptions.Added
                );
        }

        #endregion
    }
}
