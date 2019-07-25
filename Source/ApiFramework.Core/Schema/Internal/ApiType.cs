// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Extension;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal abstract class ApiType<T> : ExtensibleObject<T>, IApiType
        where T : IExtensibleObject<T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiType Implementation
        public abstract ApiTypeKind ApiTypeKind { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor, int depth)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiType(this, depth);
        }
        #endregion
    }
}