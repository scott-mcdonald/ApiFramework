// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents null in the API node document tree.</summary>
    public class ApiNullNode : ApiNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates an API null node.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API null node to parent API node.</param>
        public ApiNullNode(ApiPathMixin apiPathMixin)
            : base(CreateName(apiPathMixin), apiPathMixin)
        {
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Null;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates an API null node object.</summary>
        /// <returns>Newly created API null node.</returns>
        public static ApiNullNode Create()
        {
            var apiNullNode = new ApiNullNode(ApiPathMixin.Null);
            return apiNullNode;
        }

        /// <summary>Factory method that creates an API null node object.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API null node to parent API node.</param>
        /// <returns>Newly created API null node.</returns>
        public static ApiNullNode Create(ApiPathMixin apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);

            var apiNullNode = new ApiNullNode(apiPathMixin);
            return apiNullNode;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName(ApiPathMixin apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);

            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Null:
                    return "ApiNull";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiNull [apiPropertyName={apiPropertyName}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiNull [apiCollectionIndex={apiCollectionItemIndex}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
