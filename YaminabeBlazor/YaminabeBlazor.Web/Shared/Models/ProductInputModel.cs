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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// 商品マスタの編集画面モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.Core.Models.EditableViewModelBase" />
    /// <seealso cref="YaminabeBlazor.Core.Dtos.IProductInputDto" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductInputModel : EditableViewModelBase, IProductInputDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品コードを取得または設定します。
        /// </summary>
        /// <remarks>
        /// 以下のモデル検証を行います。
        /// <list type="bullet">
        ///     <item>
        ///         <description>必須入力。</description>
        ///     </item>
        ///     <item>
        ///         <description>文字列長（最大20文字）。</description>
        ///     </item>
        ///     <item>
        ///         <description>正規表現（半角英数字）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "商品コード")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        [RegularExpression(@"^[0-9a-zA-Z]*$", ErrorMessageResourceName = "RegularHalfAlphaNum", ErrorMessageResourceType = typeof(MessageResource))]
        public string ProductId { get; set; }

        /// <summary>
        /// 商品名を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 以下のモデル検証を行います。
        /// <list type="bullet">
        ///     <item>
        ///         <description>必須入力。</description>
        ///     </item>
        ///     <item>
        ///         <description>文字列長（最大50文字）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "商品名")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string ProductName { get; set; }

        /// <summary>
        /// ブランドコードを取得または設定します。
        /// </summary>
        /// <remarks>
        /// 以下のモデル検証を行います。
        /// <list type="bullet">
        ///     <item>
        ///         <description>必須入力。</description>
        ///     </item>
        ///     <item>
        ///         <description>文字列長（最大20文字）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "ブランドコード")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string BrandId { get; set; }

        /// <summary>
        /// ブランド名を取得または設定します。
        /// </summary>
        [Display(Name = "ブランド名")]
        public string BrandName { get; set; }

        /// <summary>
        /// カテゴリコードを取得または設定します。
        /// </summary>
        /// <remarks>
        /// 以下のモデル検証を行います。
        /// <list type="bullet">
        ///     <item>
        ///         <description>必須入力。</description>
        ///     </item>
        ///     <item>
        ///         <description>文字列長（最大20文字）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "カテゴリコード")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CatetoryId { get; set; }

        /// <summary>
        /// カテゴリ名を取得または設定します。
        /// </summary>
        [Display(Name = "カテゴリ名")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 定価を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 以下のモデル検証を行います。
        /// <list type="bullet">
        ///     <item>
        ///         <description>必須入力。</description>
        ///     </item>
        ///     <item>
        ///         <description>数値範囲（0～999999999）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "定価")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [Range(0D, 999999999D, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(MessageResource))]
        public decimal ListPrice { get; set; }

        /// <summary>
        /// 商品タグを取得または設定します。
        /// </summary>
        [Display(Name = "商品タグ")]
        public ProductTagTypeOptions ProductTagType { get; set; }


        /// <summary>
        /// 商品タグ表示を取得または設定します。
        /// </summary>
        [Display(Name = "商品タグ表示")]
        public string ProductTagTypeName { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// ブランドを設定します。
        /// </summary>
        /// <param name="item">ブランド。</param>
        public void SetBrand(BrandListItem item)
        {
            this.BrandId = item?.BrandId;
            this.BrandName = item?.BrandName;
        }

        /// <summary>
        /// 商品カテゴリを設定します。
        /// </summary>
        /// <param name="item">商品カテゴリ。</param>
        public void SetCategory(ProductCategoryListItem item)
        {
            this.CatetoryId = item?.CategoryId;
            this.CategoryName = item?.CategoryName;
        }

        /// <summary>
        /// 商品タグを設定します。
        /// </summary>
        /// <param name="items">商品タグリスト。</param>
        public void SetProductTagType(IReadOnlyList<ProductTagTypeListItem> items)
        {
            this.ProductTagType = (ProductTagTypeOptions)Enum.ToObject(typeof(ProductTagTypeOptions), items.Sum(item => (int)item.ProductTagType));
            this.ProductTagTypeName = string.Join("\r\n", items.Select(item => item.Value));
        }

        #endregion
    }
}
