using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Newtonsoft.Json;
using System.Text;
using AuthMicroservice.Model;


public class AuthServiceTestWireMock : IAsyncLifetime
{
    private WireMockServer _authServiceMockServer;

    public async Task InitializeAsync()
    {
        _authServiceMockServer = WireMockServer.Start(9090); 
        _authServiceMockServer.Given(Request.Create().WithPath("/Register").UsingPost())
            .RespondWith(Response.Create().WithStatusCode(200).WithBody("{\"result\":\"User created\"}"));
    }

    [Fact]
    public async Task Register_ShouldReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var httpClient = new HttpClient();
        var userDto = new UserDto
        {
          
            Username = "testuser",
            Email = "test@example.com",
            password = "password123"
        };

        string json = JsonConvert.SerializeObject(userDto);
        StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await httpClient.PostAsync($"{_authServiceMockServer.Urls[0]}/Register", httpContent);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Contains("User created", responseString);
        
    }

    public async Task DisposeAsync()
    {
        _authServiceMockServer?.Stop();
    }
}