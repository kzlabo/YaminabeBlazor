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
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Web.Shared.Models
{
    /// <summary>
    /// ブランドモデルを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class BrandModel : IBrandModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public string BrandId { get; set; }

        /// <inheritdoc/>
        public string BrandName { get; set; }

        /// <inheritdoc/>
        public ProductTagTypeOptions ProductTagType { get; set; }

        /// <inheritdoc/>
        public string Note { get; set; }

        #endregion
    }
}
