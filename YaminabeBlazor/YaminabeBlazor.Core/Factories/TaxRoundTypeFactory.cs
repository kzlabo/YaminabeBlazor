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
    /// <see cref="YaminabeBlazor.Core.Models.TaxRoundTypeModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class TaxRoundTypeFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 消費税端数処理区分リストのインスタンスを初期化状態で作成します。
        /// </summary>
        /// <returns>
        /// 消費税端数処理区分リストを返却します。
        /// </returns>
        public static IReadOnlyList<TaxRoundTypeModel> CreateInitialData()
        {
            var taxRoundTypes = new List<TaxRoundTypeModel>();
            taxRoundTypes.Add(new TaxRoundTypeModel(
                taxRoundType: TaxRoundTypeOptions.None,
                taxRoundTypeName: "未設定"
                ));
            taxRoundTypes.Add(new TaxRoundTypeModel(
                taxRoundType: TaxRoundTypeOptions.Round,
                taxRoundTypeName: "四捨五入"
                ));
            taxRoundTypes.Add(new TaxRoundTypeModel(
                taxRoundType: TaxRoundTypeOptions.Down,
                taxRoundTypeName: "切り捨て"
                ));
            taxRoundTypes.Add(new TaxRoundTypeModel(
                taxRoundType: TaxRoundTypeOptions.Up,
                taxRoundTypeName: "切り上げ"
                ));

            return taxRoundTypes;
        }

        #endregion
    }
}
