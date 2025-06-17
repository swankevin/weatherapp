using Xunit;

namespace WeatherApp.Tests;

public class UnitTest1
{
    [Fact]
    public void TestWeatherSuccess()
    {
        Assert.True(1 + 1 == 2);
    }

    [Fact]
    public void FailsOnPurpose()
    {
        Assert.Equal(1, 2); // will fail
    }
}