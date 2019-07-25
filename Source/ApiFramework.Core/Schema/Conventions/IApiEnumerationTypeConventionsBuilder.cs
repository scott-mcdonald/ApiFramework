// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a fluent-style builder of API enumeration type conventions.</summary>
    public interface IApiEnumerationTypeConventionsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds the built-in auto discovery of API enumeration type based annotations convention to the collection of API enumeration type conventions.</summary>
        /// <returns>A fluent-style API enumeration type conventions builder for the collection of API enumeration type conventions.</returns>
        IApiEnumerationTypeConventionsBuilder HasAnnotationDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API enumeration values convention to the collection of API enumeration type conventions.</summary>
        /// <returns>A fluent-style API enumeration type conventions builder for the collection of API enumeration type conventions.</returns>
        IApiEnumerationTypeConventionsBuilder HasEnumValueDiscoveryConvention();

        /// <summary>Adds a custom API enumeration type convention to the collection of API enumeration type conventions.</summary>
        /// <param name="convention">Convention to add to the collection of API enumeration type conventions.</param>
        /// <returns>A fluent-style API enumeration type conventions builder for the collection of API enumeration type conventions.</returns>
        IApiEnumerationTypeConventionsBuilder HasConvention(IApiEnumerationTypeConvention convention);
        #endregion
    }
}
