// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace ApiFramework.Tree.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    /// <remarks>
    /// Represents a node visitor that builds a string of the object tree by visiting the nodes in document order.
    /// </remarks>
    internal sealed class ToTreeStringNodeVisitor<TNode> : NodeVisitor<TNode>
        where TNode : Node<TNode>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToTreeStringNodeVisitor(int initialIndentSize = 0)
        {
            this.InitialIndentSize = initialIndentSize;
            this.StringBuilder     = new StringBuilder();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region NodeVisitor Overrides
        public override VisitResult Visit(TNode node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            this.AddNodeToTreeString(node, depth);

            return VisitResult.ContinueWithChildAndSiblingNodes;
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        {
            return this.StringBuilder.ToString().TrimEnd();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private int InitialIndentSize { get; }

        private StringBuilder StringBuilder { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddNodeToTreeString(TNode node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            this.AddIndentToTreeString(depth);
            this.AddNodeDescriptionToTreeString(node);
        }

        private void AddIndentToTreeString(int depth)
        {
            Contract.Requires(depth >= 0);

            var initialIndentSize = this.InitialIndentSize;
            var indentSpace       = initialIndentSize + (depth * IndentSize);
            this.StringBuilder.Append(WhitespaceAsCharacter, indentSpace);
        }

        private void AddNodeDescriptionToTreeString(TNode node)
        {
            Contract.Requires(node != null);

            var nodeDescription = CreateNodeDescription(node);
            this.StringBuilder.Append(nodeDescription);
            this.StringBuilder.AppendLine();
        }

        private static string CreateNodeDescription(TNode node)
        {
            Contract.Requires(node != null);

            var nodeHasAttributes = node.HasAttributes();
            if (nodeHasAttributes)
            {
                var attributesAsStrings           = String.Join(WhitespaceAsString, node.Attributes());
                var nodeDescriptionWithAttributes = $"<{node.Name} {attributesAsStrings}>";
                return nodeDescriptionWithAttributes;
            }

            var nodeDescription = $"<{node.Name}>";
            return nodeDescription;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const char   WhitespaceAsCharacter = ' ';
        private const string WhitespaceAsString    = " ";
        private const int    IndentSize            = 2;
        #endregion
    }
}
