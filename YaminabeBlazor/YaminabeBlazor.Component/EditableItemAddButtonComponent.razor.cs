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
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントに対するアイテム追加コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データ行の型。<</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class EditableItemAddButtonComponent<TItem> 
        where TItem : EditableViewModelBase, new()
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public EditableViewCollectionModel<TItem> Items { get; set; }

        /// <summary>
        /// 子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// アイテム追加ボタンの使用不可状態を取得または設定します。
        /// </summary>
        /// <returns>
        /// アイテム追加ボタンが使用不可な場合は <c>true</c> 。使用可の場合は <c>false</c> を返却します。
        /// </returns>
        /// <remarks>
        /// 原則としてデータリストのインスタンスが作成されていない場合は使用不可とする。
        /// </remarks>
        private bool IsDisabled()
        {
            return this.Items == null;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// アイテムリストに新規アイテムを追加します。
        /// </summary>
        public void AddNewItem()
        {
            this.Items.AddNewItem();
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 追加ボタン押下時の処理を行います。
        /// </summary>
        private void OnAddItemClick()
        {
            this.AddNewItem();
        }

        #endregion
    }
}
