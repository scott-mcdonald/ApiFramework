// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Extension;

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata of an API scalar (singular, individual, primitive) value.</summary>
    public interface IApiScalarType : IApiNamedType, IExtensibleObject<IApiScalarType>
    {
    }
}