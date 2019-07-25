// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiNamingConventionsConfiguration : ApiConventionsConfiguration<IApiNamingConvention>
                                                       , IApiNamingConventionsBuilder
                                                       , IApiNamingConventionsFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiNamingConventionsBuilder Implementation
        public IApiNamingConventionsBuilder HasCamelCaseConvention()
        {
            return this.WasConventionAlreadyAdded(ApiCamelCaseNamingConvention) ? this : this.HasConvention(ApiCamelCaseNamingConvention);
        }

        public IApiNamingConventionsBuilder HasKebabCaseConvention()
        {
            return this.WasConventionAlreadyAdded(ApiKebabCaseNamingConvention) ? this : this.HasConvention(ApiKebabCaseNamingConvention);
        }

        public IApiNamingConventionsBuilder HasLowerCaseConvention()
        {
            return this.WasConventionAlreadyAdded(ApiLowerCaseNamingConvention) ? this : this.HasConvention(ApiLowerCaseNamingConvention);
        }

        public IApiNamingConventionsBuilder HasPascalCaseConvention()
        {
            return this.WasConventionAlreadyAdded(ApiPascalCaseNamingConvention) ? this : this.HasConvention(ApiPascalCaseNamingConvention);
        }

        public IApiNamingConventionsBuilder HasPluralizeConvention()
        {
            return this.WasConventionAlreadyAdded(ApiPluralizeNamingConvention) ? this : this.HasConvention(ApiPluralizeNamingConvention);
        }

        public IApiNamingConventionsBuilder HasSingularizeConvention()
        {
            return this.WasConventionAlreadyAdded(ApiSingularizeNamingConvention) ? this : this.HasConvention(ApiSingularizeNamingConvention);
        }

        public IApiNamingConventionsBuilder HasUpperCaseConvention()
        {
            return this.WasConventionAlreadyAdded(ApiUpperCaseNamingConvention) ? this : this.HasConvention(ApiUpperCaseNamingConvention);
        }

        public IApiNamingConventionsBuilder HasConvention(IApiNamingConvention convention)
        {
            this.HasConventionImpl(convention);
            return this;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static IApiNamingConvention ApiCamelCaseNamingConvention   { get; } = new ApiCamelCaseNamingConvention();
        private static IApiNamingConvention ApiKebabCaseNamingConvention   { get; } = new ApiKebabCaseNamingConvention();
        private static IApiNamingConvention ApiLowerCaseNamingConvention   { get; } = new ApiLowerCaseNamingConvention();
        private static IApiNamingConvention ApiPascalCaseNamingConvention  { get; } = new ApiPascalCaseNamingConvention();
        private static IApiNamingConvention ApiPluralizeNamingConvention   { get; } = new ApiPluralizeNamingConvention();
        private static IApiNamingConvention ApiSingularizeNamingConvention { get; } = new ApiSingularizeNamingConvention();
        private static IApiNamingConvention ApiUpperCaseNamingConvention   { get; } = new ApiUpperCaseNamingConvention();
        #endregion
    }
}