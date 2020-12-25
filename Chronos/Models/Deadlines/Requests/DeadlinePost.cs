using System;

namespace Chronos.Models.Deadlines.Requests {
    public class DeadlinePost {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Category.Category Category { get; set; }
    }
}