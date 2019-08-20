// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiPathMixin"/>
    /// <summary>Represents a child/parent property path relationship between a parent and child node.</summary>
    public class ApiPropertyPathMixin : ApiPathMixin
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a child/parent property path mixin with the given property name.</summary>
        /// <param name="apiName">API property name to create the API path mixin with.</param>
        public ApiPropertyPathMixin(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            this.ApiName = apiName;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiPathMixin Overrides
        public override ApiPathMixinKind ApiKind => ApiPathMixinKind.Property;
        #endregion

        #region Properties
        /// <summary>Gets the API property name of the API property path mixin object.</summary>
        public string ApiName { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiPropertyPathMixin)} [{nameof(this.ApiName)}={this.ApiName}]";
        }
        #endregion
    }
}
