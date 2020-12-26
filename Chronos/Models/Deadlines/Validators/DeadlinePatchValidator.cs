using System;
using Chronos.Models.Deadlines.Requests;
using FluentValidation;

namespace Chronos.Models.Deadlines.Validators {
    public class DeadlinePatchValidator : AbstractValidator<DeadlinePatch> {
        public DeadlinePatchValidator() {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Date.Date).GreaterThanOrEqualTo(DateTime.Today.Date);
        }
    }
}