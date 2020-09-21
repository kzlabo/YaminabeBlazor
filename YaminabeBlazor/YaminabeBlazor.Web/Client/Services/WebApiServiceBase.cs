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

using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YaminabeBlazor.Component.Core.Models;
using YaminabeBlazor.Component.Services;
using YaminabeBlazor.Web.Shared.Dtos;
using YaminabeBlazor.Web.Shared.Resources;

namespace YaminabeBlazor.Web.Client.Services
{
    /// <summary>
    /// 基底のWebApiサービスを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public abstract class WebApiServiceBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// シングルトン <see cref="HttpClient"/> を注入します。
        /// </summary>
        protected HttpClient _http;

        /// <summary>
        /// シングルトン <see cref="ApplicationStateService"/> を注入します。
        /// </summary>
        protected IApplicationStateService _applicationState;

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="WebApiServiceBase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="http">シングルトン <see cref="HttpClient"/>。</param>
        /// <param name="applicationState">シングルトン <see cref="ApplicationStateService"/>。</param>
        public WebApiServiceBase(
            HttpClient http,
            IApplicationStateService applicationState
            )
        {
            this._http = http;
            this._applicationState = applicationState;
        }

        #endregion

        #region -------------------- method --------------------

        /// <summary>
        /// 応答情報に応じたメッセージを通知します。
        /// </summary>
        /// <param name="response">応答情報。</param>
        private async Task SetNotificationMessageFromHttpResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    try
                    {
                        var errorDto = await response.Content.ReadFromJsonAsync<ErrorResponseDto>();
                        foreach (var error in errorDto.Errors)
                        {
                            this._applicationState.AddNotificationMessage(
                                WordResource.ErrorInfo,
                                error.Message,
                                NotificationMessageModel.NotiferTypeOptions.Error
                                );
                        }
                    }
                    catch
                    {
                        this._applicationState.AddNotificationMessage(
                            WordResource.ErrorInfo,
                            MessageResource.HttpStatusCode400,
                            NotificationMessageModel.NotiferTypeOptions.Error
                            );
                    }
                    return;

                case HttpStatusCode.Unauthorized:
                    this._applicationState.AddNotificationMessage(
                        WordResource.ErrorInfo,
                        MessageResource.HttpStatusCode401,
                        NotificationMessageModel.NotiferTypeOptions.Error
                        );
                    return;

                default:
                    return;
            }
        }

        /// <summary>
        /// <see cref="HttpClientJsonExtensions.GetJsonAsync{T}(HttpClient, string)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TResult">レスポンスデータの型。</typeparam>
        /// <param name="requestUri">URL。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> および <see cref="{T}"/> を返却します。
        /// </returns>
        protected async Task<(HttpStatusCode HttpStatusCode, TResult JsonData)> GetMethod<TResult>(
            string requestUri
            )
        {
            using var response = await this._http.GetAsync(requestUri);

            await this.SetNotificationMessageFromHttpResponse(response);
            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<TResult>());
            }

            return (response.StatusCode, default);
        }

        /// <summary>
        /// <see cref="HttpClientJsonExtensions.PutJsonAsync(HttpClient, string, object)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TValue">リクエストデータの型。</typeparam>
        /// <param name="requestUri">URL。</param>
        /// <param name="value">リクエストデータ。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> を返却します。
        /// </returns>
        protected async Task<HttpStatusCode> PutMethod<TValue>(
            string requestUri,
            TValue value
            )
        {
            using var response = await this._http.PutAsJsonAsync<TValue>(requestUri, value);

            await this.SetNotificationMessageFromHttpResponse(response);

            return response.StatusCode;
        }

        /// <summary>
        /// <see cref="HttpClientJsonExtensions.PutJsonAsync(HttpClient, string, object)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TValue">リクエストデータの型。</typeparam>
        /// <typeparam name="TResult">レスポンスデータの型。</typeparam>
        /// <param name="requestUri">TリクエストURI。</param>
        /// <param name="value">リクエストデータ。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> および <see cref="{T}"/> を返却します。
        /// </returns>
        protected async Task<(HttpStatusCode HttpStatusCode, TResult JsonData)> PutMethod<TValue, TResult>(
            string requestUri,
            TValue value
            )
        {
            using var response = await this._http.PutAsJsonAsync<TValue>(requestUri, value);

            await this.SetNotificationMessageFromHttpResponse(response);

            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<TResult>());
            }

            return (response.StatusCode, default);
        }

        /// <summary>
        /// <see cref="HttpClientJsonExtensions.PostJsonAsync(HttpClient, string, object)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TValue">リクエストデータの型。</typeparam>
        /// <param name="requestUri">URL。</param>
        /// <param name="value">リクエストデータ。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> を返却します。
        /// </returns>
        protected async Task<HttpStatusCode> PostMethod<TValue>(
            string requestUri,
            TValue value
            )
        {
            using var response = await this._http.PostAsJsonAsync<TValue>(requestUri, value);

            await this.SetNotificationMessageFromHttpResponse(response);

            return response.StatusCode;
        }

        /// <summary>
        /// <see cref="HttpClientJsonExtensions.PostJsonAsync(HttpClient, string, object)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TValue">リクエストデータの型。</typeparam>
        /// <typeparam name="TResult">レスポンスデータの型。</typeparam>
        /// <param name="requestUri">TリクエストURI。</param>
        /// <param name="value">リクエストデータ。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> および <see cref="{T}"/> を返却します。
        /// </returns>
        protected async Task<(HttpStatusCode HttpStatusCode, TResult JsonData)> PostMethod<TValue, TResult>(
            string requestUri,
            TValue value
            )
        {
            using var response = await this._http.PostAsJsonAsync<TValue>(requestUri, value);

            await this.SetNotificationMessageFromHttpResponse(response);

            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<TResult>());
            }

            return (response.StatusCode, default);
        }

        /// <summary>
        /// <see cref="HttpClient.DeleteAsync(string)"/> の代替メソッド。
        /// </summary>
        /// <param name="requestUri">URL。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> を返却します。
        /// </returns>
        protected async Task<HttpStatusCode> DeleteMethod(
            string requestUri
            )
        {
            using var response = await this._http.DeleteAsync(requestUri);

            await this.SetNotificationMessageFromHttpResponse(response);

            return response.StatusCode;
        }

        /// <summary>
        /// <see cref="HttpClient.DeleteAsync(string)"/> の代替メソッド。
        /// </summary>
        /// <typeparam name="TResult">レスポンスデータの型。</typeparam>
        /// <param name="requestUri">TリクエストURI。</param>
        /// <returns>
        /// <see cref="HttpStatusCode"/> および <see cref="{T}"/> を返却します。
        /// </returns>
        protected async Task<(HttpStatusCode HttpStatusCode, TResult JsonData)> DeleteMethod<TResult>(
            string requestUri
            )
        {
            using var response = await this._http.DeleteAsync(requestUri);

            await this.SetNotificationMessageFromHttpResponse(response);

            if (response.IsSuccessStatusCode == true)
            {
                return (response.StatusCode, await response.Content.ReadFromJsonAsync<TResult>());
            }

            return (response.StatusCode, default);
        }

        #endregion
    }
}
