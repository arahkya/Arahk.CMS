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
}