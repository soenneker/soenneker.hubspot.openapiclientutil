using Soenneker.HubSpot.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.HubSpot.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class HubSpotOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IHubSpotOpenApiClientUtil _openapiclientutil;

    public HubSpotOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IHubSpotOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
