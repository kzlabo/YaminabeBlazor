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
    /// 締日区分モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.ICutoffDateTypeModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CutoffDateTypeModel : ICutoffDateTypeModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public CutoffDateTypeOptions CutoffDateType { get; }

        /// <inheritdoc/>
        public string CutoffDateTypeName { get; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CutoffDateTypeModel"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="cutoffDateType">締日区分。</param>
        /// <param name="cutoffDateTypeName">締日区分名。</param>
        public CutoffDateTypeModel(
            CutoffDateTypeOptions cutoffDateType,
            string cutoffDateTypeName
            )
        {
            this.CutoffDateType = cutoffDateType;
            this.CutoffDateTypeName = cutoffDateTypeName;
        }

        #endregion
    }
}
