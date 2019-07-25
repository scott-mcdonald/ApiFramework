// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Exceptions
{
    /// <summary>
    /// Represents an error in API document serialization.
    /// </summary>
    public class ApiSerializationException : ApiException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSerializationException()
        { }

        public ApiSerializationException(string message)
            : base(message)
        { }

        public ApiSerializationException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}