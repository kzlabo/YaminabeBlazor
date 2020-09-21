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

namespace YaminabeBlazor.Component
{
    /// <summary>
    /// 基底のレイヤーコンポーネントを表します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public interface ILayerComponentBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// コンポーネント名を取得または設定します。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// コンポーネントの表示順を取得または設定します。
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// コンポーネントの表示状態を取得または設定します。
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// コンポーネントの活性状態を取得または設定します。
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// コンポーネントのクラス名を取得します。
        /// </summary>
        string ClassName { get; }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// コンポーネントを表示します。
        /// </summary>
        /// <param name="index">表示順。</param>
        void Show(int index);

        /// <summary>
        /// コンポーネントを非表示にします。
        /// </summary>
        void Hide();

        #endregion
    }
}
