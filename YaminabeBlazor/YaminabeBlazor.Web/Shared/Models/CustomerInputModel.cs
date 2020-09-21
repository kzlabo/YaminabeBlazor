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
using System.ComponentModel.DataAnnotations;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// 得意先マスタの編集画面モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.Core.Models.EditableViewModelBase" />
    /// <seealso cref="YaminabeBlazor.Core.Dtos.ICustomerInputDto" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class CustomerInputModel : EditableViewModelBase, ICustomerInputDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 得意先コードを取得します。
        /// </summary>
        [Display(Name = "得意先コード")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CustomerId { get; set; }

        /// <summary>
        /// 得意先名を取得または設定します。
        /// </summary>
        [Display(Name = "得意先名")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }

        /// <summary>
        /// 得意先名カナを取得または設定します。
        /// </summary>
        [Display(Name = "得意先名カナ")]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CustomerKanaName { get; set; }

        /// <summary>
        /// 得意先略名を取得または設定します。
        /// </summary>
        [Display(Name = "得意先略名")]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CustomerShortName { get; set; }

        /// <summary>
        /// 設立年月日を取得または設定します。
        /// </summary>
        [Display(Name = "設立年月日")]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>
        /// 代表者を取得または設定します。
        /// </summary>
        [Display(Name = "代表者")]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Ceo { get; set; }

        /// <summary>
        /// 郵便番号を取得または設定します。
        /// </summary>
        [Display(Name = "郵便番号")]
        [StringLength(7, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string PostalCode { get; set; }

        /// <summary>
        /// 住所を取得または設定します。
        /// </summary>
        [Display(Name = "住所")]
        [StringLength(500, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Address { get; set; }

        /// <summary>
        /// 電話番号を取得または設定します。
        /// </summary>
        [Display(Name = "電話番号")]
        [StringLength(11, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Tel { get; set; }

        /// <summary>
        /// FAX番号を取得または設定します。
        /// </summary>
        [Display(Name = "FAX番号")]
        [StringLength(11, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Fax { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        [Display(Name = "メールアドレス")]
        [StringLength(256, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Mail { get; set; }

        /// <summary>
        /// ホームページを取得または設定します。
        /// </summary>
        [Display(Name = "ホームページアドレス")]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Hp { get; set; }

        /// <summary>
        /// 締日区分を取得または設定します。
        /// </summary>
        [Display(Name = "締日区分")]
        public CutoffDateTypeOptions CutoffDateType { get; set; }

        /// <summary>
        /// 締日区分名を取得または設定します。
        /// </summary>
        [Display(Name = "締日区分名")]
        public string CutoffDateTypeName { get; set; }

        /// <summary>
        /// 締日を取得または設定します。
        /// </summary>
        [Display(Name = "締日")]
        public int CutoffDate { get; set; }

        /// <summary>
        /// 回収日区分を取得または設定します。
        /// </summary>
        [Display(Name = "回収日区分")]
        public CollectionDateTypeOptions CollectionDateType { get; set; }

        /// <summary>
        /// 回収日区分を取得または設定します。
        /// </summary>
        [Display(Name = "回収日区分名")]
        public string CollectionDateTypeName { get; set; }

        /// <summary>
        /// 回収日を取得または設定します。
        /// </summary>
        [Display(Name = "回収日")]
        public int CollectionDate { get; set; }

        /// <summary>
        /// 消費税区分を取得または設定します。
        /// </summary>
        [Display(Name = "消費税区分")]
        public TaxTypeOptions TaxType { get; set; }

        /// <summary>
        /// 消費税区分名を取得または設定します。
        /// </summary>
        [Display(Name = "消費税区分名")]
        public string TaxTypeName { get; set; }

        /// <summary>
        /// 消費税計算区分を取得または設定します。
        /// </summary>
        [Display(Name = "消費税計算区分")]
        public TaxCalcTypeOptions TaxCalcType { get; set; }

        /// <summary>
        /// 消費税計算区分名を取得または設定します。
        /// </summary>
        [Display(Name = "消費税計算区分名")]
        public string TaxCalcTypeName { get; set; }

        /// <summary>
        /// 消費税端数処理区分を取得または設定します。
        /// </summary>
        [Display(Name = "消費税端数処理区分")]
        public TaxRoundTypeOptions TaxRoundType { get; set; }

        /// <summary>
        /// 消費税端数処理区分名を取得または設定します。
        /// </summary>
        [Display(Name = "消費税端数処理区分名")]
        public string TaxRoundTypeName { get; set; }

        /// <summary>
        /// メモを取得または設定します。
        /// </summary>
        [Display(Name = "メモ")]
        [StringLength(200, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Note { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 締日区分を設定します。
        /// </summary>
        /// <param name="item">締日区分。</param>
        public void SetCutoffDateType(CutoffDateTypeListItem item)
        {
            this.CutoffDateType = item.CutoffDateType;
            this.CutoffDateTypeName = item.CutoffDateTypeName;
        }

        /// <summary>
        /// 回収日区分を設定します。
        /// </summary>
        /// <param name="item">回収日区分。</param>
        public void SetCollectionDateType(CollectionDateTypeListItem item)
        {
            this.CollectionDateType = item.CollectionDateType;
            this.CollectionDateTypeName = item.CollectionDateTypeName;
        }

        /// <summary>
        /// 消費税区分を設定します。
        /// </summary>
        /// <param name="item">消費税区分。</param>
        public void SetTaxType(TaxTypeListItem item)
        {
            this.TaxType = item.TaxType;
            this.TaxTypeName = item.TaxTypeName;
        }

        /// <summary>
        /// 消費税計算区分を設定します。
        /// </summary>
        /// <param name="item">消費税計算区分。</param>
        public void SetTaxCalcType(TaxCalcTypeListItem item)
        {
            this.TaxCalcType = item.TaxCalcType;
            this.TaxCalcTypeName = item.TaxCalcTypeName;
        }

        /// <summary>
        /// 消費税端数処理区分を設定します。
        /// </summary>
        /// <param name="item">消費税端数処理区分。</param>
        public void SetTaxRoundType(TaxRoundTypeListItem item)
        {
            this.TaxRoundType = item.TaxRoundType;
            this.TaxRoundTypeName = item.TaxRoundTypeName;
        }

        #endregion
    }
}
