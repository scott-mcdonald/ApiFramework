// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

// ReSharper disable CheckNamespace
namespace ApiFramework
{
    /// <summary>
    /// Extension methods for the .NET <see cref="Object"/> class.
    /// </summary>
    public static class ObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Returns the string result of the <c>ToString</c> method on an object even if the object is null.
        /// If the object is null, returns a null string.
        /// </summary>
        public static string SafeToString(this object source)
        {
            return source?.ToString();
        }

        /// <summary>
        /// Specialization of the <c>SafeToString</c> for an implementation of the <see cref="IFormattable"/> interface.
        /// </summary>
        public static string SafeToString(this IFormattable source, string format, IFormatProvider formatProvider)
        {
            return source?.ToString(format, formatProvider);
        }
        #endregion
    }
}
