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
using YaminabeBlazor.Component.Core.Extensions;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Web.Shared.Models;
using YaminabeBlazor.Web.Shared.Services;

namespace YaminabeBlazor.Web.Client.Stub.Services
{
    /// <summary>
    /// 得意先マスタリストのApiサービスを表します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Web.Shared.Services.ICustomersApiService" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class CustomersApiService : ICustomersApiService
    {
        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="CustomersApiService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public CustomersApiService()
        {
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 得意先マスタリストを取得します。
        /// </summary>
        /// <returns>
        /// 得意先マスタリストを返却します。
        /// </returns>
        public async Task<(HttpStatusCode HttpStatusCode, List<CustomerInputModel> Customers)> Get()
        {
            return await Task<List<CustomerInputModel>>.Run(() =>
            {
                var customers = new List<CustomerInputModel>();

                foreach (var customer in DataBase.Customers)
                {
                    customers.Add(new CustomerInputModel()
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        CustomerKanaName = customer.CustomerKanaName,
                        CustomerShortName = customer.CustomerShortName,
                        EstablishmentDate = customer.EstablishmentDate,
                        Ceo = customer.Ceo,
                        PostalCode = customer.PostalCode,
                        Address = customer.Address,
                        Tel = customer.Tel,
                        Fax = customer.Fax,
                        Mail = customer.Mail,
                        Hp = customer.Hp,
                        CutoffDateType = customer.CutoffDateType,
                        CutoffDateTypeName = DataBase.CutoffDateTypes.First(c => c.CutoffDateType.Equals(customer.CutoffDateType)).CutoffDateTypeName,
                        CutoffDate = customer.CutoffDate,
                        CollectionDateType = customer.CollectionDateType,
                        CollectionDateTypeName =  DataBase.CollectionDateTypes.First(c => c.CollectionDateType.Equals(customer.CollectionDateType)).CollectionDateTypeName,
                        CollectionDate = customer.CollectionDate,
                        TaxType = customer.TaxType,
                        TaxTypeName = DataBase.TaxTypes.First(t => t.TaxType.Equals(customer.TaxType)).TaxTypeName,
                        TaxCalcType = customer.TaxCalcType,
                        TaxCalcTypeName = DataBase.TaxCalcTypes.First(t => t.TaxCalcType.Equals(customer.TaxCalcType)).TaxCalcTypeName,
                        TaxRoundType = customer.TaxRoundType,
                        TaxRoundTypeName = DataBase.TaxRoundTypes.First(t => t.TaxRoundType.Equals(customer.TaxRoundType)).TaxRoundTypeName,
                        Note = customer.Note
                    });
                }

                return (HttpStatusCode.OK, customers);
            });
        }

        /// <summary>
        /// 得意先マスタリストを更新します。
        /// </summary>
        /// <param name="input">得意先マスタリストの更新対象。</param>
        public Task<HttpStatusCode> Put(IEnumerable<CustomerInputModel> input)
        {
            return Task.Run(() =>
            {
                var addedCustomers = input.GetAdded();
                var modifiedCustomers = input.GetModified();
                var deletedCustomers = input.GetDeleted();
                var updateDateTime = DateTime.Now;
                var updateUserId = "Stub";

                // 追加
                foreach (var customer in addedCustomers)
                {
                    var addedBrand = DataBase.Customers.FirstOrDefault(c => c.CustomerId.Equals(customer.CustomerId));
                    if (addedBrand == null)
                    {
                        DataBase.Customers.Add(CustomerFactory.Create(
                            customerId: customer.CustomerId,
                            customerName: customer.CustomerName,
                            customerKanaName: customer.CustomerKanaName,
                            customerShortName: customer.CustomerShortName,
                            establishmentDate: customer.EstablishmentDate,
                            ceo: customer.Ceo,
                            postalCode: customer.PostalCode,
                            address: customer.Address,
                            tel: customer.Tel,
                            fax: customer.Fax,
                            mail: customer.Mail,
                            hp: customer.Hp,
                            cutoffDateType: customer.CutoffDateType,
                            cutoffDate: customer.CutoffDate,
                            collectionDateType: customer.CollectionDateType,
                            collectionDate: customer.CollectionDate,
                            taxType: customer.TaxType,
                            taxCalcType: customer.TaxCalcType,
                            taxRoundType: customer.TaxRoundType,
                            note: customer.Note,
                            updateDateTime: updateDateTime,
                            updateUserId: updateUserId
                            ));
                    }
                }

                // 更新
                foreach (var customer in modifiedCustomers)
                {
                    var modifiedCustomer = DataBase.Customers.FirstOrDefault(c => c.CustomerId.Equals(customer.CustomerId));
                    if (modifiedCustomer == null)
                    {
                        continue;
                    }
                    modifiedCustomer.CustomerName = customer.CustomerName;
                    modifiedCustomer.CustomerKanaName = customer.CustomerKanaName;
                    modifiedCustomer.CustomerShortName = customer.CustomerShortName;
                    modifiedCustomer.EstablishmentDate = customer.EstablishmentDate;
                    modifiedCustomer.Ceo = customer.Ceo;
                    modifiedCustomer.PostalCode = customer.PostalCode;
                    modifiedCustomer.Address = customer.Address;
                    modifiedCustomer.Tel = customer.Tel;
                    modifiedCustomer.Fax = customer.Fax;
                    modifiedCustomer.Mail = customer.Mail;
                    modifiedCustomer.Hp = customer.Hp;
                    modifiedCustomer.CutoffDateType = customer.CutoffDateType;
                    modifiedCustomer.CutoffDate = customer.CutoffDate;
                    modifiedCustomer.CollectionDateType = customer.CollectionDateType;
                    modifiedCustomer.CollectionDate = customer.CollectionDate;
                    modifiedCustomer.TaxType = customer.TaxType;
                    modifiedCustomer.TaxCalcType = customer.TaxCalcType;
                    modifiedCustomer.TaxRoundType = customer.TaxRoundType;
                    modifiedCustomer.Note = customer.Note;
                    modifiedCustomer.UpdateDateTime = updateDateTime;
                    modifiedCustomer.UpdateUserId = updateUserId;
                }

                // 削除
                foreach (var customer in deletedCustomers)
                {
                    var deletedCustomer = DataBase.Customers.FirstOrDefault(c => c.CustomerId.Equals(customer.CustomerId));
                    if (deletedCustomer == null)
                    {
                        continue;
                    }
                    DataBase.Customers.Remove(deletedCustomer);
                }

                return HttpStatusCode.OK;
            });
        }

        #endregion
    }
}
