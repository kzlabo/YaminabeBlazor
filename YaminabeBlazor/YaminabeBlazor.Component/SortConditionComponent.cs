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
using System.Collections.Generic;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// ソート条件コンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class SortConditionComponent : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// フィルタコンポーネントを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeSortConditions")]
        private List<SortConditionComponent> SorteConditions { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// ソート条件にマッピングするプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string PropertyName { get; set; }

        /// <summary>
        /// ソート条件名を取得または設定します。
        /// </summary>
        [Parameter]
        public string DisplayName { get; set; }

        /// <summary>
        /// ソート種別を取得または設定します。
        /// </summary>
        [Parameter]
        public SortOptions SortOption { get; set; } = SortOptions.None;

        /// <summary>
        /// ソート順を取得または設定します。
        /// </summary>
        [Parameter]
        public int? SortNumber { get; set; } = null;

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// コンポーネントのレンダリング後に処理を行います。
        /// </summary>
        /// <param name="firstRender">初回。</param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            // ソートコンポーネントに自身を追加。
            if (this.SorteConditions != null &&
                this.SorteConditions.Contains(this) == false)
            {
                this.SorteConditions.Add(this);
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// ソート種別を設定します。
        /// </summary>
        /// <param name="option">ソート種別。</param>
        internal void SetOption(SortOptions option)
        {
            this.SortOption = option;
        }

        /// <summary>
        /// ソート順を設定します。
        /// </summary>
        /// <param name="number">ソート順。</param>
        internal void SetNumber(int? number)
        {
            this.SortNumber = number;
        }

        #endregion
    }
}
