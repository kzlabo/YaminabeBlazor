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

using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Shared.ListItems
{
    /// <summary>
    /// 消費税計算区分マスタのリスト選択モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Models.TaxCalcTypeModel" />
    /// <seealso cref="YaminabeBlazor.Component.Core.Models.IListItemModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class TaxCalcTypeListItem : TaxCalcTypeModel, IListItemModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// キー値を取得または設定します。
        /// </summary>
        public string Key => ((int)this.TaxCalcType).ToString();

        /// <summary>
        /// 表示値を取得または設定します。
        /// </summary>
        public string Value => this.TaxCalcTypeName;

        #endregion
    }
}
