// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Internal;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents an API scalar primitive in the API node document tree.</summary>
    public abstract class ApiScalarNode : ApiNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Scalar;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates a strongly typed API scalar node object.</summary>
        /// <typeparam name="TScalar">CLR scalar type the API scalar node represents.</typeparam>
        /// <param name="apiPathMixin">Represents the path relationship from the API scalar node to parent API node.</param>
        /// <param name="clrValue">CLR scalar value that is contained by the API scalar node.</param>
        /// <returns>Newly created strongly typed API scalar node object.</returns>
        public static ApiScalarNode Create<TScalar>(ApiPathMixin apiPathMixin, TScalar clrValue)
        {
            Contract.Requires(apiPathMixin != null);

            var apiScalarNode = new ApiScalarNode<TScalar>(apiPathMixin, clrValue);
            return apiScalarNode;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiScalarNode(string apiName, ApiPathMixin apiPathMixin)
            : base(apiName, apiPathMixin)
        {
        }
        #endregion
    }

    /// <inheritdoc cref="ApiScalarNode"/>
    /// <typeparam name="TScalar">CLR scalar type the API scalar node represents.</typeparam>
    public class ApiScalarNode<TScalar> : ApiScalarNode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a strongly typed API scalar node.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API scalar node to parent API node.</param>
        /// <param name="clrValue">CLR scalar value that is contained by the API scalar node.</param>
        public ApiScalarNode(ApiPathMixin apiPathMixin, TScalar clrValue)
            : base(CreateName(apiPathMixin, clrValue), apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);

            this.ClrValue = clrValue;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the CLR scalar value contained by the API scalar node.</summary>
        public TScalar ClrValue { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName(ApiPathMixin apiPathMixin, TScalar clrValue)
        {
            Contract.Requires(apiPathMixin != null);

            var clrValueString   = ClrScalarUtilities<TScalar>.Stringify(clrValue);
            var clrValueTypeName = typeof(TScalar).Name;

            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Null:
                    return $"ApiScalar [clrValue={clrValueString} {{{clrValueTypeName}}}]";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiScalar [apiPropertyName={apiPropertyName} clrValue={clrValueString} {{{clrValueTypeName}}}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiScalar [apiCollectionIndex={apiCollectionItemIndex} clrValue={clrValueString} {{{clrValueTypeName}}}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
