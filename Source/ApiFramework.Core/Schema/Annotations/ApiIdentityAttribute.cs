// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the API property that provides unique identify for an API type.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiIdentityAttribute : Attribute
    { }
}