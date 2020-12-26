﻿using System;

namespace Chronos.Models.Deadlines
{
    public class Deadline
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Category.Category Category { get; set; }
    }
}
