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

namespace YaminabeBlazor.Component.Extensions
{
    /// <summary>
    /// 文字列拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class StringExtensions
    {
        #region -------------------- method --------------------

        /// <summary>
        /// 文字列のHTMLエンコードを行います。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <returns>
        /// HTMLエンコードされた文字列を返却します。
        /// </returns>
        public static string HtmlEncode(
            this string value
            )
        {
            return System.Web.HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// 文字列内の改行コードに対して <br/> タグを設定します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <param name="target">ターゲット文字列。</param>
        /// <returns>
        /// <br/> タグを付与した文字列を返却します。
        /// </returns>
        public static string HtmlBreak(
            this string value
            )
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                return value;
            }
            return value.Replace("\r\n", "<__br/>").Replace("\r", "<__br/>").Replace("\n", "<__br/>");
        }

        /// <summary>
        /// 文字列内の指定文字列に対して <c>mark</c> タグを設定します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <param name="target">ターゲット文字列。</param>
        /// <returns>
        /// <c>mark</c> タグを付与した文字列を返却します。
        /// </returns>
        public static string HtmlMark(
            this string value,
            string target
            )
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                return value;
            }
            if (string.IsNullOrEmpty(target) == true)
            {
                return value;
            }
            return value.Replace(target, $"<__mark>{target}</__mark>");
        }

        /// <summary>
        /// 文字列内のエンコードされた定義タグに対してデコードを行います。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <returns>
        /// デコードした文字列を返却します。
        /// </returns>
        public static string HtmlSafeDecode(
            this string value
            )
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                return value;
            }
            return value.HtmlEncode().Replace("<__br/>".HtmlEncode(), "<br/>").Replace("<__mark>".HtmlEncode(), "<mark>").Replace("</__mark>".HtmlEncode(), "</mark>");
        }


        /// <summary>
        /// 文字列を <see cref="MarkupString"/> に変換します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <returns>
        /// <see cref="MarkupString"/> に変換した文字列を返却します。
        /// </returns>
        public static MarkupString ConvertToMarkupString(
            this string value
            )
        {
            return (MarkupString)value;
        }


        /// <summary>
        /// 文字列内の指定文字列に対して <c>mark</c> タグを設定し、かつ <see cref="MarkupString"/> に変換します。
        /// </summary>
        /// <param name="value">インスタンス。</param>
        /// <param name="target">ターゲット文字列。</param>
        /// <returns>
        /// <c>mark</c> タグを付与した文字列に対して <see cref="MarkupString"/> に変換した文字列を返却します。
        /// </returns>
        public static MarkupString HtmlMarkAndConvertToMarkupString(
            this string value,
            string target
            )
        {
            return value.HtmlMark(target).ConvertToMarkupString();
        }

        #endregion
    }
}
