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
using System.Data.SqlTypes;

namespace YaminabeBlazor.Infrastructure.Extensions
{
    /// <summary>
    /// 日時拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class DateTimeExtensions
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 日時がSQLServerに格納可能な範囲か検証し、範囲を超える場合は範囲内の値に変換します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <returns>
        /// SQLServerに格納可能な範囲値。
        /// </returns>
        public static DateTime SqlServerValue(
            this DateTime value
            )
        {
            if (SqlDateTime.MinValue.Value > value)
            {
                return SqlDateTime.MinValue.Value;
            }

            if (SqlDateTime.MaxValue.Value < value)
            {
                return SqlDateTime.MaxValue.Value;
            }

            return value;
        }

        /// <summary>
        /// 日時がSQLServerに格納可能な範囲か検証し、範囲を超える場合は範囲内の値に変換します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <returns>
        /// SQLServerに格納可能な範囲値。
        /// </returns>
        public static DateTime? SqlServerValue(
            this DateTime? value
            )
        {
            if (value == null)
            {
                return value;
            }

            return value.Value.SqlServerValue();
        }

        #endregion
    }
}
