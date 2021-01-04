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
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// グリッドコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TItem">データアイテムの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynGridView<TItem> : ynGridViewBase<TItem>
        where TItem : class
    {
        #region -------------------- field --------------------

        protected CollectionNavigateContext<TItem> _temporaryCollectionNavigateContext = new CollectionNavigateContext<TItem>();
        protected List<EditableContext> _temporaryContexts = new List<EditableContext>();

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// ページング種別を取得または設定します。
        /// </summary>
        [Parameter]
        public PagingTypeOptions PagingType
        {
            get
            {
                return this._collectionNavigateContext.PagingContext.PagingType;
            }
            set
            {
                this._collectionNavigateContext.PagingContext.PagingType = value;

            }
        }

        /// <summary>
        /// ページ位置を取得または設定します。
        /// </summary>
        [Parameter]
        public int PageIndex
        {
            get
            {
                return this._collectionNavigateContext.PagingContext.PageIndex;
            }
            set
            {
                this._collectionNavigateContext.PagingContext.PageIndex = value;
            }
        }

        /// <summary>
        /// 1ページ内のデータ数を取得または設定します。
        /// </summary>
        [Parameter]
        public int PageRowSize
        {
            get
            {
                return this._collectionNavigateContext.PagingContext.PageRowSize;
            }
            set
            {
                this._collectionNavigateContext.PagingContext.PageRowSize = value;
            }
        }

        /// <summary>
        /// ページグループ位置を取得まはた設定します。
        /// </summary>
        [Parameter]
        public int PageGroupIndex
        {
            get
            {
                return this._collectionNavigateContext.PagingContext.PageGroupIndex;
            }
            set
            {
                this._collectionNavigateContext.PagingContext.PageGroupIndex = value;
            }
        }

        /// <summary>
        /// ページグループのサイズを取得または設定します。
        /// </summary>
        [Parameter]
        public int PageGroupSize
        {
            get
            {
                return this._collectionNavigateContext.PagingContext.PageGroupSize;
            }
            set
            {
                this._collectionNavigateContext.PagingContext.PageGroupSize = value;
            }
        }

        /// <summary>
        /// 一時領域のサイズを取得または設定します。
        /// </summary>
        [Parameter]
        public int TemporarySize { get; set; } = 100;

        /// <summary>
        /// 表示対象データアイテムリストを取得します。
        /// </summary>
        /// <remarks>]
        /// 一時領域の編集状態の場合は、一時領域用のデータアイテムリストを返却します。
        /// </remarks>
        public override List<EditableContext> ViewEditableContexts
        {
            get
            {
                if (this.IsTemporary == true)
                {
                    return this._temporaryContexts;
                }
                else
                {
                    return this._editableContexts;
                }
            }
        }

        /// <summary>
        /// 追加入力モードテキストを取得します。
        /// </summary>
        private string AddGridModeText
        {
            get
            {
                return this.Options.GetWordResouce(nameof(WordResource.AddGridMode));
            }
        }

        /// <summary>
        /// 編集入力モードテキストを取得します。
        /// </summary>
        private string EditGridModeText
        {
            get
            {
                return this.Options.GetWordResouce(nameof(WordResource.EditGridMode));
            }
        }

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// 仮想化表示の使用有無を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 仮想化表示を使用する場合は <c>true</c> 。仮想化表示を使用しない場合は <c>false</c> 。
        /// 仮想化表示を使用することで表示が高速化される反面、画面スクロール時に再描画が行われるためにチラツキが発生します。
        /// </remarks>
        [Parameter]
        public bool IsVirtualize { get; set; } = false;

        /// <summary>
        /// グリッドコマンドの使用許可を取得または設定します。
        /// </summary>
        /// <remarks>
        /// グリッドコマンドを使用する場合は <c>true</c> 。グリッドコマンドを使用しない場合は <c>false</c> 。
        /// </remarks>
        [Parameter]
        public bool AllowCommand { get; set; } = true;

        /// <summary>
        /// 一時領域の編集状態を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 一時領域の編集状態の場合は <c>true</c> 。一時領域の編集状態ではない場合は <c>false</c> 。
        /// </remarks>
        public bool IsTemporary { get; set; } = false;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="ynGridView"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ynGridView()
        {
            this._collectionNavigateContext.OnNavigate += OnNavigate;
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// 解放処理を行います。
        /// </summary>
        public override void Dispose()
        {
            this._collectionNavigateContext.OnNavigate -= OnNavigate;

            base.Dispose();
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 表示データアイテムリストの再読み込みが必要な場合は再読み込み
            if (this._shouldLoad == true)
            {
                LoadEditableViewItems();
            }

            // レンダリングツリー作成

            // グリッドコマンド
            if (this.AllowCommand == true)
            {
                builder.OpenElement(1000, "div");
                builder.AddAttribute(1001, "class", "yn-grid-command");

                builder.OpenElement(1002, "ul");
                builder.AddAttribute(1003, "class", "yn-toggle");

                builder.OpenElement(1004, "li");
                builder.OpenElement(1005, "button");
                builder.AddAttribute(1006, "type", "button");
                builder.AddAttribute(1007, "data-yn-selected", this.IsTemporary == false);
                builder.AddAttribute(1008, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    OnEditMode();
                }));
                builder.AddContent(1009, this.EditGridModeText);
                builder.CloseElement();
                builder.CloseElement();

                builder.OpenElement(1010, "li");
                builder.OpenElement(1011, "button");
                builder.AddAttribute(1012, "type", "button");
                builder.AddAttribute(1013, "data-yn-selected", this.IsTemporary == true);
                builder.AddAttribute(1014, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    OnAddMode();
                }));
                builder.AddContent(1015, this.AddGridModeText);
                builder.CloseElement();
                builder.CloseElement();

                builder.CloseElement();

                // ページネーション
                builder.OpenComponent<ynPageNation>(1003);
                builder.AddAttribute(1004, "Context", this._collectionNavigateContext.PagingContext);
                builder.CloseComponent();

                builder.CloseElement();
            }

            builder.OpenElement(2000, "table");
            builder.AddMultipleAttributes(2001, this.AdditionalAttributes);

            if (this.Colgroups != null)
            {
                builder.AddContent(2100, this.Colgroups);
            }

            if (this.Header != null)
            {
                builder.OpenElement(2200, "thead");
                builder.OpenComponent<SortContainer>(2201);
                builder.AddAttribute(2202, "Context", this._collectionNavigateContext.SortContext);
                builder.AddAttribute(2203, "ChildContent", (RenderFragment)(builder1 =>
                {
                    builder1.OpenComponent<FilterContainer>(2204);
                    builder1.AddAttribute(2205, "Context", this._collectionNavigateContext.FilterContext);
                    builder1.AddAttribute(2206, "ChildContent", (RenderFragment)(builder2 =>
                    {
                        builder2.OpenComponent<CascadingValue<Type>>(2207);
                        builder2.AddAttribute(2208, "Value", typeof(TItem));
                        builder2.AddAttribute(2209, "Name", "CascadeItemType");
                        builder2.AddAttribute(2210, "ChildContent", (RenderFragment)(builder3 =>
                        {
                            this.Header.Invoke(builder3);
                        }));
                        builder2.CloseComponent();
                        //builder2.AddContent(2207, this.Header);
                    }));
                    builder1.CloseComponent();

                }));
                builder.CloseComponent();
                builder.CloseElement();
            }

            if (this.ViewEditableContexts.Count == 0)
            {
                builder.OpenElement(2300, "tbody");
                builder.AddContent(2301, "");
                builder.CloseElement();
            }
            else
            {
                builder.OpenElement(2300, "tbody");

                var itemIndex = 100000;

                if (this.IsVirtualize == true)
                {
                    builder.OpenRegion(itemIndex);

                    builder.OpenComponent<Virtualize<EditableContext>>(0);
                    builder.AddAttribute(1, "Items", this.ViewEditableContexts);
                    builder.AddAttribute(2, "ChildContent", (RenderFragment<EditableContext>)(context => (builder2) =>
                    {
                        builder2.OpenComponent<ItemContainer>(0);
                        builder2.AddAttribute(1, "Context", context);
                        builder2.AddAttribute(2, "ChildContent", (RenderFragment)(builder3 =>
                        {
                            builder3.OpenComponent<FilterContainer>(0);
                            builder3.AddAttribute(1, "Context", this._collectionNavigateContext.FilterContext);
                            builder3.AddAttribute(2, "ChildContent", (RenderFragment)(builder4 =>
                            {
                                if (this.EditTemplate != null && context.EditModeOption == EditModeOptions.Edit)
                                {
                                    builder4.AddContent(3, this.EditTemplate(context));
                                }
                                else
                                {
                                    builder4.AddContent(3, this.RowTemplate(context));
                                }
                            }));
                            builder3.CloseComponent();
                        }));
                        builder2.CloseComponent();
                    }));
                    builder.CloseComponent();

                    builder.CloseRegion();
                }
                else
                {
                    foreach (var editableContext in this.ViewEditableContexts)
                    {
                        builder.OpenRegion(itemIndex);

                        builder.OpenComponent<ItemContainer>(0);
                        builder.AddAttribute(1, "Context", editableContext);
                        builder.AddAttribute(2, "ChildContent", (RenderFragment)(builder2 =>
                        {
                            builder2.OpenComponent<FilterContainer>(0);
                            builder2.AddAttribute(1, "Context", this._collectionNavigateContext.FilterContext);
                            builder2.AddAttribute(2, "ChildContent", (RenderFragment)(builder3 =>
                            {
                                if (this.EditTemplate != null && editableContext.EditModeOption == EditModeOptions.Edit)
                                {
                                    builder3.AddContent(3, this.EditTemplate(editableContext));
                                }
                                else
                                {
                                    builder3.AddContent(3, this.RowTemplate(editableContext));
                                }
                            }));
                            builder2.CloseComponent();
                        }));
                        builder.CloseComponent();

                        builder.CloseRegion();

                        itemIndex++;
                    }
                }

                builder.CloseElement();
            }
            builder.CloseElement();
        }

        /// <inheritdoc/>
        protected override void OnItemComplete(EditableContext context)
        {
            base.OnItemComplete(context);

            if (this.IsTemporary == true)
            {
                ClearDelegate(context);
                this._temporaryContexts.Remove(context);
                this._collectionNavigateContext.Add((TItem)context.Item);

                SetTemporary();

                this._shouldLoad = true;

                StateHasChanged();
            }
        }

        /// <summary>
        /// データアイテムリストの操作発生時の処理を行います。
        /// </summary>
        /// <param name="eventArgs">コレクション操作イベント引数。</param>
        private void OnNavigate(CollectionNavigateEventArgs eventArgs)
        {
            this._shouldLoad = true;

            this.StateHasChanged();
        }

        /// <summary>
        /// 追加入力モードに移行します。
        /// </summary>
        private void OnAddMode()
        {
            SetTemporary();

            this.IsTemporary = true;
        }

        /// <summary>
        /// 編集モードに移行します。
        /// </summary>
        private void OnEditMode()
        {
            this.IsTemporary = false;
        }

        /// <summary>
        /// 一時領域にデータアイテムコンテキストを設定します。
        /// </summary>
        private void SetTemporary()
        {
            for (var i = this._temporaryContexts.Count; i <= this.TemporarySize; i++)
            {
                var editableContext = new EditableContext(EditModeOptions.Edit, EditStateOptions.Added | EditStateOptions.UnChanged, (TItem)Activator.CreateInstance(typeof(TItem)));

                SetDelegate(editableContext);

                this._temporaryContexts.Add(editableContext);
            }
        }

        #endregion
    }
}
