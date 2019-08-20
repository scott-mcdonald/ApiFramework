// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Document.Tree
{
    /// <summary>Represents basic guidance about an API path mixin.</summary>
    public enum ApiPathMixinKind
    {
        #region Enumeration Values
        /// <summary>Represents the associated API node is neither a named property nor a collection item of the parent API node.</summary>
        Null,

        /// <summary>Represents the associated API node is a named property of the parent object API node.</summary>
        Property,

        /// <summary>Represents the associated API node is an indexed collection item of the parent collection API node.</summary>
        CollectionItem
        #endregion
    }
}
