// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.XUnit
{
    /// <summary>Aggregates a collection of xunit test objects into a composite xunit test object as in the composite design pattern.</summary>
    public class XUnitTestAggregate : IXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public XUnitTestAggregate(string name, IEnumerable<XUnitTest> unitTestCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.UnitTestCollection = unitTestCollection;
        }

        public XUnitTestAggregate(string name, params XUnitTest[] unitTestCollection)
            : this(name, unitTestCollection.AsEnumerable())
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Name { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IXUnitTest Implementation
        public void Execute(XUnitTests xUnitTests)
        {
            xUnitTests.WriteLine("Test Name: {0}", this.Name);
            xUnitTests.WriteDoubleDashedLine();
            xUnitTests.WriteLine();

            foreach (var unitTest in this.UnitTestCollection)
            {
                unitTest.Execute(xUnitTests);
                xUnitTests.WriteDashedLine();
                xUnitTests.WriteLine();
            }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IEnumerable<XUnitTest> UnitTestCollection { get; }
        #endregion
    }
}