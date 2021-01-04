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
using System;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 基底リピーターコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データアイテムの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class ynRepeaterBase<TItem> : ComponentBase, IDisposable
    {
        #region -------------------- field --------------------

        protected bool _shouldLoad = false;

        protected CollectionNavigateContext<TItem> _collectionNavigateContext = new CollectionNavigateContext<TItem>();
        protected List<EditableContext> _editableContexts = new List<EditableContext>();

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
        /// データアイテムリストを取得または設定します。
        /// </summary>
        [Parameter]
        public List<TItem> Items
        {
            get
            {
                return this._collectionNavigateContext.Items;
            }
            set
            {
                if (this.Items == null || this.Items.Equals(value) == false)
                {
                    this._collectionNavigateContext.Items = value;
                    this._shouldLoad = true;
                }
            }
        }

        /// <summary>
        /// ヘッダテンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment Header { get; set; }

        /// <summary>
        /// 行テンプレートを取得または設定します。
        /// </summary>
        /// <remarks>
        /// <see cref="EditTemplate"/> が指定されていない場合は、常に <see cref="RowTemplate"/> が使用されます。
        /// </remarks>
        [Parameter]
        public RenderFragment<EditableContext> RowTemplate { get; set; }

        /// <summary>
        /// 編集行テンプレートを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment<EditableContext> EditTemplate { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// 表示対象データアイテムリストを取得します
        /// </summary>
        public virtual List<EditableContext> ViewEditableContexts
        {
            get
            {
                return this._editableContexts;
            }
        }

        /// <summary>
        /// 追加対象データアイテムリストを取得します。
        /// </summary>
        public HashSet<TItem> AddedItems { get; } = new HashSet<TItem>();

        /// <summary>
        /// 更新対象データアイテムリストを取得します。
        /// </summary>
        public HashSet<TItem> ChangedItems { get; } = new HashSet<TItem>();

        /// <summary>
        /// 削除対象データアイテムリストを取得します。
        /// </summary>
        public HashSet<TItem> DeletedItems { get; } = new HashSet<TItem>();

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public virtual void Dispose()
        {
            foreach (var editableContext in this._editableContexts)
            {
                ClearDelegate(editableContext);
            }
            this._editableContexts.Clear();
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 表示データアイテムリストを読込みます。
        /// </summary>
        protected virtual void LoadEditableViewItems()
        {
            // デリゲートを削除
            foreach (var editableContext in this._editableContexts)
            {
                ClearDelegate(editableContext);
            }
            this._editableContexts.Clear();

            // 操作済みデータアイテムリストを取得
            var navigatedItems = this._collectionNavigateContext.NavigatedItems;
            if (navigatedItems != null)
            {
                foreach (var item in navigatedItems)
                {
                    var editableContext = new EditableContext(EditModeOptions.Read, EditStateOptions.UnChanged, item);

                    SetDelegate(editableContext);

                    this._editableContexts.Add(editableContext);
                }
            }

            this._shouldLoad = false;
        }

        /// <summary>
        /// データアイテムの確定を行います。
        /// </summary>
        /// <param name="context">編集データコンテキスト。</param>
        protected virtual void OnItemComplete(EditableContext context)
        {
            if (context.EditState.HasFlag(EditStateOptions.Added) == true)
            {
                this.AddedItems.Add((TItem)context.Item);
            }
            else
            {
                this.ChangedItems.Add((TItem)context.Item);
            }
        }

        /// <summary>
        /// データアイテムの削除を行います。
        /// </summary>
        /// <param name="context">編集データコンテキスト。</param>
        protected virtual void OnItemDeleted(EditableContext context)
        {
            var item = (TItem)context.Item;

            // 編集データアイテムは論理削除
            // 追加データアイテムは物理削除
            if (this.AddedItems.Contains(item) == false)
            {
                this.DeletedItems.Add(item);
            }
            else
            {
                this.AddedItems.Remove(item);
            }

            // 編集データリスト側は削除
            if (this.ChangedItems.Contains(item) == true)
            {
                this.ChangedItems.Remove(item);
            }

            this._collectionNavigateContext.Remove(item);

            this._shouldLoad = true;
        }

        /// <summary>
        /// デリゲートを登録します。
        /// </summary>
        /// <param name="context">編集データコンテキスト。</param>
        protected virtual void SetDelegate(EditableContext context)
        {
            context.StateHasChanged += this.StateHasChanged;
            context.OnComplete += OnItemComplete;
            context.OnDelete += OnItemDeleted;
        }

        /// <summary>
        /// デリゲートを削除します。
        /// </summary>
        /// <param name="context">編集データコンテキスト。</param>
        protected virtual void ClearDelegate(EditableContext context)
        {
            context.StateHasChanged -= this.StateHasChanged;
            context.OnComplete -= OnItemComplete;
            context.OnDelete -= OnItemDeleted;
        }

        #endregion
    }
}
