using Common.Models;
using Newtonsoft.Json;
using System.Text;

namespace Api.Features;

public class IntegrationService
{
    private readonly string mediaType = "application/json";
    private readonly string _baseUri;
    private readonly HttpClient _httpClient;
    private readonly ILogger<IntegrationService> _logger;

    public IntegrationService(IHttpClientFactory httpClientFactory, ILogger<IntegrationService> logger, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _baseUri = configuration.GetValue<string>("MockApiURI")!;
    }

    //public async Task<SensorData> GetAggregateData(Guid id)
    //{
    //string url = $"{_baseUri}classes/{classId}/parents";

//HttpResponseMessage response = await _httpClient.GetAsync(url);
//_logger.LogInformation($"Sent http GET request to url: {url}.");

//string jsonResponse = await response.Content.ReadAsStringAsync();
//_logger.LogInformation($"Http response: {jsonResponse}");

//if (!response.IsSuccessStatusCode)
//{
//    _logger.LogError($"GET request failed.");
//    return null;
//}

//return JsonConvert.DeserializeObject<SensorData>(jsonResponse)!;
//}

//public async Task<SensorData> GetSensorData(Guid id)
//{
//string url = $"{_baseUri}instances/{classId}";

//HttpResponseMessage response = await _httpClient.GetAsync(url);
//_logger.LogInformation($"Sent http GET request to url: {url}.");

//string jsonResponse = await response.Content.ReadAsStringAsync();
//_logger.LogInformation($"Http response: {jsonResponse}");

//if (!response.IsSuccessStatusCode)
//{
//    _logger.LogError($"GET request failed.");
//    return null;
//}

//return JsonConvert.DeserializeObject<List<RdsppInstance>>(jsonResponse)!;
//}

//public async Task<SensorData> PostAggregateData(Guid id)
//{
//string url = $"{_baseUri}instances/children";
//var payload = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, mediaType);

//HttpResponseMessage response = await _httpClient.PostAsync(url, payload);
//_logger.LogInformation($"Sent http POST request to url: {url}.");

//string jsonResponse = await response.Content.ReadAsStringAsync();
//_logger.LogInformation($"Http response: {jsonResponse}");

//if (!response.IsSuccessStatusCode)
//{
//    _logger.LogError($"POST request failed.");
//    return null;
//}

//return JsonConvert.DeserializeObject<Ecosystem>(jsonResponse)!;
//}

//public async Task<SensorData> PostSensorData(Guid id)
//{
//    //string url = $"{_baseUri}instances";
//var payload = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, mediaType);

//HttpResponseMessage response = await _httpClient.PostAsync(url, payload);
//_logger.LogInformation($"Sent http POST request to url: {url}.");

//string jsonResponse = await response.Content.ReadAsStringAsync();
//_logger.LogInformation($"Http response: {jsonResponse}");

//if (!response.IsSuccessStatusCode)
//{
//    _logger.LogError($"POST request failed.");
//    return null;
//}

//return JsonConvert.DeserializeObject<SensorData>(jsonResponse)!;
//    }
}
