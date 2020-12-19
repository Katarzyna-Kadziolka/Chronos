using System;

namespace Chronos.Models.ToDoTasks.Requests {
    public class ToDoTaskPost {
        public string ToDoTaskText { get; set; }
        public DateTime Date { get; set; }
        public Category.Category Category { get; set; }
    }
}