// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Threading.Tasks;

namespace ApiFramework.XUnit
{
    /// <summary>Captures boilerplate code for an individual asynchronous xunit test executed asynchronously inside an xunit tests object.</summary>
    public abstract class XUnitTestAsync : IXUnitTestAsync
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IXUnitTestAsync Implementation
        public async Task ExecuteAsync(XUnitTests xUnitTests)
        {
            this.XUnitTests = xUnitTests;

            this.WriteLine("Test Name: {0}", this.Name);
            this.WriteLine();

            await this.ArrangeAsync();
            await this.ActAsync();
            await this.AssertAsync();
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        { return this.Name; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected XUnitTestAsync(string name)
        { this.Name = name; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region XUnitTest Overrides
        protected virtual Task ArrangeAsync()
        { return Task.CompletedTask; }

        protected virtual Task ActAsync()
        { return Task.CompletedTask; }

        protected virtual Task AssertAsync()
        { return Task.CompletedTask; }
        #endregion

        #region Write Methods
        protected void WriteLine()
        { this.XUnitTests.WriteLine(); }

        protected void WriteLine(string message)
        { this.XUnitTests.WriteLine(message); }

        protected void WriteLine(string format, params object[] args)
        { this.XUnitTests.WriteLine(format, args); }

        protected void WriteDashedLine()
        { this.XUnitTests.WriteDashedLine(); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private XUnitTests XUnitTests { get; set; }
        #endregion
    }
}