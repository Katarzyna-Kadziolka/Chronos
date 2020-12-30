using System;
using Chronos.Models.Deadlines.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.DeadlineController {
    class DeadlinePostValidatorTests {
        
        [Test]
        public void DeadlinePostValidation_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new DeadlinePostValidator();
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Name = string.Empty;
            // Act
            var result = validator.TestValidate(deadlinePost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Test]
        public void DeadlinePostValidation_DateFromThePast_ShouldReturnError() {
            // Arrange
            var validator = new DeadlinePostValidator();
            var deadlinePost = TestData.Deadline.CreateDeadlinePost();
            deadlinePost.Date = DateTime.Today.AddDays(-1);
            // Act
            var result = validator.TestValidate(deadlinePost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Date.Date);
        }
    }
}