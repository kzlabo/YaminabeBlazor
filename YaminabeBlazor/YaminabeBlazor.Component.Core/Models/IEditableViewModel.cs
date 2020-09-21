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

namespace YaminabeBlazor.Component.Core.Models
{
    /// <summary>
    /// 編集画面モデルを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface IEditableViewModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 一意キーを取得します。
        /// </summary>
        Guid UniqueId { get; }

        /// <summary>
        /// 編集モードを取得または設定します。
        /// </summary>
        EditModeOptions EditMode { get; set; }

        /// <summary>
        /// 編集状態を取得または設定します。
        /// </summary>
        EditStateOptions EditState { get; set; }

        /// <summary>
        /// カレントアイテムかどうかの判定を取得または設定します。
        /// </summary>
        bool IsCurrent { get; set; }

        /// <summary>
        /// 行番号を取得または設定します。
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// 状態変更アクションを取得または設定します。
        /// </summary>
        Action StateHasChanged { get; set; }

        /// <summary>
        /// 削除アクションを取得または設定します。
        /// </summary>
        Action<IEditableViewModel> ItemRemoved { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 編集モードを編集に設定します。
        /// </summary>
        void SetEdit();

        /// <summary>
        /// 編集モードを読取に設定します。
        /// </summary>
        void SetRead();

        /// <summary>
        /// 編集状態を削除に設定します。
        /// </summary>
        void SetDelete();

        /// <summary>
        /// 編集状態から削除を除きます。
        /// </summary>
        void ReSetDelete();

        /// <summary>
        /// アイテムを削除します。
        /// </summary>
        void Remove();

        #endregion
    }
}
