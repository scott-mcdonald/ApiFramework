// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown for an invalid operation in the API schema component.
    /// </summary>
    public class ApiSchemaException : ApiException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSchemaException()
        { }

        public ApiSchemaException(string message)
            : base(message)
        { }

        public ApiSchemaException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}