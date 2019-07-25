// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a naming convention to apply when building API names.</summary>
    public interface IApiNamingConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Create a new name by applying a naming convention to an old name.</summary>
        /// <param name="oldName">The old name to consume and apply the naming convention to.</param>
        /// <param name="apiConventionSettings">Optional API convention settings used to build and create an API schema, can be null.</param>
        /// <returns>The new name produced by applying the naming convention.</returns>
        string Apply(string oldName, ApiConventionSettings apiConventionSettings);
        #endregion
    }
}
