// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.XUnit
{
    /// <summary>Captures boilerplate code for an individual xunit test executed inside an xunit tests object.</summary>
    public abstract class XUnitTest : IXUnitTest
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IXUnitTest Implementation
        public void Execute(XUnitTests xUnitTests)
        {
            this.XUnitTests = xUnitTests;

            this.WriteLine("Test Name: {0}", this.Name);
            this.WriteLine();

            this.Arrange();
            this.Act();
            this.Assert();
        }
        #endregion

        #region Object Overrides
        public override string ToString()
        { return this.Name; }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected XUnitTest(string name)
        { this.Name = name; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region XUnitTest Overrides
        protected virtual void Arrange()
        { }

        protected virtual void Act()
        { }

        protected virtual void Assert()
        { }
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