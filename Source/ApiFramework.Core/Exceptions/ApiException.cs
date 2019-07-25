// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Exceptions
{
    /// <summary>
    /// Represents errors that occur during API framework execution.
    /// </summary>
    /// <remarks>
    /// Intended primarily to be a base-class for more concrete exceptions.
    ///
    /// TBD: Will need to include API Error object management when available.
    /// </remarks>
    public class ApiException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiException()
        { }

        public ApiException(string message)
            : base(message)
        { }

        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}