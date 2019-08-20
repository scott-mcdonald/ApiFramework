// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiObjectNode"/>
    /// <summary>API node that represents an API object that has identity and optional relationships (to-one or to-many) to other API resource nodes.</summary>
    public class ApiResourceNode : ApiObjectNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates an API resource node that has no API child nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API resource node to parent API node.</param>
        /// <param name="apiResourceIdentifier">Represents the API identity of the API resource node.</param>
        public ApiResourceNode(ApiPathMixin apiPathMixin, ApiResourceIdentifier apiResourceIdentifier)
            : base(apiPathMixin, apiResourceIdentifier.ApiType, CreateName(apiPathMixin, apiResourceIdentifier))
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiResourceIdentifier != null);

            this.ApiResourceIdentifier = apiResourceIdentifier;
        }

        /// <summary>Creates an API resource node that has API child nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API resource node to parent API node.</param>
        /// <param name="apiResourceIdentifier">Represents the API identity of the API resource node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API resource node indexed by API property name.</param>
        public ApiResourceNode(ApiPathMixin apiPathMixin, ApiResourceIdentifier apiResourceIdentifier, IEnumerable<ApiNode> apiNodes)
            : base(apiPathMixin, apiResourceIdentifier.ApiType, CreateName(apiPathMixin, apiResourceIdentifier), apiNodes)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiResourceIdentifier != null);

            this.ApiResourceIdentifier = apiResourceIdentifier;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Resource;
        #endregion

        #region Properties
        /// <summary>Gets the API resource identity of the API resource node.</summary>
        public ApiResourceIdentifier ApiResourceIdentifier { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates an API resource node object.</summary>
        /// <param name="apiResourceIdentifier">Represents the API identity of the API resource node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API resource node indexed by API property name.</param>
        /// <returns>Newly created API resource node.</returns>
        public static ApiResourceNode Create(ApiResourceIdentifier apiResourceIdentifier, params ApiNode[] apiNodes)
        {
            Contract.Requires(apiResourceIdentifier != null);

            var apiResourceNode = new ApiResourceNode(ApiPathMixin.Null, apiResourceIdentifier, apiNodes.AsEnumerable());
            return apiResourceNode;
        }

        /// <summary>Factory method that creates an API resource node object.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API resource node to parent API node.</param>
        /// <param name="apiResourceIdentifier">Represents the API identity of the API resource node.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API resource node indexed by API property name.</param>
        /// <returns>Newly created API resource node.</returns>
        public static ApiResourceNode Create(ApiPathMixin apiPathMixin, ApiResourceIdentifier apiResourceIdentifier, params ApiNode[] apiNodes)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiResourceIdentifier != null);

            var apiResourceNode = new ApiResourceNode(apiPathMixin, apiResourceIdentifier, apiNodes.AsEnumerable());
            return apiResourceNode;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName(ApiPathMixin apiPathMixin, ApiResourceIdentifier apiResourceIdentifier)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiResourceIdentifier != null);

            var apiType          = apiResourceIdentifier.ApiType;
            var apiIdString      = apiResourceIdentifier.ApiIdString;
            var apiIdTypeName    = apiResourceIdentifier.ApiIdTypeName;
            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Null:
                    return $"ApiResource [apiType={apiType} apiId={apiIdString} {{{apiIdTypeName}}}]";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiResource [apiPropertyName={apiPropertyName} apiType={apiType} apiId={apiIdString} {{{apiIdTypeName}}}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiResource [apiCollectionIndex={apiCollectionItemIndex} apiType={apiType} apiId={apiIdString} {{{apiIdTypeName}}}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
