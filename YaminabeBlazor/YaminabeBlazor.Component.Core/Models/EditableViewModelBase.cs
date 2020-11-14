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
using System.Text.Json.Serialization;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component.Core.Models
{
    /// <summary>
    /// 基底の編集画面モデルを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableViewModelBase : IEditableViewModel
    {
        #region -------------------- property --------------------

        /// <inheritdoc/>
        public Guid UniqueId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public EditModeOptions EditMode { get; set; } = EditModeOptions.Read;

        /// <inheritdoc/>
        public EditStateOptions EditState { get; set; } = EditStateOptions.UnChanged;

        /// <inheritdoc/>
        public bool IsCurrent { get; set; }

        /// <inheritdoc/>
        public int Index { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public Action StateHasChanged { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public Action<IEditableViewModel> ItemRemoved { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 編集モードを指定したモードに設定します。
        /// </summary>
        /// <param name="editMode">モード。</param>
        private protected void SetEditMode(EditModeOptions editMode)
        {
            this.EditMode = editMode;
            this.StateHasChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void SetEdit()
        {
            this.SetEditMode(EditModeOptions.Edit);
        }

        /// <inheritdoc/>
        public void SetRead()
        {
            this.SetEditMode(EditModeOptions.Read);
        }

        /// <summary>
        /// 編集状態を指定した状態に設定します。
        /// </summary>
        /// <param name="editState">状態。</param>
        private protected void SetEditState(EditStateOptions editState)
        {
            this.EditState |= editState;
            this.StateHasChanged?.Invoke();
        }

        /// <summary>
        /// 編集状態から指定した状態に除きます。
        /// </summary>
        /// <param name="editState">状態。</param>
        private protected void ReSetEditState(EditStateOptions editState)
        {
            this.EditState &= ~editState;
            this.StateHasChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void SetDelete()
        {
            this.SetEditState(EditStateOptions.Deleted);
        }

        /// <inheritdoc/>
        public void ReSetDelete()
        {
            this.ReSetEditState(EditStateOptions.Deleted);
        }

        /// <inheritdoc/>
        public void Remove()
        {
            this.ItemRemoved?.Invoke(this);
        }

        #endregion
    }
}
