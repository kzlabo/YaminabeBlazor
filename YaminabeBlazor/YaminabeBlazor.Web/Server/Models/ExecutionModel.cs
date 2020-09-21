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

using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Web.Server.Models
{
    /// <summary>
    /// 実行モデルを提供します。
    /// </summary>
    /// <seealso cref="YaminabeBlazor.Core.Models.IExecutionModel" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ExecutionModel : IExecutionModel
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 実行ユーザーコードを取得または設定します。
        /// </summary>
        private string _userId;

        /// <summary>
        /// 実行開始時間を取得または設定します。
        /// </summary>
        private DateTime? _executeDateTime;

        #endregion

        #region -------------------- property --------------------

        /// <summary>
        /// HttpContextアクセサを取得します。
        /// </summary>
        private IHttpContextAccessor HttpContextAccessor { get; }

        /// <summary>
        /// 実行ユーザーコードを取得または設定します。
        /// </summary>
        public string UserId
        {
            get
            {
                return this._userId ?? this.HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            }
            set
            {
                this._userId = value;
            }
        }

        /// <summary>
        /// 実行開始時間を取得または設定します。
        /// </summary>
        public DateTime ExecuteDateTime
        {
            get
            {
                return this._executeDateTime ?? DateTime.Now;
            }
            set
            {
                this._executeDateTime = value;
            }
        }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="BrandsGetService"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="httpContextAccessor">HttpContextアクセサ。</param>
        public ExecutionModel(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }

        #endregion
    }
}
