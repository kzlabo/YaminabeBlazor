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

namespace YaminabeBlazor.Component.Settings
{
    /// <summary>
    /// 編集可能コンポーネントに対するアイテム編集コンポーネントの設定を表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IEditableItemEditSetting
    {
        #region -------------------- property --------------------

        /// <summary>
        /// <see cref="EditableItemEditButtonComponent"/> の <see cref="EditableItemEditButtonComponent.DefaultContent"/> に対するグローバル指定。
        /// </summary>
        /// <value>
        /// <EditableItemEditButtonComponent>
        ///     <DefaultContent>
        ///         ここに指定するグローバルコンテンツ
        ///     </DefaultContent>
        ///     <EditContent>
        ///         ・・・
        ///     </EditContent>
        /// </EditableItemEditButtonComponent>
        /// </value>
        RenderFragment DefaultContent { get; set; }

        /// <summary>
        /// <see cref="EditableItemEditButtonComponent"/> の <see cref="EditableItemEditButtonComponent.EditContent"/> に対するグローバル指定。
        /// </summary>
        /// <value>
        /// <EditableItemEditButtonComponent>
        ///     <DefaultContent>
        ///         ・・・
        ///     </DefaultContent>
        ///     <EditContent>
        ///         ここに指定するグローバルコンテンツ
        ///     </EditContent>
        /// </EditableItemEditButtonComponent>
        /// </value>
        RenderFragment EditContent { get; set; }

        /// <summary>
        /// <see cref="EditableItemEditButtonComponent"/> の <see cref="EditableItemEditButtonComponent.DefaultText"/> に対するグローバル指定。
        /// </summary>
        string DefaultText { get; set; }

        /// <summary>
        /// <see cref="EditableItemEditButtonComponent"/> の <see cref="EditableItemEditButtonComponent.EditText"/> に対するグローバル指定。
        /// </summary>
        string EditText { get; set; }

        #endregion
    }
}
