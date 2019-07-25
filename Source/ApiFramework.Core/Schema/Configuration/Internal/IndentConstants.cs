// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class IndentConstants
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Constants
        public const int ApiCollectionType                = 6;
        public const int ApiMutableCollectionType         = 6;
        public const int ApiMutableCollectionTypeModifier = 8;

        public const int ApiEnumerationTypes               = 2;
        public const int ApiEnumerationType                = 4;
        public const int ApiMutableEnumerationType         = 4;
        public const int ApiMutableEnumerationTypeModifier = 6;

        public const int ApiEnumerationValue                = 6;
        public const int ApiMutableEnumerationValue         = 6;
        public const int ApiMutableEnumerationValueModifier = 8;

        public const int ApiObjectTypes                   = 2;
        public const int ApiObjectType                    = 4;
        public const int ApiMutableObjectType             = 4;
        public const int ApiMutableObjectTypeModifier     = 6;
        public const int ApiMutableObjectTypeIdentity     = 6;
        public const int ApiMutableObjectTypeRelationship = 6;

        public const int ApiProperty                = 6;
        public const int ApiMutableProperty         = 6;
        public const int ApiMutablePropertyModifier = 8;

        public const int ApiResolveTypes      = 2;
        public const int ApiResolveObjectType = 4;
        public const int ApiResolveItemType   = 6;

        public const int ApiScalarTypes               = 2;
        public const int ApiScalarType                = 4;
        public const int ApiMutableScalarType         = 4;
        public const int ApiMutableScalarTypeModifier = 6;

        public const int ApiSchema                = 0;
        public const int ApiMutableSchema         = 2;
        public const int ApiMutableSchemaModifier = 4;
        #endregion
    }
}