using Search.Core.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using Microsoft.AspNetCore.Http;
using Search.Core.DataTransferObjects.Movies;
using System.Text.Json;

namespace Search.Core
{
    public class WebAPIClient : IWebAPIClient
    {
        protected IHttpClientFactory _factory;
        public WebAPIClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        /// <summary>
        /// Execute a request by get
        /// </summary>
        /// <param name="httpClientName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(string httpClientName, string url)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = HttpMethod.Get;
                    request_.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);
                    HttpClient client = _factory.CreateClient(httpClientName);
                    httpResponse = await client.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                }
            }
            catch
            {
                httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);                
            }
            return httpResponse;
        }
        public async Task<Tuple<bool, Tout>> Response_Read<Tout>(HttpResponseMessage responseMessage)
        {
            Tuple<bool,Tout?> result;
            Tout? data = default(Tout);
            bool success = false;
            try
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    //Getting the response.
                    Stream streamInfo = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    StreamReader reader = new StreamReader(streamInfo);
                    string jsonResult = reader.ReadToEnd();
                    var options = new JsonDocumentOptions { AllowTrailingCommas = true };
                    //Interpreting the response and deserializing to DTO
                    
                    using (JsonDocument document = JsonDocument.Parse(jsonResult, options))
                    {
                        data = JsonSerializer.Deserialize<Tout>(document);
                    }
                    success = true;
                }
            }
            catch
            {
                success = false;
            }
            finally
            {
                result = new Tuple<bool,Tout?>(success, data);
            }
            return result;
        }
    }
}