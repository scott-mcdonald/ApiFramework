// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiEnumerationTypeBuilder"/>
    /// <summary>
    /// Represents the configuration to build and create an API scalar type in an API schema.
    /// </summary>
    public abstract class ApiEnumerationTypeConfiguration : ApiTypeConfiguration, IApiEnumerationTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiEnumerationTypeBuilder Implementation
        IApiEnumerationTypeBuilder IApiEnumerationTypeBuilder.HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableEnumerationTypeFactory.AddModifier(apiConfigurationLevel,
                                                              x =>
                                                              {
                                                                  ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableEnumerationTypeModifier));
                                                                  x.ApiName = apiName;
                                                              },
                                                              CallerTypeName);
            return this;
        }

        IApiEnumerationTypeBuilder IApiEnumerationTypeBuilder.HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableEnumerationTypeFactory.AddModifier(apiConfigurationLevel,
                                                              x =>
                                                              {
                                                                  ApiFrameworkLog.Trace($"Modified {nameof(x.ApiDescription)} '{x.ApiDescription}' => '{apiDescription}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableEnumerationTypeModifier));
                                                                  x.ApiDescription = apiDescription;
                                                              },
                                                              CallerTypeName);
            return this;
        }

        IApiEnumerationTypeBuilder IApiEnumerationTypeBuilder.ApiEnumerationValue(string clrName, int clrOrdinal, Func<IApiEnumerationValueBuilder, IApiEnumerationValueBuilder> configuration)
        {
            Contract.Requires(clrName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableEnumerationTypeFactory.AddModifier(apiConfigurationLevel,
                                                              x =>
                                                              {
                                                                  var apiPrecedenceStack               = this.ApiPrecedenceStack;
                                                                  var apiEnumerationValueConfiguration = GetOrAddApiEnumerationValueConfiguration(x, clrName, clrOrdinal, apiConfigurationLevel, apiPrecedenceStack);
                                                                  configuration?.Invoke(apiEnumerationValueConfiguration);
                                                              },
                                                              CallerTypeName);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiEnumerationTypeConfiguration(Type clrEnumerationType, ApiPrecedenceStack apiPrecedenceStack)
            : base(clrEnumerationType)
        {
            Contract.Requires(clrEnumerationType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack               = apiPrecedenceStack;
            this.ApiMutableEnumerationTypeFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationType>(this.CreateApiMutableEnumerationTypeFactory(clrEnumerationType), IndentConstants.ApiMutableEnumerationType);
        }

        internal ApiEnumerationTypeConfiguration(Type clrEnumerationType, ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationType> apiMutableEnumerationTypeFactory)
            : base(clrEnumerationType)
        {
            Contract.Requires(clrEnumerationType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack               = apiPrecedenceStack;
            this.ApiMutableEnumerationTypeFactory = apiMutableEnumerationTypeFactory;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiTypeKind ApiTypeKind => ApiTypeKind.Enumeration;

        internal override ApiMutableFactory ApiMutableFactory => this.ApiMutableEnumerationTypeFactory;
        #endregion

        #region Properties
        internal ApiPrecedenceStack ApiPrecedenceStack { get; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiMutableType CreateApiMutableType(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var apiMutableEnumerationType = this.ApiMutableEnumerationTypeFactory.Create(apiMutableSchema);
            return apiMutableEnumerationType;
        }

        internal override IApiType CreateApiType(ApiMutableType apiMutableType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableEnumerationType = (ApiMutableEnumerationType)apiMutableType;

            ApiFrameworkLog.Debug($"Creating {nameof(ApiEnumerationType)}".Indent(IndentConstants.ApiEnumerationType));
            var apiEnumerationType = CreateApiEnumerationType(apiMutableEnumerationType);
            ApiFrameworkLog.Debug($"Created {apiEnumerationType}".Indent(IndentConstants.ApiEnumerationType));

            return apiEnumerationType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationType> ApiMutableEnumerationTypeFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyApiEnumerationTypeNameConventions(Type clrEnumerationType, IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(clrEnumerationType != null);
            Contract.Requires(apiConventionSet != null);

            var apiEnumerationTypeNameConventions = apiConventionSet?.ApiEnumerationTypeNameConventions;
            if (apiEnumerationTypeNameConventions == null)
                return;

            var apiEnumerationTypeNameConventionsCollection = apiEnumerationTypeNameConventions.SafeToReadOnlyCollection();
            if (!apiEnumerationTypeNameConventionsCollection.Any())
                return;

            var apiName = clrEnumerationType.Name;
            apiName = apiEnumerationTypeNameConventionsCollection.Aggregate(apiName, (current, apiNamingConvention) => apiNamingConvention.Apply(current, apiConventionSettings));

            var apiEnumerationTypeBuilder = (IApiEnumerationTypeBuilder)this;
            apiEnumerationTypeBuilder.HasName(apiName);
        }

        private void ApplyApiEnumerationTypeConventions(IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiConventionSet != null);

            var apiEnumerationTypeConventions = apiConventionSet?.ApiEnumerationTypeConventions;
            if (apiEnumerationTypeConventions == null)
                return;

            foreach (var apiEnumerationTypeConvention in apiEnumerationTypeConventions)
            {
                apiEnumerationTypeConvention.Apply(this, apiConventionSettings);
            }
        }

        private Func<ApiMutableSchema, ApiMutableEnumerationType> CreateApiMutableEnumerationTypeFactory(Type clrEnumerationType)
        {
            Contract.Requires(clrEnumerationType != null);

            ApiMutableEnumerationType ApiMutableEnumerationTypeFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(apiMutableSchema != null);

                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                var apiConventionSet      = apiMutableSchema?.ApiConventionSet;
                var apiConventionSettings = apiMutableSchema?.ApiConventionSettings;
                this.ApplyApiEnumerationTypeNameConventions(clrEnumerationType, apiConventionSet, apiConventionSettings);
                this.ApplyApiEnumerationTypeConventions(apiConventionSet, apiConventionSettings);

                this.ApiPrecedenceStack.Pop();

                // Create API scalar type context
                var apiDefaultName        = clrEnumerationType.Name;
                var apiDefaultDescription = clrEnumerationType.CreateDefaultApiEnumerationTypeDescription();
                var apiMutableEnumerationType = new ApiMutableEnumerationType
                {
                    ApiMutableSchema   = apiMutableSchema,
                    ApiName            = apiDefaultName,
                    ApiDescription     = apiDefaultDescription,
                    ClrEnumerationType = clrEnumerationType
                };
                return apiMutableEnumerationType;
            }

            return ApiMutableEnumerationTypeFactory;
        }

        private static IApiEnumerationType CreateApiEnumerationType(ApiMutableEnumerationType apiMutableEnumerationType)
        {
            Contract.Requires(apiMutableEnumerationType != null);

            var apiName              = apiMutableEnumerationType.ApiName;
            var apiDescription       = apiMutableEnumerationType.ApiDescription;
            var apiEnumerationValues = CreateApiEnumValues(apiMutableEnumerationType);
            var clrEnumerationType   = apiMutableEnumerationType.ClrEnumerationType;
            var apiEnumerationType   = ApiTypeFactory.CreateApiEnumerationType(apiName, apiDescription, apiEnumerationValues, clrEnumerationType);
            return apiEnumerationType;
        }

        private static IReadOnlyCollection<IApiEnumerationValue> CreateApiEnumValues(ApiMutableEnumerationType apiMutableEnumerationType)
        {
            Contract.Requires(apiMutableEnumerationType != null);

            var apiMutableSchema = apiMutableEnumerationType.ApiMutableSchema;
            var apiEnumerationValueConfigurations = apiMutableEnumerationType.ApiEnumerationValueConfigurations
                                                                             .OrderBy(x => x.Key)
                                                                             .Select(x => x.Value)
                                                                             .ToList();
            var apiEnumValues = apiEnumerationValueConfigurations.Select(x => x.Create(apiMutableSchema))
                                                                 .ToList();
            return apiEnumValues;
        }

        private static ApiEnumerationValueConfiguration GetOrAddApiEnumerationValueConfiguration(ApiMutableEnumerationType apiMutableEnumerationType,
                                                                                                 string                    clrName,
                                                                                                 int                       clrOrdinal,
                                                                                                 ApiPrecedenceLevel        apiPrecedenceLevel,
                                                                                                 ApiPrecedenceStack        apiPrecedenceStack)
        {
            Contract.Requires(apiMutableEnumerationType != null);
            Contract.Requires(clrName.SafeHasContent());
            Contract.Requires(apiPrecedenceStack != null);

            if (apiMutableEnumerationType.ApiEnumerationValueConfigurations.TryGetValue(clrOrdinal, out var apiEnumerationValueConfigurationExisting))
            {
                return apiEnumerationValueConfigurationExisting;
            }

            var apiEnumerationValueConfigurationNew = new ApiEnumerationValueConfiguration(clrName, clrOrdinal, apiPrecedenceStack);

            apiMutableEnumerationType.ApiEnumerationValueConfigurations.Add(clrOrdinal, apiEnumerationValueConfigurationNew);

            ApiFrameworkLog.Trace($"Added {nameof(ApiEnumerationValue)} [{nameof(ApiEnumerationValue.ClrName)}={clrName}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableEnumerationTypeModifier));

            return apiEnumerationValueConfigurationNew;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiEnumerationTypeConfiguration);
        #endregion
    }

    /// <inheritdoc cref="IApiEnumerationTypeBuilder{TEnumeration}"/>
    /// <inheritdoc cref="ApiEnumerationTypeConfiguration"/>
    /// <typeparam name="TEnumeration">
    /// The CLR scalar type associated to the API scalar type for CLR serializing/deserializing purposes.
    /// </typeparam>
    public class ApiEnumerationTypeConfiguration<TEnumeration> : ApiEnumerationTypeConfiguration, IApiEnumerationTypeBuilder<TEnumeration>
        where TEnumeration : Enum
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiEnumerationTypeConfiguration()
            : base(typeof(TEnumeration), new ApiPrecedenceStack(ApiPrecedenceLevel.TypeConfiguration))
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiEnumerationTypeBuilder<TEnumeration> Implementation
        public IApiEnumerationTypeBuilder<TEnumeration> HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiEnumerationTypeBuilder = (IApiEnumerationTypeBuilder)this;
            apiEnumerationTypeBuilder.HasName(apiName);
            return this;
        }

        public IApiEnumerationTypeBuilder<TEnumeration> HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiEnumerationTypeBuilder = (IApiEnumerationTypeBuilder)this;
            apiEnumerationTypeBuilder.HasDescription(apiDescription);
            return this;
        }

        public IApiEnumerationTypeBuilder<TEnumeration> ApiEnumerationValue(Expression<Func<TEnumeration>>                                 clrEnumerationValueSelector,
                                                                            Func<IApiEnumerationValueBuilder, IApiEnumerationValueBuilder> configuration)
        {
            Contract.Requires(clrEnumerationValueSelector != null);

            var clrEnumerationValueExpression               = clrEnumerationValueSelector.Body;
            var clrEnumerationValueExpressionExpressionType = clrEnumerationValueExpression.NodeType;
            if (clrEnumerationValueExpressionExpressionType != ExpressionType.Constant)
            {
                throw new ArgumentException("Invalid expression, must be a ConstantExpression.");
            }

            var clrEnumerationValueConstantExpression = (ConstantExpression)clrEnumerationValueExpression;
            var clrEnumerationValue                   = (TEnumeration)clrEnumerationValueConstantExpression.Value;
            var clrEnumerationValueConvertible        = (IConvertible)clrEnumerationValue;
            var clrName                               = clrEnumerationValueConvertible.ToString(CultureInfo.InvariantCulture);
            var clrOrdinal                            = clrEnumerationValueConvertible.ToInt32(CultureInfo.InvariantCulture);

            var ApiEnumerationTypeBuilder = (IApiEnumerationTypeBuilder)this;
            ApiEnumerationTypeBuilder.ApiEnumerationValue(clrName, clrOrdinal, configuration);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiEnumerationTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack)
            : base(typeof(TEnumeration), apiPrecedenceStack)
        {
        }

        internal ApiEnumerationTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationType> apiMutableEnumerationTypeFactory)
            : base(typeof(TEnumeration), apiPrecedenceStack, apiMutableEnumerationTypeFactory)
        {
        }
        #endregion
    }
}