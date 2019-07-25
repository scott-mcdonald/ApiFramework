// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>
    /// Represents metadata of an API enumeration where an enumeration is a special type of scalar that is restricted to a particular set of allowed values.
    /// </summary>
    public interface IApiEnumerationType : IApiNamedType, IExtensibleObject<IApiEnumerationType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the collection of all allowed API enumeration values in the API enumeration type.</summary>
        IEnumerable<IApiEnumerationValue> ApiEnumerationValues { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Try and get the API enumeration value by API name.</summary>
        /// <param name="apiName">API name to lookup the API enumeration value by.</param>
        /// <param name="apiEnumerationValue">The API enumeration value if it exists in the API enumeration type, null otherwise.</param>
        /// <returns>True if API enumeration value exists in the API enumeration type, false otherwise.</returns>
        bool TryGetApiEnumerationValueByApiName(string apiName, out IApiEnumerationValue apiEnumerationValue);

        /// <summary>Try and get the API enumeration value by CLR name.</summary>
        /// <param name="clrName">CLR name to lookup the API enumeration value by.</param>
        /// <param name="apiEnumerationValue">The API enumeration value if it exists in the API enumeration type, null otherwise.</param>
        /// <returns>True if API enumeration value exists in the API enumeration type, false otherwise.</returns>
        bool TryGetApiEnumerationValueByClrName(string clrName, out IApiEnumerationValue apiEnumerationValue);

        /// <summary>Try and get the API enumeration value by CLR ordinal.</summary>
        /// <param name="clrOrdinal">CLR ordinal to lookup the API enumeration value by.</param>
        /// <param name="apiEnumerationValue">The API enumeration value if it exists in the API enumeration type, null otherwise.</param>
        /// <returns>True if API enumeration value exists in the API enumeration type, false otherwise.</returns>
        bool TryGetApiEnumerationValueByClrOrdinal(int clrOrdinal, out IApiEnumerationValue apiEnumerationValue);
        #endregion
    }
}
