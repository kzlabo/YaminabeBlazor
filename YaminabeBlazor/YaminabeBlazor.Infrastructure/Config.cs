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

using Dapper;
using System.Linq;
using System.Reflection;
using YaminabeBlazor.Infrastructure.Attributes;
using YaminabeBlazor.Infrastructure.Dtos;

namespace YaminabeBlazor.Infrastructure
{
    /// <summary>
    /// インフラストラクチャの設定を提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class Config
    {
        #region -------------------- method --------------------

        /// <summary>
        /// インフラストラクチャの設定を登録します。
        /// </summary>
        public static void Register()
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IDapperDto).IsAssignableFrom(t) && typeof(IDapperDto) != t)
                .ToList()
                .ForEach(d =>
                {
                    SqlMapper.SetTypeMap(
                        d,
                        new CustomPropertyTypeMap(
                            d,
                            (type, columnName) => type.GetProperties().FirstOrDefault(
                                prop => prop.Name == columnName || prop.GetCustomAttributes(true).OfType<DapperMapAttribute>().Any(attr => attr.Name == columnName)))
                        );
                });
        }

        #endregion
    }
}
