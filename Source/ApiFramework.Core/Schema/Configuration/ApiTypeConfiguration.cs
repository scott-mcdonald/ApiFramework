// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiTypeBuilder"/>
    public abstract class ApiTypeConfiguration : IApiTypeBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiTypeBuilder Implementation
        public string ClrName => this.ClrType.Name;

        public Type ClrType { get; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiTypeConfiguration(Type clrType)
        {
            Contract.Requires(clrType != null);

            this.ClrType = clrType;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal abstract ApiTypeKind ApiTypeKind { get; }

        internal abstract ApiMutableFactory ApiMutableFactory { get; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal abstract ApiMutableType CreateApiMutableType(ApiMutableSchema apiMutableSchema);

        internal abstract IApiType CreateApiType(ApiMutableType apiMutableType, ApiSchemaProxy apiSchemaProxy);
        #endregion
    }
}