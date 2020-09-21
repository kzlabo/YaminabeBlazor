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
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Core.Factories
{
    /// <summary>
    /// <see cref="YaminabeBlazor.Core.Models.TaxCalcTypeModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class TaxCalcTypeFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 消費税計算区分リストのインスタンスを初期化状態で作成します。
        /// </summary>
        /// <returns>
        /// 消費税計算区分リストを返却します。
        /// </returns>
        public static IReadOnlyList<TaxCalcTypeModel> CreateInitialData()
        {
            var taxCalcTypes = new List<TaxCalcTypeModel>();
            taxCalcTypes.Add(new TaxCalcTypeModel(
                taxCalcType: TaxCalcTypeOptions.None,
                taxCalcTypeName: "未設定"
                ));
            taxCalcTypes.Add(new TaxCalcTypeModel(
                taxCalcType: TaxCalcTypeOptions.Detail,
                taxCalcTypeName: "明細"
                ));
            taxCalcTypes.Add(new TaxCalcTypeModel(
                taxCalcType: TaxCalcTypeOptions.Total,
                taxCalcTypeName: "合計"
                ));

            return taxCalcTypes;
        }

        #endregion
    }
}
