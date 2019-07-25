// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSchemaConventionsConfiguration : ApiConventionsConfiguration<IApiSchemaConvention>
        , IApiSchemaConventionsBuilder
        , IApiSchemaConventionsFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiSchemaConventionsBuilder Implementation
        public IApiSchemaConventionsBuilder HasAnnotationDiscoveryConvention()
        { return this.WasConventionAlreadyAdded(ApiAnnotationDiscoverySchemaConvention) ? this : this.HasConvention(ApiAnnotationDiscoverySchemaConvention); } 

        public IApiSchemaConventionsBuilder HasConfigurationDiscoveryConvention()
        { return this.WasConventionAlreadyAdded(ApiConfigurationDiscoverySchemaConvention) ? this : this.HasConvention(ApiConfigurationDiscoverySchemaConvention); } 

        public IApiSchemaConventionsBuilder HasTypeDiscoveryConvention()
        { return this.WasConventionAlreadyAdded(ApiTypeDiscoverySchemaConvention) ? this : this.HasConvention(ApiTypeDiscoverySchemaConvention); } 

        public IApiSchemaConventionsBuilder HasConvention(IApiSchemaConvention convention)
        {
            this.HasConventionImpl(convention);
            return this;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static IApiSchemaConvention ApiAnnotationDiscoverySchemaConvention    { get; } = new ApiAnnotationDiscoverySchemaConvention();
        private static IApiSchemaConvention ApiConfigurationDiscoverySchemaConvention { get; } = new ApiConfigurationDiscoverySchemaConvention();
        private static IApiSchemaConvention ApiTypeDiscoverySchemaConvention          { get; } = new ApiTypeDiscoverySchemaConvention();
        #endregion
    }
}