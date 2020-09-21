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
using YaminabeBlazor.Component.Settings;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントに対するアイテム削除コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class EditableItemDeleteButtonComponent : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="IEditableItemEditSetting"/> を注入します。
        /// </summary>
        [Inject]
        private IEditableItemDeleteSetting Settings { get; set; }

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public IEditableViewModel Item { get; set; }

        /// <summary>
        /// デフォルトの子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment DefaultContent { get; set; }

        /// <summary>
        /// 削除状態の子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment DeleteContent { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定後初期化処理を行います。
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // ローカル指定がなくグローバル設定が存在する場合は上書き
            this.DefaultContent = this.DefaultContent ?? this.Settings.DefaultContent;
            this.DeleteContent = this.DeleteContent ?? this.Settings.DeleteContent;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// アイテムリストにアイテムを削除します。
        /// </summary>
        /// <remarks>
        /// 新規追加データ行以外は論理削除。
        /// </remarks>
        private void DeleteItem()
        {
            if (this.Item.EditState.HasFlag(EditStateOptions.Added))
            {
                // 新規追加データ行は物理削除
                this.Item.Remove();
            }
            else
            {
                // 既存データ行は論理削除
                this.Item.SetDelete();
            }
        }

        /// <summary>
        /// アイテムリストにアイテムを復元します。
        /// </summary>
        private void RestoreDeleteIten()
        {
            this.Item.ReSetDelete();
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 削除ボタン押下時の処理を行います。
        /// </summary>
        private void OnDeleteClick()
        {
            this.DeleteItem();
        }

        /// <summary>
        /// 削除キャンセルボタン押下時の処理を行います。
        /// </summary>
        private void OnDeleteCancelClick()
        {
            this.RestoreDeleteIten();
        }

        #endregion
    }
}
