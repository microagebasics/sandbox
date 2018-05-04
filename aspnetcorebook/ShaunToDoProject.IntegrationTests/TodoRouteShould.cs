using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShaunToDoProject.IntegrationTests
{
  public class TodoRouteShould : IClassFixture<TestFixture>
  {

    private readonly HttpClient _client;

    public TodoRouteShould(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task ChallengeAnonymousUser()
    {
      // Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

      // Act
      var response = await _client.SendAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
      Assert.Equal("http://localhost:5001/Account/Login?ReturnUrl=%2Ftodo", response.Headers.Location.ToString());

    }
  }
}
