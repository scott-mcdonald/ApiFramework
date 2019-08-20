// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrEnumUtilities<TEnum>
        where TEnum : Enum
    {
        // PUBLIC METHODS //////////////////////////////////////////////////
        #region Methods
        public static string Stringify(TEnum clrEnum)
        {
            return clrEnum.ToString();
        }
        #endregion
    }
}