using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Askianoor.AdminPanel.Data.Models;

namespace Askianoor.AdminPanel.Services
{
    public static class ServiceExtensions
    {
        #region API Calls

        #region Get

        public static async Task<T> GetJsonAsync<T, U>(this HttpClient httpClient, string url, string token, U bodyContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (bodyContent != null)
            {
                string json = bodyContent.ToString();
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                await ErrorHandling(response);
            }
            return default;
        }

        #endregion

        #region Post

        public static async Task<T> PostJsonAsync<T, U>(this HttpClient httpClient, string url, string token, U bodyContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(bodyContent);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                await ErrorHandling(response);
            }
            return default;
        }

        #endregion

        #region Put

        public static async Task<bool> PutJsonAsync<T>(this HttpClient httpClient, string url, string token, T bodyContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(bodyContent);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                await ErrorHandling(response);
            }
            return default;
        }

        #endregion

        #region Delete

        public static async Task<bool> DeleteJsonAsync<T>(this HttpClient httpClient, string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                await ErrorHandling(response);
            }
            return default;
        }

        #endregion

        #endregion

        #region Error Handling

        private static async Task ErrorHandling(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        RaiseBadRequestError(responseString);
                    }; break;

                case HttpStatusCode.NotFound:
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        RaiseNotFoundError(responseString);
                    }; break;

                case HttpStatusCode.InternalServerError:
                    {
                        RaiseInternalServerError();
                    }; break;

                case HttpStatusCode.Unauthorized:
                    {
                        RaiseUnauthorizedError();
                    }; break;

                case HttpStatusCode.Forbidden:
                    {
                        RaiseForbiddenError();
                    }; break;

                case HttpStatusCode.GatewayTimeout:
                    {
                        RaiseGatewayTimeoutError();
                    }; break;

                case HttpStatusCode.ServiceUnavailable:
                    {
                        RaiseServiceUnavailableError();
                    }; break;

                case HttpStatusCode.MethodNotAllowed:
                    {
                        RaiseMethodNotAllowedError();
                    }; break;
            }

        }

        private static void RaiseBadRequestError(string response)
        {
            ErrorResponse err = JsonConvert.DeserializeObject<ErrorResponse>(response);
            throw new Exception(err.ErrorMessage);
        }

        private static void RaiseNotFoundError(string response)
        {
            if (string.IsNullOrEmpty(response))
                throw new Exception(EnResource.NotFound);
            else
            {
                ErrorResponse err = JsonConvert.DeserializeObject<ErrorResponse>(response);
                throw new Exception(err.ErrorMessage);
            }
        }

        private static void RaiseUnauthorizedError()
        {
            throw new Exception(EnResource.Unauthorized);
        }

        private static void RaiseInternalServerError()
        {
            throw new Exception(EnResource.InternalServerError);
        }

        private static void RaiseForbiddenError()
        {
            throw new Exception(EnResource.Forbidden);
        }

        private static void RaiseGatewayTimeoutError()
        {
            throw new Exception(EnResource.GatewayTimeout);
        }

        private static void RaiseServiceUnavailableError()
        {
            throw new Exception(EnResource.ServiceUnavailable);
        }

        private static void RaiseMethodNotAllowedError()
        {
            throw new Exception(EnResource.MethodNotAllowed);
        }

        #endregion
    }
}
