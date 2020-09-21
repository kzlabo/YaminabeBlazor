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

namespace YaminabeBlazor.Component.Core.Enums
{
    #region -------------------- enum --------------------

    /// <summary>
    /// 編集モードオプションを提供します。
    /// </summary>
    public enum EditModeOptions
    {
        /// <summary>
        /// 編集モード未設定を示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 参照モードを示します。
        /// </summary>
        Read = 1,
        /// <summary>
        /// 編集モードを示します。
        /// </summary>
        Edit = 1 << 1
    }

    /// <summary>
    /// 編集状態を提供します。
    /// </summary>
    [Flags]
    public enum EditStateOptions
    {
        /// <summary>
        /// 編集状態未設定を示します。
        /// </summary>
        None = 0,
        /// <summary>
        /// 未編集を示します。
        /// </summary>
        UnChanged = 1,
        /// <summary>
        /// 追加を示します。
        /// </summary>
        Added = 1 << 1,
        /// <summary>
        /// 更新を示します。
        /// </summary>
        Changed = 1 << 2,
        /// <summary>
        /// 削除を示します。
        /// </summary>
        Deleted = 1 << 3
    }

    #endregion
}
