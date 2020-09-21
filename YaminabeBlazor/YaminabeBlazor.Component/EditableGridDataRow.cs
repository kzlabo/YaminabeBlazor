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
using System.Collections.Generic;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能グリッドコンポーネントのデータ行コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableGridDataRow : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 行クラスを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "RowClass")]
        public string RowClass { get; set; }

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// 行タグを取得または設定します。
        /// </summary>
        [Parameter]
        public string RowTag { get; set; } = "tr";

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// コンポーネントのクラス名を取得します。
        /// </summary>
        public virtual string ClassName
        {
            get
            {
                var className = string.Empty;
                if (this.AdditionalAttributes != null)
                {
                    className = this.AdditionalAttributes.ContainsKey("class") ? this.AdditionalAttributes["class"].ToString() : string.Empty;
                }
                className += $" {this.RowClass}";
                return className;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, this.RowTag);
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.AddAttribute(2, "class", this.ClassName);
            builder.AddContent(3, this.ChildContent);
            builder.CloseElement();
        }

        #endregion
    }
}
