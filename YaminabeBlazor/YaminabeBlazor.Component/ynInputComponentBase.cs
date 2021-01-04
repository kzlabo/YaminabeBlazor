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
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Enums;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 入力コンポーネントの基底コンポーネントを提供します。
    /// </summary>
    /// <typeparam name="TValue">値の型。</typeparam>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class ynInputComponentBase<TValue> : ComponentBase
    {
        #region -------------------- field --------------------

        private Type _nullableUnderlyingType;
        private object _item;
        private string _title;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネントの設定アクセサを取得または設定します。
        /// </summary>
        [Inject]
        protected IOptions<YaminabeBlazorOptions> OptionsAccessor { get; set; }

        /// <summary>
        /// コンポーネントの設定アクセサを取得します。
        /// </summary>
        /// <remarks>
        /// <c>OptionsAccessor.Value</c> でのアクセスを省略。
        /// </remarks>
        protected YaminabeBlazorOptions Options
        {
            get
            {
                return this.OptionsAccessor.Value;
            }
        }

        /// <summary>
        /// データコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeEditableContext")]
        public EditableContext EditableContext { get; set; }

        /// <summary>
        /// フィルタコンテキストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "CascadeFilterContext")]
        public FilterContext FilterContext { get; set; }

        /// <summary>
        /// データアイテムを取得または設定します。
        /// </summary>
        [Parameter]
        public object Item
        {
            get
            {
                if (this.EditableContext == null)
                {
                    return this._item;
                }
                return this.EditableContext.Item;
            }
            set
            {
                this._item = value;
            }
        }
        
        /// <summary>
        /// バインドプロパティ名を取得または設定します。
        /// </summary>
        [Parameter]
        public virtual string PropertyName { get; set; }

        /// <summary>
        /// カレント値を取得または設定します。
        /// </summary>
        protected virtual TValue CurrentValue
        {
            get
            {
                return this.Options.GetValue<TValue>(this.Item, this.PropertyName);
            }
            set
            {
                var currentValue = this.Options.GetValue<TValue>(this.Item, this.PropertyName);
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, currentValue);
                if (hasChanged == true)
                {
                    this.Options.SetValue(this.Item, this.PropertyName, value);

                    if (this.EditableContext != null)
                    {
                        this.EditableContext.EditState |= EditStateOptions.Changed;
                    }
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

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// フィルタ条件文字列を取得します。
        /// </summary>
        public virtual string FilterConditionValue
        {
            get
            {
                if (this.FilterContext == null)
                {
                    return null;
                }
                if (this.FilterContext.FilterConditions.TryGetValue(this.PropertyName, out var @filterCondition) == false)
                {
                    return null;
                }
                return @filterCondition.FilterText;
            }
        }

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        /// <remarks>
        /// <see cref="Title"/> プロパティを設定していない場合、
        /// モデルのプロパティに <see cref="DisplayAttribute"/> を付与している場合は <see cref="DisplayAttribute.Name"/> を取得。
        /// </remarks>
        [Parameter]
        public string Title
        {
            get
            {
                var title = this._title;
                if (title == null && this.Item != null)
                {
                    var property = this.Item.GetType().GetProperty(this.PropertyName);
                    var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayAttribute));
                    if (attribute != null)
                    {
                        title = attribute.Name;
                    }
                }
                return title;
            }
            set
            {
                this._title = value;
            }
        }

        /// <summary>
        /// タイトルの幅を取得または設定します。（既定値：35%）
        /// </summary>
        [Parameter]
        public string TitleWidth { get; set; } = "35%";

        /// <summary>
        /// 項目の幅を取得または設定します。（既定値：65%）
        /// </summary>
        [Parameter]
        public string ControlWidth { get; set; } = "65%";

        #endregion

        #region -------------------- is / can --------------------

        /// <summary>
        /// タイトル表示有無を取得または設定します。
        /// </summary>
        [Parameter]
        public bool IsTitle { get; set; } = false;

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
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (this.IsTitle == false)
            {
                BuildRenderTreeComponent(builder);
            }
            else
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "style", $"display:flex;");

                builder.OpenElement(2, "div");
                builder.AddAttribute(3, "style", $"flex-basis:{this.TitleWidth}");
                builder.AddContent(4, this.Title);
                builder.CloseElement();

                builder.OpenElement(5, "div");
                builder.AddAttribute(6, "style", $"flex-basis:{this.ControlWidth}");
                builder.OpenRegion(1000000);
                BuildRenderTreeComponent(builder);
                builder.CloseRegion();
                builder.CloseElement();

                builder.CloseElement();
            }
        }

        /// <summary>
        /// コンポーネントのレンダリングを行います。
        /// </summary>
        /// <param name="builder">ビルダ。</param>
        protected virtual void BuildRenderTreeComponent(RenderTreeBuilder builder)
        {
        }

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
