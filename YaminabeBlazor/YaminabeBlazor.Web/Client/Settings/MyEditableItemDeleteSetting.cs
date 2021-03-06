﻿/*
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

using Microsoft.AspNetCore.Components;
using YaminabeBlazor.Component.Settings;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Client.Settings
{
    /// <summary>
    /// 編集可能コンポーネントに対するアイテム削除コンポーネントの設定を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class MyEditableItemDeleteSetting : IEditableItemDeleteSetting
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public RenderFragment DefaultContent { get; set; } = builder =>
        {
            builder.AddContent(0, WordResource.Delete);
        };

        /// <inheritdoc/>
        public RenderFragment DeleteContent { get; set; } = builder =>
        {
            builder.AddContent(0, WordResource.Undo);
        };

        #endregion
    }
}
