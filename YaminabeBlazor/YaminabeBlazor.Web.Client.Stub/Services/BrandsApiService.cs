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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// ブランドマスタリストのApiサービスを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.IBrandsApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class BrandsApiService : IBrandsApiService
    {
        #region -------------------- field --------------------

        private DataBase _dataBase;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="database">データベース。</param>
        public BrandsApiService(DataBase database)
        {
            this._dataBase = database;
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public async Task<(HttpStatusCode HttpStatusCode, List<BrandInputModel> Brands)> Get()
        {
            return await Task<List<BrandInputModel>>.Run(() =>
            {
                var brands = new List<BrandInputModel>();

                foreach (var brand in this._dataBase.Brands)
                {
                    brands.Add(new BrandInputModel()
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        Note = brand.Note
                    });
                }

                return (HttpStatusCode.OK, brands);
            });
        }

        /// <inheritdoc/>
        public Task<HttpStatusCode> Put(
            IEnumerable<BrandInputModel> addedItems,
            IEnumerable<BrandInputModel> changedItems,
            IEnumerable<BrandInputModel> deletedItems
            )
        {
            return Task.Run(() =>
            {
                var updateDateTime = DateTime.Now;
                var updateUserId = "Stub";

                // 追加
                foreach (var brand in addedItems)
                {
                    var addedBrand = this._dataBase.Brands.FirstOrDefault(p => p.BrandId.Equals(brand.BrandId));
                    if (addedBrand == null)
                    {
                        this._dataBase.Brands.Add(BrandFactory.Create(
                            brandId: brand.BrandId,
                            brandName: brand.BrandName,
                            note: brand.Note,
                            updateDateTime: updateDateTime,
                            updateUserId: updateUserId
                            ));
                    }
                }

                // 更新
                foreach (var brand in changedItems)
                {
                    var modifiedBrand = this._dataBase.Brands.FirstOrDefault(p => p.BrandId.Equals(brand.BrandId));
                    if (modifiedBrand == null)
                    {
                        continue;
                    }
                    modifiedBrand.BrandName = brand.BrandName;
                    modifiedBrand.Note = brand.Note;
                    modifiedBrand.UpdateDateTime = updateDateTime;
                    modifiedBrand.UpdateUserId = updateUserId;
                }

                // 削除
                foreach (var brand in deletedItems)
                {
                    var deletedBrand = this._dataBase.Brands.FirstOrDefault(p => p.BrandId.Equals(brand.BrandId));
                    if (deletedBrand == null)
                    {
                        continue;
                    }
                    this._dataBase.Brands.Remove(deletedBrand);
                }

                return HttpStatusCode.OK;
            });
        }

        #endregion
    }
}
