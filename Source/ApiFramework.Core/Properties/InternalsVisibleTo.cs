﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Runtime.CompilerServices;

// Allow framework assemblies to be friend assemblies of this assembly

// Allow unit test assemblies to be friend assemblies of this assembly
[assembly: InternalsVisibleTo("ApiFramework.Core.Tests")]