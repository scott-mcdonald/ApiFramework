// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>Represents object relationship metadata of an API object type.</summary>
    public interface IApiRelationship : IExtensibleObject<IApiRelationship>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the relationship API property on the API object type.</summary>
        /// <remarks>Is one of the API properties contained in the API object type.</remarks>
        IApiProperty ApiProperty { get; }

        /// <summary>Gets the relationship cardinality to the other API object type.</summary>
        ApiRelationshipCardinality ApiCardinality { get; }

        /// <summary>Gets the related API object type.</summary>
        IApiObjectType ApiRelatedType { get; }
        #endregion
    }
}
