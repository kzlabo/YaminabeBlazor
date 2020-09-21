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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YaminabeBlazor.Web.Shared.Models;

namespace YaminabeBlazor.Web.Server.Controllers
{
    /// <summary>
    /// JWTのコントローラーを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ロガーを取得します。
        /// </summary>
        private ILogger<JWTController> _logger { get; }

        /// <summary>
        /// アプリケーションコンフィグを取得します。
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="JWTController"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="config">The configuration.</param>
        public JWTController(IConfiguration config)
        {
            this._config = config;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// トークンを発行します。
        /// </summary>
        /// <param name="input">ログイン入力情報。</param>
        /// <returns>
        /// ログイン認証時にはトークンを取得します、ログイン失敗時にはトークンにnullが返却されます。
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public LoginAuthorizedModel Post(LoginInputModel input)
        {
            if(input.UserId == "kz" && input.Password == "labo")
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, input.UserId)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["JwtSettings:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: this._config["JwtSettings:Issuer"],
                    audience: this._config["JwtSettings:Issuer"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds
                    );

                return new LoginAuthorizedModel(
                    userId: input.UserId,
                    token: new JwtSecurityTokenHandler().WriteToken(token)
                    );
            }

            return new LoginAuthorizedModel(
                userId: null,
                token: null
                );
        }

        #endregion
    }
}