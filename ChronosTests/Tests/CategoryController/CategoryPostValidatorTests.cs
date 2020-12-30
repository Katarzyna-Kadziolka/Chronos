using Chronos.Models.Category.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.CategoryController {
    class CategoryPostValidatorTests {

        [Test]
        public void CategoryPostValidation_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new CategoryPostValidator();
            var categoryPost = TestData.Category.CreateCategoryPost();
            categoryPost.Name = string.Empty;
            // Act
            var result = validator.TestValidate(categoryPost);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
        }
    }
}