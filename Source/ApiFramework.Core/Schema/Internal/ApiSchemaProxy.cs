// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSchemaProxy : IApiSchemaProxy
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public IApiSchema Subject { get; private set; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal void Initialize(IApiSchema subject)
        {
            Contract.Requires(subject != null);

            this.Subject = subject;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Invariant Methods
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Subject != null);
        }
        #endregion
    }
}