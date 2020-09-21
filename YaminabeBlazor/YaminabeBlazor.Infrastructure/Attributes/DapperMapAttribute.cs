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

namespace YaminabeBlazor.Infrastructure.Attributes
{
    /// <summary>
    /// Dapperとデータベーステーブルのマッピング属性を提供します。
    /// </summary>
    /// <example>
    ///     <code language="C#" numberLines="true">
    ///         public class ProductDto
    ///         {
    ///             [DapperMap(Name = "商品コード")]
    ///             public string ProductId { get; set; }
    ///             
    ///             [DapperMap(Name = "商品名")]
    ///             public string ProductName { get; set; }
    ///         }
    ///         
    ///         public class ProductGetRepository
    ///         {
    ///             public ProductDto Get(string productId)
    ///             {
    ///                 var builder = new SqlConnectionStringBuilder();
    ///                 builder.DataSource = @"(localdb)\ProjectsV13";
    ///                 builder.InitialCatalog = "YaminabeBlazor.db";
    ///                 
    ///                 using var connection = new SqlConnection(builder.ToString());
    ///                 return connection.Query&lt;ProductDto&gt;(
    ///                     sql: "SELECT * FROM [M_商品] WHERE [商品コード] = @productId",
    ///                     param: new {
    ///                         productId = productId
    ///                     }
    ///                 );
    ///             }
    ///         }
    ///     </code>
    /// </example>
    /// <seealso cref="System.Attribute" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DapperMapAttribute : Attribute
    {
        #region -------------------- property --------------------

        /// <summary>
        /// マッピング先のカラム名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// プロパティをコピーの除外対象とするかどうかを指定します。
        /// </summary>
        /// <value>
        ///   除外対象とする場合は <c>true</c>。 除外しない場合は <c>false</c> を指定します。
        /// </value>
        public bool Ignore { get; set; } = false;

        #endregion
    }
}
