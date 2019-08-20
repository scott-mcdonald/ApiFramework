// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Document.Tree
{
    /// <inheritdoc cref="ApiPathMixin"/>
    /// <summary>
    /// Represents a null path relationship between a parent and child node.
    ///
    /// Implementation of the <c>null object</c> design pattern.
    /// </summary>
    public class ApiNullPathMixin : ApiPathMixin
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiPathMixin Overrides
        public override ApiPathMixinKind ApiKind => ApiPathMixinKind.Null;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiNullPathMixin)}";
        }
        #endregion
    }
}
