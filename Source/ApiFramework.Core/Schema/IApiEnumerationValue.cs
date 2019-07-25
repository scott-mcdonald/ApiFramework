// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata of an individual API enumeration value on a containing API enumeration type.</summary>
    public interface IApiEnumerationValue
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API enumeration value name.</summary>
        /// <remarks>Must be unique among all API enumeration value names on the containing API enumeration.</remarks>
        string ApiName { get; }

        /// <summary>Gets the optional API enumeration value description.</summary>
        string ApiDescription { get; }

        /// <summary>Gets the CLR enumeration value name.</summary>
        /// <remarks>Represents the CLR enumeration value name that is the materialization of the API enumeration in CLR code.</remarks>
        string ClrName { get; }

        /// <summary>Gets the CLR enumeration value ordinal.</summary>
        /// <remarks>Represents the CLR enumeration value ordinal that is the materialization of the API enumeration in CLR code.</remarks>
        int ClrOrdinal { get; }
        #endregion
    }
}
