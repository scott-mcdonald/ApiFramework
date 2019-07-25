﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Tree
{
    /// <summary>
    /// Represents guidance on how to continue visiting the 1-N tree.
    /// </summary>
    public enum VisitResult
    {
        /// <summary>
        /// Visiting continues for both child and sibling nodes (if any) in document order.
        /// </summary>
        ContinueWithChildAndSiblingNodes,

        /// <summary>
        /// Visiting continues for sibling nodes only (if any) in document order.
        /// </summary>
        ContinueWithSiblingNodesOnly,

        /// <summary>
        /// Visiting is done and should stop immediately.
        /// </summary>
        Done
    }
}