// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Internal;

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiNode"/>
    /// <summary>API node that represents an API enumeration primitive in the API node document tree.</summary>
    public abstract class ApiEnumerationNode : ApiNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiNode Overrides
        public override ApiNodeKind ApiKind => ApiNodeKind.Enumeration;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        /// <summary>Factory method that creates a strongly typed API enumeration node object.</summary>
        /// <typeparam name="TEnum">CLR enumeration type the API enumeration node represents.</typeparam>
        /// <param name="apiPathMixin">Represents the path relationship from the API enumeration node to parent API node.</param>
        /// <param name="apiValue">API enumeration value that is contained by the API enumeration node.</param>
        /// <param name="clrValue">CLR enumeration value that is contained by the API enumeration node.</param>
        /// <returns>Newly created strongly typed API enumeration node object.</returns>
        public static ApiEnumerationNode Create<TEnum>(ApiPathMixin apiPathMixin, string apiValue, TEnum clrValue)
            where TEnum : Enum
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiValue.SafeHasContent());

            var apiEnumNode = new ApiEnumerationNode<TEnum>(apiPathMixin, apiValue, clrValue);
            return apiEnumNode;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiEnumerationNode(string apiName, ApiPathMixin apiPathMixin)
            : base(apiName, apiPathMixin)
        {
        }
        #endregion
    }

    public class ApiEnumerationNode<TEnum> : ApiEnumerationNode
        where TEnum : Enum
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>Creates a strongly typed API enumeration node object.</summary>
        /// <param name="apiPathMixin">Represents the path relationship from the API enumeration node to parent API node.</param>
        /// <param name="apiValue">API enumeration value that is contained by the API enumeration node.</param>
        /// <param name="clrValue">CLR enumeration value that is contained by the API enumeration node.</param>
        public ApiEnumerationNode(ApiPathMixin apiPathMixin, string apiValue, TEnum clrValue)
            : base(CreateName(apiPathMixin, apiValue, clrValue), apiPathMixin)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiValue.SafeHasContent());

            this.ApiValue = apiValue;
            this.ClrValue = clrValue;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API enumeration value contained by the API enumeration node.</summary>
        public string ApiValue { get; }

        /// <summary>Gets the CLR enumeration value contained by the API enumeration node.</summary>
        public TEnum ClrValue { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreateName(ApiPathMixin apiPathMixin, string apiValue, TEnum clrValue)
        {
            Contract.Requires(apiPathMixin != null);
            Contract.Requires(apiValue.SafeHasContent());

            var clrValueString   = ClrEnumUtilities<TEnum>.Stringify(clrValue);
            var clrValueTypeName = typeof(TEnum).Name;

            var apiNodeMixinKind = apiPathMixin.ApiKind;
            switch (apiNodeMixinKind)
            {
                case ApiPathMixinKind.Null:
                    return $"ApiEnum [apiValue={apiValue} clrValue={clrValueString} {{{clrValueTypeName}}}]";

                case ApiPathMixinKind.Property:
                    var apiPropertyNodeMixin = (ApiPropertyPathMixin)apiPathMixin;
                    var apiPropertyName      = apiPropertyNodeMixin.ApiName;
                    return $"ApiEnum [apiPropertyName={apiPropertyName} apiValue={apiValue} clrValue={clrValueString} {{{clrValueTypeName}}}]";

                case ApiPathMixinKind.CollectionItem:
                    var apiCollectionItemNodeMixin = (ApiCollectionItemPathMixin)apiPathMixin;
                    var apiCollectionItemIndex     = apiCollectionItemNodeMixin.ApiIndex;
                    return $"ApiEnum [apiCollectionIndex={apiCollectionItemIndex} apiValue={apiValue} clrValue={clrValueString} {{{clrValueTypeName}}}]";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
