using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronos.Models.Deadlines.Requests {
    public class DeadlinePatch {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Category.Category Category { get; set; }
    }
}