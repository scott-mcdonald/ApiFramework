// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Schema
{
    /// <summary>Extension methods for the <see cref="IApiProperty"/> interface.</summary>
    public static class ApiPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>Gets if the API property is required to have a value.</summary>
        /// <returns>True if the API property is required to have a value, false otherwise.</returns>
        public static bool IsRequired(this IApiProperty apiProperty)
        {
            Contract.Requires(apiProperty != null);

            return apiProperty.ApiTypeModifiers.HasFlag(ApiTypeModifiers.Required);
        }
        #endregion
    }
}