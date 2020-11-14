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

using System.ComponentModel.DataAnnotations;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Core.Dtos;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// 商品カテゴリマスタの編集画面モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.Core.Models.EditableViewModelBase" />
    /// <seealso cref="YaminabeBlazor.Core.Dtos.IProductCategoryInputDto" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ProductCategoryInputModel : EditableViewModelBase, IProductCategoryInputDto
    {
        #region -------------------- property --------------------

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
        ///     <item>
        ///         <description>正規表現（半角英数字）。</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Display(Name = "カテゴリコード")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        [RegularExpression(@"^[0-9a-zA-Z]*$", ErrorMessageResourceName = "RegularHalfAlphaNum", ErrorMessageResourceType = typeof(MessageResource))]
        public string CategoryId { get; set; }

        /// <summary>
        /// カテゴリ名を取得または設定します。
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
        [Display(Name = "カテゴリ名")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }

        #endregion
    }
}
