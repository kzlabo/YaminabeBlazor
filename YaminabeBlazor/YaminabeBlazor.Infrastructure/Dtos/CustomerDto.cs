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
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;
using YaminabeBlazor.Infrastructure.Attributes;
using YaminabeBlazor.Infrastructure.Extensions;

namespace YaminabeBlazor.Infrastructure.Dtos
{
    /// <summary>
    /// 得意先マスタの転送オブジェクトを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.EntityDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IDapperDto" />
    /// <seealso cref="YaminabeBlazor.Infrastructure.Dtos.IEntityConverter{YaminabeBlazor.Core.Models.ICustomerEntityModel}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerDto : EntityDto, IDapperDto, IEntityConverter<ICustomerEntityModel>
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先コードを取得します。
        /// </summary>
        [DapperMap(Name = "得意先コード")]
        public string CustomerId { get; set; }

        /// <summary>
        /// 得意先名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "得意先名")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 得意先名カナを取得または設定します。
        /// </summary>
        [DapperMap(Name = "得意先名カナ")]
        public string CustomerKanaName { get; set; }

        /// <summary>
        /// 得意先略名を取得または設定します。
        /// </summary>
        [DapperMap(Name = "得意先略名")]
        public string CustomerShortName { get; set; }

        /// <summary>
        /// 設立年月日を取得または設定します。
        /// </summary>
        [DapperMap(Name = "設立年月日")]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>
        /// 代表者を取得または設定します。
        /// </summary>
        [DapperMap(Name = "代表者")]
        public string Ceo { get; set; }

        /// <summary>
        /// 郵便番号を取得または設定します。
        /// </summary>
        [DapperMap(Name = "郵便番号")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 住所を取得または設定します。
        /// </summary>
        [DapperMap(Name = "住所")]
        public string Address { get; set; }

        /// <summary>
        /// 電話番号を取得または設定します。
        /// </summary>
        [DapperMap(Name = "電話番号")]
        public string Tel { get; set; }

        /// <summary>
        /// FAX番号を取得または設定します。
        /// </summary>
        [DapperMap(Name = "FAX番号")]
        public string Fax { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        [DapperMap(Name = "メールアドレス")]
        public string Mail { get; set; }

        /// <summary>
        /// ホームページを取得または設定します。
        /// </summary>
        [DapperMap(Name = "ホームページアドレス")]
        public string Hp { get; set; }

        /// <summary>
        /// 締日区分を取得または設定します。
        /// </summary>
        [DapperMap(Name = "締日区分")]
        public CutoffDateTypeOptions CutoffDateType { get; set; }

        /// <summary>
        /// 締日を取得または設定します。
        /// </summary>
        [DapperMap(Name = "締日")]
        public int CutoffDate { get; set; }

        /// <summary>
        /// 回収日区分を取得または設定します。
        /// </summary>
        [DapperMap(Name = "回収日区分")]
        public CollectionDateTypeOptions CollectionDateType { get; set; }

        /// <summary>
        /// 回収日を取得または設定します。
        /// </summary>
        [DapperMap(Name = "回収日")]
        public int CollectionDate { get; set; }

        /// <summary>
        /// 消費税区分を取得または設定します。
        /// </summary>
        [DapperMap(Name = "消費税区分")]
        public TaxTypeOptions TaxType { get; set; }

        /// <summary>
        /// 消費税計算区分を取得または設定します。
        /// </summary>
        [DapperMap(Name = "消費税計算区分")]
        public TaxCalcTypeOptions TaxCalcType { get; set; }

        /// <summary>
        /// 消費税端数処理区分を取得または設定します。
        /// </summary>
        [DapperMap(Name = "消費税端数処理区分")]
        public TaxRoundTypeOptions TaxRoundType { get; set; }

        /// <summary>
        /// メモを取得または設定します。
        /// </summary>
        [DapperMap(Name = "メモ")]
        public string Note { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// エンティティを転送オブジェクトに変換して取り込みます。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        public void ConvertFromEntity(ICustomerEntityModel entity)
        {
            this.CustomerId = entity.CustomerId;
            this.CustomerName = entity.CustomerName;
            this.CustomerKanaName = entity.CustomerKanaName;
            this.CustomerShortName = entity.CustomerShortName;
            this.EstablishmentDate = entity.EstablishmentDate.SqlServerValue();
            this.Ceo = entity.Ceo;
            this.PostalCode = entity.PostalCode;
            this.Address = entity.Address;
            this.Tel = entity.Tel;
            this.Fax = entity.Fax;
            this.Mail = entity.Mail;
            this.Hp = entity.Hp;
            this.CutoffDateType = entity.CutoffDateType;
            this.CutoffDate = entity.CutoffDate;
            this.CollectionDateType = entity.CollectionDateType;
            this.CollectionDate = entity.CollectionDate;
            this.TaxType = entity.TaxType;
            this.TaxCalcType = entity.TaxCalcType;
            this.TaxRoundType = entity.TaxRoundType;
            this.Note = entity.Note;
            this.UpdateDateTime = entity.UpdateDateTime.SqlServerValue();
            this.UpdateUserId = entity.UpdateUserId;
            this.EntityState = entity.EntityState;
        }

        /// <summary>
        /// 転送オブジェクトをエンティティに変換します。
        /// </summary>
        /// <returns>
        /// エンティティを返却します。
        /// </returns>
        public ICustomerEntityModel ConvertToEntity()
        {
            return CustomerFactory.Load(
                customerId: this.CustomerId,
                customerName: this.CustomerName,
                customerKanaName: this.CustomerKanaName,
                customerShortName: this.CustomerShortName,
                establishmentDate: this.EstablishmentDate,
                ceo: this.Ceo,
                postalCode: this.PostalCode,
                address: this.Address,
                tel: this.Tel,
                fax: this.Fax,
                mail: this.Mail,
                hp: this.Hp,
                cutoffDateType: this.CutoffDateType,
                cutoffDate: this.CutoffDate,
                collectionDateType: this.CollectionDateType,
                collectionDate: this.CollectionDate,
                taxType: this.TaxType,
                taxCalcType: this.TaxCalcType,
                taxRoundType: this.TaxRoundType,
                note: this.Note,
                updateDateTime: this.UpdateDateTime,
                updateUserId: this.UpdateUserId
                );
        }

        #endregion
    }
}
