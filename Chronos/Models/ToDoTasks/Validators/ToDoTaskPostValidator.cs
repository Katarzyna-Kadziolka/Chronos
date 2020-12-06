using System;
using Chronos.Models.ToDoTasks.Requests;
using FluentValidation;

namespace Chronos.Models.ToDoTasks.Validators {
    public class ToDoTaskPostValidator : AbstractValidator<ToDoTaskPost> {
        public ToDoTaskPostValidator() {
            RuleFor(a => a.ToDoTaskText).NotEmpty();
            RuleFor(a => a.Date.Date).GreaterThanOrEqualTo(DateTime.Now.Date);
        }
    }
}