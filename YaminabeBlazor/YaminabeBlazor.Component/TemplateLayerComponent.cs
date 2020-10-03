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
using Microsoft.Extensions.Options;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// テンプレートレイヤーコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Component.LayerComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class TemplateLayerComponent : LayerComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        private IOptions<YaminabeBlazorOptions> OptionsAccesor { get; set; }

        /// <summary>
        /// 子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// 閉じるボタンテキストを取得します。
        /// </summary>
        private string CloseText
        {
            get
            {
                return this.OptionsAccesor.Value.GetWordResouce(nameof(WordResource.Close));
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
            if (this.IsVisible == true)
            {
                builder.OpenElement(0, "div");
                builder.AddMultipleAttributes(1, this.AdditionalAttributes);
                builder.AddAttribute(2, "class", $"layer-component layer-content {this.ClassName}");

                builder.OpenRegion(1000);
                this.BuildRenderTreeHideAction(builder);
                builder.CloseRegion();

                builder.OpenElement(3, "div");
                builder.AddContent(4, ChildContent);
                builder.CloseElement();
                builder.CloseElement();
            }
        }

        /// <summary>
        /// 閉じるアクションコンテンツのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected virtual void BuildRenderTreeHideAction(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.OpenElement(1, "button");
            builder.AddAttribute(2, "type", "button");
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, OnHideClick));
            builder.AddContent(4, this.CloseText);
            builder.CloseElement();
            builder.CloseElement();
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// ×ボタン押下時の処理を行います。
        /// </summary>
        protected void OnHideClick()
        {
            this.Hide();
        }

        #endregion
    }
}
