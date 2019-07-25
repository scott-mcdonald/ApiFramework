// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata of an API collection.</summary>
    public interface IApiCollectionType : IApiType, IExtensibleObject<IApiCollectionType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API collection item type.</summary>
        IApiType ApiItemType { get; }

        /// <summary>Gets the optional API collection item type modifiers.</summary>
        ApiTypeModifiers ApiItemTypeModifiers { get; }
        #endregion
    }
}
