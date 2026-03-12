using Soenneker.HubSpot.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.HubSpot.OpenApiClientUtil.Abstract;

/// <summary>
/// A .NET thread-safe singleton HttpClient for 
/// </summary>
public interface IHubSpotOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<HubSpotOpenApiClient> Get(CancellationToken cancellationToken = default);
}