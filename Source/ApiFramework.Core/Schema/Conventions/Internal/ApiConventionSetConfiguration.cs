// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiConventionSetConfiguration : IApiConventionSetBuilder, IApiConventionSetFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiConventionsBuilder Implementation
        public IApiEnumerationTypeConventionsBuilder ApiEnumerationTypeConventions()
        {
            return this.ApiEnumerationTypeConventionsConfiguration;
        }

        public IApiNamingConventionsBuilder ApiEnumerationTypeNameConventions()
        {
            return this.ApiEnumerationTypeNameConventionsConfiguration;
        }

        public IApiNamingConventionsBuilder ApiEnumerationValueNameConventions()
        {
            return this.ApiEnumerationValueNameConventionsConfiguration;
        }

        public IApiObjectTypeConventionsBuilder ApiObjectTypeConventions()
        {
            return this.ApiObjectTypeConventionsConfiguration;
        }

        public IApiNamingConventionsBuilder ApiObjectTypeNameConventions()
        {
            return this.ApiObjectTypeNameConventionsConfiguration;
        }

        public IApiNamingConventionsBuilder ApiPropertyNameConventions()
        {
            return this.ApiPropertyNameConventionsConfiguration;
        }

        public IApiNamingConventionsBuilder ApiScalarTypeNameConventions()
        {
            return this.ApiScalarTypeNameConventionsConfiguration;
        }

        public IApiSchemaConventionsBuilder ApiSchemaConventions()
        {
            return this.ApiSchemaConventionsConfiguration;
        }
        #endregion

        #region IApiConventionsFactory Implementation
        public IApiConventionSet Create()
        {
            var apiEnumTypeConventions       = this.ApiEnumerationTypeConventionsConfiguration?.Create();
            var apiEnumTypeNameConventions   = this.ApiEnumerationTypeNameConventionsConfiguration?.Create();
            var apiEnumValueNameConventions  = this.ApiEnumerationValueNameConventionsConfiguration?.Create();
            var apiObjectTypeConventions     = this.ApiObjectTypeConventionsConfiguration?.Create();
            var apiObjectTypeNameConventions = this.ApiObjectTypeNameConventionsConfiguration?.Create();
            var apiPropertyNameConventions   = this.ApiPropertyNameConventionsConfiguration?.Create();
            var apiScalarTypeNameConventions = this.ApiScalarTypeNameConventionsConfiguration?.Create();
            var apiSchemaConventions         = this.ApiSchemaConventionsConfiguration?.Create();

            var apiConventionSet = new ApiConventionSet(apiEnumTypeConventions,
                                                        apiEnumTypeNameConventions,
                                                        apiEnumValueNameConventions,
                                                        apiObjectTypeConventions,
                                                        apiObjectTypeNameConventions,
                                                        apiPropertyNameConventions,
                                                        apiScalarTypeNameConventions,
                                                        apiSchemaConventions);
            return apiConventionSet;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiEnumerationTypeConventionsConfiguration ApiEnumerationTypeConventionsConfiguration      { get; } = new ApiEnumerationTypeConventionsConfiguration();
        private ApiNamingConventionsConfiguration          ApiEnumerationTypeNameConventionsConfiguration  { get; } = new ApiNamingConventionsConfiguration();
        private ApiNamingConventionsConfiguration          ApiEnumerationValueNameConventionsConfiguration { get; } = new ApiNamingConventionsConfiguration();
        private ApiObjectTypeConventionsConfiguration      ApiObjectTypeConventionsConfiguration           { get; } = new ApiObjectTypeConventionsConfiguration();
        private ApiNamingConventionsConfiguration          ApiObjectTypeNameConventionsConfiguration       { get; } = new ApiNamingConventionsConfiguration();
        private ApiNamingConventionsConfiguration          ApiPropertyNameConventionsConfiguration         { get; } = new ApiNamingConventionsConfiguration();
        private ApiNamingConventionsConfiguration          ApiScalarTypeNameConventionsConfiguration       { get; } = new ApiNamingConventionsConfiguration();
        private ApiSchemaConventionsConfiguration          ApiSchemaConventionsConfiguration               { get; } = new ApiSchemaConventionsConfiguration();
        #endregion
    }
}