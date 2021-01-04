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
using System.Threading.Tasks;
using YaminabeBlazor.Component;
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Web.Client.Helps;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Resources;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Pages
{
    /// <summary>
    /// 商品カテゴリマスタリストページを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Pages.MyPageBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class ProductCategoriesMaintenance : MyPageBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// <see cref="ProductCategoriesWebApiService"/> を注入します。
        /// </summary>
        [Inject]
        private IProductCategoriesApiService ProductCategoriesWebApi { get; set; }

        /// <summary>
        /// 商品カテゴリマスタ編集グリッドを取得または設定します。
        /// </summary>
        private ynGridView<ProductCategoryInputModel> ProductCategoryGridView { get; set; }

        /// <summary>
        /// ヘルプレイヤーを取得または設定します。
        /// </summary>
        private ProductCategoriesMaintenanceHelp HelpPanel { get; set; }

        /// <summary>
        /// 商品カテゴリマスタリストを取得または設定します。
        /// </summary>
        public List<ProductCategoryInputModel> ProductCategories { get; set; } = new List<ProductCategoryInputModel>();

        /// <summary>
        /// 追加対象商品カテゴリマスタリストを取得します。
        /// </summary>
        public IEnumerable<ProductCategoryInputModel> AddedProductCategories
        {
            get
            {
                return this.ProductCategoryGridView.AddedItems;
            }
        }

        /// <summary>
        /// 更新対象商品カテゴリマスタリストを取得します。
        /// </summary>
        public IEnumerable<ProductCategoryInputModel> ChangedProductCategories
        {
            get
            {
                return this.ProductCategoryGridView.ChangedItems;
            }
        }

        /// <summary>
        /// 削除対象商品カテゴリマスタリストを取得します。
        /// </summary>

        public IEnumerable<ProductCategoryInputModel> DeletedProductCategories
        {
            get
            {
                return this.ProductCategoryGridView.DeletedItems;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 初期設定を行います。
        /// </summary>
        /// <returns>
        /// 初期設定に成功した場合は <c>true</c> 。初期設定に失敗した場合は <c>false</c>。
        /// </returns>
        protected async override Task<bool> InitializeSetting()
        {
            await this.SearchProductCategories();

            this.StateHasChanged();

            return true;
        }

        /// <summary>
        /// 商品カテゴリマスタリストを検索します。
        /// </summary>
        private async Task SearchProductCategories()
        {
            var result = await this.ProductCategoriesWebApi.Get();
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return;
            }
            this.ProductCategories = result.ProductCategories;
        }

        /// <summary>
        /// 商品カテゴリマスタリストを更新します。
        /// </summary>
        private async Task PutProductCategories()
        {
            var putResut = await this.ProductCategoriesWebApi.Put(this.AddedProductCategories, this.ChangedProductCategories, this.DeletedProductCategories);
            if (putResut.IsSuccessStatusCode() == false)
            {
                return;
            }
            await this.SearchProductCategories();

            this.ApplicationState.AddNotificationMessage(WordResource.CompleteInfo, string.Format(MessageResource.Complete, WordResource.Commit));

            StateHasChanged();
        }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// 検索ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnSearchClick()
        {
            this.ProductCategories = null;

            await this.SearchProductCategories();
        }

        /// <summary>
        /// 確定ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnPutClick()
        {
            // 編集モードのリストが存在する場合は処理不可
            if (this.ProductCategories.IsEdit() == true)
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
                await this.PutProductCategories();
            }
        }

        /// <summary>
        /// ヘルプボタン押下時の処理を行います。
        /// </summary>
        private void OnHelpClick()
        {
            this.HelpPanel.Show();
        }

        #endregion
    }
}
