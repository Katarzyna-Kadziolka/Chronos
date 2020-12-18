using System;
using Chronos.Models.ToDoTasks.Requests;

namespace ChronosTests.Helpers.Data {
    public class ToDoTaskTestData {
        public ToDoTaskPost CreateToDoTaskPost() {
            return new ToDoTaskPost {
                ToDoTaskText = "Test"+Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }

        public ToDoTaskPatch CreateDoTaskPatch() {
            return new ToDoTaskPatch {
                ToDoTaskText = "Test"+Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }
    }
}