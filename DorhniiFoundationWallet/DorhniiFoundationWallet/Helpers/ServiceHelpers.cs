using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.Helpers
{
    /// <summary>
    /// This class use for handle get and post service request
    /// </summary>
    public sealed class ServiceHelpers
    {
        #region Private Members      
        // private readonly string apiURL = string.Empty;
        private static readonly object lockInstance = new object();
        private static ServiceHelpers instance = null;
        ResponseModel responseModel = null;
        #endregion

        public static ServiceHelpers Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockInstance)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceHelpers();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Method for Post Request
        /// </summary>
        /// <param name="strSerializedData"></param>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> PostRequest(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                var httpClient = new HttpClient();
                if (isHeader)
                {
                    httpClient.DefaultRequestHeaders.Add(StringConstant.API_KEY_HttpAuthorization, string.Concat(StringConstant.API_KEY_BEARER, " ", token));
                }

                string baseUrl = StringConstant.API_KEY_URL;
                string serviceUrl = string.Empty;
                serviceUrl = baseUrl + strMethod;

                httpClient.Timeout = TimeSpan.FromSeconds(120);
                var uri = new Uri(serviceUrl);
                var content = new StringContent(strSerializedData, Encoding.UTF8, StringConstant.JSONContentType);
                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = true,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }

                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = result
                };
                return responseModel;
            }
            catch (WebException e)
            {
                WebResponse response1 = e.Response;
                string text;
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    text = "{error:\"nullResponse\",error_description:\"Operation time out, Please try again.\"}";
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;

                }
                else if (response1 == null)
                {
                    text = "{error:\"nullResponse\",error_description:\"Operation time out, Please try again.\"}";
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;
                }
                else
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    Stream data = response1.GetResponseStream();

                    var reader = new StreamReader(data);
                    text = await reader.ReadToEndAsync();
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HttpRequestException " + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("TimeoutException" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("TaskCanceledException" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
        }

        /// <summary>
        /// Method for Get Request
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="isHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModel> GetRequest(string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;

                var httpClient = new HttpClient();
                if (isHeader)
                {
                    httpClient.DefaultRequestHeaders.Add(StringConstant.API_KEY_HttpAuthorization, token);
                }
                string baseUrl = StringConstant.API_KEY_URL;

                string serviceUrl = baseUrl + strMethod;
                httpClient.Timeout = TimeSpan.FromSeconds(20);
                var uri = new Uri(serviceUrl);
                HttpResponseMessage response = null;
                response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = true,
                        Data = result
                    };
                    return responseModel;
                }

                else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = result
                    };
                    return responseModel;
                }
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (WebException e)
            {
                WebResponse response1 = e.Response;
                string text;
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    text = "{error:\"nullResponse\",error_description:\"Operation time out, Please try again.\"}";
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;
                }
                else if (response1 == null)
                {
                    text = "{error:\"nullResponse\",error_description:\"Operation time out, Please try again.\"}";
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;
                }
                else
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response1;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    Stream data = response1.GetResponseStream();

                    var reader = new StreamReader(data);
                    text = await reader.ReadToEndAsync();
                    responseModel = new ResponseModel
                    {
                        IsSuccess = false,
                        Data = text
                    };
                    return responseModel;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HttpRequestException" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("TimeoutException" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("TaskCanceledException" + ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                responseModel = new ResponseModel
                {
                    IsSuccess = false,
                    Data = string.Empty
                };
                return responseModel;
            }
        }

    }
}
