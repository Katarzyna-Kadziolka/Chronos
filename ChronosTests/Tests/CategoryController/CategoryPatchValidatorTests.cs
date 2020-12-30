using Chronos.Models.Category.Validators;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.CategoryController {
    class CategoryPatchValidatorTests {

        [Test]
        public void CategoryPatchValidator_EmptyText_ShouldReturnError() {
            // Arrange
            var validator = new CategoryPatchValidator();
            var categoryPatch = TestData.Category.CreateCategoryPatch();
            categoryPatch.Name = string.Empty;
            // Act
            var result = validator.TestValidate(categoryPatch);
            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(a => a.Name);
        }
    }
}