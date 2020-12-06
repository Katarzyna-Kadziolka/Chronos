using System.Collections.Generic;
using Chronos.Models.ToDoTasks;

namespace Chronos.Data {
    public class ToDoTaskDb {
        public static List<ToDoTask> Tasks { get; } = new List<ToDoTask>();
    }
}