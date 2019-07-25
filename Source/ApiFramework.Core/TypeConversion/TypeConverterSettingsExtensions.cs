// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Globalization;

namespace ApiFramework.TypeConversion
{
    /// <summary>
    /// Extension methods built from the <c>TypeConverterSettings</c> class.
    /// </summary>
    public static class TypeConverterSettingsExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static string SafeGetFormat(this TypeConverterSettings settings)
        { return settings?.Format; }

        public static IFormatProvider SafeGetFormatProvider(this TypeConverterSettings settings)
        { return settings?.FormatProvider; }

        public static DateTimeStyles SafeGetDateTimeStyles(this TypeConverterSettings settings)
        { return settings?.DateTimeStyles ?? DefaultDateTimeStyles; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const DateTimeStyles DefaultDateTimeStyles = DateTimeStyles.RoundtripKind;
        #endregion
    }
}
