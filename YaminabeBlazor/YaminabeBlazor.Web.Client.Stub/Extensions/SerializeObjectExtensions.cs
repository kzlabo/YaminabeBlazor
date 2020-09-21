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

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YaminabeBlazor.Web.Client.Stub.Extensions
{
    /// <summary>
    /// シリアライズ可能オブジェクトの拡張メソッドを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class SerializeObjectExtensions
    {
        /// <summary>
        /// ディープコピーを行います。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <param name="original">元のインスタンス。</param>
        /// <returns>
        /// ディープコピーされたインスタンスを返却します。
        /// </returns>
        public static T DeepCopy<T>(this T original)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, original);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}
