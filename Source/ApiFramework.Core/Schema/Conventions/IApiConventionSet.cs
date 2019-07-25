// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a set of API conventions used for building and creating an API schema.</summary>
    public interface IApiConventionSet
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API enumeration type conventions to apply when creating API enumeration types.</summary>
        IEnumerable<IApiEnumerationTypeConvention> ApiEnumerationTypeConventions { get; }

        /// <summary>Gets the API naming conventions to apply when creating API enum type names.</summary>
        IEnumerable<IApiNamingConvention> ApiEnumerationTypeNameConventions { get; }

        /// <summary>Gets the API naming conventions to apply when creating API enum value names.</summary>
        IEnumerable<IApiNamingConvention> ApiEnumerationValueNameConventions { get; }

        /// <summary>Gets the API object type conventions to apply when creating API object types.</summary>
        IEnumerable<IApiObjectTypeConvention> ApiObjectTypeConventions { get; }

        /// <summary>Gets the API naming conventions to apply when creating API object type names.</summary>
        IEnumerable<IApiNamingConvention> ApiObjectTypeNameConventions { get; }

        /// <summary>Gets the API naming conventions to apply when creating API property names.</summary>
        IEnumerable<IApiNamingConvention> ApiPropertyNameConventions { get; }

        /// <summary>Gets the API naming conventions to apply when creating API scalar type names.</summary>
        IEnumerable<IApiNamingConvention> ApiScalarTypeNameConventions { get; }

        /// <summary>Gets the API schema conventions to apply when creating an API schema.</summary>
        IEnumerable<IApiSchemaConvention> ApiSchemaConventions { get; }
        #endregion
    }
}
