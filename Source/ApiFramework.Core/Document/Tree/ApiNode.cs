// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ApiFramework.Tree;

namespace ApiFramework.Document.Tree
{
    /// <summary>Represents a node in an API Document Object Model (DOM) tree. Designed to be the base class for all API node types in an API DOM tree.</summary>
    public abstract class ApiNode : Node<ApiNode>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API node kind that gives basic guidance on the actual concrete type of the API node.</summary>
        public abstract ApiNodeKind ApiKind { get; }

        /// <summary>Gets the API path mixin which represents the relationship between child/parent API nodes. Will never be <code>null</code> but could represent the "null" API path mixin.</summary>
        public ApiPathMixin ApiPathMixin { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Path Methods
        /// <summary>Gets the "path" collection of API nodes starting with this API node and traversing up the API document tree to the root API document node.</summary>
        /// <returns>Collection of <see cref="ApiPathMixin"/> objects that represents the path of API nodes starting at this API node up to the root API node.</returns>
        public IEnumerable<ApiPathMixin> GetDocumentPath()
        {
            var apiDocumentPath = this.ParentNodesIncludeSelf()
                                      .TakeWhile(x => !x.ApiPathMixin.IsNull())
                                      .Select(x => x.ApiPathMixin)
                                      .ToList();
            apiDocumentPath.Reverse();
            return apiDocumentPath;
        }

        /// <summary>Gets the "path" string of API nodes starting with this API node and traversing up the API document tree to the root API document node.</summary>
        /// <returns>String that represents the path of API nodes starting at this API node up to the root API node.</returns>
        public string GetDocumentPathString()
        {
            var apiDocumentPath = this.GetDocumentPath().SafeToReadOnlyList();
            if (apiDocumentPath.Any() == false)
                return String.Empty;

            var apiDocumentPathStringBuilder = new StringBuilder();
            apiDocumentPathStringBuilder.Append(GetDocumentPathSegmentString(apiDocumentPath[0]));

            var apiDocumentPathCount = apiDocumentPath.Count;
            for (var i = 1; i < apiDocumentPathCount; ++i)
            {
                var apiDocumentPathSegment = apiDocumentPath[i];
                if (apiDocumentPathSegment.IsProperty())
                {
                    apiDocumentPathStringBuilder.Append('.');
                }

                var apiDocumentPathSegmentString = GetDocumentPathSegmentString(apiDocumentPathSegment);
                apiDocumentPathStringBuilder.Append(apiDocumentPathSegmentString);
            }

            var apiDocumentPathString = apiDocumentPathStringBuilder.ToString();
            return apiDocumentPathString;
        }
        #endregion

        #region Predicate Methods
        /// <summary>Predicate if the API node represents <code>null</code>.</summary>
        /// <returns>True is the API node represents <code>null</code>, false otherwise.</returns>
        public bool IsNullNode()
        {
            return this.ApiKind == ApiNodeKind.Null;
        }

        /// <summary>Predicate if the API node represents a collection.</summary>
        /// <returns>True if the API node represents a collection, false otherwise.</returns>
        public bool IsCollectionNode()
        {
            return this.ApiKind == ApiNodeKind.Collection;
        }

        /// <summary>Predicate if the API node represents the document root.</summary>
        /// <returns>True if the API node represents the root of the DOM tree, false otherwise.</returns>
        public bool IsDocumentNode()
        {
            return this.ApiKind == ApiNodeKind.Document;
        }

        /// <summary>Predicate if the API node represents an enumeration.</summary>
        /// <returns>True if the API node represents an enumeration, false otherwise.</returns>
        public bool IsEnumerationNode()
        {
            return this.ApiKind == ApiNodeKind.Enumeration;
        }

        /// <summary>Predicate if the API node represents an object.</summary>
        /// <returns>True if the API node represents an object, false otherwise.</returns>
        public bool IsObjectNode()
        {
            return this.ApiKind == ApiNodeKind.Object;
        }

        /// <summary>Predicate if the API node represents an object with identity and optional relationships - aka a resource.</summary>
        /// <returns>True if the API node represents a resource, false otherwise.</returns>
        public bool IsResourceNode()
        {
            return this.ApiKind == ApiNodeKind.Resource;
        }

        /// <summary>Predicate if the API node represents a primitive scalar.</summary>
        /// <returns>True if the API node represents a scalar, false otherwise.</returns>
        public bool IsScalarNode()
        {
            return this.ApiKind == ApiNodeKind.Scalar;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiNode(string apiName)
            : this(apiName, ApiPathMixin.Null, Enumerable.Empty<ApiNode>())
        {
        }

        protected ApiNode(string apiName, ApiPathMixin apiPathMixin)
            : this(apiName, apiPathMixin, Enumerable.Empty<ApiNode>())
        {
        }

        protected ApiNode(string apiName, ApiPathMixin apiPathMixin, IEnumerable<ApiNode> apiNodes)
            : base(apiName, apiNodes)
        {
            Contract.Requires(apiPathMixin != null);

            this.ApiPathMixin = apiPathMixin;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string GetDocumentPathSegmentString(ApiPathMixin apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);

            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Property:
                {
                    var apiPropertyNodeMixin         = (ApiPropertyPathMixin)apiPathMixin;
                    var apiDocumentPathStringSegment = apiPropertyNodeMixin.ApiName;
                    return apiDocumentPathStringSegment;
                }
                case ApiPathMixinKind.CollectionItem:
                {
                    var apiCollectionItemNodeMixin   = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex       = apiCollectionItemNodeMixin.ApiIndex;
                    var apiDocumentPathStringSegment = $"[{apiCollectionItemIndex}]";
                    return apiDocumentPathStringSegment;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
