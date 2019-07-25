// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Exceptions;
using ApiFramework.Reflection;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiEnumerationType : ApiNamedType<IApiEnumerationType>, IApiEnumerationType
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        public ApiEnumerationType(string                            apiName,
                                  string                            apiDescription,
                                  IEnumerable<IApiEnumerationValue> apiEnumerationValues,
                                  Type                              clrEnumerationType)
            : base(apiName, apiDescription, clrEnumerationType)
        {
            Contract.Requires(clrEnumerationType != null);

            if (TypeReflection.IsEnum(clrEnumerationType) == false)
            {
                var message = $"Unable to create an API enumeration type, the CLR type [name={clrEnumerationType.Name}] is not a CLR enumeration type.";
                throw new ApiSchemaException(message);
            }

            this.ApiEnumerationValues = apiEnumerationValues.SafeToReadOnlyCollection();

            this.ApiEnumerationValueByApiNameDictionary  = this.ApiEnumerationValues.ToDictionary(x => x.ApiName);
            this.ApiEnumerationValueByClrNameDictionary  = this.ApiEnumerationValues.ToDictionary(x => x.ClrName);
            this.ApiEnumerationValueByClrValueDictionary = this.ApiEnumerationValues.ToDictionary(x => x.ClrOrdinal);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiType Overrides
        public override ApiTypeKind ApiTypeKind => ApiTypeKind.Enumeration;
        #endregion

        #region IApiEnumerationType Implementation
        public IEnumerable<IApiEnumerationValue> ApiEnumerationValues { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiEnumerationType)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ClrType)}={this.ClrType.Name}]";
        }
        #endregion

        #region IApiEnumerationType Implementation
        public bool TryGetApiEnumerationValueByApiName(string apiName, out IApiEnumerationValue apiEnumValue)
        {
            Contract.Requires(apiName != null);

            return this.ApiEnumerationValueByApiNameDictionary.TryGetValue(apiName, out apiEnumValue);
        }

        public bool TryGetApiEnumerationValueByClrName(string clrName, out IApiEnumerationValue apiEnumValue)
        {
            Contract.Requires(clrName != null);

            return this.ApiEnumerationValueByClrNameDictionary.TryGetValue(clrName, out apiEnumValue);
        }

        public bool TryGetApiEnumerationValueByClrOrdinal(int clrOrdinal, out IApiEnumerationValue apiEnumValue)
        {
            return this.ApiEnumerationValueByClrValueDictionary.TryGetValue(clrOrdinal, out apiEnumValue);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiEnumerationType ExtensionOwner => this;
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<string, IApiEnumerationValue> ApiEnumerationValueByApiNameDictionary  { get; }
        private IDictionary<string, IApiEnumerationValue> ApiEnumerationValueByClrNameDictionary  { get; }
        private IDictionary<int, IApiEnumerationValue>    ApiEnumerationValueByClrValueDictionary { get; }
        #endregion
    }
}