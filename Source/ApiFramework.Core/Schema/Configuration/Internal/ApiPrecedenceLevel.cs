﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal enum ApiPrecedenceLevel
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Convention,
        Annotation,
        TypeConfiguration,
        FluentApi
        #endregion
    }
}
