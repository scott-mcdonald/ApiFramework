// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using Humanizer;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSingularizeNamingConvention : IApiNamingConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiNamingConvention Implementation
        public string Apply(string oldName, ApiConventionSettings apiConventionSettings)
        {
            if (String.IsNullOrWhiteSpace(oldName))
                return oldName;

            var newName = oldName.Singularize(inputIsKnownToBePlural: false);
            return newName;
        }
        #endregion
    }
}