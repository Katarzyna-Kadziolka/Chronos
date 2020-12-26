using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Chronos.Models.Deadlines;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using NUnit.Framework;

namespace ChronosTests.Tests.DeadlineController
{
    public class DeadlineControllerIntegrationTests
    {
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup() {
            var factory = new ChronosWebApplicationFactory();
            _client = factory.CreateClient();
        }

        [OneTimeTearDown]
        public void CleanUp() {
            _client.Dispose();
        }

        [Test]
        public async Task PostDeadline_CorrectToDoTask_ShouldReturnNewToDoTask() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            // Act
            var response = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await response.Content.ReadFromJsonAsync<Deadline>();
            // Assert
            deadline.Id.Should().NotBeEmpty();
            deadline.Name.Should().Be(deadlinePost.Name);
            deadline.Date.Should().Be(deadlinePost.Date);

            //var deleteResponse = await _client.DeleteAsync($"api/deadline/{deadline.Id}");
            //deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
