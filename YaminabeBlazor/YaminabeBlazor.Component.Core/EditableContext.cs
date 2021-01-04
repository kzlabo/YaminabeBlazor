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
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component.Core
{
    /// <summary>
    /// 編集可能データコンテキストを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableContext
    {
        #region -------------------- delegate --------------------

        public Action StateHasChanged;
        public Action<EditableContext> OnEdit;
        public Action<EditableContext> OnComplete;
        public Action<EditableContext> OnDelete;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 編集モードオプションを取得または設定します。
        /// </summary>
        public EditModeOptions EditModeOption { get; set; }

        /// <summary>
        /// 編集状態を取得または設定します。
        /// </summary>
        public EditStateOptions EditState { get; set; }

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        public object Item { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="EditableContext"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="editModeOption">編集モード。</param>
        /// <param name="editStateOption">編集状態。</param>
        /// <param name="item">データアイテム。</param>
        public EditableContext(
            EditModeOptions editModeOption = EditModeOptions.Read,
            EditStateOptions editStateOption = EditStateOptions.UnChanged,
            object item = default
            )
        {
            this.EditModeOption = editModeOption;
            this.EditState = editStateOption;
            this.Item = item;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// データアイテムをキャストして取得します。
        /// </summary>
        /// <typeparam name="TItem">データの型。</typeparam>
        /// <returns>
        /// データアイテムをキャストして返却します。
        /// </returns>
        public TItem CastItem<TItem>() where TItem : class
        {
            return this.Item as TItem;
        }

        #endregion
    }
}
