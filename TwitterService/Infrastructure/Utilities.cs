using System;
using System.Text.RegularExpressions;

namespace TwitterService.Infrastructure {

    /// <summary>
    /// Utilities for validations
    /// </summary>
    /// <!-- Created by: David Rivera -->
    internal sealed class Utilities {

        /// <summary>
        /// Determines whether <para>Value</para> contains some data and returns <c>true</ c> if it is empty.
        /// </summary>
        /// <param name="Value">Object to validate</param>
        /// <returns>Returns <c>true</c> if it is empty, otherwise <c>false</c></returns>
        internal static bool IsNullObject(object Value) {
            if (Value == null || System.Convert.IsDBNull(Value)) { return true; }
            if (Value.GetType() == typeof(string) && String.IsNullOrWhiteSpace((string)Value)) { return true; }
            return false;
        }
        /// <summary>
        /// Validates whether <para>Value</para> is of type <c>string</c>
        /// </summary>
        /// <param name="Value">Object to validate</param>
        /// <returns>Returns the text without leading and trailing spaces</returns>
        internal static string CheckString(object Value) {
            if (IsNullObject(Value)) { return ""; }
            return Value.ToString().Trim();
        }

        /// <summary>
        /// Validates whether <para>Value</para> is a numeric <c>string</c>
        /// </summary>
        /// <param name="Value">Object to validate</param>
        /// <returns>Returns the numeric <c>string</c></returns>
        internal static string CheckNumber(object Value) {
            string sNumero = CheckString(Value);
            Regex reg = new Regex(@"^(-?\d+(?:\.\d+)?).*");
            sNumero = Regex.Replace(sNumero, @"\s|,", "");
            if (!reg.IsMatch(sNumero)) return "0";
            return reg.Replace(sNumero, "$1");
        }

    }//end class
}//end namespace