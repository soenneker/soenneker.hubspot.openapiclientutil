using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HubSpot.Client.Abstract;
using Soenneker.HubSpot.OpenApiClient;
using Soenneker.HubSpot.OpenApiClientUtil.Abstract;
using Soenneker.Kiota.BearerAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.HubSpot.OpenApiClientUtil;

///<inheritdoc cref="IHubSpotOpenApiClientUtil"/>
public sealed class HubSpotOpenApiClientUtil : IHubSpotOpenApiClientUtil
{
    private readonly AsyncSingleton<HubSpotOpenApiClient> _client;
    private readonly IConfiguration _configuration;
    private readonly IHubSpotClientUtil _httpClientUtil;

    public HubSpotOpenApiClientUtil(IHubSpotClientUtil httpClientUtil, IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClientUtil = httpClientUtil;
        _client = new AsyncSingleton<HubSpotOpenApiClient>(CreateClient);
    }

    private async ValueTask<HubSpotOpenApiClient> CreateClient(CancellationToken token)
    {
        var apiKey = _configuration.GetValueStrict<string>("HubSpot:Token");

        HttpClient httpClient = await _httpClientUtil.Get(token).NoSync();

        var requestAdapter = new HttpClientRequestAdapter(new BearerAuthenticationProvider(apiKey), httpClient: httpClient);

        return new HubSpotOpenApiClient(requestAdapter);
    }

    public ValueTask<HubSpotOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}