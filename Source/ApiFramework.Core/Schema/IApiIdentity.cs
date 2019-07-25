// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>Represents object identity metadata of an API object type.</summary>
    public interface IApiIdentity : IExtensibleObject<IApiIdentity>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the identity API property on the API object type.</summary>
        IApiProperty ApiProperty { get; }
        #endregion
    }
}
