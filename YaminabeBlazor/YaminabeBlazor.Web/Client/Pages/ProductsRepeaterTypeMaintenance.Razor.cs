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

using System.Collections.Generic;
using System.Threading.Tasks;
using YaminabeBlazor.Component;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Web.Client.Components;
using YaminabeBlazor.Web.Client.Helps;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Client.Pages
{
    /// <summary>
    /// 商品マスタリストページ（リピーター形式タイプ）を提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Pages.ProductsMaintenanceBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class ProductsRepeaterTypeMaintenance : ProductsMaintenanceBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品マスタ編集リピーターを取得または設定します。
        /// </summary>
        private ynRepeater<ProductInputModel> MasterRepeater { get; set; }

        /// <summary>
        /// ブランド選択レイヤーを取得または設定します。
        /// </summary>
        private BrandSelector BrandSelectorPanel { get; set; }

        /// <summary>
        /// ヘルプレイヤーを取得または設定します。
        /// </summary>
        private ProductsMaintenanceHelp HelpPanel { get; set; }

        /// <summary>
        /// 追加対象商品マスタリストを取得します。
        /// </summary>
        protected override IEnumerable<ProductInputModel> AddedProducts
        {
            get
            {
                return this.MasterRepeater.AddedItems;
            }
        }

        /// <summary>
        /// 更新対象商品マスタリストを取得します。
        /// </summary>
        protected override IEnumerable<ProductInputModel> ChangedProducts
        {
            get
            {
                return this.MasterRepeater.ChangedItems;
            }
        }

        /// <summary>
        /// 削除対象商品マスタリストを取得します。
        /// </summary>

        protected override IEnumerable<ProductInputModel> DeletedProducts
        {
            get
            {
                return this.MasterRepeater.DeletedItems;
            }
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 検索ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnSearchClick()
        {
            this.Products = null;

            await this.SearchProducts();
        }

        /// <summary>
        /// 確定ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnPutClick()
        {
            // 編集モードのリストが存在する場合は処理不可
            if (this.Products.IsEdit() == true)
            {
                this.ApplicationState.AddNotificationMessage(
                    WordResource.CautionInfo,
                    string.Format(MessageResource.CanNotExecute, WordResource.EditMode, WordResource.Commit),
                    NotificationMessageModel.NotiferTypeOptions.Caution
                    );
                return;
            }

            var confirmResult = await this.JavascriptRuntime.Confirm(string.Format(MessageResource.ExecuteConfirm, WordResource.Commit));
            if (confirmResult == true)
            {
                await this.PutProducts();
            }
        }

        /// <summary>
        /// ヘルプボタン押下時の処理を行います。
        /// </summary>
        private void OnHelpClick()
        {
            this.HelpPanel.Show();
        }

        /// <summary>
        /// ブランド選択ボタン押下時の処理を行います。
        /// </summary>
        /// <param name="context">商品編集データコンテキスト。</param>
        protected void OnBrandSelectClick(EditableContext context)
        {
            this.BrandSelectorPanel.Show((BrandListItem brand) =>
            {
                var product = context.CastItem<ProductInputModel>();
                product.SetBrand(brand);
                context.StateHasChanged();
            });
        }

        #endregion
    }
}
