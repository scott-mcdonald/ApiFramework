// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata of an individual API named property on a containing API object.</summary>
    public interface IApiProperty : IExtensibleObject<IApiProperty>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API property name.</summary>
        /// <remarks>Must be unique among all API property names on the containing API object.</remarks>
        string ApiName { get; }

        /// <summary>Gets the optional API property description.</summary>
        string ApiDescription { get; }

        /// <summary>Gets the API property type.</summary>
        IApiType ApiType { get; }

        /// <summary>Gets the optional API property type modifiers.</summary>
        ApiTypeModifiers ApiTypeModifiers { get; }

        /// <summary>Gets the CLR property name associated with the API property.</summary>
        /// <remarks>Represents the CLR property name that is the materialization of the API property in CLR code.</remarks>
        string ClrName { get; }
        #endregion
    }
}