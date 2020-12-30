using System;
using Chronos.Models.Deadlines.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.DeadlineController {
    class DeadlinePatchValidatorTests {
        [Test]
        public void DeadlinePatchValidator_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new DeadlinePatchValidator();
            var deadlinePatch = TestData.Deadline.CreateDeadlinePatch();
            deadlinePatch.Name = string.Empty;
            // Act
            var result = validator.TestValidate(deadlinePatch);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Test]
        public void DeadlinePatchValidation_DateFromThePast_ShouldReturnError() {
            // Arrange
            var validator = new DeadlinePatchValidator();
            var deadlinePatch = TestData.Deadline.CreateDeadlinePatch();
            deadlinePatch.Date = DateTime.Today.AddDays(-1);
            // Act
            var result = validator.TestValidate(deadlinePatch);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Date.Date);
        }
    }
}