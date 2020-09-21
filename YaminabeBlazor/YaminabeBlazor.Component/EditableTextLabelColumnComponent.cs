﻿/*
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
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Extensions;

namespace YaminabeBlazor.Component
{
    public class EditableTextLabelColumnComponent : ComponentBase
    {
        #region -------------------- field --------------------

        /// <summary>
        /// NULL許容型の元の型を取得または設定します。
        /// </summary>
        private Type _nullableUnderlyingType;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// 書式を取得または設定します。
        /// </summary>
        [Parameter]
        public string Format { get; set; }

        /// <summary>
        /// バインド項目のプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public string PropertyName { get; set; }

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        [Parameter]
        public IEditableViewModel Item { get; set; }

        /// <summary>
        /// フィルタ条件値リストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeFilterConditionValues")]
        public Dictionary<string, (string FilterText, bool PartialMatch)> FilterConditionValues { get; set; }

        /// <summary>
        /// フィルタ条件文字列を取得または設定します。
        /// </summary>
        public string FilterConditionValue
        {
            get
            {
                if (this.FilterConditionValues == null)
                {
                    return null;
                }
                if (this.FilterConditionValues.TryGetValue(this.PropertyName, out var filterConditionConfig) == false)
                {
                    return null;
                }
                return filterConditionConfig.FilterText;
            }
        }

        /// <summary>
        /// カレント値を取得または設定します。
        /// </summary>
        protected object CurrentValue
        {
            get
            {
                return this.Item.GetType().GetProperty(this.PropertyName).GetValue(this.Item);
            }
            set
            {
                this.Item.GetType().GetProperty(this.PropertyName).SetValue(this.Item, value);
            }
        }

        /// <summary>
        /// カレント値を文字列で取得または設定します。
        /// </summary>
        protected string CurrentValueAsString
        {
            get
            {
                return this.FormatValueAsString(this.CurrentValue);
            }
            set
            {
                if (this._nullableUnderlyingType != null && string.IsNullOrEmpty(value) == true)
                {
                    this.CurrentValue = default;
                }
                else if (this.TryParseValueFromString(value, out var parsedValue) == true)
                {
                    this.CurrentValue = parsedValue;
                }
                else
                {
                }
            }
        }

        /// <summary>
        /// 表示用文字列を取得または設定します。
        /// </summary>
        private MarkupString DisplayValue
        {
            get
            {
                return this.CurrentValueAsString.HtmlBreak().HtmlMark(this.FilterConditionValue).HtmlSafeDecode().ConvertToMarkupString();
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定を行います。
        /// </summary>
        /// <param name="parameters">パラメータリスト。</param>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            this._nullableUnderlyingType = Nullable.GetUnderlyingType(this.Item.GetType().GetProperty(this.PropertyName).GetType());

            return base.SetParametersAsync(parameters);
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, this.DisplayValue);
        }

        /// <summary>
        /// 値を文字列に変換した取得します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>
        /// 値を文字列に変換して返却します。
        /// </returns>
        protected string FormatValueAsString(object value)
        {
            if (this.Format == null || value == null)
            {
                return value?.ToString();
            }

            return string.Format(string.Format($"{{0:{this.Format}}}", value));
        }

        /// <summary>
        /// 入力値をバインド項目値の型に変換します。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">返却値。</param>
        /// <returns>
        /// 入力値をバインド項目値の型に変換し、変換に成功した場合は <c>true</c> 。変換に失敗した場合は <c>false</c> を返却します。
        /// </returns>
        protected bool TryParseValueFromString(string value, out object result)
        {
            if (BindConverter.TryConvertTo<object>(value, CultureInfo.InvariantCulture, out result) == true)
            {
            }
            else
            {
                result = default;
            }
            return true;
        }

        #endregion
    }
}
