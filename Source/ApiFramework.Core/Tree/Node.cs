// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Tree.Internal;

namespace ApiFramework.Tree
{
    /// <summary>
    /// Represents a node within a 1-N tree.
    /// </summary>
    public class Node<TNode>
        where TNode : Node<TNode>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Node(string name, params TNode[] nodes)
            : this(name, nodes.AsEnumerable())
        {
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the name of this node.</summary>
        public string Name { get; }

        /// <summary>Gets the root node of the tree.</summary>
        public TNode RootNode { get; private set; }

        /// <summary>Gets the parent node of this node.</summary>
        public TNode ParentNode { get; private set; }

        /// <summary>Gets the previous sibling node of this node.</summary>
        public TNode PreviousNode { get; private set; }

        /// <summary>Gets the next sibling node of this node.</summary>
        public TNode NextNode { get; private set; }

        /// <summary>Gets the first child node of this node.</summary>
        public TNode FirstNode { get; private set; }

        /// <summary>Gets the last child node of this node.</summary>
        public TNode LastNode { get; private set; }

        /// <summary>Gets the first attribute of this node.</summary>
        public NodeAttribute FirstAttribute { get; private set; }

        /// <summary>Gets the last attribute of this node.</summary>
        public NodeAttribute LastAttribute { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return this.Name;
        }
        #endregion

        #region Attribute Methods
        /// <summary>
        /// Adds an attribute to this node.
        /// </summary>
        /// <param name="newAttribute">Attribute to add to this node.</param>
        public void AddAttribute(NodeAttribute newAttribute)
        {
            Contract.Requires(newAttribute != null);

            // Validate attribute has not already been added to the node.
            newAttribute.ValidateHasNotBeenAdded();

            // Add attribute to the node.

            // Handle special case of this being the first attribute being
            // added to this node.
            if (this.HasAttributes() == false)
            {
                // Add first attribute to this node.
                this.FirstAttribute = newAttribute;
                this.LastAttribute  = newAttribute;

                newAttribute.NextAttribute     = null;
                newAttribute.PreviousAttribute = null;
                return;
            }

            // Add subsequent attributes to this node.
            var previousLastAttribute = this.LastAttribute;
            this.LastAttribute                  = newAttribute;
            previousLastAttribute.NextAttribute = newAttribute;

            newAttribute.NextAttribute     = null;
            newAttribute.PreviousAttribute = previousLastAttribute;
        }

        /// <summary>Adds a collection of attributes to this node.</summary>
        /// <param name="attributeCollection">Child nodes to add to this parent node.</param>
        public void AddAttributes(IEnumerable<NodeAttribute> attributeCollection)
        {
            if (attributeCollection == null)
                return;

            foreach (var attribute in attributeCollection)
            {
                this.AddAttribute(attribute);
            }
        }

        /// <summary>
        /// Returns if this node has any attributes.
        /// </summary>
        /// <returns>True if this node has attributes, false otherwise.</returns>
        public bool HasAttributes()
        {
            return this.FirstAttribute != null;
        }

        /// <summary>Removes the old attribute from this attribute.</summary>
        /// <param name="oldAttribute">Attribute to remove from this node.</param>
        public void RemoveAttribute(NodeAttribute oldAttribute)
        {
            Contract.Requires(oldAttribute != null);

            // Ensure old attribute exists before removing.
            var oldAttributeExists = this.Attributes()
                                         .Any(x => ReferenceEquals(x, oldAttribute));
            if (oldAttributeExists == false)
                return;

            /////////////////////////////////////////////////////////////////
            // Remove old attribute.
            /////////////////////////////////////////////////////////////////

            // 1. If the first attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.FirstAttribute))
            {
                this.FirstAttribute = oldAttribute.NextAttribute;
            }

            // 2. If the last attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.LastAttribute))
            {
                this.LastAttribute = oldAttribute.PreviousAttribute;
            }

            // 3. If the old attribute previous attribute was not null.
            if (oldAttribute.PreviousAttribute != null)
            {
                oldAttribute.PreviousAttribute.NextAttribute = oldAttribute.NextAttribute;
            }

            // 4. If the old attribute next attribute was not null.
            if (oldAttribute.NextAttribute != null)
            {
                oldAttribute.NextAttribute.PreviousAttribute = oldAttribute.PreviousAttribute;
            }
        }

        /// <summary>
        /// Replaces the old attribute with the new attribute for this node.
        /// </summary>
        /// <param name="oldAttribute">Attribute to remove from this node.</param>
        /// <param name="newAttribute">Attribute to add to this node.</param>
        public void ReplaceAttribute(NodeAttribute oldAttribute, NodeAttribute newAttribute)
        {
            Contract.Requires(oldAttribute != null);
            Contract.Requires(newAttribute != null);

            // Ensure old attribute exists as a child before removing.
            var oldAttributeExists = this.Attributes()
                                         .Any(x => ReferenceEquals(x, oldAttribute));
            if (oldAttributeExists == false)
                return;

            // Validate attribute has not already been added to the tree.
            newAttribute.ValidateHasNotBeenAdded();

            /////////////////////////////////////////////////////////////////
            // Replace old attribute with new attribute.
            /////////////////////////////////////////////////////////////////
            newAttribute.NextAttribute     = oldAttribute.NextAttribute;
            newAttribute.PreviousAttribute = oldAttribute.PreviousAttribute;

            // 1. If the first attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.FirstAttribute))
            {
                this.FirstAttribute = newAttribute;
            }

            // 2. If the last attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.LastAttribute))
            {
                this.LastAttribute = newAttribute;
            }

            // 3. If the old attribute previous attribute was not null.
            if (oldAttribute.PreviousAttribute != null)
            {
                oldAttribute.PreviousAttribute.NextAttribute = newAttribute;
            }

            // 4. If the old attribute next attribute was not null.
            if (oldAttribute.NextAttribute != null)
            {
                oldAttribute.NextAttribute.PreviousAttribute = newAttribute;
            }
        }
        #endregion

        #region LINQ Methods
        /// <summary>Returns a collection of the attributes for this node.</summary>
        public IEnumerable<NodeAttribute> Attributes()
        {
            var attribute = this.FirstAttribute;
            while (attribute != null)
            {
                yield return attribute;
                attribute = attribute.NextAttribute;
            }
        }

        /// <summary>Returns a collection of the direct child nodes for this node, in document order.</summary>
        public IEnumerable<TNode> ChildNodes()
        {
            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (node != null)
            {
                yield return node;
                node = node.NextNode;
            }
        }

        /// <summary>Returns a collection starting with this node followed by the direct child nodes for this node, in document order.</summary>
        public IEnumerable<TNode> ChildNodesIncludeSelf()
        {
            yield return (TNode)this;

            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (node != null)
            {
                yield return node;
                node = node.NextNode;
            }
        }

        /// <summary>Returns a collection of the descendant nodes for this node, in document order.</summary>
        public IEnumerable<TNode> DescendantNodes()
        {
            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (true)
            {
                yield return node;

                if (node.FirstNode != null)
                {
                    node = node.FirstNode; // walk down
                }
                else
                {
                    while (node.NextNode == null)
                    {
                        if (ReferenceEquals(node, this))
                            yield break;

                        node = node.ParentNode; // walk up ...
                    }

                    node = node.NextNode; // ... and right
                }
            }
        }

        /// <summary>Returns a collection starting with this node followed by the descendant nodes for this node, in document order.</summary>
        public IEnumerable<TNode> DescendantNodesIncludeSelf()
        {
            yield return (TNode)this;

            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (true)
            {
                yield return node;

                if (node.FirstNode != null)
                {
                    node = node.FirstNode; // walk down
                }
                else
                {
                    while (node.NextNode == null)
                    {
                        if (ReferenceEquals(node, this))
                            yield break;

                        node = node.ParentNode; // walk up ...
                    }

                    node = node.NextNode; // ... and right
                }
            }
        }

        /// <summary>Returns a collection of the parent nodes for this node.</summary>
        public IEnumerable<TNode> ParentNodes()
        {
            if (this.HasParentNode() == false)
                yield break;

            var node = this.ParentNode;
            while (node != null)
            {
                yield return node;
                node = node.ParentNode;
            }
        }

        /// <summary>Returns a collection starting with this node followed by the parent nodes for this node.</summary>
        public IEnumerable<TNode> ParentNodesIncludeSelf()
        {
            yield return (TNode)this;

            if (this.HasParentNode() == false)
                yield break;

            var node = this.ParentNode;
            while (node != null)
            {
                yield return node;
                node = node.ParentNode;
            }
        }
        #endregion

        #region Node Methods
        /// <summary>
        /// Adds the new node as child of this node.
        /// </summary>
        /// <param name="newNode">Child node to add to this parent node.</param>
        public void AddNode(TNode newNode)
        {
            Contract.Requires(newNode != null);

            this.AddNodeImpl(newNode);
        }

        /// <summary>
        /// Adds the new node collection as children of this node.
        /// </summary>
        /// <param name="newNodeCollection">Child nodes to add to this parent node.</param>
        public void AddNodes(IEnumerable<TNode> newNodeCollection)
        {
            this.AddNodesImpl(newNodeCollection);
        }

        /// <summary>
        /// Returns if this node has any children nodes.
        /// </summary>
        /// <returns>True if this node has children nodes, false otherwise.</returns>
        public bool HasNodes()
        {
            return this.FirstNode != null;
        }

        /// <summary>
        /// Returns if this node has a parent node.
        /// </summary>
        /// <returns>True if this node has a parent node, false otherwise.</returns>
        public bool HasParentNode()
        {
            return this.ParentNode != null;
        }

        /// <summary>
        /// Removes the old node as a child from this node.
        /// </summary>
        /// <param name="oldNode">Child node to remove from this parent node.</param>
        public void RemoveNode(TNode oldNode)
        {
            Contract.Requires(oldNode != null);

            // Ensure old node exists as a child before removing.
            var oldNodeExistsAsChild = ReferenceEquals(this, oldNode.ParentNode);
            if (oldNodeExistsAsChild == false)
                return;

            /////////////////////////////////////////////////////////////////
            // Remove old node.
            /////////////////////////////////////////////////////////////////

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this node was the old node.
            if (ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = oldNode.NextNode;
            }

            // 2. If the last node of this node was the old node.
            if (ReferenceEquals(oldNode, this.LastNode))
            {
                this.LastNode = oldNode.PreviousNode;
            }

            // 3. If the old node previous node was not null.
            if (oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = oldNode.NextNode;
            }

            // 4. If the old node next node was not null.
            if (oldNode.NextNode != null)
            {
                oldNode.NextNode.PreviousNode = oldNode.PreviousNode;
            }
        }

        /// <summary>
        /// Replaces the old node with the new node as a child for this node.
        /// </summary>
        /// <param name="oldNode">Child node to remove from this parent node.</param>
        /// <param name="newNode">Child node to add to this parent node.</param>
        public void ReplaceNode(TNode oldNode, TNode newNode)
        {
            Contract.Requires(oldNode != null);
            Contract.Requires(newNode != null);

            // Ensure old node exists as a child before removing.
            var oldNodeExistsAsChild = ReferenceEquals(this, oldNode.ParentNode);
            if (oldNodeExistsAsChild == false)
                return;

            // Validate node has not already been added to the tree.
            newNode.ValidateHasNotBeenAdded();

            /////////////////////////////////////////////////////////////////
            // Replace old node with new node.
            /////////////////////////////////////////////////////////////////
            newNode.RootNode     = oldNode.RootNode;
            newNode.ParentNode   = oldNode.ParentNode;
            newNode.NextNode     = oldNode.NextNode;
            newNode.PreviousNode = oldNode.PreviousNode;

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this node was the old node.
            if (ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = newNode;
            }

            // 2. If the last node of this node was the old node.
            if (ReferenceEquals(oldNode, this.LastNode))
            {
                this.LastNode = newNode;
            }

            // 3. If the old node previous node was not null.
            if (oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = newNode;
            }

            // 4. If the old node next node was not null.
            if (oldNode.NextNode != null)
            {
                oldNode.NextNode.PreviousNode = newNode;
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>Create a string representation of this 1-N tree.</summary>
        public string ToTreeString(int initialIndentSize = 0)
        {
            var treeStringNodeVisitor = new ToTreeStringNodeVisitor<TNode>(initialIndentSize);
            this.Accept(treeStringNodeVisitor, 0);

            var treeString = treeStringNodeVisitor.ToString();
            return treeString;
        }
        #endregion

        #region Visitor Methods
        public void Accept(NodeVisitor<TNode> nodeVisitor)
        {
            this.Accept(nodeVisitor, 0);
        }

        public void Accept(NodeVisitor<TNode> nodeVisitor, int depth)
        {
            Contract.Requires(nodeVisitor != null);
            Contract.Requires(depth       >= 0);

            var visitResult = nodeVisitor.Visit((TNode)this, depth);
            if (visitResult == VisitResult.Done)
                return;

            if (this.FirstNode != null)
            {
                switch (visitResult)
                {
                    case VisitResult.ContinueWithChildAndSiblingNodes:
                    {
                        this.FirstNode.Accept(nodeVisitor, depth + 1);
                        break;
                    }
                }
            }

            if (this.NextNode == null)
                return;

            switch (visitResult)
            {
                case VisitResult.ContinueWithChildAndSiblingNodes:
                case VisitResult.ContinueWithSiblingNodesOnly:
                {
                    this.NextNode.Accept(nodeVisitor, depth);
                    break;
                }
            }
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected Node(string name)
        {
            Contract.Requires(name.SafeHasContent());

            this.Name     = name;
            this.RootNode = (TNode)this;
        }

        protected Node(string name, TNode node)
        {
            Contract.Requires(name.SafeHasContent());

            this.Name     = name;
            this.RootNode = (TNode)this;

            this.AddNodeImpl(node);
        }

        protected Node(string name, IEnumerable<TNode> nodes)
        {
            Contract.Requires(name.SafeHasContent());

            this.Name     = name;
            this.RootNode = (TNode)this;

            this.AddNodesImpl(nodes);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddNodeImpl(TNode newNode)
        {
            if (newNode == null)
                return;

            // Validate node has not already been added to the tree.
            newNode.ValidateHasNotBeenAdded();

            // Add node to the object graph.
            newNode.RootNode   = this.RootNode;
            newNode.ParentNode = (TNode)this;

            // Handle special case of this being the first child node being
            // added to this node.
            if (this.HasNodes() == false)
            {
                // Add first child node to this node.
                this.FirstNode = newNode;
                this.LastNode  = newNode;

                newNode.NextNode     = null;
                newNode.PreviousNode = null;
                return;
            }

            // Add subsequent child nodes to this node.
            var previousLastNode = this.LastNode;
            this.LastNode             = newNode;
            previousLastNode.NextNode = newNode;

            newNode.NextNode     = null;
            newNode.PreviousNode = previousLastNode;
        }

        private void AddNodesImpl(IEnumerable<TNode> newNodeCollection)
        {
            if (newNodeCollection == null)
                return;

            foreach (var newNode in newNodeCollection)
            {
                this.AddNodeImpl(newNode);
            }
        }

        private void ThrowExceptionForAlreadyBeenAdded()
        {
            var message = $"{this.Name} node has already been added to a tree.";
            throw new TreeException(message);
        }

        private void ValidateHasNotBeenAdded()
        {
            if (this.ParentNode == null && this.PreviousNode == null && this.NextNode == null)
                return;

            this.ThrowExceptionForAlreadyBeenAdded();
        }
        #endregion
    }
}
