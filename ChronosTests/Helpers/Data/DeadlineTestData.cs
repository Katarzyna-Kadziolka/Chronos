using System;
using Chronos.Models.Deadlines.Requests;

namespace ChronosTests.Helpers.Data {
    public class DeadlineTestData {
        public DeadlinePost CreateDeadlinePost() {
            return new DeadlinePost() {
                Name = "Test" + Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }

        public DeadlinePatch CreateDeadlinePatch() {
            return new DeadlinePatch() {
                Name = "Test" + Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1)
            };
        }
    }
}