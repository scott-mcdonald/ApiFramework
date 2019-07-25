// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSimpleTypeResolver : IApiTypeResolver
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSimpleTypeResolver(IApiType apiType)
        {
            Contract.Requires(apiType != null);

            this.ApiType = apiType;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiTypeResolver Implementation
        public bool CanResolve()
        {
            return true;
        }

        public IApiType Resolve()
        {
            return this.ApiType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IApiType ApiType { get; }
        #endregion
    }
}