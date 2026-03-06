using Soenneker.HubSpot.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.HubSpot.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class HubSpotOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IHubSpotOpenApiClientUtil _openapiclientutil;

    public HubSpotOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IHubSpotOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
