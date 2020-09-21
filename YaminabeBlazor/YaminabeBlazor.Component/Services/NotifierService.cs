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

using System;
using System.Threading.Tasks;

namespace YaminabeBlazor.Component.Services
{
    /// <summary>
    /// コンポーネントの更新通知サービスを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class NotifierService
    {
        #region -------------------- feild --------------------

        /// <summary>
        /// コンポーネントの状態更新通知イベントを取得または設定します。
        /// </summary>
        internal event Func<Task> Notify;

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントの状態更新を通知します。
        /// </summary>
        public async Task Update()
        {
            if (Notify != null)
            {
                await Notify.Invoke();
            }
        }

        #endregion

    }
}
