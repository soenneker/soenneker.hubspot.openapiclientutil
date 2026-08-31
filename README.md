[![](https://img.shields.io/nuget/v/soenneker.hubspot.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.hubspot.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.hubspot.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.hubspot.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.hubspot.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.hubspot.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.hubspot.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.hubspot.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.HubSpot.OpenApiClientUtil

Create and reuse authenticated HubSpot generated clients for one or more private apps.

## Install

```bash
dotnet add package Soenneker.HubSpot.OpenApiClientUtil
```

## Configuration

```json
{
  "HubSpot": {
    "Token": "<private app access token>"
  }
}
```

The parameterless `Get()` requires `HubSpot:Token`. You can omit it when every call supplies a token explicitly.

## Register

```csharp
using Soenneker.HubSpot.OpenApiClientUtil.Registrars;

services.AddHubSpotOpenApiClientUtilAsScoped();
```

The scoped utility deliberately keeps `IHubSpotClientUtil` singleton. Disposing a scope releases that utility's generated-client cache without tearing down the long-lived HTTP clients used by later scopes. Use `AddHubSpotOpenApiClientUtilAsSingleton()` when the generated-client cache should also live for the application lifetime.

## Usage

```csharp
using Soenneker.HubSpot.OpenApiClient;
using Soenneker.HubSpot.OpenApiClient.Models;
using Soenneker.HubSpot.OpenApiClientUtil.Abstract;

HubSpotOpenApiClient client = await clientUtil.Get(cancellationToken);

CollectionResponsePublicOwnerForwardPaging? owners =
    await client.Crm.Owners.TwoZeroTwoSixZeroThree.GetAsync(
        cancellationToken: cancellationToken);
```

To work with multiple HubSpot accounts, pass each token explicitly:

```csharp
HubSpotOpenApiClient tenantClient = await clientUtil.Get(
    tenantAccessToken,
    cancellationToken);
```

Calls using the same token on the same utility instance reuse the generated client. Different tokens receive separate generated clients and separate authenticated HTTP clients.

Authentication is applied by the underlying HTTP provider; the Kiota adapter does not add a second bearer header. Let the service container dispose the utility and provider rather than disposing cached clients directly.
