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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net;
using YaminabeBlazor.Web.Shared.Dtos;

namespace YaminabeBlazor.Web.Server.Attributes
{
    /// <summary>
    /// 検証フィルタを提供します。
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public class ValidateActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// アクション実行前の処理を行います。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // モデル検証エラーの場合は処理終了
            if(context.ModelState.IsValid == false)
            {
                // モデルエラーの取得
                var errors = new List<ErrorResponseDto.ErrorArgument>();
                foreach(var value in context.ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(
                            new ErrorResponseDto.ErrorArgument()
                            {
                                Message = error.ErrorMessage
                            });
                    }
                }

                var responseDto = new ErrorResponseDto();
                responseDto.Errors = errors;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.Headers.Clear();

                context.Result = new ObjectResult(responseDto);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
