using System;
using System.Net.Http;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.ToDoTaskController
{
    public class ToDoTaskPatchValidatorTests
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
        public void ToDoTaskPatchValidator_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new ToDoTaskPatchValidator();
            var toDoTaskPatch = TestData.ToDoTask.CreateToDoTaskPatch();
            toDoTaskPatch.Name = string.Empty;
            // Act
            var result = validator.TestValidate(toDoTaskPatch);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
        }
        [Test]
        public void ToDoTaskPatchValidation_DateFromThePast_ShouldReturnError() {
            // Arrange
            var validator = new ToDoTaskPatchValidator();
            var toDoTaskPatch = TestData.ToDoTask.CreateToDoTaskPatch();
            toDoTaskPatch.Date = DateTime.Today.AddDays(-1);
            // Act
            var result = validator.TestValidate(toDoTaskPatch);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Date.Date);
        }
    }
}
