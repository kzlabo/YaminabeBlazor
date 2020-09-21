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
using System;
using System.Collections.Generic;
using System.Linq;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントの選択コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TDropModel">ドロップダウンリストの型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.EditableInputComponentBase{TValue}" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class EditableInputSelectComponentBase<TValue, TDropModel> : EditableInputComponentBase<TValue>
        where TDropModel : IListItemModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 値リストを取得または設定します。
        /// </summary>
        [Parameter]
        public IEnumerable<TDropModel> DropItems { get; set; }

        /// <summary>
        /// バインド項目値の変更時コールバック処理を取得または設定します。
        /// </summary>
        [Parameter]
        public Action<TDropModel> SelectedCallBack { get; set; }

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        protected MarkupString DisplayValue
        {
            get
            {
                var dropItem = GetSelectedDropItem();
                var displayString = dropItem?.Value;

                return displayString.HtmlBreak().HtmlMark(this.FilterConditionValue).HtmlSafeDecode().ConvertToMarkupString();
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 値を文字列に変換した取得します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>
        /// 値を文字列に変換して返却します。
        /// </returns>
        protected override string FormatValueAsString(TValue value)
        {
            // 入力値が列挙型の場合はドロップダウンリストのキー文字列と比較するために文字列に変換
            if (typeof(TValue).IsEnum == true)
            {
                return ((int)(object)value).ToString();
            }
            else
            {
                return value?.ToString();
            }
        }

        /// <summary>
        /// 入力値の文字列型への変換を行います。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">戻り値</param>
        /// <param name="validationErrorMessage">変換エラーメッセージ。</param>
        /// <returns>
        /// 入力値を文字列型に変換した値を返却します。
        /// </returns>
        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue).IsEnum == true)
            {
                if (int.TryParse(value, out var intValue) == true)
                {
                    result = (TValue)Enum.ToObject(typeof(TValue), intValue);
                }
                else
                {
                    result = (TValue)Enum.ToObject(typeof(TValue), 0);
                }
            }
            else
            {
                result = (TValue)(object)value;
            }
            validationErrorMessage = null;
            return true;
        }

        /// <summary>
        /// 選択したリストアイテムを取得します。
        /// </summary>
        /// <returns>
        /// 選択したリストアイテムを返却します。
        /// </returns>
        public TDropModel GetSelectedDropItem()
        {
            if (this.DropItems == null || this.Value == null)
            {
                return default;
            }

            // 選択値を文字列に変換
            var selectedValue = this.CurrentValueAsString;

            // 選択アイテムを取得して返却
            return this.DropItems.FirstOrDefault(dropItem => dropItem.Key == selectedValue);
        }

        #endregion
    }
}
