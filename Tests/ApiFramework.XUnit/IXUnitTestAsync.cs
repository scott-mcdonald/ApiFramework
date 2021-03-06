﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Threading.Tasks;

namespace ApiFramework.XUnit
{
    /// <summary>Represents an individual asynchronous xunit test executed inside an xunit tests object.</summary>
    public interface IXUnitTestAsync
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Task ExecuteAsync(XUnitTests xUnitTests);
        #endregion
    }
}