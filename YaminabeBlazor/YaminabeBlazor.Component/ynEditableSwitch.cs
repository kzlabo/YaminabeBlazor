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
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集状態切替ボタンコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynEditableSwitch : ynComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 検証結果通知方法を取得または設定します。
        /// </summary>
        [Inject]
        private IValidationNotifier ValidationNotifier { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.Context == null || this.Context.EditModeOption == EditModeOptions.Edit)
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "type", "button");
                builder.AddMultipleAttributes(2, this.AdditionalAttributes);
                builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    Complete();
                }));
                builder.AddContent(4, this.Options.GetWordResouce(nameof(WordResource.Complete)));
                builder.CloseElement();
            }
            else
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "type", "button");
                builder.AddMultipleAttributes(2, this.AdditionalAttributes);
                builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    Edit();
                }));
                builder.AddContent(4, this.Options.GetWordResouce(nameof(WordResource.Edit)));
                builder.CloseElement();
            }
        }

        /// <summary>
        /// 編集を開始します。
        /// </summary>
        protected virtual void Edit()
        {
            this.Context.EditModeOption = EditModeOptions.Edit;
            this.Context.OnEdit?.Invoke(this.Context);
            this.Context.StateHasChanged?.Invoke();
        }

        /// <summary>
        /// 編集を確定します。
        /// </summary>
        protected virtual void Complete()
        {
            var editContext = new EditContext(this.Context.Item);
            editContext.AddDataAnnotationsValidation();

            var result = editContext.Validate();
            if (result == false)
            {
                this.ValidationNotifier.ValidationErrorNotice(editContext.GetValidationMessages().First());
                return;
            }

            this.Context.EditModeOption = EditModeOptions.Read;
            this.Context.OnComplete?.Invoke(this.Context);
            this.Context.StateHasChanged?.Invoke(); ;
        }

        #endregion
    }
}
