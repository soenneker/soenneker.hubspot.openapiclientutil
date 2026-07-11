using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HubSpot.Client.Abstract;
using Soenneker.HubSpot.OpenApiClient;
using Soenneker.HubSpot.OpenApiClientUtil.Abstract;
using Soenneker.Kiota.BearerAuthenticationProvider;

namespace Soenneker.HubSpot.OpenApiClientUtil;

///<inheritdoc cref="IHubSpotOpenApiClientUtil"/>
public sealed class HubSpotOpenApiClientUtil : IHubSpotOpenApiClientUtil
{
    private readonly SingletonDictionary<HubSpotOpenApiClient> _clients;
    private readonly IConfiguration _configuration;
    private readonly IHubSpotClientUtil _httpClientUtil;

    public HubSpotOpenApiClientUtil(IHubSpotClientUtil httpClientUtil, IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClientUtil = httpClientUtil;
        _clients = new SingletonDictionary<HubSpotOpenApiClient>(CreateClient);
    }

    private async ValueTask<HubSpotOpenApiClient> CreateClient(string accessToken, CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(accessToken, token).NoSync();

        var requestAdapter = new HttpClientRequestAdapter(new BearerAuthenticationProvider(accessToken), httpClient: httpClient);

        return new HubSpotOpenApiClient(requestAdapter);
    }

    public ValueTask<HubSpotOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        var accessToken = _configuration.GetValueStrict<string>("HubSpot:Token");

        return Get(accessToken, cancellationToken);
    }

    public ValueTask<HubSpotOpenApiClient> Get(string accessToken, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

        return _clients.Get(accessToken, cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _clients.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
