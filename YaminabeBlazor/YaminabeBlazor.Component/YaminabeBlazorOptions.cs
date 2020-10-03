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

using System.Reflection;
using System.Resources;

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

        private ResourceManager _WoardResourceManager;

        /// <summary>
        /// コンポーネント内で利用する単語リソースを取得または設定します。
        /// </summary>
        public ResourceManager WoardResourceManager
        {
            get
            {
                if (this._WoardResourceManager == null)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    this._WoardResourceManager = new ResourceManager($"{assembly.GetName().Name}.WordResource", assembly);
                }
                return this._WoardResourceManager;
            }
            set
            {
                this._WoardResourceManager = value;
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

        #endregion
    }
}
