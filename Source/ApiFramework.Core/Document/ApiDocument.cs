// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text;

using ApiFramework.Document.Tree;

namespace ApiFramework.Document
{
    /// <summary>
    /// Represents the API document that is exchanged between client/server communication in an API request/response paradigm.
    /// The API document is composed of a document type that offers basic guidance on the type/intent of primary data and the document object model represented as an n-ary tree of API nodes.
    ///
    /// The document type can be one of the following general descriptions:
    /// <list type="table">
    ///     <listheader>  
    ///         <term>Name</term>  
    ///         <description>Description</description>  
    ///     </listheader>  
    ///     <item>  
    ///         <term>Data Document</term> 
    ///         <description>The primary data is a single API object/resource or null node. Used when an API call intent is 0 or 1 object/resource.</description>
    ///     </item>  
    ///     <item>  
    ///         <term>Data Collection Document</term> 
    ///         <description>The primary data is a single API collection node of API object/resource nodes. Used when an API call intent is many objects/resources</description>
    ///     </item>  
    ///     <item>  
    ///         <term>Errors Document</term> 
    ///         <description>The primary data is a single API collection node of API error nodes. Used when an error occurred in executing the API request.</description>
    ///     </item>  
    /// </list>
    ///
    /// The document tree is an n-ary tree representing the document object model itself.
    /// The document tree is agnostic about any specific protocol with the pipeline design allowing bootstrapped protocol readers/writers to adapt the API document to concrete/specific API protocols and content types.
    /// </summary>
    public class ApiDocument
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>
        /// Creates an API document object given the API document type and tree.
        /// </summary>
        /// <param name="apiDocumentType">The API document enumeration to create the API document with.</param>
        /// <param name="apiDocumentTree">the API document n-ary tree to create the API document with.</param>
        public ApiDocument(ApiDocumentType apiDocumentType, ApiDocumentNode apiDocumentTree)
        {
            Contract.Requires(apiDocumentTree != null);

            this.ApiDocumentType = apiDocumentType;
            this.ApiDocumentTree = apiDocumentTree;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets basic guidance on the API document type.</summary>
        public ApiDocumentType ApiDocumentType { get; }

        /// <summary>Gets the document object model as an N-Ary tree.</summary>
        public ApiDocumentNode ApiDocumentTree { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(ApiDocument)}");

            stringBuilder.AppendLine($"  {nameof(this.ApiDocumentType)}: {this.ApiDocumentType}");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"  {nameof(this.ApiDocumentTree)}:");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"{this.ApiDocumentTree.ToTreeString(4)}");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
