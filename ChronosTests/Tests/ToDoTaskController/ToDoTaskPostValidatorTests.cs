using System;
using System.Net.Http;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.ToDoTaskController {
    public class ToDoTaskPostValidatorTests {
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
        public void ToDoPostValidation_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new ToDoTaskPostValidator();
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.ToDoTaskText = string.Empty;
            // Act
            var result = validator.TestValidate(toDoTaskPost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.ToDoTaskText);
        }

        [Test]
        public void ToDoPostValidation_DateFromThePast_ShouldReturnError() {
            // Arrange
            var validator = new ToDoTaskPostValidator();
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Date = DateTime.Today.AddDays(-1);
            // Act
            var result = validator.TestValidate(toDoTaskPost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Date.Date);
        }
    }
}