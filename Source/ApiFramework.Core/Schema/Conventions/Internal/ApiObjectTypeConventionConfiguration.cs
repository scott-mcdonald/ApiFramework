// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiObjectTypeConventionsConfiguration : ApiConventionsConfiguration<IApiObjectTypeConvention>
        , IApiObjectTypeConventionsBuilder
        , IApiObjectTypeConventionsFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConventionsBuilder Implementation
        public IApiObjectTypeConventionsBuilder HasAnnotationDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiAnnotationDiscoveryObjectTypeConvention) ? this : this.HasConvention(ApiAnnotationDiscoveryObjectTypeConvention);
        }

        public IApiObjectTypeConventionsBuilder HasIdentityDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiIdentityDiscoveryObjectTypeConvention) ? this : this.HasConvention(ApiIdentityDiscoveryObjectTypeConvention);
        }

        public IApiObjectTypeConventionsBuilder HasPropertyDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiPropertyDiscoveryObjectTypeConvention) ? this : this.HasConvention(ApiPropertyDiscoveryObjectTypeConvention);
        }

        public IApiObjectTypeConventionsBuilder HasRelationshipDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiRelationshipDiscoveryObjectTypeConvention) ? this : this.HasConvention(ApiRelationshipDiscoveryObjectTypeConvention);
        }

        public IApiObjectTypeConventionsBuilder HasConvention(IApiObjectTypeConvention convention)
        {
            this.HasConventionImpl(convention);
            return this;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static IApiObjectTypeConvention ApiAnnotationDiscoveryObjectTypeConvention   { get; } = new ApiAnnotationDiscoveryObjectTypeConvention();
        private static IApiObjectTypeConvention ApiIdentityDiscoveryObjectTypeConvention     { get; } = new ApiIdentityDiscoveryObjectTypeConvention();
        private static IApiObjectTypeConvention ApiPropertyDiscoveryObjectTypeConvention     { get; } = new ApiPropertyDiscoveryObjectTypeConvention();
        private static IApiObjectTypeConvention ApiRelationshipDiscoveryObjectTypeConvention { get; } = new ApiRelationshipDiscoveryObjectTypeConvention();
        #endregion
    }
}