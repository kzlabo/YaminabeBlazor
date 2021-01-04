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
using YaminabeBlazor.Component.Core.Enums;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 選択コンポーネントの基底コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <typeparam name="TList">リストの型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ynInputSelectBase<TValue, TList> : ynInputComponentBase<TValue>
        where TList : IListItemModel
    {
        #region -------------------- property --------------------

        private string _valueName;
        private string _textName;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 値リストを取得または設定します。
        /// </summary>
        [Parameter]
        public IEnumerable<TList> ListItems { get; set; }

        /// <summary>
        /// バインド項目値の選択時コールバック処理を取得または設定します。
        /// </summary>
        [Parameter]
        public Action<TList> SelectedCallBack { get; set; }

        /// <summary>
        /// バインドプロパティ名を取得または設定します。
        /// </summary>
        private new string PropertyName
        {
            get
            {
                return this.ValueName;
            }
        }

        /// <summary>
        /// 値バインドプロパティ名を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 隠蔽した <see cref="PropertyName"/> にプロパティ名を同時に設定する。
        /// </remarks>
        [Parameter]
        public string ValueName
        {
            get
            {
                return this._valueName;
            }
            set
            {
                this._valueName = value;
                base.PropertyName = value;
            }
        }

        /// <summary>
        /// テキストバインドプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string TextName
        {
            get
            {
                return this._textName;
            }
            set
            {
                this._textName = value;
            }
        }

        /// <summary>
        /// カレント値を取得または設定します。
        /// </summary>
        protected override TValue CurrentValue
        {
            get
            {
                return this.Options.GetValue<TValue>(this.Item, this.ValueName);
            }
            set
            {
                var currentValue = this.Options.GetValue<TValue>(this.Item, this.ValueName);
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, currentValue);
                if (hasChanged == true)
                {
                    this.Options.SetValue(this.Item, this.ValueName, value);

                    var selectedItem = GetSelectedItem();

                    this.Options.SetValue(this.Item, this.TextName, selectedItem.Value);

                    if (this.EditableContext != null)
                    {
                        this.EditableContext.EditState |= EditStateOptions.Changed;
                    }
                }
            }
        }

        /// <summary>
        /// カレントテキストを取得または設定します。
        /// </summary>
        protected string CurrentText
        {
            get
            {
                return this.Options.GetValue<string>(this.Item, this.TextName);
            }
        }
        /// <summary>
        /// フィルタ条件文字列を取得します。
        /// </summary>
        /// <remarks>
        /// 選択形式コンポーネントの場合はプロパティのバインドを <see cref="ValueName"/> と <see cref="TextName"/> に分けて設定する。
        /// フィルタ条件および画面表示に使用されるプロパティは <see cref="TextName"/> になるので <see cref="ynInputComponentBase{TValue}"/> で <see cref="PropertyName"/> を参照していた部分を変更する。
        /// </remarks>
        public override string FilterConditionValue
        {
            get
            {
                if (this.FilterContext == null)
                {
                    return null;
                }
                if (this.FilterContext.FilterConditions.TryGetValue(this.TextName, out var @filterCondition) == false)
                {
                    return null;
                }
                return @filterCondition.FilterText;
            }
        }

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        protected MarkupString DisplayValue
        {
            get
            {
                return this.CurrentText.HtmlBreak().HtmlMark(this.FilterConditionValue).HtmlSafeDecode().ConvertToMarkupString();
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
        /// 入力値をバインド項目値の型に変換します。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">返却値。</param>
        /// <returns>
        /// 入力値をバインド項目値の型に変換し、変換に成功した場合は <c>true</c> 。変換に失敗した場合は <c>false</c> を返却します。
        /// </returns>
        protected override bool TryParseValueFromString(string value, out TValue result)
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
            return true;
        }

        /// <summary>
        /// 選択したリストアイテムを取得します。
        /// </summary>
        /// <returns>
        /// 選択したリストアイテムを返却します。
        /// </returns>
        public TList GetSelectedItem()
        {
            if (this.ListItems == null || this.CurrentValue == null)
            {
                return default;
            }

            // 選択値を文字列に変換
            var selectedValue = this.CurrentValueAsString;

            // 選択アイテムを取得して返却
            return this.ListItems.FirstOrDefault(dropItem => dropItem.Key == selectedValue);
        }

        #endregion
    }
}
