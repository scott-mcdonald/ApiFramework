// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a fluent-style builder of API object type conventions.</summary>
    public interface IApiObjectTypeConventionsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds the built-in auto discovery of API object type based annotations convention to the collection of API object type conventions.</summary>
        /// <returns>A fluent-style API object type conventions builder for the collection of API object type conventions.</returns>
        IApiObjectTypeConventionsBuilder HasAnnotationDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API object identity convention to the collection of API object type conventions.</summary>
        /// <returns>A fluent-style API object type conventions builder for the collection of API object type conventions.</returns>
        IApiObjectTypeConventionsBuilder HasIdentityDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API properties convention to the collection of API object type conventions.</summary>
        /// <returns>A fluent-style API object type conventions builder for the collection of API object type conventions.</returns>
        IApiObjectTypeConventionsBuilder HasPropertyDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API object relationships convention to the collection of API object type conventions.</summary>
        /// <returns>A fluent-style API object type conventions builder for the collection of API object type conventions.</returns>
        IApiObjectTypeConventionsBuilder HasRelationshipDiscoveryConvention();

        /// <summary>Adds a custom API object type convention to the collection of API object type conventions.</summary>
        /// <param name="convention">Convention to add to the collection of API object type conventions.</param>
        /// <returns>A fluent-style API object type conventions builder for the collection of API object type conventions.</returns>
        IApiObjectTypeConventionsBuilder HasConvention(IApiObjectTypeConvention convention);
        #endregion
    }
}
