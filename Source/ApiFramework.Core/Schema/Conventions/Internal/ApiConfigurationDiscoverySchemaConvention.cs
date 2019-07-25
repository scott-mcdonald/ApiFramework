// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Expressions;
using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiConfigurationDiscoverySchemaConvention : IApiSchemaConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiSchemaConvention Implementation
        public void Apply(IApiSchemaBuilder apiSchemaBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiSchemaBuilder != null);

            apiConventionSettings = apiConventionSettings ?? ApiConventionSettings.Empty;
            var apiDiscoverySettings = apiConventionSettings.ApiDiscoverySettings;
            var assemblies           = apiDiscoverySettings.Assemblies;
            var clrTypes = assemblies.SelectMany(x => x.GetExportedTypes())
                                     .ToList();

            foreach (var clrType in clrTypes)
            {
                if (ClrTypeDiscoveryRules.CanAddApiTypeConfiguration(clrType) == false)
                    continue;

                var apiConfigurationDiscoveryPredicateResult = apiDiscoverySettings.ApiConfigurationDiscoveryPredicate?.Invoke(clrType) ?? true;
                if (apiConfigurationDiscoveryPredicateResult == false)
                    continue;

                var apiTypeConfigurationNewExpression = ExpressionBuilder.New<ApiTypeConfiguration>(clrType);
                var apiTypeConfigurationNewLambda     = apiTypeConfigurationNewExpression.Compile();
                var apiTypeConfiguration              = apiTypeConfigurationNewLambda();

                apiSchemaBuilder.HasConfiguration(apiTypeConfiguration);
            }
        }
        #endregion
    }
}