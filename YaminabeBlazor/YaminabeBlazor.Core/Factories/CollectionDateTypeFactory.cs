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
    /// <see cref="YaminabeBlazor.Core.Models.CollectionDateTypeModel"/> クラスのインスタンスを作成するためのメソッドのセットを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CollectionDateTypeFactory
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 回収日リストのインスタンスを初期化状態で作成します。
        /// </summary>
        /// <returns>
        /// 回収日リストを返却します。
        /// </returns>
        public static IReadOnlyList<CollectionDateTypeModel> CreateInitialData()
        {
            var collectionDateTypes = new List<CollectionDateTypeModel>();
            collectionDateTypes.Add(new CollectionDateTypeModel(
                collectionDateType: CollectionDateTypeOptions.None,
                collectionDateTypeName: "未設定"
                ));
            collectionDateTypes.Add(new CollectionDateTypeModel(
                collectionDateType: CollectionDateTypeOptions.CurrentMonth,
                collectionDateTypeName: "当月"
                ));
            collectionDateTypes.Add(new CollectionDateTypeModel(
                collectionDateType: CollectionDateTypeOptions.NextMonth,
                collectionDateTypeName: "翌月"
                ));
            collectionDateTypes.Add(new CollectionDateTypeModel(
                collectionDateType: CollectionDateTypeOptions.AfterNextMonth,
                collectionDateTypeName: "翌々月"
                ));

            return collectionDateTypes;
        }

        #endregion
    }
}
