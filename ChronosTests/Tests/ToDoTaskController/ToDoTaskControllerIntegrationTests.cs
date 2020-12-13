using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Chronos.Models.ToDoTasks;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
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
            toDoTask.ToDoTaskText.Should().Be(toDoTaskPost.ToDoTaskText);
            toDoTask.Date.Should().Be(toDoTaskPost.Date);
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
        }
        [Test]
        public async Task GetToDoTasks_DateFromTodayDateToNull_ShouldReturnToDoTasksList() {
            // Arrange
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today;
            var postResponse = await _client.PostAsJsonAsync("api/toDoTask", toDoTaskPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var toDoTask = await postResponse.Content.ReadFromJsonAsync<ToDoTask>();
            // Act
            var query = new Dictionary<string, string> {
                ["dateFrom"] = DateTime.Today.ToString("s")
            };
            var getResponse = await _client.GetAsync("api/toDoTask/", query);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await getResponse.Content.ReadFromJsonAsync<List<ToDoTask>>();
            // Assert
            tasks.Should().NotBeEmpty();
            tasks.Should().ContainEquivalentOf(toDoTask);
        }
    }
}