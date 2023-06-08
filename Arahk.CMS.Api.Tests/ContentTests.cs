using System.Net;
using Arahk.CMS.Api.Models.Content;

namespace Arahk.CMS.Api.Tests;

public class ContentTests : IClassFixture<TestWebApplicationFactory>
{
    private TestWebApplicationFactory _factory;

    public ContentTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void CreateSuccessTest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/content");
        request.Content = JsonContent.Create(new CreateContentModel
        {
            Title = "Test Title",
            Message = "Test Message"
        });

        // Action        
        HttpResponseMessage responseMessage = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async void UpdateSuccessTest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, "/content");
        request.Content = JsonContent.Create(new EditContentModel
        {
            Id = Guid.Parse("a186c328-2f7d-4872-8f71-cabd985a7c83"),
            Title = "Test Title",
            Message = "Test Message"
        });

        // Action        
        HttpResponseMessage responseMessage = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }
}