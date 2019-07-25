// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Exceptions
{
    /// <summary>
    /// Represents an error in API document deserialization.
    /// </summary>
    public class ApiDeserializationException : ApiException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiDeserializationException()
        { }

        public ApiDeserializationException(string message)
            : base(message)
        { }

        public ApiDeserializationException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}