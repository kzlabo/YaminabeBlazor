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
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Web.Shared.ListItems;

namespace YaminabeBlazor.Web.Client.Components
{
    /// <summary>
    /// ブランド選択コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class BrandSelector : ComponentBase
    {
        #region -------------------- delegate --------------------

        private Action<BrandListItem> OnSelect;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 選択対象のブランドリストを取得または設定します。
        /// </summary>
        [Parameter]
        public List<BrandListItem> BrandList { get; set; }

        /// <summary>
        /// ブランド選択時のコールバック処理を設定します。
        /// </summary>
        [Parameter]
        public Action<BrandListItem> OnSelectCallBack
        {
            set
            {
                this.OnSelect = value;
            }
        }

        /// <summary>
        /// オプションバーを取得または設定します。
        /// </summary>
        private OptionBar OptionBar { get; set; }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// パネルを表示します。
        /// </summary>
        /// <param name="callback">ブランド選択時コールバック処理。</param>
        public void Show(Action<BrandListItem> callback)
        {
            this.OnSelectCallBack = callback;
            this.OptionBar.Show();
        }

        /// <summary>
        /// ブランドボタン押下時の処理を行います。
        /// </summary>
        /// <param name="item">ブランド編集データコンテキスト。</param>
        private void OnBrandClick(EditableContext context)
        {
            this.OnSelect?.Invoke(context.CastItem<BrandListItem>());
            this.OptionBar.Hide();
        }

        #endregion
    }
}
