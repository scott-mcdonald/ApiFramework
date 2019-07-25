// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of an API enumeration value.
    /// </summary>
    /// <remarks>
    /// API enumeration value configuration created by the fluent-style builder will take precedence over any API enumeration value configuration created by convention.
    /// </remarks>
    public interface IApiEnumerationValueBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the required name of the API enumeration value.</summary>
        /// <param name="apiName">Name to set the API enumeration value with.</param>
        /// <returns>A fluent-style builder for the API enumeration value.</returns>
        IApiEnumerationValueBuilder HasName(string apiName);

        /// <summary>Sets the optional description of the API enumeration value.</summary>
        /// <param name="apiDescription">Description to set the API enumeration value with.</param>
        /// <returns>A fluent-style builder for the API enumeration value.</returns>
        IApiEnumerationValueBuilder HasDescription(string apiDescription);
        #endregion
    }
}
