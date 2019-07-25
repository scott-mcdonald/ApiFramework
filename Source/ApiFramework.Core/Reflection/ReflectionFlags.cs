// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Reflection
{
    /// <summary>
    /// Reflection flags that control what reflected types are included in a search by the <see cref="TypeReflection"/> static class.
    /// </summary>
    [Flags]
    public enum ReflectionFlags
    {
        /// <summary>Specifies no reflection flag.</summary>
        Default = 0,

        /// <summary>Specifies that the case of the member name should not be considered when searching.</summary>
        IgnoreCase = 1,

        /// <summary>Specifies that only members declared at the level of the supplied type's hierarchy should be considered. Inherited members are not considered.</summary>
        DeclaredOnly = 2,

        /// <summary>Specifies that instance members are to be included in the search.</summary>
        Instance = 4,

        /// <summary>Specifies that static members are to be included in the search.</summary>
        Static = 8,

        /// <summary>Specifies that public members are to be included in the search.</summary>
        Public = 16,

        /// <summary>Specifies that non-public members are to be included in the search.</summary>
        NonPublic = 32,
    }
}