// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Document
{
    /// <summary>Represents basic guidance about the type of primary data of an API document.</summary>
    public enum ApiDocumentType
    {
        /// <summary>Represents primary data being a single data object.</summary>
        ApiDataDocument,

        /// <summary>Represents primary data being a collection of data objects.</summary>
        ApiDataCollectionDocument,

        /// <summary>Represents primary data being a collection of error objects.</summary>
        ApiErrorsDocument
    }
}
