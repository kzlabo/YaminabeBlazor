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

using YaminabeBlazor.Core.Enums;

namespace YaminabeBlazor.Core.Models
{
    /// <summary>
    /// 商品モデルを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IProductModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品コードを取得します。
        /// </summary>
        string ProductId { get; }

        /// <summary>
        /// 商品名を取得または設定します。
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// ブランドコードを取得または設定します。
        /// </summary>
        string BrandId { get; set; }

        /// <summary>
        /// カテゴリコードを取得または設定します。
        /// </summary>
        string CatetoryId { get; set; }

        /// <summary>
        /// 定価を取得または設定します。
        /// </summary>
        decimal ListPrice { get; set; }

        /// <summary>
        /// 商品タグを取得または設定します。
        /// </summary>
        ProductTagTypeOptions ProductTagType { get; set; }

        #endregion
    }
}
