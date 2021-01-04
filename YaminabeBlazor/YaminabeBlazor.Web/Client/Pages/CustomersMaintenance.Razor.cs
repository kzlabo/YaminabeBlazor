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
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Resources;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Pages
{
    /// <summary>
    /// 得意先マスタリストページを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Pages.MyPageBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class CustomersMaintenance : MyPageBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// <see cref="CustomersApiService"/> を注入します。
        /// </summary>
        [Inject]
        private ICustomersApiService CustomersWebApi { get; set; }

        /// <summary>
        /// <see cref="CustomerMaintenanceSettingApiService"/> を注入します。
        /// </summary>
        [Inject]
        protected ICustomerMaintenanceSettingApiService CustomerMaintenanceSettingWebApi { get; set; }

        /// <summary>
        /// 得意先マスタ編集グリッドを取得または設定します。
        /// </summary>
        private ynGridView<CustomerInputModel> MasterGridView { get; set; }

        /// <summary>
        /// ヘルプレイヤーを取得または設定します。
        /// </summary>
        private CustomersMaintenanceHelp HelpPanel { get; set; }

        /// <summary>
        /// 得意先マスタリストを取得または設定します。
        /// </summary>
        public List<CustomerInputModel> Customers { get; set; } = new List<CustomerInputModel>();

        /// <summary>
        /// 締日マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public ListItemViewCollectionModel<CutoffDateTypeListItem> CutoffDateTypeList { get; } = 
            new ListItemViewCollectionModel<CutoffDateTypeListItem>();

        /// <summary>
        /// 回収日マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public ListItemViewCollectionModel<CollectionDateTypeListItem> CollectionDateTypeList { get; } = 
            new ListItemViewCollectionModel<CollectionDateTypeListItem>();

        /// <summary>
        /// 消費税区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public ListItemViewCollectionModel<TaxTypeListItem> TaxTypeList { get; } = 
            new ListItemViewCollectionModel<TaxTypeListItem>();

        /// <summary>
        /// 消費税計算区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public ListItemViewCollectionModel<TaxCalcTypeListItem> TaxCalcTypeList { get; } = 
            new ListItemViewCollectionModel<TaxCalcTypeListItem>();

        /// <summary>
        /// 消費税端数処理区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public ListItemViewCollectionModel<TaxRoundTypeListItem> TaxRoundTypeList { get; } = 
            new ListItemViewCollectionModel<TaxRoundTypeListItem>();

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
            var result = await this.CustomerMaintenanceSettingWebApi.Get();
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return false;
            }
            this.CutoffDateTypeList.Load(result.CustomerMaintenanceSetting.CutoffDateTypeList);
            this.CollectionDateTypeList.Load(result.CustomerMaintenanceSetting.CollectionDateTypeList);
            this.TaxTypeList.Load(result.CustomerMaintenanceSetting.TaxTypeList);
            this.TaxCalcTypeList.Load(result.CustomerMaintenanceSetting.TaxCalcTypeList);
            this.TaxRoundTypeList.Load(result.CustomerMaintenanceSetting.TaxRoundTypeList);

            await this.SearchCustomers();

            this.StateHasChanged();

            return true;
        }

        /// <summary>
        /// 得意先マスタリストを検索します。
        /// </summary>
        private async Task SearchCustomers()
        {
            var result = await this.CustomersWebApi.Get();
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return;
            }
            this.Customers = result.Customers;
        }

        /// <summary>
        /// 得意先マスタリストを更新します。
        /// </summary>
        private async Task PutCustomers()
        {
            var putResut = await this.CustomersWebApi.Put(this.MasterGridView.AddedItems, this.MasterGridView.ChangedItems, this.MasterGridView.DeletedItems);
            if (putResut.IsSuccessStatusCode() == false)
            {
                return;
            }
            await this.SearchCustomers();

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
            this.Customers = null;

            await this.SearchCustomers();
        }

        /// <summary>
        /// 確定ボタン押下時の処理を行います。
        /// </summary>
        private async Task OnPutClick()
        {
            // 編集モードのリストが存在する場合は処理不可
            if (this.Customers.IsEdit() == true)
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
                await this.PutCustomers();
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
