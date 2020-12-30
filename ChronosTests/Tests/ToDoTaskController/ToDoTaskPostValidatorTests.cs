using System;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.ToDoTaskController {
    public class ToDoTaskPostValidatorTests {

        [Test]
        public void ToDoPostValidation_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new ToDoTaskPostValidator();
            var toDoTaskPost = TestData.ToDoTask.CreateToDoTaskPost();
            toDoTaskPost.Name = string.Empty;
            // Act
            var result = validator.TestValidate(toDoTaskPost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
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