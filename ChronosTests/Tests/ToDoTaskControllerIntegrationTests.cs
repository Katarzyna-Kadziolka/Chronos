using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Chronos.Models.ToDoTasks;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests {
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
        public void ToDoPostValidation_EmptyText_ShouldReturnError() {
            var validator = new ToDoTaskPostValidator();
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.ToDoTaskText = string.Empty;

            var result = validator.TestValidate(toDoTaskPost);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.ToDoTaskText);
        }
    }
}