// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiConventionSet : IApiConventionSet
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiConventionSet(IEnumerable<IApiEnumerationTypeConvention> apiEnumTypeConventions,
                                IEnumerable<IApiNamingConvention>          apiEnumTypeNameConventions,
                                IEnumerable<IApiNamingConvention>          apiEnumValueNameConventions,
                                IEnumerable<IApiObjectTypeConvention>      apiObjectTypeConventions,
                                IEnumerable<IApiNamingConvention>          apiObjectTypeNameConventions,
                                IEnumerable<IApiNamingConvention>          apiPropertyNameConventions,
                                IEnumerable<IApiNamingConvention>          apiScalarTypeNameConventions,
                                IEnumerable<IApiSchemaConvention>          apiSchemaConventions)
        {
            this.ApiEnumerationTypeConventions      = apiEnumTypeConventions.EmptyIfNull();
            this.ApiEnumerationTypeNameConventions  = apiEnumTypeNameConventions.EmptyIfNull();
            this.ApiEnumerationValueNameConventions = apiEnumValueNameConventions.EmptyIfNull();
            this.ApiObjectTypeConventions           = apiObjectTypeConventions.EmptyIfNull();
            this.ApiObjectTypeNameConventions       = apiObjectTypeNameConventions.EmptyIfNull();
            this.ApiPropertyNameConventions         = apiPropertyNameConventions.EmptyIfNull();
            this.ApiScalarTypeNameConventions       = apiScalarTypeNameConventions.EmptyIfNull();
            this.ApiSchemaConventions               = apiSchemaConventions.EmptyIfNull();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiSchemaConventionSet Implementation
        public IEnumerable<IApiEnumerationTypeConvention> ApiEnumerationTypeConventions      { get; }
        public IEnumerable<IApiNamingConvention>          ApiEnumerationTypeNameConventions  { get; }
        public IEnumerable<IApiNamingConvention>          ApiEnumerationValueNameConventions { get; }
        public IEnumerable<IApiObjectTypeConvention>      ApiObjectTypeConventions           { get; }
        public IEnumerable<IApiNamingConvention>          ApiObjectTypeNameConventions       { get; }
        public IEnumerable<IApiNamingConvention>          ApiPropertyNameConventions         { get; }
        public IEnumerable<IApiNamingConvention>          ApiScalarTypeNameConventions       { get; }
        public IEnumerable<IApiSchemaConvention>          ApiSchemaConventions               { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{typeof(ApiConventionSet).Name}");

            foreach (var convention in this.ApiEnumerationTypeConventions)
            {
                stringBuilder.AppendLine($"  ApiEnumerationTypeConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiEnumerationTypeNameConventions)
            {
                stringBuilder.AppendLine($"  ApiEnumerationTypeNameConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiEnumerationValueNameConventions)
            {
                stringBuilder.AppendLine($"  ApiEnumerationValueNameConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiObjectTypeConventions)
            {
                stringBuilder.AppendLine($"  ApiObjectTypeConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiObjectTypeNameConventions)
            {
                stringBuilder.AppendLine($"  ApiObjectTypeNameConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiPropertyNameConventions)
            {
                stringBuilder.AppendLine($"  ApiPropertyNameConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiScalarTypeNameConventions)
            {
                stringBuilder.AppendLine($"  ApiScalarTypeNameConvention [type={convention.GetType().Name}]");
            }

            foreach (var convention in this.ApiSchemaConventions)
            {
                stringBuilder.AppendLine($"  ApiSchemaConvention [type={convention.GetType().Name}]");
            }

            return stringBuilder.ToString().TrimEnd();
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly ApiConventionSet Empty = new ApiConventionSet(Enumerable.Empty<IApiEnumerationTypeConvention>(),
                                                                             Enumerable.Empty<IApiNamingConvention>(),
                                                                             Enumerable.Empty<IApiNamingConvention>(),
                                                                             Enumerable.Empty<IApiObjectTypeConvention>(),
                                                                             Enumerable.Empty<IApiNamingConvention>(),
                                                                             Enumerable.Empty<IApiNamingConvention>(),
                                                                             Enumerable.Empty<IApiNamingConvention>(),
                                                                             Enumerable.Empty<IApiSchemaConvention>());
        #endregion
    }
}