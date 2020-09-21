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
    /// <see cref="YaminabeBlazor.Core.Models.CutoffDateTypeModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CutoffDateTypeFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 締日リストのインスタンスを初期化状態で作成します。
        /// </summary>
        /// <returns>
        /// 締日リストを返却します。
        /// </returns>
        public static IReadOnlyList<CutoffDateTypeModel> CreateInitialData()
        {
            var cutoffDateTypes = new List<CutoffDateTypeModel>();
            cutoffDateTypes.Add(new CutoffDateTypeModel(
                cutoffDateType: CutoffDateTypeOptions.None,
                cutoffDateTypeName: "未設定"
                ));
            cutoffDateTypes.Add(new CutoffDateTypeModel(
                cutoffDateType: CutoffDateTypeOptions.CurrentMonth,
                cutoffDateTypeName: "当月"
                ));
            cutoffDateTypes.Add(new CutoffDateTypeModel(
                cutoffDateType: CutoffDateTypeOptions.NextMonth,
                cutoffDateTypeName: "翌月"
                ));
            cutoffDateTypes.Add(new CutoffDateTypeModel(
                cutoffDateType: CutoffDateTypeOptions.AfterNextMonth,
                cutoffDateTypeName: "翌々月"
                ));

            return cutoffDateTypes;
        }

        #endregion
    }
}
