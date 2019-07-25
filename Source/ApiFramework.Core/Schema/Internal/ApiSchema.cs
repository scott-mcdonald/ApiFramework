// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSchema : IApiSchema
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSchema(string                           apiName,
                         IEnumerable<IApiEnumerationType> apiEnumerationTypes,
                         IEnumerable<IApiObjectType>      apiObjectTypes,
                         IEnumerable<IApiScalarType>      apiScalarTypes)
        {
            this.ApiName = apiName ?? ApiDefaultName;

            // API Enumeration Types
            var apiEnumerationTypesCollection = apiEnumerationTypes.EmptyIfNull()
                                                                   .OrderBy(x => x.ApiName)
                                                                   .SafeToReadOnlyCollection();

            this.ApiEnumerationTypes                   = apiEnumerationTypesCollection;
            this.ApiNameToApiEnumerationTypeDictionary = apiEnumerationTypesCollection.ToDictionary(x => x.ApiName);
            this.ClrTypeToApiEnumerationTypeDictionary = apiEnumerationTypesCollection.ToDictionary(x => x.ClrType);

            // API Object Types
            var apiObjectTypesCollection = apiObjectTypes.EmptyIfNull()
                                                         .OrderBy(x => x.ApiName)
                                                         .SafeToReadOnlyCollection();

            this.ApiObjectTypes                   = apiObjectTypesCollection;
            this.ApiNameToApiObjectTypeDictionary = apiObjectTypesCollection.ToDictionary(x => x.ApiName);
            this.ClrTypeToApiObjectTypeDictionary = apiObjectTypesCollection.ToDictionary(x => x.ClrType);

            // API Scalar Types
            var apiScalarTypesCollection = apiScalarTypes.EmptyIfNull()
                                                         .OrderBy(x => x.ApiName)
                                                         .SafeToReadOnlyCollection();

            this.ApiScalarTypes                   = apiScalarTypesCollection;
            this.ApiNameToApiScalarTypeDictionary = apiScalarTypesCollection.ToDictionary(x => x.ApiName);
            this.ClrTypeToApiScalarTypeDictionary = apiScalarTypesCollection.ToDictionary(x => x.ClrType);

            // API Types
            // ReSharper disable RedundantEnumerableCastCall
            var apiNamedTypes = this.ApiEnumerationTypes.Cast<IApiNamedType>()
                                    .Concat(this.ApiObjectTypes.Cast<IApiNamedType>())
                                    .Concat(this.ApiScalarTypes.Cast<IApiNamedType>())
                                    .OrderBy(x => x.ApiName)
                                    .ToList();
            // ReSharper restore RedundantEnumerableCastCall

            this.ApiNamedTypes                   = apiNamedTypes;
            this.ApiNameToApiNamedTypeDictionary = apiNamedTypes.ToDictionary(x => x.ApiName);
            this.ClrTypeToApiNamedTypeDictionary = apiNamedTypes.ToDictionary(x => x.ClrType);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiSchema Implementation
        public string ApiName { get; }

        public IEnumerable<IApiNamedType> ApiNamedTypes { get; }

        public IEnumerable<IApiEnumerationType> ApiEnumerationTypes { get; }

        public IEnumerable<IApiObjectType> ApiObjectTypes { get; }

        public IEnumerable<IApiScalarType> ApiScalarTypes { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiSchema Implementation
        public string ToTreeString()
        {
            var apiToTreeStringVisitor = new ToTreeStringApiVisitor();

            this.Accept(apiToTreeStringVisitor);

            var apiTreeString = apiToTreeStringVisitor.ToString();
            return apiTreeString;
        }

        public bool TryGetApiNamedType(string apiName, out IApiNamedType apiNamedType)
        {
            if (!String.IsNullOrWhiteSpace(apiName))
                return this.ApiNameToApiNamedTypeDictionary.TryGetValue(apiName, out apiNamedType);

            apiNamedType = null;
            return false;
        }

        public bool TryGetApiNamedType(Type clrType, out IApiNamedType apiNamedType)
        {
            if (clrType != null)
                return this.ClrTypeToApiNamedTypeDictionary.TryGetValue(clrType, out apiNamedType);

            apiNamedType = null;
            return false;
        }

        public bool TryGetApiEnumerationType(string apiName, out IApiEnumerationType apiEnumerationType)
        {
            if (!String.IsNullOrWhiteSpace(apiName))
                return this.ApiNameToApiEnumerationTypeDictionary.TryGetValue(apiName, out apiEnumerationType);

            apiEnumerationType = null;
            return false;
        }

        public bool TryGetApiEnumerationType(Type clrType, out IApiEnumerationType apiEnumerationType)
        {
            if (clrType != null)
                return this.ClrTypeToApiEnumerationTypeDictionary.TryGetValue(clrType, out apiEnumerationType);

            apiEnumerationType = null;
            return false;
        }

        public bool TryGetApiObjectType(string apiName, out IApiObjectType apiObjectType)
        {
            if (!String.IsNullOrWhiteSpace(apiName))
                return this.ApiNameToApiObjectTypeDictionary.TryGetValue(apiName, out apiObjectType);

            apiObjectType = null;
            return false;
        }

        public bool TryGetApiObjectType(Type clrType, out IApiObjectType apiObjectType)
        {
            if (clrType != null)
                return this.ClrTypeToApiObjectTypeDictionary.TryGetValue(clrType, out apiObjectType);

            apiObjectType = null;
            return false;
        }

        public bool TryGetApiScalarType(string apiName, out IApiScalarType apiScalarType)
        {
            if (!String.IsNullOrWhiteSpace(apiName))
                return this.ApiNameToApiScalarTypeDictionary.TryGetValue(apiName, out apiScalarType);

            apiScalarType = null;
            return false;
        }

        public bool TryGetApiScalarType(Type clrType, out IApiScalarType apiScalarType)
        {
            if (clrType != null)
                return this.ClrTypeToApiScalarTypeDictionary.TryGetValue(clrType, out apiScalarType);

            apiScalarType = null;
            return false;
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiSchema)} [{nameof(this.ApiName)}={this.ApiName}]";
        }
        #endregion

        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiSchema(this);
        }
        #endregion

        // INTERNAL FIELDS //////////////////////////////////////////////////
        #region Constants
        internal const string ApiDefaultName = "API Schema";
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyDictionary<string, IApiNamedType> ApiNameToApiNamedTypeDictionary { get; }
        private IReadOnlyDictionary<Type, IApiNamedType>   ClrTypeToApiNamedTypeDictionary { get; }

        private IReadOnlyDictionary<string, IApiEnumerationType> ApiNameToApiEnumerationTypeDictionary { get; }
        private IReadOnlyDictionary<Type, IApiEnumerationType>   ClrTypeToApiEnumerationTypeDictionary { get; }

        private IReadOnlyDictionary<string, IApiObjectType> ApiNameToApiObjectTypeDictionary { get; }
        private IReadOnlyDictionary<Type, IApiObjectType>   ClrTypeToApiObjectTypeDictionary { get; }

        private IReadOnlyDictionary<string, IApiScalarType> ApiNameToApiScalarTypeDictionary { get; }
        private IReadOnlyDictionary<Type, IApiScalarType>   ClrTypeToApiScalarTypeDictionary { get; }
        #endregion
    }
}