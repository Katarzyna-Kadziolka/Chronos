using System;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.ToDoTaskController {
    public class ToDoTaskPatchValidatorTests {

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