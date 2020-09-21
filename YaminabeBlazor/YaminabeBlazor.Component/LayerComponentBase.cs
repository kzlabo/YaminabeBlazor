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
using YaminabeBlazor.Component.Services;

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 基底のレイヤーコンポーネントを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// <seealso cref="YaminabeBlazor.Component.ILayerComponentBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class LayerComponentBase : ComponentBase, ILayerComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="NotifierService"/> を注入します。
        /// </summary>
        [Inject]
        protected NotifierService Notifier { get; set; }

        /// <summary>
        /// <see cref="LayerManagerComponent"/> 配下に作成されたレイヤーコンポーネントリストを取得または設定します。
        /// </summary>
        [CascadingParameter(Name = "Layers")]
        protected List<ILayerComponentBase> Layers { get; set; }

        /// <summary>
        /// 追加属性を取得または設定します。
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public string Name { get; set; }

        /// <inheritdoc/>
        public int Index { get; set; } = -1;

        /// <inheritdoc/>
        public bool IsVisible { get; set; } = false;

        /// <inheritdoc/>
        public bool IsActive { get; set; } = false;

        /// <inheritdoc/>
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
                    className += " none";
                }
                return className;
            }
        }

        #endregion

        #region -------------------- life cycle --------------------

        /// <summary>
        /// コンポーネントのレンダリング後に処理を行います。
        /// </summary>
        /// <param name="firstRender">初回。</param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            // オプションコンポーネントリストに自身を追加。
            this.Layers.Add(this);
        }

        #endregion

        #region -------------------- method --------------------

        /// <inheritdoc/>
        public async void Show(int index = 0)
        {
            // 全て非活性状態に設定
            foreach (var layer in this.Layers)
            {
                layer.IsActive = false;
            }

            // 自身を表示し活性化
            this.IsVisible = true;
            this.IsActive = true;
            this.Index = index;

            await this.Notifier.Update();
        }

        /// <inheritdoc/>
        public async void Hide()
        {
            // 自身を非表示にし非活性
            this.IsVisible = false;
            this.IsActive = false;
            this.Index = -1;

            // 表示対象のうち表示順が最大のコンポーネントを活性化
            var option = this.Layers.OrderByDescending(o => o.Index).FirstOrDefault(o => o.IsVisible == true);
            if (option != null)
            {
                option.IsActive = true;
            }

            await this.Notifier.Update();
        }

        #endregion
    }
}
