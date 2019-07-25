// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Extensions
{
    public class DictionaryTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DictionaryTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetValueTestData))]
        public void TestGetValue(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetValueTestData = new[]
                                                                        {
                                                                            new object[] {new GetValueUnitTest<int, int>("WithIntToIntDictionaryAndExistingKey",    new Dictionary<int, int> {{24, 42}}, 24, 42,           false)},
                                                                            new object[] {new GetValueUnitTest<int, int>("WithIntToIntDictionaryAndNonExistingKey", new Dictionary<int, int> {{24, 42}}, 68, default(int), true)},

                                                                            new object[] {new GetValueUnitTest<string, string>("WithStringToStringDictionaryAndExistingKey",    new Dictionary<string, string> {{"24", "42"}}, "24", "42",            false)},
                                                                            new object[] {new GetValueUnitTest<string, string>("WithStringToStringDictionaryAndNonExistingKey", new Dictionary<string, string> {{"24", "42"}}, "68", default(string), true)},
                                                                        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class GetValueUnitTest<TKey, TValue> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetValueUnitTest(string name, IDictionary<TKey, TValue> dictionary, TKey key, TValue expectedValue, bool expectedExceptionThrown)
                : base(name)
            {
                this.Dictionary              = dictionary;
                this.Key                     = key;
                this.ExpectedValue           = expectedValue;
                this.ExpectedExceptionThrown = expectedExceptionThrown;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected");
                this.WriteLine("  Value           = {0}", this.ExpectedValue);
                this.WriteLine("  ExceptionThrown = {0}", this.ExpectedExceptionThrown);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.ActualExceptionThrown = false;
                try
                {
                    this.ActualValue = this.Dictionary.GetValue(this.Key);
                }
                catch (Exception)
                {
                    this.ActualExceptionThrown = true;
                    this.ActualValue           = default(TValue);
                }

                this.WriteLine("Actual");
                this.WriteLine("  Value           = {0}", this.ActualValue);
                this.WriteLine("  ExceptionThrown = {0}", this.ActualExceptionThrown);
            }

            protected override void Assert()
            {
                this.ActualExceptionThrown.Should().Be(this.ExpectedExceptionThrown);
                this.ActualValue.Should().BeEquivalentTo(this.ExpectedValue);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TValue ActualValue           { get; set; }
            private bool   ActualExceptionThrown { get; set; }
            #endregion

            #region User Supplied Properties
            private IDictionary<TKey, TValue> Dictionary              { get; }
            private TKey                      Key                     { get; }
            private TValue                    ExpectedValue           { get; }
            private bool                      ExpectedExceptionThrown { get; }
            #endregion
        }
        #endregion
    }
}