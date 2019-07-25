// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace ApiFramework.Schema
{
    /// <summary>
    /// Represents the capabilities of the API service. It is the contract between the server and client.
    /// </summary>
    /// <remarks>
    /// The schema at a high level:
    /// <list type="bullet">
    /// <item><description>Defines object types that define the domain model of the API service.</description></item>
    /// <item><description>Defines enumeration types used as primitives for object types.</description></item>
    /// <item><description>Defines scalar types with used as primitives for object types.</description></item>
    /// </list>
    /// </remarks>
    public interface IApiSchema
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the name of the API schema.</summary>
        string ApiName { get; }

        /// <summary>Gets the collection of all API named types in the API schema.</summary>
        IEnumerable<IApiNamedType> ApiNamedTypes { get; }

        /// <summary>Gets the collection of all API enumeration types in the API schema.</summary>
        IEnumerable<IApiEnumerationType> ApiEnumerationTypes { get; }

        /// <summary>Gets the collection of all API object types in the API schema.</summary>
        IEnumerable<IApiObjectType> ApiObjectTypes { get; }

        /// <summary>Gets the collection of all API scalar types in the API schema.</summary>
        IEnumerable<IApiScalarType> ApiScalarTypes { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Creates a string representation of the API schema as a n-ary tree.</summary>
        /// <returns>The API schema tree represented as a string.</returns>
        string ToTreeString();

        /// <summary>Try and get the API named type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API named type by.</param>
        /// <param name="apiNamedType">The API named type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API named type exists in the API schema, false otherwise.</returns>
        bool TryGetApiNamedType(string apiName, out IApiNamedType apiNamedType);

        /// <summary>Try and get the API named type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API named type by.</param>
        /// <param name="apiNamedType">The API named type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API named type exists in the API schema, false otherwise.</returns>
        bool TryGetApiNamedType(Type clrType, out IApiNamedType apiNamedType);

        /// <summary>Try and get the API enumeration type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API enumeration type by.</param>
        /// <param name="apiEnumerationType">The API enumeration type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API enumeration type exists in the API schema, false otherwise.</returns>
        bool TryGetApiEnumerationType(string apiName, out IApiEnumerationType apiEnumerationType);

        /// <summary>Try and get the API enumeration type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API enumeration type by.</param>
        /// <param name="apiEnumerationType">The API enumeration type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API enumeration type exists in the API schema, false otherwise.</returns>
        bool TryGetApiEnumerationType(Type clrType, out IApiEnumerationType apiEnumerationType);

        /// <summary>Try and get the API object type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API object type by.</param>
        /// <param name="apiObjectType">The API object type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API object type exists in the API schema, false otherwise.</returns>
        bool TryGetApiObjectType(string apiName, out IApiObjectType apiObjectType);

        /// <summary>Try and get the API object type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API object type by.</param>
        /// <param name="apiObjectType">The API object type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API object type exists in the API schema, false otherwise.</returns>
        bool TryGetApiObjectType(Type clrType, out IApiObjectType apiObjectType);

        /// <summary>Try and get the API scalar type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API scalar type by.</param>
        /// <param name="apiScalarType">The API scalar type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API scalar type exists in the API schema, false otherwise.</returns>
        bool TryGetApiScalarType(string apiName, out IApiScalarType apiScalarType);

        /// <summary>Try and get the API scalar type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API scalar type by.</param>
        /// <param name="apiScalarType">The API scalar type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API scalar type exists in the API schema, false otherwise.</returns>
        bool TryGetApiScalarType(Type clrType, out IApiScalarType apiScalarType);
        #endregion
    }
}
