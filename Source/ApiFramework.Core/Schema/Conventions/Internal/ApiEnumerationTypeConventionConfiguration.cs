// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiEnumerationTypeConventionsConfiguration : ApiConventionsConfiguration<IApiEnumerationTypeConvention>
                                                                , IApiEnumerationTypeConventionsBuilder
                                                                , IApiEnumerationTypeConventionsFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiEnumerationTypeConventionsBuilder Implementation
        public IApiEnumerationTypeConventionsBuilder HasAnnotationDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiAnnotationDiscoveryEnumTypeConvention) ? this : this.HasConvention(ApiAnnotationDiscoveryEnumTypeConvention);
        }

        public IApiEnumerationTypeConventionsBuilder HasEnumValueDiscoveryConvention()
        {
            return this.WasConventionAlreadyAdded(ApiEnumerationValueDiscoveryEnumTypeConvention) ? this : this.HasConvention(ApiEnumerationValueDiscoveryEnumTypeConvention);
        }

        public IApiEnumerationTypeConventionsBuilder HasConvention(IApiEnumerationTypeConvention convention)
        {
            this.HasConventionImpl(convention);
            return this;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static IApiEnumerationTypeConvention ApiAnnotationDiscoveryEnumTypeConvention       { get; } = new ApiAnnotationDiscoveryEnumTypeConvention();
        private static IApiEnumerationTypeConvention ApiEnumerationValueDiscoveryEnumTypeConvention { get; } = new ApiEnumerationValueDiscoveryEnumTypeConvention();
        #endregion
    }
}