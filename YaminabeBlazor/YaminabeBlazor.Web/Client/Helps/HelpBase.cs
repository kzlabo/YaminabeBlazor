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
using YaminabeBlazor.Web.Client.Components;

namespace YaminabeBlazor.Web.Client.Helps
{
    /// <summary>
    /// 基底のヘルプコンポーネントを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2021/01/01" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class HelpBase : ComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// オプションバーを取得または設定します。
        /// </summary>
        protected OptionBar OptionBar { get; set; }

        #endregion

        #region -------------------- event --------------------

        /// <summary>
        /// パネルを表示します。
        /// </summary>
        public void Show()
        {
            this.OptionBar.Show();
        }

        #endregion
    }
}
