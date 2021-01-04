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
using System;
using System.Collections.Generic;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 一時入力切替ボタンコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynTemporarySwitch : ComponentBase
    {
        #region -------------------- field --------------------

        public Action OnEdit;
        public Action OnComplete;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        protected IOptions<YaminabeBlazorOptions> OptionsAccessor { get; set; }

        /// <summary>
        /// コンポーネントの設定アクセサを取得します。
        /// </summary>
        /// <remarks>
        /// <c>OptionsAccessor.Value</c> でのアクセスを省略。
        /// </remarks>
        protected YaminabeBlazorOptions Options
        {
            get
            {
                return this.OptionsAccessor.Value;
            }
        }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 編集判定を取得または設定します。
        /// </summary>
        public bool IsEdit { get; set; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.IsEdit == true)
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "type", "button");
                builder.AddMultipleAttributes(2, this.AdditionalAttributes);
                builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    Complete();
                }));
                builder.AddContent(4, this.Options.GetWordResouce(nameof(WordResource.EditGridMode)));
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
                builder.AddContent(4, this.Options.GetWordResouce(nameof(WordResource.AddGridMode)));
                builder.CloseElement();
            }
        }

        /// <summary>
        /// 編集を開始します。
        /// </summary>
        protected virtual void Edit()
        {
            this.IsEdit = true;
            this.OnEdit?.Invoke();
        }

        /// <summary>
        /// 編集を確定します。
        /// </summary>
        protected virtual void Complete()
        {
            this.IsEdit = false;
            this.OnComplete?.Invoke();
        }

        #endregion
    }
}
