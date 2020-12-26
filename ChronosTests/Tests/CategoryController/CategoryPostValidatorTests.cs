using System.Net.Http;
using Chronos.Models.Category.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.CategoryController {
    class CategoryPostValidatorTests {
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