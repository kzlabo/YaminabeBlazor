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

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントの基底のコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class EditableComponentBase : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeItem")]
        public EditableViewModelBase Item { get; set; }

        /// <summary>
        /// フィルタ条件文字列を取得または設定します。
        /// </summary>
        [Parameter]
        public string FilterConditionValue { get; set; }

        /// <summary>
        /// 編集可とする状態を取得または設定します。
        /// </summary>
        [Parameter]
        public EditStateOptions EditableState { get; set; } = EditStateOptions.None;

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        #endregion
    }
}
