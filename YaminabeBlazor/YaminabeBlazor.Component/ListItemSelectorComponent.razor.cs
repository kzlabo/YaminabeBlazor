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
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// リスト選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TListItem">リストアイテムの型。</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <seealso cref="YaminabeBlazor.Component.IFilterComponent" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class ListItemSelectorComponent<TListItem> : ComponentBase, IFilterComponent
        where TListItem : IListItemModel, new()
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 表示リストを取得または設定します。
        /// </summary>
        [Parameter]
        public ListItemViewCollectionModel<TListItem> ListItems { get; set; } = null;


        /// <summary>
        /// 列グループテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment TableColgroup { get; set; }

        /// <summary>
        /// ヘッダテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment TableHeader { get; set; }

        /// <summary>
        /// 行テンプレート（読取）を取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment<TListItem> RowTemplate { get; set; }

        /// <summary>
        /// フィルタ条件リストを取得または設定します。
        /// </summary>
        public List<FilterConditionComponent> FilterConditions { get; set; } = new List<FilterConditionComponent>();

        /// <summary>
        /// フィルタ条件値リストを取得または設定します。
        /// </summary>
        public Dictionary<string, (string FilterText, bool PartialMatch)> FilterConditionValues { get; set; } = new Dictionary<string, (string FilterText, bool PartialMatch)>();

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// フィルタ処理を実行します。
        /// </summary>
        public void FilterOn()
        {
            // フィルタ条件コンポーネントリストから入力されている条件のみフィルタ条件として指定
            var filterConditionValues = new Dictionary<string, (string FilterText, bool PartialMatch)>();
            foreach (var filterCondition in this.FilterConditions)
            {
                if (string.IsNullOrWhiteSpace(filterCondition.FilterText))
                {
                    continue;
                }
                filterConditionValues[filterCondition.PropertyName] = (filterCondition.FilterText, filterCondition.PartialMatch);
            }
            this.FilterConditionValues = filterConditionValues;
            this.ListItems.FilterConditionValues = filterConditionValues;
            this.StateHasChanged();
        }

        /// <summary>
        /// フィルタを無効に設定します。
        /// </summary>
        public void FilterOff()
        {
            this.StateHasChanged();
        }

        #endregion
    }
}
