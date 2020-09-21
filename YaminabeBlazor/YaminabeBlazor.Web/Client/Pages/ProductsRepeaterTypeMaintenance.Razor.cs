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

using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;
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
        #region -------------------- event --------------------

        /// <summary>
        /// 検索ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnSearchClick()
        {
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
        /// 並び替えボタン押下時の処理を行います。
        /// </summary>
        private void OnSortClick()
        {
            this.LayerManager.Show("SortOption");
        }

        /// <summary>
        /// ヘルプボタン押下時の処理を行います。
        /// </summary>
        private void OnHelpClick()
        {
            this.LayerManager.Show("HelpOption");
        }

        /// <summary>
        /// ブランド選択ボタン押下時の処理を行います。
        /// </summary>
        protected void OnBrandSelectClick()
        {
            this.LayerManager.Show("BrandSelectorOption");
        }

        #endregion
    }
}
