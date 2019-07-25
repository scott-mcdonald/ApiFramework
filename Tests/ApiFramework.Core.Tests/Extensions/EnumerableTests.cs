// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using ApiFramework.XUnit;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Extensions
{
    public class EnumerableTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public EnumerableTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(EmptyIfNullTestData))]
        public void TestEnumerableEmptyIfNull(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(IsNullOrEmptyTestData))]
        public void TestEnumerableIsNullOrEmpty(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(SafeCastTestData))]
        public void TestEnumerableSafeCast(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(SafeToArrayTestData))]
        public void TestEnumerableSafeToArray(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(SafeToListTestData))]
        public void TestEnumerableSafeToList(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(SafeToReadOnlyCollectionTestData))]
        public void TestEnumerableSafeToReadOnlyCollection(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(SafeToReadOnlyListTestData))]
        public void TestEnumerableSafeToReadOnlyList(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> EmptyIfNullTestData = new[]
            {
                new object[] { new EmptyIfNullUnitTest<string>("WithNullCollection", default(IEnumerable<string>), Enumerable.Empty<string>()) },
                new object[] { new EmptyIfNullUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), Enumerable.Empty<string>()) },
                new object[] { new EmptyIfNullUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, new[] { "String 1", "String 2", "String 3" }) },
            };

        public static readonly IEnumerable<object[]> IsNullOrEmptyTestData = new[]
            {
                new object[] { new IsNullOrEmptyUnitTest<string>("WithNullCollection", default(IEnumerable<string>), true) },
                new object[] { new IsNullOrEmptyUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), true) },
                new object[] { new IsNullOrEmptyUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, false) },
            };

        public static readonly IEnumerable<object[]> SafeCastTestData = new[]
            {
                new object[] { new SafeCastUnitTest<object, string>("WithNullCollection", default(IEnumerable<object>), Enumerable.Empty<string>()) },
                new object[] { new SafeCastUnitTest<object, string>("WithEmptyCollection", Enumerable.Empty<object>(), Enumerable.Empty<string>()) },
                new object[] { new SafeCastUnitTest<object, string>("WithNonEmptyCollection", new object[] { "String 1", "String 2", "String 3" }, new [] { "String 1", "String 2", "String 3" }) },
            };

        public static readonly IEnumerable<object[]> SafeToArrayTestData = new[]
            {
                new object[] { new SafeToArrayUnitTest<string>("WithNullCollection", default(IEnumerable<string>), new string[]{ }) },
                new object[] { new SafeToArrayUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), new string[]{ }) },
                new object[] { new SafeToArrayUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, new[] { "String 1", "String 2", "String 3" }) },
            };

        public static readonly IEnumerable<object[]> SafeToListTestData = new[]
            {
                new object[] { new SafeToListUnitTest<string>("WithNullCollection", default(IEnumerable<string>), new List<string>()) },
                new object[] { new SafeToListUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), new List<string>()) },
                new object[] { new SafeToListUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, new List<string> { "String 1", "String 2", "String 3" }) },
            };

        public static readonly IEnumerable<object[]> SafeToReadOnlyCollectionTestData = new[]
            {
                new object[] { new SafeToReadOnlyCollectionUnitTest<string>("WithNullCollection", default(IEnumerable<string>), new List<string>()) },
                new object[] { new SafeToReadOnlyCollectionUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), new List<string>()) },
                new object[] { new SafeToReadOnlyCollectionUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, new List<string> { "String 1", "String 2", "String 3" }) },
            };

        public static readonly IEnumerable<object[]> SafeToReadOnlyListTestData = new[]
            {
                new object[] { new SafeToReadOnlyListUnitTest<string>("WithNullCollection", default(IEnumerable<string>), new List<string>()) },
                new object[] { new SafeToReadOnlyListUnitTest<string>("WithEmptyCollection", Enumerable.Empty<string>(), new List<string>()) },
                new object[] { new SafeToReadOnlyListUnitTest<string>("WithNonEmptyCollection", new[] { "String 1", "String 2", "String 3" }, new List<string> { "String 1", "String 2", "String 3" }) },
            };
        // ReSharper restore MemberCanBePrivate.Global
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class EmptyIfNullUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public EmptyIfNullUnitTest(string name, IEnumerable<T> original, IEnumerable<T> expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.EmptyIfNull();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IEnumerable<T> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            IEnumerable<T> Expected { get; set; }
            #endregion
        }

        private class IsNullOrEmptyUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public IsNullOrEmptyUnitTest(string name, IEnumerable<T> original, bool expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                this.WriteLine("Expected IsNullOrEmpty: {0}", this.Expected);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.IsNullOrEmpty();

                this.WriteLine("Actual IsNullOrEmpty: {0}", this.Actual);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().Be(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            bool Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            bool Expected { get; set; }
            #endregion
        }

        private class SafeCastUnitTest<TFrom, TTo> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public SafeCastUnitTest(string name, IEnumerable<TFrom> original, IEnumerable<TTo> expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(TFrom).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected IEnumerable<{0}> as JSON", typeof(TTo).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.SafeCast<TTo>();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual IEnumerable<{0}> as JSON", typeof(TTo).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IEnumerable<TTo> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<TFrom> Original { get; set; }
            IEnumerable<TTo> Expected { get; set; }
            #endregion
        }

        private class SafeToArrayUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public SafeToArrayUnitTest(string name, IEnumerable<T> original, T[] expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected <{0}> [] as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.SafeToArray();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual {0} [] as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            T[] Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            T[] Expected { get; set; }
            #endregion
        }

        private class SafeToListUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public SafeToListUnitTest(string name, IEnumerable<T> original, List<T> expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected List<{0}> as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.SafeToList();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual List<{0}> as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IList<T> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            IList<T> Expected { get; set; }
            #endregion
        }

        private class SafeToReadOnlyCollectionUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public SafeToReadOnlyCollectionUnitTest(string name, IEnumerable<T> original, IReadOnlyCollection<T> expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected IReadOnlyCollection<{0}> as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.SafeToReadOnlyCollection();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual IReadOnlyCollection<{0}> as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IReadOnlyCollection<T> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            IReadOnlyCollection<T> Expected { get; set; }
            #endregion
        }

        private class SafeToReadOnlyListUnitTest<T> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public SafeToReadOnlyListUnitTest(string name, IEnumerable<T> original, IReadOnlyList<T> expected)
                : base(name)
            {
                this.Original = original;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var originalJson = JsonConvert.SerializeObject(this.Original);
                this.WriteLine("Original IEnumerable<{0}> as JSON", typeof(T).Name);
                this.WriteLine(originalJson);
                this.WriteLine();

                var expectedJson = JsonConvert.SerializeObject(this.Expected);
                this.WriteLine("Expected IReadOnlyList<{0}> as JSON", typeof(T).Name);
                this.WriteLine(expectedJson);
                this.WriteLine();
            }

            protected override void Act()
            {
                this.Actual = this.Original.SafeToReadOnlyList();

                var actualJson = JsonConvert.SerializeObject(this.Actual);
                this.WriteLine("Actual IReadOnlyList<{0}> as JSON", typeof(T).Name);
                this.WriteLine(actualJson);
                this.WriteLine();
            }

            protected override void Assert()
            {
                var expected = this.Expected;
                var actual = this.Actual;

                actual.Should().BeEquivalentTo(expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            IReadOnlyList<T> Actual { get; set; }
            #endregion

            #region User Supplied Properties
            IEnumerable<T> Original { get; set; }
            IReadOnlyList<T> Expected { get; set; }
            #endregion
        }
        #endregion
    }
}