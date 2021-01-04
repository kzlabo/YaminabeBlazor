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
using System;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// レイヤーコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynLayerPanel : ComponentBase, IDisposable
    {
        #region -------------------- property --------------------

        /// <summary>
        /// レイヤーコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeLayerContext")]
        public LayerContext LayerContext { get; set; }

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// パネル名を取得または設定します。
        /// </summary>
        [Parameter]
        public string Name { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// パネルコンテキストを取得または設定します。
        /// </summary>
        public LayerPanelContext Context { get; } = new LayerPanelContext();

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// パネルが非表示状態かどうかを判定します。
        /// </summary>
        public bool Hidden
        {
            get
            {
                return this.Context.Hidden;
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        protected override void OnInitialized()
        {
            this.LayerContext.LayerPanels.Add(this.Name, this.Context);
            this.Context.Show += Show;
            this.Context.Hide += Hide;
        }


        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public void Dispose()
        {
            this.Context.Show -= Show;
            this.Context.Hide -= Hide;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.Hidden == true)
            {
                return;
            }

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);

            this.ChildContent?.Invoke(builder);

            builder.CloseElement();
        }

        /// <summary>
        /// パネルを表示します。
        /// </summary>
        /// <param name="parentEventInvoke">親レイヤー通知フラグ。</param>
        public void Show(bool parentEventInvoke)
        {
            this.Context.Hidden = false;

            if (parentEventInvoke == true)
            {
                this.LayerContext.OnShowChildPanel?.Invoke(this.Context);
            }
        }

        /// <summary>
        /// パネルを表示します。
        /// </summary>
        public void Show()
        {
            Show(true);
        }

        /// <summary>
        /// パネルを非表示にします。
        /// </summary>
        /// <param name="parentEventInvoke">親レイヤー通知フラグ。</param>
        public void Hide(bool parentEventInvoke)
        {
            this.Context.Hidden = true;

            if (parentEventInvoke == true)
            {
                this.LayerContext.OnHideChildPanel?.Invoke(this.Context);
            }
        }

        /// <summary>
        /// パネルを非表示にします。
        /// </summary>
        public void Hide()
        {
            Hide(true);
        }

        #endregion
    }
}
