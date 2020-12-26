using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Chronos.Models.ToDoTasks;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;

namespace ChronosTests.Tests.ToDoTaskController {
    public class ToDoTaskControllerIntegrationTests {
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
        public async Task Post_CorrectToDoTask_ShouldReturnNewToDoTask() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            // Act
            var response = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await response.Content.ReadFromJsonAsync<ToDoTask>();
            // Assert
            toDoTask.Id.Should().NotBeEmpty();
            toDoTask.Name.Should().Be(toDoTaskPost.Name);
            toDoTask.Date.Should().Be(toDoTaskPost.Date);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetToDoTasks_DateFromNullDateToNull_ShouldReturnToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().NotBeEmpty();
            tasks.Should().ContainEquivalentOf(toDoTask);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetToDoTasks_DateFromTodayDateToNull_ShouldReturnToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().NotBeEmpty();
            tasks.Should().ContainEquivalentOf(toDoTask);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetToDoTasks_DateFromNullDateToToday_ShouldReturnToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            var query = new Dictionary<string, string> {
                ["dateTo"] = DateTime.Today.ToString("s")
            };
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().NotBeEmpty();
            tasks.Should().ContainEquivalentOf(toDoTask);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetToDoTasks_DateFromTodayDateToTomorrow_ShouldReturnToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today.Tomorrow();
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s"),
                ["dateTo"] = DateTime.Today.Tomorrow().ToString("s")
            }; 
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().NotBeEmpty();
            tasks.Should().ContainEquivalentOf(toDoTask);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetToDoTasks_DateFromTomorrowDateToNull_ShouldReturnBadRequest() {
            // Arrange
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.Tomorrow().ToString("s"),
            };
            var expectedError = $"DateTo {DateTime.Today} cannot be before dateFrom {DateTime.Today.Tomorrow()}.";
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await getResponse.Content.ReadAsStringAsync();
            error.Should().Be(expectedError);
        }
        [Test]
        public async Task GetToDoTasks_DateFromTodayDateToTomorrowTaskNotInRange_ShouldReturnEmptyToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today.AddDays(2);
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s"),
                ["dateTo"] = DateTime.Today.Tomorrow().ToString("s")
            }; 
            // Act
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().BeEmpty();

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task DeleteTask_CorrectId_ShouldReturnOk() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task DeleteTask_BadId_ShouldReturnNotFound() {
            // Arrange 
            var id = new Guid();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task DeleteTask_CorrectIdRemoveSecondTime_ShouldReturnNotFound() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            // Act
            var secondDeleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            // Assert
            secondDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetToDoTaskById_CorrectId_ShouldReturnToDoTask() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Act
            var getResponse = await _client.GetAsync($"api/toDoTask/{toDoTask.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = await getResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Assert
            task.Should().IsSameOrEqualTo(toDoTask);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetTaskById_BadId_ShouldReturnNotFound() {
            // Arrange 
            var id = new Guid();
            // Act
            var getResponse = await _client.GetAsync($"api/toDoTask/{id}");
            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task PatchTask_CorrectToDoTask_ShouldReturnUpdatedToDoTask() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Act
            var toDoTaskPatch = TestData.ToDoTask.CreateToDoTaskPatch();
            var patchResponse = await _client.PatchAsJsonAsync($"api/toDoTask/{toDoTask.Id}", toDoTaskPatch);
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await patchResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Assert
            responseContent.Name.Should().Be(toDoTaskPatch.Name);
            responseContent.Date.Should().Be(toDoTaskPatch.Date);
            responseContent.Category.Should().Be(toDoTaskPatch.Category);

            var deleteResponse = await _client.DeleteAsync($"api/toDoTask/{toDoTask.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}