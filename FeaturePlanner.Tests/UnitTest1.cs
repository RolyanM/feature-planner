using FeaturePlanner.Web.Services;
using Microsoft.Extensions.Options;

namespace FeaturePlanner.Tests;

public class AzureOpenAiServiceTests
{
    [Fact]
    public void ServiceCanBeInstantiatedWithValidConfiguration()
    {
        // Arrange
        var options = new AzureOpenAiOptions
        {
            Endpoint = "https://test.openai.azure.com/",
            DeploymentName = "test-deployment",
            ApiKey = "test-key-12345"
        };
        var optionsWrapper = Options.Create(options);

        // Act
        var service = new AzureOpenAiService(optionsWrapper);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void ServiceThrowsWhenEndpointMissing()
    {
        // Arrange
        var options = new AzureOpenAiOptions
        {
            Endpoint = "", // Empty endpoint
            DeploymentName = "test-deployment",
            ApiKey = "test-key-12345"
        };
        var optionsWrapper = Options.Create(options);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new AzureOpenAiService(optionsWrapper));
    }

    [Fact]
    public void ServiceThrowsWhenDeploymentNameMissing()
    {
        // Arrange
        var options = new AzureOpenAiOptions
        {
            Endpoint = "https://test.openai.azure.com/",
            DeploymentName = "", // Empty deployment name
            ApiKey = "test-key-12345"
        };
        var optionsWrapper = Options.Create(options);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new AzureOpenAiService(optionsWrapper));
    }

    [Fact]
    public void ServiceThrowsWhenApiKeyMissing()
    {
        // Arrange
        var options = new AzureOpenAiOptions
        {
            Endpoint = "https://test.openai.azure.com/",
            DeploymentName = "test-deployment",
            ApiKey = "" // Empty API key
        };
        var optionsWrapper = Options.Create(options);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new AzureOpenAiService(optionsWrapper));
    }
}