// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Reflection;

using ApiFramework.Schema.Annotations;
using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Configuration.Internal;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiAnnotationDiscoveryEnumTypeConvention : IApiEnumerationTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiEnumerationTypeBuilder apiEnumerationTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiEnumerationTypeBuilder != null);

            var clrEnumerationType                = apiEnumerationTypeBuilder.ClrType;
            var clrEnumerationFieldInfoCollection = ClrEnumerationValueDiscoveryRules.GetClrEnumerationValues(clrEnumerationType);

            var apiEnumerationTypeConfiguration = (ApiEnumerationTypeConfiguration)apiEnumerationTypeBuilder;
            var apiPrecedenceStack              = apiEnumerationTypeConfiguration.ApiPrecedenceStack;

            foreach (var clrEnumerationFieldInfo in clrEnumerationFieldInfoCollection)
            {
                HandleApiEnumerationValueAttribute(apiEnumerationTypeBuilder, apiPrecedenceStack, clrEnumerationFieldInfo);
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void HandleApiEnumerationValueAttribute(IApiEnumerationTypeBuilder apiEnumerationTypeBuilder,
                                                               ApiPrecedenceStack         apiPrecedenceStack,
                                                               FieldInfo                  clrFieldInfo)
        {
            Contract.Requires(apiEnumerationTypeBuilder != null);
            Contract.Requires(apiPrecedenceStack != null);
            Contract.Requires(clrFieldInfo != null);

            var apiEnumerationValueAttribute = (ApiEnumerationValueAttribute)Attribute.GetCustomAttribute(clrFieldInfo, typeof(ApiEnumerationValueAttribute));
            if (apiEnumerationValueAttribute == null)
                return;

            IApiEnumerationValueBuilder ApiEnumerationValueConfiguration(IApiEnumerationValueBuilder apiEnumerationValueBuilder)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);

                var apiName = apiEnumerationValueAttribute.Name;
                if (apiName != null)
                {
                    apiEnumerationValueBuilder.HasName(apiName);
                }

                var apiDescription = apiEnumerationValueAttribute.Description;
                if (apiDescription != null)
                {
                    apiEnumerationValueBuilder.HasDescription(apiDescription);
                }

                apiPrecedenceStack.Pop();

                return apiEnumerationValueBuilder;
            }

            var clrEnumerationType = apiEnumerationTypeBuilder.ClrType;
            var clrName            = clrFieldInfo.Name;
            var clrOrdinal         = (int)Enum.Parse(clrEnumerationType, clrName);

            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiEnumerationTypeBuilder.ApiEnumerationValue(clrName, clrOrdinal, ApiEnumerationValueConfiguration);
            apiPrecedenceStack.Pop();
        }
        #endregion
    }
}