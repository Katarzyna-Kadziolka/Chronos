using System.Collections.Generic;
using Chronos.Models.Category;
using Chronos.Models.ToDoTasks;

namespace Chronos.Data {
    public class ChronosDb {
        public static List<ToDoTask> Tasks { get; } = new List<ToDoTask>();
        public static List<Category> Categories { get; } = new List<Category>();
    }
}