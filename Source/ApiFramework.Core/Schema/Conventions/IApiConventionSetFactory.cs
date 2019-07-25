// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a factory for the creation of a set of API conventions used for building and creating an API schema.</summary>
    public interface IApiConventionSetFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Factory method that creates API conventions.</summary>
        /// <returns>An API conventions instance.</returns>
        IApiConventionSet Create();
        #endregion
    }
}
