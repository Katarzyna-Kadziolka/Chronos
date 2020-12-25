using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chronos.Models.Category.Validators;
using Chronos.Models.ToDoTasks.Validators;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ChronosTests.Tests.CategoryController {
    class CategoryPatchValidatorTests {
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