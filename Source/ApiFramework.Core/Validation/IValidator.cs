// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Validation
{
    /// <summary>Represents a validator for a specific instance type.</summary>
    public interface IValidator
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the instance type the validator can validate against.</summary>
        Type InstanceType { get; }
        #endregion
    }

    /// <inheritdoc cref="IValidator"/>
    /// <typeparam name="TMutableObject">The context type to help in validating the instance.</typeparam>
    /// <typeparam name="T">The instance type to validate</typeparam>
    public interface IValidator<in TMutableObject, in T> : IValidator
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Validates the instance.</summary>
        /// <param name="context">The context to help in validating the instance.</param>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>An <c>ApiValidationResult</c> object that encapsulates the result of validating the instance.</returns>
        ValidationResult Validate(TMutableObject context, T instance);
        #endregion
    }
}
