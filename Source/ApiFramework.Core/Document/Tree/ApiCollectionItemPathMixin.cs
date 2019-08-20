// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiPathMixin"/>
    /// <summary>Represents a child/parent collection item path relationship between a parent and child node.</summary>
    public class ApiCollectionItemPathMixin : ApiPathMixin
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a child/parent collection item path mixin with the given collection index.</summary>
        /// <param name="apiIndex">API collection item index to create the API collection mixin with.</param>
        public ApiCollectionItemPathMixin(int apiIndex)
        {
            this.ApiIndex = apiIndex;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiPathMixin Overrides
        public override ApiPathMixinKind ApiKind => ApiPathMixinKind.CollectionItem;
        #endregion

        #region Properties
        /// <summary>Gets the API collection index of the API collection item path mixin object.</summary>
        public int ApiIndex { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiCollectionItemPathMixin)} [{nameof(this.ApiIndex)}={this.ApiIndex}]";
        }
        #endregion
    }
}
