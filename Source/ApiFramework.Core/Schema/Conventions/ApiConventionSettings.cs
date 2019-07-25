// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents customizable settings to be used when conventions are being applied for creating an API schema.</summary>
    public class ApiConventionSettings
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets the API auto discovery settings for building API schemas.</summary>
        public ApiDiscoverySettings ApiDiscoverySettings { get; set; } = ApiDiscoverySettings.Empty;
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly ApiConventionSettings Empty = new ApiConventionSettings();
        #endregion
    }
}