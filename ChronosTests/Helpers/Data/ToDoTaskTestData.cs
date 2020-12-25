using System;
using Chronos.Models.ToDoTasks.Requests;

namespace ChronosTests.Helpers.Data {
    public class ToDoTaskTestData {
        public ToDoTaskPost CreateToDoTaskPost() {
            return new ToDoTaskPost {
                Name = "Test"+Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }

        public ToDoTaskPatch CreateToDoTaskPatch() {
            return new ToDoTaskPatch {
                ToDoTaskText = "Test"+Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }
    }
}