// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Document.Tree
{
    /// <summary>
    /// Represents a "mixin" that encapsulates the parent/child "path" relationship of the associated child API node to a parent API node.
    /// The following are the 3 child/parent path relationships:
    /// <list type="bullet">
    /// <item><description>Null path relationship.</description></item>
    /// <item><description>Property path relationship - child node is a property of the parent node.</description></item>
    /// <item><description>Collection item path relationship - child node is an indexed collection item of the parent node.</description></item>
    /// </list>
    /// </summary>
    public abstract class ApiPathMixin
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API path mixin kind.</summary>
        public abstract ApiPathMixinKind ApiKind { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates an API property path mixin.</summary>
        /// <param name="apiName">API property name of the child API node to the parent API object node</param>
        /// <returns>Newly created API property path mixin.</returns>
        public static ApiPathMixin CreatePropertyPathMixin(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            return new ApiPropertyPathMixin(apiName);
        }

        /// <summary>Factory method that creates an API collection item path mixin.</summary>
        /// <param name="apiIndex">API collection index of the child API node to the parent API collection node.</param>
        /// <returns>Newly created API collection item mixin.</returns>
        public static ApiPathMixin CreateCollectionItemPathMixin(int apiIndex)
        {
            Contract.Requires(apiIndex >= 0);

            return new ApiCollectionItemPathMixin(apiIndex);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Predicate Methods
        /// <summary>Predicate if the API path mixin represents an API collection item path mixin object.</summary>
        /// <returns>True if this is an collection item API path mixin object, false otherwise.</returns>
        public bool IsCollectionItem()
        {
            return this.ApiKind == ApiPathMixinKind.CollectionItem;
        }

        /// <summary>Predicate if the API path mixin represents an API null path mixin object.</summary>
        /// <returns>True if this is a null API path mixin, false otherwise.</returns>
        public bool IsNull()
        {
            return this.ApiKind == ApiPathMixinKind.Null;
        }

        /// <summary>Predicate if the API path mixin represents an API property path mixin object.</summary>
        /// <returns>True if this is a property API path mixin object, false otherwise.</returns>
        public bool IsProperty()
        {
            return this.ApiKind == ApiPathMixinKind.Property;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        /// <summary>Represents the null API path mixin. This field is read-only.</summary>
        public static readonly ApiPathMixin Null = new ApiNullPathMixin();
        #endregion
    }
}
