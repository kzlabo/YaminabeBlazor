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
    /// データアイテムのコンテナを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/11/15" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ItemContainer : ComponentBase
    {
        #region -------------------- field --------------------

        private bool _first = true;
        private bool _shouldRender = false;
        private EditableContext _editableContext;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 子要素を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// データコンテキストを取得または設定します。
        /// </summary>
        /// <remarks>
        /// コンテナに設定されたデータコンテキストの参照が変更された場合
        /// <list type="bullet">
        ///     <item>デリゲートを削除。</item>
        ///     <item>再レンダリングの為にレンダリング要否フラグに <c>true</c> を設定。</item>
        /// </list>
        /// </remarks>
        [Parameter]
        public EditableContext Context
        {
            get
            {
                return this._editableContext;
            }
            set
            {
                if (this._editableContext != null && ReferenceEquals(this._editableContext, value) == false)
                {
                    this._shouldRender = true;
                    this._editableContext.StateHasChanged -= this.ItemStateHanChanged;
                }
                this._editableContext = value;
                this._editableContext.StateHasChanged += this.ItemStateHanChanged;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// レンダリングの要否を返却します。
        /// </summary>
        /// <returns>
        /// レンダリングが必要な場合 <c>true</c> 。レンダリングが不要な場合は <c>false</c> 。
        /// </returns>
        protected override bool ShouldRender()
        {
            return this._first || this._shouldRender;
        }

        /// <summary>
        /// コンポーネントのレンダリング後に処理を行います。
        /// </summary>
        /// <param name="firstRender">初回。</param>
        /// <remarks>
        /// データアイテムが更新されるまで再レンダリングが発生しないように
        /// レンダリング要否フラグを <c>false</c> に設定します。
        /// </remarks>
        protected override void OnAfterRender(bool firstRender)
        {
            this._first = false;
            this._shouldRender = false;
        }

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<EditableContext>>(0);
            builder.AddAttribute(1, "Value", this.Context);
            builder.AddAttribute(2, "Name", "CascadeEditableContext");
            builder.AddAttribute(3, "ChildContent", (RenderFragment)(builder1 =>
            {
                this.ChildContent?.Invoke(builder1);
            }));
            builder.CloseComponent();
        }

        /// <summary>
        /// データアイテムの変更時の処理を行います。
        /// </summary>
        /// <remarks>
        /// データアイテムの変更を検知した場合、再レンダリングを行うために
        /// レンダリング要否フラグを <c>true</c> に設定します。
        /// </remarks>
        private void ItemStateHanChanged()
        {
            this._shouldRender = true;
        }

        #endregion
    }
}
