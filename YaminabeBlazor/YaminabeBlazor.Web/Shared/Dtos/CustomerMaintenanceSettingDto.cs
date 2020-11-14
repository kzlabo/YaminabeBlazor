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

using System.Collections.Generic;
using YaminabeBlazor.Web.Shared.ListItems;

namespace YaminabeBlazor.Web.Shared.Dtos
{
    /// <summary>
    /// 得意先マスタメンテメンテナンス設定モデルを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomerMaintenanceSettingDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 締日マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<CutoffDateTypeListItem> CutoffDateTypeList { get; set; }

        /// <summary>
        /// 回収日マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<CollectionDateTypeListItem> CollectionDateTypeList { get; set; }

        /// <summary>
        /// 消費税区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<TaxTypeListItem> TaxTypeList { get; set; }

        /// <summary>
        /// 消費税計算区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<TaxCalcTypeListItem> TaxCalcTypeList { get; set; }

        /// <summary>
        /// 消費税端数処理区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<TaxRoundTypeListItem> TaxRoundTypeList { get; set; }

        #endregion
    }
}
