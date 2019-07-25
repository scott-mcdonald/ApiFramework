// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>
    /// Represents metadata of an API object that has API named properties that can
    /// be read or written from an API service.
    /// 
    /// This metadata describes the following about an API object:
    /// 1. Required API properties metadata.
    /// 2. Optional API object identity metadata.
    /// 3. Optional API relationships metadata.
    /// </summary>
    /// <remarks>
    /// An API object type is one of the following "kinds" of API object types:
    /// 1. Complex Type
    /// 2. Resource Type
    ///
    /// An API object type that has no identity is considered a complex type and
    /// is considered a simple POCO object. An API object type that has identity
    /// is considered a resource type and is considered a POCO object that can be
    /// uniquely identified and may have relationships defined as to-one or to-many
    /// API relationships to other related API resource types.
    /// </remarks>
    public interface IApiObjectType : IApiNamedType, IExtensibleObject<IApiObjectType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the collection of all API object properties in the API object type.</summary>
        IEnumerable<IApiProperty> ApiProperties { get; }

        /// <summary>Gets the optional identity of the API object type.</summary>
        IApiIdentity ApiIdentity { get; }

        /// <summary>Gets the optional relationships from the API object type to other API object types.</summary>
        IEnumerable<IApiRelationship> ApiRelationships { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Try and get the API property by API property name.</summary>
        /// <param name="apiName">API property name to lookup the API property by.</param>
        /// <param name="apiProperty">The API property if it exists in the API object type, null otherwise.</param>
        /// <returns>True if API property exists in the API object type, false otherwise.</returns>
        bool TryGetApiPropertyByApiName(string apiName, out IApiProperty apiProperty);

        /// <summary>Try and get the API property by CLR property name.</summary>
        /// <param name="clrName">CLR property name to lookup the API property by.</param>
        /// <param name="apiProperty">The API property if it exists in the API object type, null otherwise.</param>
        /// <returns>True if API property exists in the API object type, false otherwise.</returns>
        bool TryGetApiPropertyByClrName(string clrName, out IApiProperty apiProperty);

        /// <summary>Try and get the API relationship by API property name.</summary>
        /// <param name="apiName">API property name to lookup the API relationship by.</param>
        /// <param name="apiRelationship">The API relationship if it exists in the API object type, null otherwise.</param>
        /// <returns>True if API relationship exists in the API object type, false otherwise.</returns>
        bool TryGetApiRelationshipByApiName(string apiName, out IApiRelationship apiRelationship);

        /// <summary>Try and get the API relationship by CLR property name.</summary>
        /// <param name="clrName">CLR property name to lookup the API relationship by.</param>
        /// <param name="apiRelationship">The API relationship if it exists in the API object type, null otherwise.</param>
        /// <returns>True if API relationship exists in the API object type, false otherwise.</returns>
        bool TryGetApiRelationshipByClrName(string clrName, out IApiRelationship apiRelationship);
        #endregion
    }
}