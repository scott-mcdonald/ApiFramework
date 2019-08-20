// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents the root API node of the API node document tree.</summary>
    public class ApiDocumentNode : ApiNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates an API document node with no child API nodes. Represents an empty API document.</summary>
        public ApiDocumentNode()
            : base(CreateName(), ApiPathMixin.Null, Enumerable.Empty<ApiNode>())
        {
        }

        /// <summary>Creates an API document node with child API nodes.</summary>
        /// <param name="apiNodes">Represents the collection of API child nodes owned by the API document.</param>
        public ApiDocumentNode(IEnumerable<ApiNode> apiNodes)
            : base(CreateName(), ApiPathMixin.Null, apiNodes)
        {
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Document;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates an API node document tree.</summary>
        /// <param name="apiNodes">Represents the collection of API child nodes owned by the API document.</param>
        /// <returns>Newly created API document node tree.</returns>
        public static ApiDocumentNode Create(params ApiNode[] apiNodes)
        {
            var apiDocumentNode = new ApiDocumentNode(apiNodes.AsEnumerable());
            return apiDocumentNode;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName()
        {
            return "ApiDocument";
        }
        #endregion
    }
}
