// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents an API object that is composed of child API nodes indexed by API property name in the API node document tree.</summary>
    public class ApiObjectNode : ApiNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates an API object node that has no child API nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API object node to parent API node.</param>
        /// <param name="apiType">Represents the API object type.</param>
        public ApiObjectNode(ApiPathMixin apiPathMixin, string apiType)
            : base(CreateName(apiPathMixin, apiType), apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }

        /// <summary>Creates an API object node that has child API nodes.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API object node to parent API node.</param>
        /// <param name="apiType">Represents the API object type.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API object node indexed by API property name.</param>
        public ApiObjectNode(ApiPathMixin apiPathMixin, string apiType, IEnumerable<ApiNode> apiNodes)
            : base(CreateName(apiPathMixin, apiType), apiPathMixin, apiNodes)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Object;
        #endregion

        #region Properties
        /// <summary>Gets the API type of the API object name.</summary>
        public string ApiType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates an API object node object.</summary>
        /// <param name="apiType">Represents the API object type.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API object node indexed by API property name.</param>
        /// <returns>Newly created API object node.</returns>
        public static ApiObjectNode Create(string apiType, params ApiNode[] apiNodes)
        {
            Contract.Requires(apiType.SafeHasContent());

            var apiObjectNode = new ApiObjectNode(ApiPathMixin.Null, apiType, apiNodes.AsEnumerable());
            return apiObjectNode;
        }

        /// <summary>Factory method that creates an API object node object.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API object node to parent API node.</param>
        /// <param name="apiType">Represents the API object type.</param>
        /// <param name="apiNodes">Represents the collection of API nodes owned by the API object node indexed by API property name.</param>
        /// <returns>Newly created API object node.</returns>
        public static ApiObjectNode Create(ApiPathMixin apiPathMixin, string apiType, params ApiNode[] apiNodes)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            var apiObjectNode = new ApiObjectNode(apiPathMixin, apiType, apiNodes.AsEnumerable());
            return apiObjectNode;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiObjectNode(ApiPathMixin apiPathMixin, string apiType, string apiName)
            : base(apiName, apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }

        protected ApiObjectNode(ApiPathMixin apiPathMixin, string apiType, string apiName, params ApiNode[] apiNodes)
            : base(apiName, apiPathMixin, apiNodes.AsEnumerable())
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }

        protected ApiObjectNode(ApiPathMixin apiPathMixin, string apiType, string apiName, IEnumerable<ApiNode> apiNodes)
            : base(apiName, apiPathMixin, apiNodes)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName(ApiPathMixin apiPathMixin, string apiType)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiType.SafeHasContent());

            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Null:
                    return $"ApiObject [apiType={apiType}]";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiObject [apiPropertyName={apiPropertyName} apiType={apiType}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiObject [apiCollectionIndex={apiCollectionItemIndex} apiType={apiType}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
