using System;
using Chronos.Models.ToDoTasks.Requests;
using FluentValidation;

namespace Chronos.Models.ToDoTasks.Validators
{
    public class ToDoTaskPatchValidator : AbstractValidator<ToDoTaskPatch>
    {
        public ToDoTaskPatchValidator() {
            RuleFor(a => a.ToDoTaskText).NotEmpty();
            RuleFor(a => a.Date.Date).GreaterThanOrEqualTo(DateTime.Today.Date);
        }
    }
}
