// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiEnumerationValue : IApiEnumerationValue
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiEnumerationValue(string apiName,
                                   string apiDescription,
                                   string clrName,
                                   int    clrOrdinal)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrName.SafeHasContent());

            this.ApiName        = apiName;
            this.ApiDescription = apiDescription;
            this.ClrName        = clrName;
            this.ClrOrdinal     = clrOrdinal;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiEnumValue Implementation
        public string ApiName        { get; }
        public string ApiDescription { get; }
        public string ClrName        { get; }
        public int    ClrOrdinal     { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiEnumerationValue)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ClrName)}={this.ClrName} {nameof(this.ClrOrdinal)}={this.ClrOrdinal}]";
        }
        #endregion

        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor, int depth)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiEnumerationValue(this, depth);
        }
        #endregion
    }
}