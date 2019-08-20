// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Internal;
using ApiFramework.Reflection;

namespace ApiFramework.Document
{
    /// <summary>
    /// Represents an object that identifies an individual API resource.
    ///
    /// Implements the <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> abstractions making this class useful as a key in associative containers.
    /// </summary>
    public abstract class ApiResourceIdentifier : IEquatable<ApiResourceIdentifier>, IComparable<ApiResourceIdentifier>, IComparable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Represents the API type name that describe API resource objects that share common schema at the object level.
        /// </summary>
        public string ApiType { get; }

        /// <summary>Represents the API identifier as a string that uniquely identifies an API object that have the same API type.</summary>
        public abstract string ApiIdString { get; }

        /// <summary>Represents the CLR identifier type name of the API resource.</summary>
        public abstract string ApiIdTypeName { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public abstract override bool Equals(object obj);

        public abstract override int  GetHashCode();
        #endregion

        #region IEquatable<T> Implementation
        public abstract bool Equals(ApiResourceIdentifier other);
        #endregion

        #region IComparable<T> Implementation
        public abstract int CompareTo(ApiResourceIdentifier other);
        #endregion

        #region IComparable Implementation
        public abstract int CompareTo(object obj);
        #endregion

        #region Factory Methods
        /// <summary>
        /// Factory method to create an <see cref="ApiResourceIdentifier"/> that encapsulates a strongly typed CLR identifier.
        /// </summary>
        /// <typeparam name="T">CLR identifier type</typeparam>
        /// <param name="apiType">API type string</param>
        /// <param name="apiId">CLR identifier</param>
        /// <returns>Newly created API resource identifier</returns>
        public static ApiResourceIdentifier Create<T>(string apiType, T apiId)
        {
            Contract.Requires(apiType.SafeHasContent());

            var apiResourceIdentifier = new ApiResourceIdentifier<T>(apiType, apiId);
            return apiResourceIdentifier;
        }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Equality Operators
        public static bool operator ==(ApiResourceIdentifier a, ApiResourceIdentifier b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ApiResourceIdentifier a, ApiResourceIdentifier b)
        {
            return !(a == b);
        }
        #endregion

        #region Comparison Operators
        public static bool operator <(ApiResourceIdentifier left, ApiResourceIdentifier right)
        {
            if (left == null && right == null)
                return false;

            if (left == null)
                return true;

            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ApiResourceIdentifier left, ApiResourceIdentifier right)
        {
            if (left == null)
                return true;

            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ApiResourceIdentifier left, ApiResourceIdentifier right)
        {
            if (left == null)
                return false;

            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ApiResourceIdentifier left, ApiResourceIdentifier right)
        {
            if (left == null && right == null)
                return true;

            if (left == null)
                return false;

            return left.CompareTo(right) >= 0;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ApiResourceIdentifier(string apiType)
        {
            Contract.Requires(apiType.SafeHasContent());

            this.ApiType = apiType;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Invariant Methods
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ApiType.SafeHasContent());
        }
        #endregion
    }

    /// <inheritdoc cref="ApiResourceIdentifier"/>
    /// <typeparam name="T">CLR type of the identifier property.</typeparam>
    public class ApiResourceIdentifier<T> : ApiResourceIdentifier
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiResourceIdentifier(string apiType, T apiId)
            : base(apiType)
        {
            this.ApiId = apiId;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiResourceIdentifier Overrides
        public override string ApiIdString   => ClrScalarUtilities<T>.Stringify(this.ApiId);
        public override string ApiIdTypeName => typeof(T).Name;
        #endregion

        #region Properties
        public T ApiId { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override bool Equals(object obj)
        {
            return this.Equals((ApiResourceIdentifier)obj);
        }

        public override int GetHashCode()
        {
            var apiTypeHashCode = this.ApiType.GetHashCode();
            var apiIdHashCode   = ApiIdIsValueType ? this.ApiId.GetHashCode() : ((object)this.ApiId ?? String.Empty).GetHashCode();
            return apiTypeHashCode ^ apiIdHashCode;
        }

        public override string ToString()
        {
            var apiType       = this.ApiType;
            var apiIdString   = this.ApiIdString;
            var apiIdTypeName = this.ApiIdTypeName;

            return $"{nameof(ApiResourceIdentifier)} [apiType={apiType} apiId={apiIdString} {{{apiIdTypeName}}}]";
        }
        #endregion

        #region ApiResourceIdentifier Overrides
        public override bool Equals(ApiResourceIdentifier apiResourceIdentifier)
        {
            if (ReferenceEquals(this, apiResourceIdentifier))
                return true;

            if (apiResourceIdentifier == null)
                return false;

            var apiType = apiResourceIdentifier.ApiType;
            if (this.ApiType != apiType)
                return false;

            var apiResourceIdentifierOfT = (ApiResourceIdentifier<T>)apiResourceIdentifier;
            var apiId                    = apiResourceIdentifierOfT.ApiId;
            return ApiIdEquals(this.ApiId, apiId);
        }

        public override int CompareTo(ApiResourceIdentifier apiResourceIdentifier)
        {
            if (ReferenceEquals(this, apiResourceIdentifier))
                return 0;

            if (apiResourceIdentifier == null)
                return 1;

            var apiType = apiResourceIdentifier.ApiType;
            // ReSharper disable once StringCompareToIsCultureSpecific
            var apiTypeCompareTo = this.ApiType.CompareTo(apiType);
            if (apiTypeCompareTo != 0)
            {
                return apiTypeCompareTo;
            }

            var apiResourceIdentifierOfT = (ApiResourceIdentifier<T>)apiResourceIdentifier;
            var apiId                    = apiResourceIdentifierOfT.ApiId;
            return ApiIdCompareTo(this.ApiId, apiId);
        }

        public override int CompareTo(object obj)
        {
            return this.CompareTo((ApiResourceIdentifier)obj);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static bool ApiIdEquals(T apiId, T apiIdOther)
        {
            // Use IEquatable<T>.Equals if possible.
            if (ApiIdIsImplementationOfIEquatableOfT)
            {
                var apiIdAsIEquatableOfT    = (IEquatable<T>)apiId;
                var equalsFromIEquatableOfT = apiIdAsIEquatableOfT.Equals(apiIdOther);
                return equalsFromIEquatableOfT;
            }

            // Use Object.Equals as a last resort.
            var equalsFromObject = apiId.Equals(apiIdOther);
            return equalsFromObject;
        }

        private static int ApiIdCompareTo(T apiId, T apiIdOther)
        {
            // Use IComparable<T>.CompareTo if possible.
            if (ApiIdIsImplementationOfIComparableOfT)
            {
                var apiIdAsIComparableOfT       = (IComparable<T>)apiId;
                var compareToFromIComparableOfT = apiIdAsIComparableOfT.CompareTo(apiIdOther);
                return compareToFromIComparableOfT;
            }

            // Use String.CompareTo as a last resort.
            var apiIdAsString       = Convert.ToString(apiId);
            var apiIdOtherAsString  = Convert.ToString(apiIdOther);
            var compareToFromString = String.Compare(apiIdAsString, apiIdOtherAsString, StringComparison.Ordinal);
            return compareToFromString;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly bool ApiIdIsValueType                      = TypeReflection.IsValueType(typeof(T));
        private static readonly bool ApiIdIsImplementationOfIEquatableOfT  = TypeReflection.IsImplementationOf(typeof(T), typeof(IEquatable<>));
        private static readonly bool ApiIdIsImplementationOfIComparableOfT = TypeReflection.IsImplementationOf(typeof(T), typeof(IComparable<>));
        #endregion
    }
}