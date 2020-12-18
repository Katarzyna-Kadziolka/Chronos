using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronos.Models.ToDoTasks.Requests {
    public class ToDoTaskPatch {
        public string ToDoTaskText { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
    }
}