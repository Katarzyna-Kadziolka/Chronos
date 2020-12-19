using System;

namespace ChronosTests.Helpers {
    public static class DateTimeExtensions {
        public static DateTime Tomorrow(this DateTime dateTime) {
            return DateTime.Today.AddDays(1);
        }

        public static DateTime Yesterday(this DateTime dateTime) {
            return DateTime.Today.AddDays(-1);
        }

    }
}