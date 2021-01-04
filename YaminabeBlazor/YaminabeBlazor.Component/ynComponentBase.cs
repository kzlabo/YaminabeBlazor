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
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// YaminabeComponent の基底コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynComponentBase : ComponentBase
    {
        #region -------------------- field --------------------

        protected object _item;

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
        /// データコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeEditableContext")]
        public EditableContext Context { get; set; }

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        [Parameter]
        public object Item
        {
            get
            {
                if (this.Context == null)
                {
                    return this._item;
                }
                return this.Context.Item;
            }
            set
            {
                this._item = value;
            }
        }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        #endregion
    }
}
