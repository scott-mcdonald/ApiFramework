// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Configuration.Internal;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiTypeDiscoverySchemaConvention : IApiSchemaConvention
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
                var apiTypeKind = clrType.GetApiTypeKind();
                switch (apiTypeKind)
                {
                    case ApiTypeKind.Object:
                    {
                        if (ClrTypeDiscoveryRules.CanAddApiObjectType(clrType) == false)
                            continue;

                        var apiTypeDiscoveryPredicateResult = apiDiscoverySettings.ApiTypeDiscoveryPredicate?.Invoke(clrType) ?? true;
                        if (apiTypeDiscoveryPredicateResult == false)
                            continue;

                        apiSchemaBuilder.ApiObjectType(clrType);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}