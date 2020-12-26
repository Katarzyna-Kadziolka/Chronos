using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Chronos.Models.Category;
using ChronosTests.Helpers;
using ChronosTests.Helpers.Data;
using FluentAssertions;
using NUnit.Framework;

namespace ChronosTests.Tests.CategoryController {
    class CategoryControllerIntegrationTests {
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
        public async Task CategoryPost_CorrectCategory_ShouldReturnNewCategory() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            // Act
            var response = await _client.PostAsJsonAsync("api/categories", categoryPost);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await response.Content.ReadFromJsonAsync<Category>();
            // Assert
            category.Id.Should().NotBeEmpty();
            category.Name.Should().Be(categoryPost.Name);

            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task DeleteCategory_CorrectId_ShouldReturnOk() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            var postResponse = await _client.PostAsJsonAsync("api/categories", categoryPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await postResponse.Content.ReadFromJsonAsync<Category>();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task DeleteCategory_BadId_ShouldReturnNotFound() {
            // Arrange 
            var id = new Guid();
            // Act
            var deleteResponse = await _client.DeleteAsync($"api/categories/{id}");
            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task DeleteCategory_CorrectIdRemoveSecondTime_ShouldReturnNotFound() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            var postResponse = await _client.PostAsJsonAsync("api/categories", categoryPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await postResponse.Content.ReadFromJsonAsync<Category>();
            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            // Act
            var secondDeleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            // Assert
            secondDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task GetCategories_ShouldReturnCategoriesList() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            var postResponse = await _client.PostAsJsonAsync("api/categories", categoryPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await postResponse.Content.ReadFromJsonAsync<Category>();
            // Act
            var getResponse = await _client.GetAsync("api/categories");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var categories = await getResponse.Content.ReadFromJsonAsync<List<Category>>();
            // Assert
            categories.Should().NotBeEmpty();
            categories.Should().ContainEquivalentOf(category);

            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetCategoriesById_CorrectId_ShouldReturnCategory() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            var postResponse = await _client.PostAsJsonAsync("api/categories", categoryPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await postResponse.Content.ReadFromJsonAsync<Category>();
            // Act
            var getResponse = await _client.GetAsync($"api/categories/{category.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var categoryResponse = await getResponse.Content.ReadFromJsonAsync<Category>();
            // Assert
            categoryResponse.Id.Should().Be(category.Id);
            categoryResponse.Name.Should().Be(category.Name);

            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task GetCategoriesById_BadId_ShouldReturnNotFound() {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var getResponse = await _client.GetAsync($"api/categories/{id}");
            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task PatchCategory_CorrectCategory_ShouldReturnUpdatedCategory() {
            // Arrange
            var categoryPost = TestData.Category.CreateCategoryPost();
            var postResponse = await _client.PostAsJsonAsync("api/categories", categoryPost);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var category = await postResponse.Content.ReadFromJsonAsync<Category>();
            // Act
            var categoryPatch = TestData.Category.CreateCategoryPatch();
            var patchResponse = await _client.PatchAsJsonAsync($"api/categories/{category.Id}", categoryPatch);
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await patchResponse.Content.ReadFromJsonAsync<Category>();
            // Assert
            responseContent.Name.Should().Be(categoryPatch.Name);

            var deleteResponse = await _client.DeleteAsync($"api/categories/{category.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}