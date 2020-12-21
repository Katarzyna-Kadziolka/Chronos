using System;
using Chronos.Models.Category.Requests;

namespace ChronosTests.Helpers.Data {
    public class CategoryTestData {
        public CategoryPost CreateCategoryPost() {
            return new CategoryPost {
                Name = "Test" + Guid.NewGuid()
            };
        }

        public CategoryPatch CreateCategoryPatch() {
            return new CategoryPatch {
                Name = "Test" + Guid.NewGuid()
            };
        }
    }
}