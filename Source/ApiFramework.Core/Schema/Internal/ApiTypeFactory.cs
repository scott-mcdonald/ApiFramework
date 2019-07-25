// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using ApiFramework.Reflection;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ApiTypeFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static ApiCollectionType CreateApiCollectionType(IApiType         apiItemType,
                                                                ApiTypeModifiers apiItemTypeModifiers)
        {
            Contract.Requires(apiItemType != null);

            var apiCollectionType = new ApiCollectionType(apiItemType,
                                                          apiItemTypeModifiers);
            return apiCollectionType;
        }

        public static ApiCollectionType CreateApiCollectionType(IApiTypeResolver apiItemTypeResolver,
                                                                ApiTypeModifiers apiItemTypeModifiers)
        {
            Contract.Requires(apiItemTypeResolver != null);

            var apiCollectionType = new ApiCollectionType(apiItemTypeResolver,
                                                          apiItemTypeModifiers);
            return apiCollectionType;
        }

        public static ApiEnumerationType CreateApiEnumerationType(string                            apiName,
                                                                  string                            apiDescription,
                                                                  IEnumerable<IApiEnumerationValue> apiEnumerationValues,
                                                                  Type                              clrType)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrType != null);

            var apiEnumerationValuesCollection = apiEnumerationValues.SafeToReadOnlyCollection();
            var apiEnumerationType = new ApiEnumerationType(apiName,
                                                            apiDescription,
                                                            apiEnumerationValuesCollection,
                                                            clrType);
            return apiEnumerationType;
        }

        public static ApiEnumerationValue CreateApiEnumerationValue(string apiName,
                                                                    string apiDescription,
                                                                    string clrName,
                                                                    int    clrOrdinal)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrName.SafeHasContent());

            var apiEnumerationValue = new ApiEnumerationValue(apiName, apiDescription, clrName, clrOrdinal);
            return apiEnumerationValue;
        }

        public static ApiIdentity CreateApiIdentity(IApiProperty apiProperty)
        {
            Contract.Requires(apiProperty != null);

            var apiIdentity = new ApiIdentity(apiProperty);
            return apiIdentity;
        }

        public static ApiObjectType CreateApiObjectType(string                        apiName,
                                                        string                        apiDescription,
                                                        IEnumerable<IApiProperty>     apiProperties,
                                                        IApiIdentity                  apiIdentity,
                                                        IEnumerable<IApiRelationship> apiRelationships,
                                                        Type                          clrType)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrType != null);

            var apiPropertiesCollection    = apiProperties.SafeToReadOnlyCollection();
            var apiRelationshipsCollection = apiRelationships.SafeToReadOnlyCollection();
            var apiObjectType = new ApiObjectType(apiName,
                                                  apiDescription,
                                                  apiPropertiesCollection,
                                                  apiIdentity,
                                                  apiRelationshipsCollection,
                                                  clrType);
            return apiObjectType;
        }

        public static ApiProperty CreateApiProperty(string           apiName,
                                                    string           apiDescription,
                                                    IApiType         apiType,
                                                    ApiTypeModifiers apiTypeModifiers,
                                                    string           clrName)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiType != null);
            Contract.Requires(clrName.SafeHasContent());

            var apiProperty = new ApiProperty(apiName,
                                              apiDescription,
                                              apiType,
                                              apiTypeModifiers,
                                              clrName);
            return apiProperty;
        }

        public static ApiProperty CreateApiProperty(string           apiName,
                                                    string           apiDescription,
                                                    IApiTypeResolver apiTypeResolver,
                                                    ApiTypeModifiers apiTypeModifiers,
                                                    string           clrName)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiTypeResolver != null);
            Contract.Requires(clrName.SafeHasContent());

            var apiProperty = new ApiProperty(apiName,
                                              apiDescription,
                                              apiTypeResolver,
                                              apiTypeModifiers,
                                              clrName);
            return apiProperty;
        }

        public static ApiRelationship CreateApiRelationship(IApiProperty               apiProperty,
                                                            ApiRelationshipCardinality apiCardinality,
                                                            IApiType                   apiRelatedType)
        {
            Contract.Requires(apiProperty != null);

            var apiRelationship = new ApiRelationship(apiProperty,
                                                      apiCardinality,
                                                      apiRelatedType);
            return apiRelationship;
        }

        public static ApiRelationship CreateApiRelationship(IApiProperty               apiProperty,
                                                            ApiRelationshipCardinality apiCardinality,
                                                            IApiTypeResolver           apiRelatedTypeResolver)
        {
            Contract.Requires(apiProperty != null);

            var apiRelationship = new ApiRelationship(apiProperty,
                                                      apiCardinality,
                                                      apiRelatedTypeResolver);
            return apiRelationship;
        }

        public static ApiScalarType CreateApiScalarType(string apiName,
                                                        string apiDescription,
                                                        Type   clrType)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrType != null);

            var apiScalarType = new ApiScalarType(apiName,
                                                  apiDescription,
                                                  clrType);
            return apiScalarType;
        }

        public static ApiSchema CreateApiSchema(string                           apiName,
                                                IEnumerable<IApiEnumerationType> apiEnumerationTypes,
                                                IEnumerable<IApiObjectType>      apiObjectTypes,
                                                IEnumerable<IApiScalarType>      apiScalarTypes)
        {
            var apiSchema = new ApiSchema(apiName, apiEnumerationTypes, apiObjectTypes, apiScalarTypes);
            return apiSchema;
        }
        #endregion
    }

    internal static class ApiTypeFactory<T>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static ApiEnumerationType CreateApiEnumerationType(string                            apiName,
                                                                  string                            apiDescription,
                                                                  IEnumerable<IApiEnumerationValue> apiEnumerationValues)
        {
            Contract.Requires(apiName.SafeHasContent());

            var clrType = typeof(T);
            var apiEnumerationType = ApiTypeFactory.CreateApiEnumerationType(apiName,
                                                                             apiDescription,
                                                                             apiEnumerationValues,
                                                                             clrType);
            return apiEnumerationType;
        }

        public static ApiObjectType CreateApiObjectType(string                        apiName,
                                                        string                        apiDescription,
                                                        IEnumerable<IApiProperty>     apiProperties,
                                                        IApiIdentity                  apiIdentity,
                                                        IEnumerable<IApiRelationship> apiRelationships)
        {
            Contract.Requires(apiName.SafeHasContent());

            var clrType = typeof(T);
            var apiObjectType = ApiTypeFactory.CreateApiObjectType(apiName,
                                                                   apiDescription,
                                                                   apiProperties,
                                                                   apiIdentity,
                                                                   apiRelationships,
                                                                   clrType);
            return apiObjectType;
        }

        public static ApiProperty CreateApiProperty<TProperty>(string                         apiName,
                                                               IApiType                       apiType,
                                                               ApiTypeModifiers               apiTypeModifiers,
                                                               Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiType != null);

            var clrName = StaticReflection.GetMemberName(clrPropertySelector);
            var apiProperty = ApiTypeFactory.CreateApiProperty(apiName,
                                                               null,
                                                               apiType,
                                                               apiTypeModifiers,
                                                               clrName);
            return apiProperty;
        }

        public static ApiProperty CreateApiProperty<TProperty>(string                         apiName,
                                                               string                         apiDescription,
                                                               IApiType                       apiType,
                                                               ApiTypeModifiers               apiTypeModifiers,
                                                               Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiType != null);

            var clrName = StaticReflection.GetMemberName(clrPropertySelector);
            var apiProperty = ApiTypeFactory.CreateApiProperty(apiName,
                                                               apiDescription,
                                                               apiType,
                                                               apiTypeModifiers,
                                                               clrName);
            return apiProperty;
        }

        public static ApiProperty CreateApiProperty<TProperty>(string                         apiName,
                                                               IApiTypeResolver               apiTypeResolver,
                                                               ApiTypeModifiers               apiTypeModifiers,
                                                               Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiTypeResolver != null);

            var clrName = StaticReflection.GetMemberName(clrPropertySelector);
            var apiProperty = ApiTypeFactory.CreateApiProperty(apiName,
                                                               null,
                                                               apiTypeResolver,
                                                               apiTypeModifiers,
                                                               clrName);
            return apiProperty;
        }

        public static ApiProperty CreateApiProperty<TProperty>(string                         apiName,
                                                               string                         apiDescription,
                                                               IApiTypeResolver               apiTypeResolver,
                                                               ApiTypeModifiers               apiTypeModifiers,
                                                               Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiTypeResolver != null);

            var clrName = StaticReflection.GetMemberName(clrPropertySelector);
            var apiProperty = ApiTypeFactory.CreateApiProperty(apiName,
                                                               apiDescription,
                                                               apiTypeResolver,
                                                               apiTypeModifiers,
                                                               clrName);
            return apiProperty;
        }

        public static ApiScalarType CreateApiScalarType(string apiName,
                                                        string apiDescription)
        {
            Contract.Requires(apiName.SafeHasContent());

            var clrType = typeof(T);
            var apiScalarType = ApiTypeFactory.CreateApiScalarType(apiName,
                                                                   apiDescription,
                                                                   clrType);
            return apiScalarType;
        }
        #endregion
    }
}