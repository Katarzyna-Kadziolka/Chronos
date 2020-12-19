using System;

namespace Chronos.Models.ToDoTasks {
    public class ToDoTask {
        public Guid Id { get; set; }
        public string ToDoTaskText { get; set; }
        public DateTime Date { get; set; }
        public Category.Category Category { get; set; }
    }
}