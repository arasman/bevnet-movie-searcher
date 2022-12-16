using Microsoft.AspNetCore.Mvc;
using Search.Core;
using Search.Core.DataTransferObjects.Movies;
using Search.Core.Interfaces;
using System.Net;
using System.Text.Json;

namespace Search.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ResponseCache(CacheProfileName = "MovieSearch_Default")]
    public class MoviesController : ControllerBase
    {
        protected readonly ILogger<MoviesController> _logger;
        protected readonly IWebAPIClient _apiClient;
        protected readonly IConfiguration _config;
        public MoviesController(ILogger<MoviesController> logger, IWebAPIClient apiClient, IConfiguration config)
        {
            _logger = logger;
            _apiClient = apiClient;
            _config = config;
        }        
        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery] string? title, [FromQuery] int? page)
        {
            _logger.LogInformation("Operating the request to store in cache.");
            Search_Response response = new Search_Response() { error = true, message = Search_Result.InternalServerError};
            try
            {
                //Getting the external api base address
                string? baseUrl = _config.GetSection("ExternalAPI:hackerrank").Value;
                if (string.IsNullOrEmpty(baseUrl)) return StatusCode((int)HttpStatusCode.InternalServerError, response);

                //Establishing the rest of the url and parameters by querystring
                string url = $"{baseUrl}/movies/search";
                if (!string.IsNullOrEmpty(title) && page != null && page.HasValue && page.Value > 0)
                    url = $"{url}?Title={title}&page={page}";
                else if (!string.IsNullOrEmpty(title) && (page == null || (page != null && !page.HasValue) || (page != null && page.HasValue && page.Value <= 0)))
                    url = $"{url}?Title={title}";
                else if (string.IsNullOrEmpty(title) && page != null && page.HasValue && page.Value > 0)
                    url = $"{url}?page={page}";

                //Executing the request to the external API
                HttpResponseMessage httpResponse = await _apiClient.Get(HttpClient_Name.Default, url);
                Tuple<bool, Search_ExternalAPI_Response?> data = await _apiClient.Response_Read<Search_ExternalAPI_Response?>(httpResponse);

                if (!data.Item1) return StatusCode((int)HttpStatusCode.InternalServerError, response);
                
                //Only when there are records as part of the response is considered as success.
                if (data.Item2 != null && data.Item2.data != null && data.Item2.data.Count() > 0)
                {
                    response.error = false;
                    response.message = Search_Result.Success;
                    response.result = data.Item2;
                }
                else response.message = Search_Result.NoData;

                //Establishing the status-code and response body.
                if (!response.error)
                    return StatusCode((int)HttpStatusCode.OK, response);
                else if (response.message.Equals(Search_Result.NoData))
                    return StatusCode((int)HttpStatusCode.OK, response);
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
            catch
            {
                response.message = Search_Result.GeneralException;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
