using System.Net;
using Arahk.CMS.Api.Models.Content;
using Arahk.CMS.Infrastructure.Persistants;

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

        HttpRequestMessage request = new(HttpMethod.Post, "/content");
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

        HttpRequestMessage request = new(HttpMethod.Patch, "/content");
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

    [Fact]
    public async void ListSuccessTest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        HttpRequestMessage request = new(HttpMethod.Get, "/content/list");

        // Action
        HttpResponseMessage responseMessage = await client.SendAsync(request);
        IEnumerable<ContentListItemModel>? items = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<ContentListItemModel>>();

        // Assert
        Assert.NotNull(items);
        Assert.Equal(1, items.Count());
        Assert.Equal("a186c328-2f7d-4872-8f71-cabd985a7c83", items.First().Id.ToString());
        Assert.Equal("Existed Title", items.First().Title);
        Assert.Equal("Existed Message", items.First().Message);
    }

    [Fact]
    public async void DeleteSuccessTest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        HttpRequestMessage request = new(HttpMethod.Delete, $"/content/a186c328-2f7d-4872-8f71-cabd985a7c83");

        // Action
        HttpResponseMessage responseMessage = await client.SendAsync(request);        

        // Assert
        HttpRequestMessage requestList = new(HttpMethod.Get, "/content/list");
        HttpResponseMessage responseListMessage = await client.SendAsync(requestList);
        IEnumerable<ContentListItemModel>? items = await responseListMessage.Content.ReadFromJsonAsync<IEnumerable<ContentListItemModel>>();

        Assert.NotNull(items);
        Assert.Empty(items);
    }
}
