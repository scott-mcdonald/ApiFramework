// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiEnumerationValueConfiguration : IApiEnumerationValueBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiEnumerationValueConfiguration(string clrName, int clrOrdinal, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrName.SafeHasContent());
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack                = apiPrecedenceStack;
            this.ApiMutableEnumerationValueFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationValue>(this.CreateApiMutableEnumerationValueFactory(clrName, clrOrdinal), IndentConstants.ApiMutableEnumerationValue);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiEnumerationValueBuilder Implementation
        public IApiEnumerationValueBuilder HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableEnumerationValueFactory.AddModifier(apiConfigurationLevel,
                                                               x =>
                                                               {
                                                                   ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableEnumerationValueModifier));
                                                                   x.ApiName = apiName;
                                                               },
                                                               CallerTypeName);
            return this;
        }

        public IApiEnumerationValueBuilder HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableEnumerationValueFactory.AddModifier(apiConfigurationLevel,
                                                               x =>
                                                               {
                                                                   ApiFrameworkLog.Trace(
                                                                       $"Modified {nameof(x.ApiDescription)} '{x.ApiDescription}' => '{apiDescription}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableEnumerationValueModifier));
                                                                   x.ApiDescription = apiDescription;
                                                               },
                                                               CallerTypeName);
            return this;
        }
        #endregion

        #region Factory Methods
        public IApiEnumerationValue Create(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var apiMutableEnumerationValue = this.ApiMutableEnumerationValueFactory.Create(apiMutableSchema);

            var apiEnumerationValue = CreateApiEnumerationValue(apiMutableEnumerationValue);
            ApiFrameworkLog.Debug($"Created {apiEnumerationValue}".Indent(IndentConstants.ApiEnumerationValue));

            return apiEnumerationValue;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiPrecedenceStack ApiPrecedenceStack { get; }

        private ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationValue> ApiMutableEnumerationValueFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyApiEnumerationValueNameConventions(string clrName, IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiConventionSet != null);
            Contract.Requires(clrName.SafeHasContent());

            var apiEnumerationValueNameConventions = apiConventionSet?.ApiEnumerationValueNameConventions;
            if (apiEnumerationValueNameConventions == null)
                return;

            var apiEnumerationValueNameConventionsCollection = apiEnumerationValueNameConventions.SafeToReadOnlyCollection();
            if (!apiEnumerationValueNameConventionsCollection.Any())
                return;

            var apiName = clrName;
            apiName = apiEnumerationValueNameConventionsCollection.Aggregate(apiName, (current, apiNamingConvention) => apiNamingConvention.Apply(current, apiConventionSettings));

            var apiEnumerationValueBuilder = (IApiEnumerationValueBuilder)this;
            apiEnumerationValueBuilder.HasName(apiName);
        }

        private Func<ApiMutableSchema, ApiMutableEnumerationValue> CreateApiMutableEnumerationValueFactory(string clrName, int clrOrdinal)
        {
            Contract.Requires(clrName.SafeHasContent());

            ApiMutableEnumerationValue ApiMutableEnumerationValueFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(clrName.SafeHasContent());

                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                var apiConventionSet      = apiMutableSchema?.ApiConventionSet;
                var apiConventionSettings = apiMutableSchema?.ApiConventionSettings;
                this.ApplyApiEnumerationValueNameConventions(clrName, apiConventionSet, apiConventionSettings);

                this.ApiPrecedenceStack.Pop();

                // Create API scalar type context
                var apiDefaultName = clrName;
                var apiMutableEnumerationValue = new ApiMutableEnumerationValue
                {
                    ApiName    = apiDefaultName,
                    ClrName    = clrName,
                    ClrOrdinal = clrOrdinal
                };
                return apiMutableEnumerationValue;
            }

            return ApiMutableEnumerationValueFactory;
        }

        private static IApiEnumerationValue CreateApiEnumerationValue(ApiMutableEnumerationValue apiMutableEnumerationValue)
        {
            Contract.Requires(apiMutableEnumerationValue != null);

            var apiName             = apiMutableEnumerationValue.ApiName;
            var apiDescription      = apiMutableEnumerationValue.ApiDescription;
            var clrName             = apiMutableEnumerationValue.ClrName;
            var clrOrdinal          = apiMutableEnumerationValue.ClrOrdinal;
            var apiEnumerationValue = ApiTypeFactory.CreateApiEnumerationValue(apiName, apiDescription, clrName, clrOrdinal);
            return apiEnumerationValue;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiEnumerationValueConfiguration);
        #endregion
    }
}