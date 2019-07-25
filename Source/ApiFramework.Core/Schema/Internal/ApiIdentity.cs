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
    internal class ApiIdentity : ExtensibleObject<IApiIdentity>, IApiIdentity
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiIdentity(IApiProperty apiProperty)
        {
            Contract.Requires(apiProperty != null);

            this.ApiProperty = apiProperty;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiIdentity Implementation
        public IApiProperty ApiProperty { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiIdentity)} [{nameof(this.ApiProperty.ApiName)}={this.ApiProperty.ApiName}]";
        }
        #endregion

        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor, int depth)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiIdentity(this, depth);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiIdentity ExtensionOwner => this;
        #endregion
    }
}