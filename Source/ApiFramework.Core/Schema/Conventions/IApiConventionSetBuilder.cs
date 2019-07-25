// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a fluent-style builder for a set of API conventions used for building and creating an API schema.</summary>
    public interface IApiConventionSetBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Builds conventions for an API enumeration type by invoking builder methods on an API enumeration type configuration object.</summary>
        /// <returns>A fluent-style API enumeration type conventions builder for an API enumeration type.</returns>
        IApiEnumerationTypeConventionsBuilder ApiEnumerationTypeConventions();

        /// <summary>Builds naming conventions for API name of an API enumeration type.</summary>
        /// <returns>A fluent-style API naming conventions builder for API name of an API enumeration type.</returns>
        IApiNamingConventionsBuilder ApiEnumerationTypeNameConventions();

        /// <summary>Builds naming conventions for API name of an API enumeration value.</summary>
        /// <returns>A fluent-style API naming conventions builder for API name of an API enumeration value.</returns>
        IApiNamingConventionsBuilder ApiEnumerationValueNameConventions();

        /// <summary>Builds conventions for an API object type by invoking builder methods on an API object type configuration object.</summary>
        /// <returns>A fluent-style API object type conventions builder for an API object type.</returns>
        IApiObjectTypeConventionsBuilder ApiObjectTypeConventions();

        /// <summary>Builds naming conventions for API name of an API object type.</summary>
        /// <returns>A fluent-style API naming conventions builder for API name of an API object type.</returns>
        IApiNamingConventionsBuilder ApiObjectTypeNameConventions();

        /// <summary>Builds naming conventions for API name of an API property.</summary>
        /// <returns>A fluent-style API naming conventions builder for API name of an API property.</returns>
        IApiNamingConventionsBuilder ApiPropertyNameConventions();

        /// <summary>Builds naming conventions for API name of an API scalar type.</summary>
        /// <returns>A fluent-style API naming conventions builder for API name of an API scalar type.</returns>
        IApiNamingConventionsBuilder ApiScalarTypeNameConventions();

        /// <summary>Builds conventions for an API object type by invoking builder methods on an API object type configuration object.</summary>
        /// <returns>A fluent-style API object type conventions builder for an API object type.</returns>
        IApiSchemaConventionsBuilder ApiSchemaConventions();
        #endregion
    }
}