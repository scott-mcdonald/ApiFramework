// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Validation
{
    /// <inheritdoc cref="IValidator{TMutableObject,T}"/>
    /// <summary>Abstract validator that is intended to be a base-class for creating concrete validators.</summary>
    public abstract class Validator<TMutableObject, T> : IValidator<TMutableObject, T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IValidator Implementation
        public Type InstanceType => typeof(T);
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IValidator<TMutableObject, T> Implementation
        public abstract ValidationResult Validate(TMutableObject context, T instance);
        #endregion
    }
}