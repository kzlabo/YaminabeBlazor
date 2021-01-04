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
using Microsoft.AspNetCore.Components.Rendering;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集データアイテム削除コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynEditableDelete : ynComponentBase
    {
        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.Context != null)
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "type", "button");
                builder.AddMultipleAttributes(2, this.AdditionalAttributes);
                builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    Delete();
                }));
                builder.AddContent(4, this.Options.GetWordResouce(nameof(WordResource.Delete)));
                builder.CloseElement();
            }
        }

        /// <summary>
        /// データアイテムを削除します。
        /// </summary>
        protected virtual void Delete()
        {
            this.Context.OnDelete?.Invoke(this.Context);
            this.Context.StateHasChanged?.Invoke(); ;
        }

        #endregion
    }
}
