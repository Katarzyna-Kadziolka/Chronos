using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Chronos.Models.Deadlines;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentAssertions.Common;
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

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task DeleteDeadline_CorrectId_ShouldReturnOk() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task DeleteDeadline_BadId_ShouldReturnNotFound() {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task DeleteDeadline_CorrectIdRemoveSecondTime_ShouldReturnNotFound() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            // Act
            var secondDeleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            // Assert
            secondDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task GetDeadlines_DateFromNullDateToNull_ShouldReturnDeadlinesList() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlines = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            // Act
            var getResponse = await _client.GetAsync("api/deadlines");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlinesResponse = await getResponse.Content.ReadFromJsonAsync<List<Deadline>>();
            // Assert
            deadlinesResponse.Should().NotBeEmpty();
            deadlinesResponse.Should().ContainEquivalentOf(deadlines);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadlines.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlines_DateFromTodayDateToNull_ShouldReturnDeadlinesList() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/deadlines/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlines = await getResponse.Content.ReadFromJsonAsync<List<Deadline>>();
            // Assert
            deadlines.Should().NotBeEmpty();
            deadlines.Should().ContainEquivalentOf(deadline);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlines_DateFromNullDateToToday_ShouldReturnDeadlinesList() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            var query = new Dictionary<string, string> {
                ["dateTo"] = DateTime.Today.ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/deadlines/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlines = await getResponse.Content.ReadFromJsonAsync<List<Deadline>>();
            // Assert
            deadlines.Should().NotBeEmpty();
            deadlines.Should().ContainEquivalentOf(deadline);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlines_DateFromTodayDateToTomorrow_ShouldReturnDeadlinesList() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s"),
                ["dateTo"] = DateTime.Today.Tomorrow().ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/deadlines/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlines = await getResponse.Content.ReadFromJsonAsync<List<Deadline>>();
            // Assert
            deadlines.Should().NotBeEmpty();
            deadlines.Should().ContainEquivalentOf(deadline);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlines_DateFromTomorrowDateToNull_ShouldReturnBadRequest() {
            // Arrange
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.Tomorrow().ToString("s"),
            };
            var expectedError = $"DateTo {DateTime.Today} cannot be before dateFrom {DateTime.Today.Tomorrow()}.";
            // Act
            var getResponse = await _client.GetAsync("api/deadlines/", query);
            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await getResponse.Content.ReadAsStringAsync();
            error.Should().Be(expectedError);
        }
        [Test]
        public async Task GetDeadlines_DateFromTodayDateToTomorrowDeadlineNotInRange_ShouldReturnEmptyDeadlinesList() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today.AddDays(2);
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s"),
                ["dateTo"] = DateTime.Today.Tomorrow().ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/deadlines/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadlines = await getResponse.Content.ReadFromJsonAsync<List<Deadline>>();
            // Assert
            deadlines.Should().BeEmpty();

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlineById_CorrectId_ShouldReturnDeadline() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            // Act
            var getResponse = await _client.GetAsync($"api/deadlines/{deadline.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var getsDeadline = await getResponse.Content.ReadFromJsonAsync<Deadline>();
            // Assert
            getsDeadline.Should().IsSameOrEqualTo(deadline);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetDeadlineById_BadId_ShouldReturnNotFound() {
            // Arrange 
            var id = new Guid();
            // Act
            var getResponse = await _client.GetAsync($"api/deadlines/{id}");
            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task PatchDeadline_CorrectDeadline_ShouldReturnUpdatedDeadline() {
            // Arrange
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/deadlines", deadlinePost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deadline = await postResponse.Content.ReadFromJsonAsync<Deadline>();
            // Act
            var deadlinePatch = TestData.Deadline.CreateDeadlinePatch();
            var patchResponse = await _client.PatchAsJsonAsync($"api/deadlines/{deadline.Id}", deadlinePatch);
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await patchResponse.Content.ReadFromJsonAsync<Deadline>();
            // Assert
            responseContent.Name.Should().Be(deadlinePatch.Name);
            responseContent.Date.Should().Be(deadlinePatch.Date);
            responseContent.Category.Should().Be(deadlinePatch.Category);

            var deleteResponse = await _client.DeleteAsync($"api/deadlines/{deadline.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
