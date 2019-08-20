// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents an API collection of API nodes in the API node document tree.</summary>
    public class ApiCollectionNode : ApiNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates an empty API collection node.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API collection node to parent API node.</param>
        public ApiCollectionNode(ApiPathMixin apiPathMixin)
            : base(CreateName(apiPathMixin), apiPathMixin)
        {
        }

        /// <summary>Creates an API collection node of API nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API collection node to parent API node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API collection node.</param>
        public ApiCollectionNode(ApiPathMixin apiPathMixin, params ApiNode[] apiNodes)
            : base(CreateName(apiPathMixin), apiPathMixin, apiNodes.AsEnumerable())
        {
        }

        /// <summary>Creates an API collection node of API nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API collection node to parent API node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API collection node.</param>
        public ApiCollectionNode(ApiPathMixin apiPathMixin, IEnumerable<ApiNode> apiNodes)
            : base(CreateName(apiPathMixin), apiPathMixin, apiNodes)
        {
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Collection;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Creates an API collection node of API nodes.</summary>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API collection node.</param>
        public static ApiCollectionNode Create(params ApiNode[] apiNodes)
        {
            var apiCollectionNode = new ApiCollectionNode(ApiPathMixin.Null, apiNodes.AsEnumerable());
            return apiCollectionNode;
        }

        /// <summary>Creates an API collection node of API nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API collection node to parent API node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API collection node.</param>
        public static ApiCollectionNode Create(ApiPathMixin apiPathMixin, params ApiNode[] apiNodes)
        {
            Contract.Requires(apiPathMixin != null);

            var apiCollectionNode = new ApiCollectionNode(apiPathMixin, apiNodes.AsEnumerable());
            return apiCollectionNode;
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
                    return "ApiCollection";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiCollection [apiPropertyName={apiPropertyName}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiCollection [apiCollectionIndex={apiCollectionItemIndex}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
