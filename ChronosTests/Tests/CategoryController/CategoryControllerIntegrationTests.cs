using System.Net.Http;
using ChronosTests.Helpers;
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
    }
}