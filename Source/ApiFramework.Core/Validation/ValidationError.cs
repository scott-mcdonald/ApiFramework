// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Validation
{
    /// <summary>Represents a single validation error.</summary>
    public class ValidationError
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a validation error with the given message.</summary>
        /// <param name="message">The message to create the validation error with.</param>
        public ValidationError(string message)
        {
            Contract.Requires(message.SafeHasContent());

            this.Message = message;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the message for the validation error.</summary>
        public string Message { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var typeName = this.GetType().Name;
            return $"{typeName} [message=\"{this.Message}\"]";
        }
        #endregion
    }
}
