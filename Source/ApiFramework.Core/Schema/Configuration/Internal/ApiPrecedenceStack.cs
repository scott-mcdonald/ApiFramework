// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiPrecedenceStack
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiPrecedenceStack(ApiPrecedenceLevel initialLevel)
        {
            this.Stack = new Stack<ApiPrecedenceLevel>();
            this.Stack.Push(initialLevel);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ApiPrecedenceLevel CurrentLevel => this.Stack.Peek();
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void Pop()
        {
            this.Stack.Pop();
        }

        public void Push(ApiPrecedenceLevel nextLevel)
        {
            this.Stack.Push(nextLevel);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Stack<ApiPrecedenceLevel> Stack { get; }
        #endregion
    }
}
