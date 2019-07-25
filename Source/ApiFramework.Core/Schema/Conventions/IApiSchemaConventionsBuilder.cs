// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a fluent-style builder of API schema conventions.</summary>
    public interface IApiSchemaConventionsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds the built-in auto discovery of API annotations convention to the collection of API schema conventions.</summary>
        /// <returns>A fluent-style API schema conventions builder for the collection of API schema conventions.</returns>
        IApiSchemaConventionsBuilder HasAnnotationDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API configurations convention to the collection of API schema conventions.</summary>
        /// <returns>A fluent-style API schema conventions builder for the collection of API schema conventions.</returns>
        IApiSchemaConventionsBuilder HasConfigurationDiscoveryConvention();

        /// <summary>Adds the built-in auto discovery of API types convention to the collection of API schema conventions.</summary>
        /// <returns>A fluent-style API schema conventions builder for the collection of API schema conventions.</returns>
        IApiSchemaConventionsBuilder HasTypeDiscoveryConvention();

        /// <summary>Adds a custom API schema convention to the collection of API schema conventions.</summary>
        /// <param name="convention">Convention to add to the collection of API schema conventions.</param>
        /// <returns>A fluent-style API schema conventions builder for the collection of API schema conventions.</returns>
        IApiSchemaConventionsBuilder HasConvention(IApiSchemaConvention convention);
        #endregion
    }
}
