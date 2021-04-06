using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.Core
{
   public static class StringFormat
    {
        public static string FormatStandarDate(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }

        public static string FormatStandarDateTime(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy hh:mm AA");
        }
    }
}
