using ForNurseCom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ForNurseCom.Controllers
{
    [AllowAnonymous]
    [Route("api/KBUController")]
    [ApiController]
    public class KBUController : ControllerBase
    {

     
            private readonly HttpClient _httpClient;
            private readonly ApiSettings _apiSettings;
        private readonly string externalApiUrl = "https://api.kbu.cloud/smart/source/auth.php";

        public KBUController(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
            {
                _httpClient = httpClient;
                _apiSettings = apiSettings.Value;
        }

            [HttpPost()]
            public async Task<IActionResult> RunExistingApi([FromBody] KeyRequestKBU  requestKBU)
            {
                var formData = new MultipartFormDataContent
            {
                { new StringContent(requestKBU.Key), "key" }
                    //Rs1gLGsjRiaUzlSMofw9nrJhquMAqXFl
            };

                var response = await _httpClient.PostAsync("https://api.kbu.cloud/smart/source/auth.php", formData);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }

