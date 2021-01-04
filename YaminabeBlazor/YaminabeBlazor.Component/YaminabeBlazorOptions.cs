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

using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using YaminabeBlazor.Component.Core;
using YaminabeBlazor.Component.Core.Extensions;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// YaminabeBlazor.Componentのコンフィグを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="XXXX/XX/XX" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class YaminabeBlazorOptions
    {
        #region -------------------- property --------------------

        private ResourceManager _wordResourceManager;
        private Dictionary<string, IAccessor> _accessors;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネント内で利用する単語リソースを取得または設定します。
        /// </summary>
        public ResourceManager WoardResourceManager
        {
            get
            {
                if (this._wordResourceManager == null)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    this._wordResourceManager = new ResourceManager($"{assembly.GetName().Name}.WordResource", assembly);
                }
                return this._wordResourceManager;
            }
            set
            {
                this._wordResourceManager = value;
            }
        }

        /// <summary>
        /// コンポーネント内で利用するアクセサリストを取得または設定します。
        /// </summary>
        public Dictionary<string, IAccessor> Accessors
        {
            get
            {
                if (this._accessors == null)
                {
                    this._accessors = new Dictionary<string, IAccessor>();
                }
                return this._accessors;
            }
            set
            {
                this._accessors = value;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// キー値で指定された単語を取得します。
        /// </summary>
        /// <param name="key">キー値。</param>
        /// <returns>
        /// キー値で指定された単語を返却します。
        /// </returns>
        public string GetWordResouce(string key)
        {
            return this.WoardResourceManager.GetString(key);
        }

        /// <summary>
        /// データアイテムから指定のプロパティ値を取得します。
        /// </summary>
        /// <typeparam name="T">値の型。</typeparam>
        /// <param name="item">データアイテム。</param>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns>
        /// データアイテムから指定のプロパティ値を返却します。
        /// </returns>
        public T GetValue<T>(object item, string propertyName)
        {
            return (T)GetAccessor(item, propertyName).GetValue(item);
        }

        /// <summary>
        /// データアイテムの指定のプロパティに値を設定します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="value">値。</param>
        public void SetValue(object item, string propertyName, object value)
        {
            GetAccessor(item, propertyName).SetValue(item, value);
        }

        /// <summary>
        /// アクセサ検索の為のキー値を取得します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns>
        /// アクセサ検索の為のキー値を返却します。
        /// </returns>
        /// <remarks>
        /// {オブジェクトの型名}.{プロパティ名}をキー値とします。
        /// </remarks>
        private string GetAccessorKey(object item, string propertyName)
        {
            return $"{item.GetType().Name}.{propertyName}";
        }

        /// <summary>
        /// データアイテムとプロパティ名に対応したアクセサを取得します。
        /// </summary>
        /// <param name="item">データアイテム。</param>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns>
        /// データアイテムとプロパティ名に対応したアクセサを返却します。
        /// </returns>
        /// <remarks>
        /// アクセサがリストに存在する場合はリストから取得。
        /// アクセサがリストに存在しない場合はデータアイテムのプロパティから作成します。
        /// </remarks>
        private IAccessor GetAccessor(object item, string propertyName)
        {
            var accessorKey = GetAccessorKey(item, propertyName);

            if (this.Accessors.TryGetValue(accessorKey, out var @accessor) == false)
            {
                @accessor = this.Accessors[accessorKey] = item.GetType().GetProperty(propertyName).ToAccessor();
            }
            return @accessor;
        }

        #endregion
    }
}
