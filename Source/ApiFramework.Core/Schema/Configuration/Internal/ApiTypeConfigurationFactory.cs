// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Expressions;
using ApiFramework.Reflection;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ApiTypeConfigurationFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static TApiTypeConfiguration CreateApiTypeConfiguration<TApiTypeConfiguration>(Type               clrType,
                                                                                              Type               apiTypeConfigurationOpenGenericType,
                                                                                              ApiPrecedenceStack apiPrecedenceStack)
            where TApiTypeConfiguration : ApiTypeConfiguration
        {
            Contract.Requires(clrType != null);
            Contract.Requires(apiTypeConfigurationOpenGenericType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiTypeConfigurationClosedGenericType = apiTypeConfigurationOpenGenericType.MakeGenericType(clrType);
            var apiTypeConfigurationNewExpression     = ExpressionBuilder.New<ApiPrecedenceStack, TApiTypeConfiguration>(ReflectionFlags.NonPublic, apiTypeConfigurationClosedGenericType);
            var apiTypeConfigurationNewLambda         = apiTypeConfigurationNewExpression.Compile();
            var apiTypeConfiguration                  = apiTypeConfigurationNewLambda(apiPrecedenceStack);
            return apiTypeConfiguration;
        }

        public static TApiTypeConfiguration CreateApiTypeConfiguration<TApiTypeConfiguration, TApiTypeConfigurationFactory, TApiTypeContext>(Type                         clrType,
                                                                                                                                             Type                         apiTypeConfigurationOpenGenericType,
                                                                                                                                             ApiPrecedenceStack           apiPrecedenceStack,
                                                                                                                                             TApiTypeConfigurationFactory apiTypeConfigurationFactory)
            where TApiTypeConfiguration : ApiTypeConfiguration
            where TApiTypeConfigurationFactory : ApiMutableFactory<ApiMutableSchema, TApiTypeContext>
        {
            Contract.Requires(clrType != null);
            Contract.Requires(apiTypeConfigurationOpenGenericType != null);
            Contract.Requires(apiPrecedenceStack != null);
            Contract.Requires(apiTypeConfigurationFactory != null);

            var apiTypeConfigurationClosedGenericType = apiTypeConfigurationOpenGenericType.MakeGenericType(clrType);
            var apiTypeConfigurationNewExpression     = ExpressionBuilder.New<ApiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, TApiTypeContext>, TApiTypeConfiguration>(ReflectionFlags.NonPublic, apiTypeConfigurationClosedGenericType);
            var apiTypeConfigurationNewLambda         = apiTypeConfigurationNewExpression.Compile();
            var apiTypeConfiguration                  = apiTypeConfigurationNewLambda(apiPrecedenceStack, apiTypeConfigurationFactory);
            return apiTypeConfiguration;
        }
        #endregion
    }
}