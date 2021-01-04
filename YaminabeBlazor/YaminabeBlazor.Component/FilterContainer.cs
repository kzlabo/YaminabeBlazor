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
using YaminabeBlazor.Component.Core;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// フィルタ条件のコンテナを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class FilterContainer : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// フィルタコンテキストを取得または設定します。
        /// </summary>
        [Parameter]
        public FilterContext Context { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<FilterContext>>(0);
            builder.AddAttribute(1, "Value", this.Context);
            builder.AddAttribute(2, "Name", "CascadeFilterContext");
            builder.AddAttribute(3, "ChildContent", (RenderFragment)(builder1 =>
            {
                this.ChildContent?.Invoke(builder1);
            }));
            builder.CloseComponent();
        }

        #endregion
    }
}
