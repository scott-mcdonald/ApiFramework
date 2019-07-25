﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Tree
{
    /// <summary>Represents an exception that is thrown for an invalid operation in a 1-N tree.</summary>
    public class TreeException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeException()
        {
        }

        public TreeException(string message)
            : base(message)
        {
        }

        public TreeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        #endregion
    }
}