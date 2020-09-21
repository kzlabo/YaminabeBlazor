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
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Component.Settings;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントに対するアイテム編集コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class EditableItemEditButtonComponent : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="IEditableItemEditSetting"/> を注入します。
        /// </summary>
        [Inject]
        private IEditableItemEditSetting Settings { get; set; }

        /// <summary>
        /// シングルトン <see cref="IJSRuntime"/> を注入します。
        /// </summary>
        [Inject]
        private IJSRuntime JavascriptRuntime { get; set; }

        /// <summary>
        /// EditContextを取得または設定します。
        /// </summary>
        [CascadingParameter]
        public EditContext EditContext { get; set; }

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
        /// 編集状態の子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment EditContent { get; set; }

        /// <summary>
        /// ボタンテキストの表示有無を取得または設定します。
        /// </summary>
        [Parameter]
        public bool IsTextVisible { get; set; } = false;

        /// <summary>
        /// デフォルトのボタンテキストを取得または設定します。
        /// </summary>
        [Parameter]
        public string DefaultText { get; set; }

        /// <summary>
        /// 編集状態のボタンテキストを取得または設定します。
        /// </summary>
        [Parameter]
        public string EditText { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 検証処理を取得または設定します。
        /// </summary>
        public Action<EditContext, bool> OnValidate { get; set; } 

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        protected override void OnInitialized()
        {
            this.OnValidate = this.Validate;
        }

        /// <summary>
        /// パラメータ設定後初期化処理を行います。
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // ローカル指定がなくグローバル設定が存在する場合は上書き
            this.DefaultContent = this.DefaultContent ?? this.Settings.DefaultContent;
            this.EditContent = this.EditContent ?? this.Settings.EditContent;
            this.DefaultText = this.DefaultText ?? this.Settings.DefaultText;
            this.EditText = this.EditText ?? this.Settings.EditText;
        }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// デフォルトのボタンテキストの有無を判定します。
        /// </summary>
        /// <returns>
        /// デフォルトのボタンテキストが指定されている場合は <c>true</c> 。デフォルトのボタンテキストが指定されていない場合は <c>false</c> 。
        /// </returns>
        private bool IsDefaultText
        {
            get
            {
                return !string.IsNullOrEmpty(this.DefaultText);
            }
        }

        /// <summary>
        /// 編集状態のボタンテキストの有無を判定します。
        /// </summary>
        /// <returns>
        /// 編集状態のボタンテキストが指定されている場合は <c>true</c> 。編集状態のボタンテキストが指定されていない場合は <c>false</c> 。
        /// </returns>
        private bool IsEditText
        {
            get
            {
                return !string.IsNullOrEmpty(this.EditText);
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 編集モードを編集状態に設定します。
        /// </summary>
        private void SetEdit()
        {
            this.Item.SetEdit();
        }

        /// <summary>
        /// 編集モードを読取状態に設定します。
        /// </summary>
        private void SetRead()
        {
            this.Item.SetRead();
        }

        /// <summary>
        /// 検証処理を行います。
        /// </summary>
        private async void Validate(EditContext context, bool result)
        {
            var validationMessage = context.GetValidationMessages().FirstOrDefault();
            if (validationMessage != null)
            {
                await this.JavascriptRuntime.Alert(validationMessage);
            }
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 編集ボタン押下時の処理を行います。
        /// </summary>
        private void OnEditClick()
        {
            this.SetEdit();
        }

        /// <summary>
        /// 完了ボタン押下時の処理を行います。
        /// </summary>
        private void OnCompleteClick()
        {
            if (this.EditContext != null)
            {
                var result = this.EditContext.Validate();
                this.OnValidate?.Invoke(this.EditContext, result);

                if (result == false)
                {
                    return;
                }
            }

            this.SetRead();
        }

        #endregion
    }
}
