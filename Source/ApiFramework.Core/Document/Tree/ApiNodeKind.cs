// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Document.Tree
{
    /// <summary>Represents basic guidance about an API node.</summary>
    public enum ApiNodeKind
    {
        /// <summary>API node represents an API null value.</summary>
        Null,

        /// <summary>API node represents an API collection.</summary>
        Collection,

        /// <summary>API node represents an API document.</summary>
        Document,

        /// <summary>API node represents an API enumeration.</summary>
        Enumeration,

        /// <summary>API node represents an API object.</summary>
        Object,

        /// <summary>API node represents an API resource.</summary>
        Resource,

        /// <summary>API node represents an API scalar primitive.</summary>
        Scalar
    }
}
