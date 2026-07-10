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
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<HubSpotOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client configured for a specific HubSpot private app access token.
    /// </summary>
    /// <param name="accessToken">The HubSpot private app access token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<HubSpotOpenApiClient> Get(string accessToken, CancellationToken cancellationToken = default);
}
