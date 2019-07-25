// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

// ReSharper disable CheckNamespace
namespace ApiFramework
{
    /// <summary>
    /// Extension methods for the .NET <see cref="String"/> class.
    /// </summary>
    public static class StringExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Indent a string object by the given indent width.
        /// </summary>
        public static string Indent(this string str, int indentWidth)
        {
            Contract.Requires(str != null);

            if (indentWidth <= 0)
                return str;

            var stringWidth = str.Length;
            var totalWidth = stringWidth + indentWidth;
            var indentedString = str.PadLeft(totalWidth);
            return indentedString;
        }

        /// <summary>
        /// Parse a string object into an enum.
        /// Throws an exception if unable to parse string into the specified enum.
        /// </summary>
        public static TEnum ParseEnum<TEnum>(this string str, bool ignoreCase = false)
            where TEnum : struct
        {
            Contract.Requires(str != null);

            return ignoreCase
                ? (TEnum)Enum.Parse(typeof(TEnum), str, true)
                : (TEnum)Enum.Parse(typeof(TEnum), str);
        }

        /// <summary>Remove all whitespace characters from the string.</summary>
        public static string RemoveWhitespace(this string str)
        {
            Contract.Requires(str != null);

            var strMinusWhitespaceCharacters = WhitespaceRegex.Replace(str, String.Empty);
            return strMinusWhitespaceCharacters;
        }

        /// <summary>
        /// Returns if string has any content meaning it is not null, not empty, and not consists only of white-space characters.
        /// </summary>
        public static bool SafeHasContent(this string str)
        {
            return !String.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Try and parse a string object into an enum.
        /// Returns true if successful, false otherwise.
        /// </summary>
        public static bool TryParseEnum<TEnum>(this string str, out TEnum result, bool ignoreCase = false)
            where TEnum : struct
        {
            Contract.Requires(str != null);

            return ignoreCase
                ? Enum.TryParse(str, true, out result)
                : Enum.TryParse(str, out result);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly Regex WhitespaceRegex = new Regex(@"\s", RegexOptions.CultureInvariant);
        #endregion
    }
}
