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
using System.Linq;
using YaminabeBlazor.Component.Core;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// レイヤーを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynLayer : ComponentBase, IDisposable
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 非表示状態のクラス名を取得または設定します。
        /// </summary>
        [Parameter]
        public string HideClass { get; set; } = "yn-is-none";

        /// <summary>
        /// レイヤーコンテキストを取得または設定します。
        /// </summary>
        public LayerContext Context { get; } = new LayerContext();

        /// <summary>
        /// コンポーネントのクラス名を取得します。
        /// </summary>
        private string ClassName
        {
            get
            {
                var className = string.Empty;
                if (this.AdditionalAttributes != null)
                {
                    className = this.AdditionalAttributes.ContainsKey("class") ? this.AdditionalAttributes["class"].ToString() : string.Empty;
                }
                className += $" {(this.IsHidden ? this.HideClass: string.Empty)}";
                return className;
            }
        }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// 非表示かそうかの判定を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 子パネルがすべて非表示の場合に非表示と判定する。
        /// </remarks>
        private bool IsHidden
        {
            get
            {
                return this.Context.LayerPanels.Count == this.Context.LayerPanels.Count(layerPanel => layerPanel.Value.Hidden == true);
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 初期化処理を行います。
        /// </summary>
        protected override void OnInitialized()
        {
            this.Context.OnShowChildPanel += OnShowChildPanel;
            this.Context.OnHideChildPanel += OnHideChildPanel;
        }

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public void Dispose()
        {
            this.Context.OnShowChildPanel -= OnShowChildPanel;
            this.Context.OnHideChildPanel -= OnHideChildPanel;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, this.AdditionalAttributes);
            builder.AddAttribute(2, "class", this.ClassName);

            builder.OpenComponent<CascadingValue<LayerContext>>(3);
            builder.AddAttribute(4, "Value", this.Context);
            builder.AddAttribute(5, "Name", "CascadeLayerContext");
            builder.AddAttribute(6, "ChildContent", (RenderFragment)(builder1 =>
            {
                this.ChildContent?.Invoke(builder1);
            }));
            builder.CloseComponent();

            builder.CloseElement();
        }

        /// <summary>
        /// 子パネルの表示時の処理を行います。
        /// </summary>
        /// <param name="context">パネルコンテキスト。</param>
        /// <remarks>
        /// 子パネルの表示イベントが発生したら、発生元以外の子パネルを非表示に変更する。
        /// </remarks>
        private void OnShowChildPanel(LayerPanelContext context)
        {
            foreach (var layerPanel in this.Context.LayerPanels)
            {
                if (layerPanel.Value.Equals(context) == false)
                {
                    layerPanel.Value.Hide(false);
                }
            }

            StateHasChanged();
        }

        /// <summary>
        /// 子パネルの非表示時の処理を行います。
        /// </summary>
        /// <param name="context">パネルコンテキスト。</param>
        private void OnHideChildPanel(LayerPanelContext context)
        {
            StateHasChanged();
        }

        #endregion
    }
}
