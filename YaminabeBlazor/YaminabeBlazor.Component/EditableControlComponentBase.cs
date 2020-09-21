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
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 編集可能コンポーネントの基底のコンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">入力値の型。</typeparam>
    /// <seealso cref="YaminabeBlazor.Component.EditableComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class EditableControlComponentBase<TValue> : EditableComponentBase
    {
        #region -------------------- field --------------------

        /// <summary>
        /// NULL許容型の元の型を取得または設定します。
        /// </summary>
        private Type _nullableUnderlyingType;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// バインド項目値を取得または設定します。
        /// </summary>
        [Parameter]
        public TValue Value { get; set; }

        /// <summary>
        /// バインド項目値の変更時コールバック処理を取得または設定します。
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// カレント値を取得または設定します。
        /// </summary>
        protected TValue CurrentValue
        {
            get
            {
                return this.Value;
            }
            set
            {
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, this.Value);
                if (hasChanged == true)
                {
                    this.Value = value;
                    this.Item.EditState |= EditStateOptions.Changed;
                    this.ValueChanged.InvokeAsync(value).Wait();
                }
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

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// パラメータ設定を行います。
        /// </summary>
        /// <param name="parameters">パラメータリスト。</param>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            this._nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));

            return base.SetParametersAsync(parameters);
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
        protected virtual string FormatValueAsString(TValue value)
        {
            return value?.ToString();
        }

        /// <summary>
        /// 入力値をバインド項目値の型に変換します。
        /// </summary>
        /// <param name="value">入力値。</param>
        /// <param name="result">返却値。</param>
        /// <returns>
        /// 入力値をバインド項目値の型に変換し、変換に成功した場合は <c>true</c> 。変換に失敗した場合は <c>false</c> を返却します。
        /// </returns>
        protected abstract bool TryParseValueFromString(string value, out TValue result);

        #endregion


    }
}
