// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Extension;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal abstract class ApiNamedType<T> : ApiType<T>, IApiNamedType
        where T : IExtensibleObject<T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiNamedType Implementation
        public string ApiName        { get; }
        public string ApiDescription { get; }
        public Type   ClrType        { get; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiNamedType(string apiName,
                               string apiDescription,
                               Type   clrType)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrType != null);

            this.ApiName        = apiName;
            this.ApiDescription = apiDescription;
            this.ClrType        = clrType;
        }
        #endregion
    }
}