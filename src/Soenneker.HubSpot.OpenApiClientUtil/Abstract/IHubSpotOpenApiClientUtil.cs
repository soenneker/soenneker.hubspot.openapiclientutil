using Soenneker.HubSpot.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.HubSpot.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides cached HubSpot generated clients for one or more private app access tokens.
/// </summary>
public interface IHubSpotOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets a client authenticated with the configured <c>HubSpot:Token</c>.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<HubSpotOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client configured for a specific HubSpot private app access token.
    /// </summary>
    /// <param name="accessToken">The HubSpot private app access token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<HubSpotOpenApiClient> Get(string accessToken, CancellationToken cancellationToken = default);
}
