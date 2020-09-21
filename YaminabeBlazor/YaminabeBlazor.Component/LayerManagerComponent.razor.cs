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
using System.Collections.Generic;
using System.Linq;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// レイヤー管理コンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public partial class LayerManagerComponent
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 子コンテンツを取得または設定します。
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// レイヤーコンポーネントリストを取得または設定します。
        /// </summary>
        protected List<ILayerComponentBase> Layers { get; set; } = new List<ILayerComponentBase>();

        /// <summary>
        /// レイヤーコンポーネントの表示状態を取得します。
        /// </summary>
        /// <value>
        /// レイヤーコンポーネントリストに一つでも表示状態のレイヤーコンポーネントが存在する場合は <c>true</c> 。
        /// レイヤーコンポーネントリストに一つも表示状態のレイヤーコンポーネントが存在しない場合は <c>false</c> 。
        /// </value>
        public bool IsActive
        {
            get
            {
                return this.Layers.FirstOrDefault(o => o.IsVisible == true) != null;
            }
        }

        /// <summary>
        /// レイヤーのクラス名を取得します。
        /// </summary>
        public virtual string ClassName
        {
            get
            {
                var className = string.Empty;
                if (this.AdditionalAttributes != null)
                {
                    className = this.AdditionalAttributes.ContainsKey("class") ? this.AdditionalAttributes["class"].ToString() : string.Empty;
                }
                if (this.IsActive == false)
                {
                    className += " close";
                }
                return className;
            }
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 指定したレイヤーコンポーネントを表示します。
        /// </summary>
        /// <param name="name">レイヤーコンポーネント名。</param>
        public void Show(string name)
        {
            var layer = this.Layers.FirstOrDefault(l => l.Name == name);
            var maxIndex = this.Layers.Max(l => l.Index);

            layer.Show(maxIndex + 1);
        }

        /// <summary>
        /// 指定したレイヤーコンポーネントを非表示にします。
        /// </summary>
        /// <param name="name">レイヤーコンポーネント名。</param>
        public void Hide(string name)
        {
            var layer = this.Layers.FirstOrDefault(l => l.Name == name);
            layer.Hide();
        }

        #endregion
    }
}
