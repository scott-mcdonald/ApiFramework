// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ToTreeStringApiVisitor : IApiVisitor
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiVisitor Implementation
        public void VisitApiEnumerationValue(IApiEnumerationValue apiEnumerationValue, int depth)
        {
            Contract.Requires(apiEnumerationValue != null);

            this.AddIndentToString(depth);

            var description = apiEnumerationValue.ToString();
            this.AddDescriptionToString(description);
        }

        public void VisitApiIdentity(IApiIdentity apiIdentity, int depth)
        {
            Contract.Requires(apiIdentity != null);

            this.AddIndentToString(depth);

            var description = apiIdentity.ToString();
            this.AddDescriptionToString(description);
        }

        public void VisitApiProperty(IApiProperty apiProperty, int depth)
        {
            Contract.Requires(apiProperty != null);

            this.AddIndentToString(depth);

            var description = apiProperty.ToString();
            this.AddDescriptionToString(description);
        }

        public void VisitApiRelationship(IApiRelationship apiRelationship, int depth)
        {
            Contract.Requires(apiRelationship != null);

            this.AddIndentToString(depth);

            var description = apiRelationship.ToString();
            this.AddDescriptionToString(description);
        }

        public void VisitApiSchema(IApiSchema apiSchema)
        {
            var description = apiSchema.ToString();
            this.AddDescriptionToString(description);

            // ApiEnumerationTypes
            foreach (var apiEnumerationType in apiSchema.ApiEnumerationTypes.Cast<ApiEnumerationType>())
            {
                apiEnumerationType.Accept(this, 1);
            }

            // ApiObjectTypes
            foreach (var apiObjectType in apiSchema.ApiObjectTypes.Cast<ApiObjectType>())
            {
                apiObjectType.Accept(this, 1);
            }

            // ApiScalarTypes
            foreach (var apiScalarType in apiSchema.ApiScalarTypes.Cast<ApiScalarType>())
            {
                apiScalarType.Accept(this, 1);
            }
        }

        public void VisitApiType(IApiType apiType, int depth)
        {
            Contract.Requires(apiType != null);

            this.AddIndentToString(depth);

            var apiTypeKind = apiType.ApiTypeKind;
            switch (apiTypeKind)
            {
                case ApiTypeKind.Collection:
                {
                    break;
                }

                case ApiTypeKind.Enumeration:
                {
                    var apiEnumerationType = (IApiEnumerationType)apiType;
                    var description        = apiEnumerationType.ToString();
                    this.AddDescriptionToString(description);

                    var apiEnumerationValues = apiEnumerationType.ApiEnumerationValues.Cast<ApiEnumerationValue>();
                    foreach (var apiEnumerationValue in apiEnumerationValues)
                    {
                        apiEnumerationValue.Accept(this, depth + 1);
                    }

                    break;
                }

                case ApiTypeKind.Scalar:
                {
                    var description = apiType.ToString();
                    this.AddDescriptionToString(description);
                    break;
                }

                case ApiTypeKind.Object:
                {
                    var apiObjectType = (IApiObjectType)apiType;
                    var description   = apiObjectType.ToString();
                    this.AddDescriptionToString(description);

                    var apiProperties = apiObjectType.ApiProperties.Cast<ApiProperty>();
                    foreach (var apiProperty in apiProperties)
                    {
                        apiProperty.Accept(this, depth + 1);
                    }

                    var apiIdentity = (ApiIdentity)apiObjectType.ApiIdentity;
                    apiIdentity?.Accept(this, depth + 1);

                    var apiRelationships = apiObjectType.ApiRelationships.Cast<ApiRelationship>();
                    foreach (var apiRelationship in apiRelationships)
                    {
                        apiRelationship.Accept(this, depth + 1);
                    }

                    break;
                }
            }
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        {
            return this.StringBuilder.ToString().TrimEnd();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private StringBuilder StringBuilder { get; } = new StringBuilder();
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddIndentToString(int depth)
        {
            Contract.Requires(depth >= 0);

            var indentSpace = depth * IndentSize;
            this.StringBuilder.Append(WhitespaceAsCharacter, indentSpace);
        }

        private void AddDescriptionToString(string description)
        {
            Contract.Requires(description.SafeHasContent());

            this.StringBuilder.AppendLine(description);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const char WhitespaceAsCharacter = ' ';
        private const int  IndentSize            = 2;
        #endregion
    }
}