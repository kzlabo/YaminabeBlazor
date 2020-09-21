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
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;

namespace YaminabeBlazor.Component.Core.Extensions
{
    /// <summary>
    /// 列挙リストの拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class IEnumerableExtensions
    {
        #region -------------------- is / can --------------------

        /// <summary>
        /// 編集モードのリストが存在するか判定します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// 編集モードのリストが存在する場合は <c>true</c> 。編集モードのリストが存在しない場合は <c>false</c> 。
        /// </returns>
        public static bool IsEdit<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.FirstOrDefault(i => i.EditMode.HasFlag(EditModeOptions.Edit) == true) != null;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 対象のアイテムをカレントに設定します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <param name="targetItem">対象のアイテム。</param>
        public static void SetCurrent<T>(
            this IEnumerable<T> items,
            T targetItem
            )
            where T : IEditableViewModel
        {
            foreach (var item in items)
            {
                item.IsCurrent = false;
            }
            targetItem.IsCurrent = true;
        }

        /// <summary>
        /// カレントアイテムを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// カレントアイテムを返却します。
        /// </returns>
        public static T GetCurrent<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.FirstOrDefault(item => item.IsCurrent == true);
        }

        /// <summary>
        /// PutApi対象のリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// PutApi対象のリストを返却します。
        /// </returns>
        public static IEnumerable<T> GetPutTarget<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.Where(
                i => 
                i.EditState.HasFlag(EditStateOptions.Added) == true || 
                i.EditState.HasFlag(EditStateOptions.Changed) == true ||
                i.EditState.HasFlag(EditStateOptions.Deleted) == true
                );
        }

        /// <summary>
        /// 追加分のリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// 追加分のリストを返却します。
        /// </returns>
        public static IEnumerable<T> GetAdded<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.Where(
                i => 
                i.EditState.HasFlag(EditStateOptions.Added) == true &&
                i.EditState.HasFlag(EditStateOptions.Deleted) == false
                );
        }

        /// <summary>
        /// 更新分のリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// 更新分のリストを返却します。
        /// </returns>
        public static IEnumerable<T> GetModified<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.Where(
                i =>
                i.EditState.HasFlag(EditStateOptions.Added) == false &&
                i.EditState.HasFlag(EditStateOptions.Changed) == true &&
                i.EditState.HasFlag(EditStateOptions.Deleted) == false
                );
        }

        /// <summary>
        /// 削除分のリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <returns>
        /// 削除分のリストを返却します。
        /// </returns>
        public static IEnumerable<T> GetDeleted<T>(
            this IEnumerable<T> items
            )
            where T : IEditableViewModel
        {
            return items.Where(
                i =>
                i.EditState.HasFlag(EditStateOptions.Added) == false &&
                i.EditState.HasFlag(EditStateOptions.Deleted) == true
                );
        }

        /// <summary>
        /// ソート条件で指定した順に並び替えたリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <param name="sortConditionValues">ソート条件。</param>
        /// <returns>
        /// ソート条件で指定した順に並び替えたリストを返却します。
        /// </returns>
        internal static IEnumerable<T> SortItems<T>(
            this IEnumerable<T> items,
            Dictionary<string, (string PropertyName, bool Desc)> sortConditionValues
            )
        {
            if ((sortConditionValues?.Count ?? 0) == 0)
            {
                return items;
            }

            bool first = true;
            IOrderedEnumerable<T> workintItems = null;
            foreach (var sortCondition in sortConditionValues.Values)
            {
                var prop = typeof(T).GetProperty(sortCondition.PropertyName);
                if (first == true)
                {
                    first = false;

                    if (sortCondition.Desc == false)
                    {
                        workintItems = items.OrderBy(item => prop.GetValue(item));
                    }
                    else
                    {
                        workintItems = items.OrderByDescending(item => prop.GetValue(item));
                    }
                }
                else
                {
                    if (sortCondition.Desc == false)
                    {
                        workintItems = workintItems.ThenBy(item => prop.GetValue(item));
                    }
                    else
                    {
                        workintItems = workintItems.ThenByDescending(item => prop.GetValue(item));
                    }
                }
            }
            return workintItems;
        }

        /// <summary>
        /// フィルタ条件で抽出したリストを取得します。
        /// </summary>
        /// <typeparam name="T">列挙の型。</typeparam>
        /// <param name="items">リスト。</param>
        /// <param name="filterConditionValues">フィルタ条件。</param>
        /// <returns>
        /// フィルタ条件で抽出したリストを返却します。
        /// </returns>
        internal static IEnumerable<T> FilterItems<T>(
            this IEnumerable<T> items,
            Dictionary<string, (string FilterText, bool PartialMatch)> filterConditionValues
            )
        {
            if ((filterConditionValues?.Count ?? 0) == 0)
            {
                return items;
            }

            return items?.Where(item =>
            {
                var properties = item.GetType().GetProperties();
                if (filterConditionValues != null)
                {
                    foreach (var filterCondition in filterConditionValues)
                    {
                        var property = properties.FirstOrDefault(p => p.Name.Equals(filterCondition.Key));
                        var filterConditionConfig = filterCondition.Value;
                        if (MatchFilterCondition(property.GetValue(item), filterConditionConfig.FilterText, filterConditionConfig.PartialMatch) == false)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            });
        }

        /// <summary>
        /// フィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterCondition(object value, string filterCondition, bool partialMatch)
        {
            if (value == null)
            {
                return false;
            }
            switch (value)
            {
                case string stringValue:
                    return MatchFilterConditionTypeString(stringValue, filterCondition, partialMatch);
                case short shortValue:
                    return MatchFilterConditionTypeShort(shortValue, filterCondition, partialMatch);
                case int intValue:
                    return MatchFilterConditionTypeInt(intValue, filterCondition, partialMatch);
                case long longValue:
                    return MatchFilterConditionTypeLong(longValue, filterCondition, partialMatch);
                case float floatValue:
                    return MatchFilterConditionTypeFloat(floatValue, filterCondition, partialMatch);
                case double doubleValue:
                    return MatchFilterConditionTypeDouble(doubleValue, filterCondition, partialMatch);
                case decimal decimalValue:
                    return MatchFilterConditionTypeDecimal(decimalValue, filterCondition, partialMatch);
                case DateTime datetimeValue:
                    return MatchFilterConditionTypeDateTime(datetimeValue, filterCondition, partialMatch);
                case bool boolValue:
                    return MatchFilterConditionTypeBool(boolValue, filterCondition, partialMatch);
                default:
                    return false;
            }
        }

        /// <summary>
        /// <c>string</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeString(string value, string filterCondition, bool partialMatch)
        {
            if (partialMatch == true)
            {
                return value.Contains(filterCondition);
            }
            else
            {
                return value.Equals(filterCondition);
            }
        }

        /// <summary>
        /// <c>short</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeShort(object value, string filterCondition, bool partialMatch)
        {
            short parsedValue;
            if (short.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>int</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeInt(object value, string filterCondition, bool partialMatch)
        {
            int parsedValue;
            if (int.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>long</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeLong(object value, string filterCondition, bool partialMatch)
        {
            long parsedValue;
            if (long.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>float</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeFloat(object value, string filterCondition, bool partialMatch)
        {
            float parsedValue;
            if (float.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>double</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeDouble(object value, string filterCondition, bool partialMatch)
        {
            double parsedValue;
            if (double.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>decimal</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeDecimal(object value, string filterCondition, bool partialMatch)
        {
            decimal parsedValue;
            if (decimal.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>DateTime</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeDateTime(object value, string filterCondition, bool partialMatch)
        {
            DateTime parsedValue;
            if (DateTime.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        /// <summary>
        /// <c>bool</c> 型の値がフィルタ条件に合致する値かどうかを判定します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="filterCondition">フィルタ条件。</param>
        /// <param name="partialMatch">部分一致判定。(true…部分一致 false…完全一致)</param>
        /// <returns>
        /// フィルタ条件に合致する場合は <c>true</c> 。フィルタ条件に合致しない場合は <c>false</c> を返却。
        /// </returns>
        private static bool MatchFilterConditionTypeBool(object value, string filterCondition, bool partialMatch)
        {
            bool parsedValue;
            if (bool.TryParse(filterCondition, out parsedValue) == false)
            {
                return false;
            }
            return value.Equals(parsedValue);
        }

        #endregion
    }
}
