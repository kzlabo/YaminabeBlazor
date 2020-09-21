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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 基底の編集可能繰り返しコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データ行の型。</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="YaminabeBlazor.Component.IFilterComponent" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class EditableRepeaterComponentBase<TItem> : ComponentBase, IFilterComponent, IDisposable
        where TItem : IEditableViewModel, new()
    {
        #region -------------------- property --------------------

        /// <summary>
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public EditableViewCollectionModel<TItem> Items { get; set; }

        /// <summary>
        /// ヘッダーテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// 基本行データテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment<TItem> RowTemplate { get; set; }

        /// <summary>
        /// 編集行データテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment<TItem> EditTemplate { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 編集中のクラスを取得または設定します。
        /// </summary>
        [Parameter]
        public string EditItemClass { get; set; }

        /// <summary>
        /// 削除済みのクラスを取得または設定します。
        /// </summary>
        [Parameter]
        public string DeletedItemClass { get; set; }

        /// <summary>
        /// フィルタ条件リストを取得または設定します。
        /// </summary>
        public List<FilterConditionComponent> FilterConditions { get; set; } = new List<FilterConditionComponent>();

        /// <summary>
        /// フィルタ条件値リストを取得または設定します。
        /// </summary>
        public Dictionary<string, (string FilterText, bool PartialMatch)> FilterConditionValues { get; set; } = new Dictionary<string, (string FilterText, bool PartialMatch)>();

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定を行います。
        /// </summary>
        /// <param name="parameters">パラメータリスト。</param>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (this.Items != null)
            {
                this.Items.StateHasChanged += this.StateHasChanged;
            }

            return base.SetParametersAsync(parameters);
        }

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public void Dispose()
        {
            if (this.Items != null)
            {
                this.Items.StateHasChanged -= this.StateHasChanged;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 編集状態に応じた行クラスを取得します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        /// <returns>
        /// 編集中の場合は編集中の行クラスを返却します、その他の場合はnulを返却します。
        /// </returns>
        public string GetRowClass(TItem item)
        {
            if (this.DeletedItemClass != null && item.EditState.HasFlag(EditStateOptions.Deleted))
            {
                return this.DeletedItemClass;
            }
            return item.EditMode == EditModeOptions.Edit ? this.EditItemClass : null;
        }

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
            this.Items.FilterConditionValues = filterConditionValues;
            this.StateHasChanged();
        }

        /// <summary>
        /// フィルタを無効に設定します。
        /// </summary>
        public void FilterOff()
        {
            this.Items.FilterConditionValues = null;
            this.StateHasChanged();
        }

        #endregion
    }
}
