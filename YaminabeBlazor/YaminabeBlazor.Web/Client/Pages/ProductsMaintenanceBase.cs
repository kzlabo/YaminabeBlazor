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
using System.Linq;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;
using YaminabeBlazor.Web.Client.Services;
using YaminabeBlazor.Web.Shared.ListItems;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Resources;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Pages
{
    /// <summary>
    /// 基底の商品マスタリストページを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Client.Pages.MyPageBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class ProductsMaintenanceBase : MyPageBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// <see cref="ProductsApiService"/> を注入します。
        /// </summary>
        [Inject]
        protected IProductsApiService ProductsWebApi { get; set; }

        /// <summary>
        /// <see cref="ProductMaintenanceSettingApiService"/> を注入します。
        /// </summary>
        [Inject]
        protected IProductMaintenanceSettingApiService ProductMaintenanceSettingWebApi { get; set; }

        /// <summary>
        /// 商品マスタリストを取得します。
        /// </summary>
        protected List<ProductInputModel> Products { get; set; }

        /// <summary>
        /// 追加対象商品マスタリストを取得します。
        /// </summary>
        protected abstract IEnumerable<ProductInputModel> AddedProducts { get; }

        /// <summary>
        /// 更新対象商品マスタリストを取得します。
        /// </summary>
        protected abstract IEnumerable<ProductInputModel> ChangedProducts { get; }

        /// <summary>
        /// 削除対象商品マスタリストを取得します。
        /// </summary>

        protected abstract IEnumerable<ProductInputModel> DeletedProducts { get; }

        /// <summary>
        /// ブランドマスタ選択リストを取得します。
        /// </summary>
        protected ListItemViewCollectionModel<BrandListItem> BrandList { get; } = 
            new ListItemViewCollectionModel<BrandListItem>();

        /// <summary>
        /// 商品カテゴリマスタ選択リストを取得します。
        /// </summary>
        protected ListItemViewCollectionModel<ProductCategoryListItem> ProductCategoryList { get; } = 
            new ListItemViewCollectionModel<ProductCategoryListItem>();

        /// <summary>
        /// 商品タグ区分マスタ選択リストを取得します。
        /// </summary>
        protected ListItemViewCollectionModel<ProductTagTypeListItem> ProductTagTypeList { get; } = 
            new ListItemViewCollectionModel<ProductTagTypeListItem>();

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
            var result = await this.ProductMaintenanceSettingWebApi.Get();
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return false;
            }
            this.BrandList.Load(result.ProductMaintenanceSetting.BrandList);
            this.ProductCategoryList.Load(result.ProductMaintenanceSetting.ProductCategoryList);
            this.ProductTagTypeList.Load(result.ProductMaintenanceSetting.ProductTagTypeList);

            await this.SearchProducts();

            this.StateHasChanged();

            return true;
        }

        /// <summary>
        /// 商品マスタリストを検索します。
        /// </summary>
        protected async Task SearchProducts()
        {
            var result = await this.ProductsWebApi.Get();
            if (result.HttpStatusCode.IsSuccessStatusCode() == false)
            {
                return;
            }
            this.Products = result.Products;
        }

        /// <summary>
        /// 商品マスタリストを更新します。
        /// </summary>
        protected async Task PutProducts()
        {
            var putResut = await this.ProductsWebApi.Put(this.AddedProducts, this.ChangedProducts, this.DeletedProducts);
            if (putResut.IsSuccessStatusCode() == false)
            {
                return;
            }
            await this.SearchProducts();

            this.ApplicationState.AddNotificationMessage(WordResource.CompleteInfo, string.Format(MessageResource.Complete, WordResource.Commit));

            StateHasChanged();
        }

        /// <summary>
        /// ブランド選択時の処理を行います。
        /// </summary>
        protected void SetBrand(BrandListItem brandItem)
        {
            var product = this.Products.FirstOrDefault(p => p.EditMode == EditModeOptions.Edit);
            if (product == null)
            {
                return;
            }
            product.SetBrand(brandItem);
            product.StateHasChanged();
        }

        #endregion
    }
}
