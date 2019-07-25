﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;

using ApiFramework.Reflection;
using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Expressions
{
    public class ExpressionBuilderTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ExpressionBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CallTestData))]
        public void TestExpressionBuilderCall(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(CastTestData))]
        public void TestExpressionBuilderCast(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(CastAsTestData))]
        public void TestExpressionBuilderCastAs(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(DefaultTestData))]
        public void TestExpressionBuilderDefault(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(NewTestData))]
        public void TestExpressionBuilderNew(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(PropertyGetterTestData))]
        public void TestExpressionBuilderPropertyGetter(IXUnitTest unitTest)
        { unitTest.Execute(this); }

        [Theory]
        [MemberData(nameof(PropertySetterTestData))]
        public void TestExpressionBuilderPropertySetter(IXUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly Guid TestGuid = Guid.NewGuid();
        private static readonly DateTime TestDateTime = DateTime.Now;

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> CallTestData = new[]
            {
                new object[] { new MethodCallWith0ArgumentsUnitTest<Adder, string>("WithAdderObjectAnd0ArgumentsAndReturnValue", new Adder(), "Add", Adder.DefaultSum) },
                new object[] { new MethodCallWith1ArgumentsUnitTest<Adder, string, string>("WithAdderObjectAnd1ArgumentsAndReturnValue", new Adder(), "Add", "42", "42") },
                new object[] { new MethodCallWith2ArgumentsUnitTest<Adder, string, int, string>("WithAdderObjectAnd2ArgumentsAndReturnValue", new Adder(), "Add", "20", 22, "20+22=42") },

                new object[] { new VoidMethodCallWith0ArgumentsUnitTest<Adder>("WithAdderObjectAnd0ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", (x) => x.Sum.Should().Be(Adder.DefaultSum)) },
                new object[] { new VoidMethodCallWith1ArgumentsUnitTest<Adder, string>("WithAdderObjectAnd1ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", "42", (x) => x.Sum.Should().Be("42")) },
                new object[] { new VoidMethodCallWith2ArgumentsUnitTest<Adder, string, int>("WithAdderObjectAnd2ArgumentsAndNoReturnValue", new Adder(), "AddNoReturnValue", "20", 22, (x) => x.Sum.Should().Be("20+22=42")) },

                new object[] { new StaticMethodCallWith0ArgumentsUnitTest<Adder, string>("WithAdderClassAnd0ArgumentsAndReturnValue", "StaticAdd", Adder.DefaultSum) },
                new object[] { new StaticMethodCallWith1ArgumentsUnitTest<Adder, string, string>("WithAdderClassAnd1ArgumentsAndReturnValue", "StaticAdd", "42", "42") },
                new object[] { new StaticMethodCallWith2ArgumentsUnitTest<Adder, string, int, string>("WithAdderClassAnd2ArgumentsAndReturnValue", "StaticAdd", "20", 22, "20+22=42") },

                new object[] { new StaticVoidMethodCallWith0ArgumentsUnitTest<Adder>("WithAdderClassAnd0ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", () => Adder.StaticSum.Should().Be(Adder.DefaultSum)) },
                new object[] { new StaticVoidMethodCallWith1ArgumentsUnitTest<Adder, string>("WithAdderClassAnd1ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", "42", () => Adder.StaticSum.Should().Be("42")) },
                new object[] { new StaticVoidMethodCallWith2ArgumentsUnitTest<Adder, string, int>("WithAdderClassAnd2ArgumentsAndNoReturnValue", "StaticAddNoReturnValue", "20", 22, () => Adder.StaticSum.Should().Be("20+22=42")) },

                new object[] { new MethodCallWith0ArgumentsUnitTest<IsEquals<long>, bool>("WithIsEqualsObjectAnd0ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", false) },
                new object[] { new MethodCallWith1ArgumentsUnitTest<IsEquals<long>, long, bool>("WithIsEqualsObjectAnd1ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", 20, true) },
                new object[] { new MethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long, bool>("WithIsEqualsObjectAnd2ArgumentsAndReturnValue", new IsEquals<long>(), "IsEqual", 20, 22, false) },

                new object[] { new VoidMethodCallWith0ArgumentsUnitTest<IsEquals<long>>("WithIsEqualsObjectAnd0ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", (x) => x.AreEqual.Should().Be(false)) },
                new object[] { new VoidMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long>("WithIsEqualsObjectAnd1ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", 20, (x) => x.AreEqual.Should().Be(true)) },
                new object[] { new VoidMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long>("WithIsEqualsObjectAnd2ArgumentsAndNoReturnValue", new IsEquals<long>(), "IsEqualNoReturnValue", 20, 22, (x) => x.AreEqual.Should().Be(false)) },

                new object[] { new StaticMethodCallWith0ArgumentsUnitTest<IsEquals<long>, bool>("WithIsEqualsClassAnd0ArgumentsAndReturnValue", "StaticIsEqual", false) },
                new object[] { new StaticMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long, bool>("WithIsEqualsClassAnd1ArgumentsAndReturnValue", "StaticIsEqual", 20, true) },
                new object[] { new StaticMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long, bool>("WithIsEqualsClassAnd2ArgumentsAndReturnValue", "StaticIsEqual", 20, 22, false) },

                new object[] { new StaticVoidMethodCallWith0ArgumentsUnitTest<IsEquals<long>>("WithIsEqualsClassAnd0ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", () => IsEquals<long>.StaticAreEqual0Arguments.Should().Be(false)) },
                new object[] { new StaticVoidMethodCallWith1ArgumentsUnitTest<IsEquals<long>, long>("WithIsEqualsClassAnd1ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", 20, () => IsEquals<long>.StaticAreEqual1Arguments.Should().Be(true)) },
                new object[] { new StaticVoidMethodCallWith2ArgumentsUnitTest<IsEquals<long>, long, long>("WithIsEqualsClassAnd2ArgumentsAndNoReturnValue", "StaticIsEqualNoReturnValue", 20, 22, () => IsEquals<long>.StaticAreEqual2Arguments.Should().Be(false)) },
            };

        public static readonly IEnumerable<object[]> CastTestData = new[]
            {
                new object[] { new CastUnitTest<short, long>("FromShortIntegerToLongInteger", 42, true, 42)},
                new object[] { new CastUnitTest<long, short>("FromLongIntegerToShortIntegerWithNoOverflow", 42, true, 42)},
                new object[] { new CastUnitTest<long, short>("FromLongIntegerToShortIntegerWithOverflow", 32767 + 1, false)},

                new object[] { new CastUnitTest<object, string>("FromNullObjectToString", null, true, null)},
                new object[] { new CastUnitTest<object, int>("FromNullObjectToInteger", null, false)},
                new object[] { new CastUnitTest<object, int?>("FromNullObjectToNullableInteger", null, true, new int?())},
                new object[] { new CastUnitTest<object, int?>("FromIntegerObjectToNullableInteger", 42, true, 42)},

                new object[] { new CastUnitTest<string, string>("FromStringToString", "This is a test.", true, "This is a test.")},

                new object[] { new CastUnitTest<Foo, IFoo>("FromBaseClassToInterface", new Foo(42, "42"), true, new Foo(42, "42"))},
                new object[] { new CastUnitTest<Foo, FooExtended>("FromBaseClassToDerivedClass", new Foo(42, "42"), false)},
                new object[] { new CastUnitTest<FooExtended, IFoo>("FromDerivedClassToInterface", new FooExtended(42, "42"), true, new FooExtended(42, "42"))},
                new object[] { new CastUnitTest<FooExtended, Foo>("FromDerivedClassToBaseClass", new FooExtended(42, "42"), true, new Foo(42, "42"))},
            };

        public static readonly IEnumerable<object[]> CastAsTestData = new[]
            {
                new object[] { new CastAsUnitTest<object, string>("FromNullObjectToString", null, true, null)},
                new object[] { new CastAsUnitTest<object, int?>("FromNullObjectToNullableInteger", null, true, new int?())},
                new object[] { new CastUnitTest<object, int?>("FromIntegerObjectToNullableInteger", 42, true, 42)},

                new object[] { new CastAsUnitTest<string, string>("FromStringToString", "This is a test.", true, "This is a test.")},
                new object[] { new CastAsUnitTest<string, Foo>("FromStringToBaseClass", "This is a test.", false)},

                new object[] { new CastAsUnitTest<Foo, IFoo>("FromBaseClassToInterface", new Foo(42, "42"), true, new Foo(42, "42"))},
                new object[] { new CastAsUnitTest<Foo, FooExtended>("FromBaseClassToDerivedClass", new Foo(42, "42"), false)},
                new object[] { new CastAsUnitTest<FooExtended, IFoo>("FromDerivedClassToInterface", new FooExtended(42, "42"), true, new FooExtended(42, "42"))},
                new object[] { new CastAsUnitTest<FooExtended, Foo>("FromDerivedClassToBaseClass", new FooExtended(42, "42"), true, new Foo(42, "42"))},
            };

        public static readonly IEnumerable<object[]> DefaultTestData = new[]
            {
                new object[] { new DefaultUnitTest<long?>("WithNullableType", default(long?)) },
                new object[] { new DefaultUnitTest<string>("WithReferenceType", default(string)) },
                new object[] { new DefaultUnitTest<long>("WithValueType", default(long)) },
            };

        public static readonly IEnumerable<object[]> NewTestData = new[]
            {
                new object[] { new NewWith0ArgumentsUnitTest<Foo>("WithImplicitObjectTypeAnd0Arguments", new Foo(), null) },
                new object[] { new NewWith1ArgumentsUnitTest<int, Foo>("WithImplicitObjectTypeAnd1Arguments", 42, new Foo(42), null) },
                new object[] { new NewWith2ArgumentsUnitTest<int, string, Foo>("WithImplicitObjectTypeAnd2Arguments", 42, "42", new Foo(42, "42"), null) },
                new object[] { new NewWith3ArgumentsUnitTest<int, string, decimal, Foo>("WithImplicitObjectTypeAnd3Arguments", 42, "42", 24.0m, new Foo(42, "42", 24.0m), null) },
                new object[] { new NewWith4ArgumentsUnitTest<int, string, decimal, Guid, Foo>("WithImplicitObjectTypeAnd4Arguments", 42, "42", 24.0m, TestGuid, new Foo(42, "42", 24.0m, TestGuid), null) },
                new object[] { new NewWith5ArgumentsUnitTest<int, string, decimal, Guid, DateTime, Foo>("WithImplicitObjectTypeAnd5Arguments", 42, "42", 24.0m, TestGuid, TestDateTime, new Foo(42, "42", 24.0m, TestGuid, TestDateTime), null) },

                new object[] { new NewWith0ArgumentsUnitTest<IFoo>("WithExplicitObjectTypeAnd0Arguments", new Foo(), typeof(Foo)) },
                new object[] { new NewWith1ArgumentsUnitTest<int, IFoo>("WithExplicitObjectTypeAnd1Arguments", 42, new Foo(42), typeof(Foo)) },
                new object[] { new NewWith2ArgumentsUnitTest<int, string, IFoo>("WithExplicitObjectTypeAnd2Arguments", 42, "42", new Foo(42, "42"), typeof(Foo)) },
                new object[] { new NewWith3ArgumentsUnitTest<int, string, decimal, IFoo>("WithExplicitObjectTypeAnd3Arguments", 42, "42", 24.0m, new Foo(42, "42", 24.0m), typeof(Foo)) },
                new object[] { new NewWith4ArgumentsUnitTest<int, string, decimal, Guid, IFoo>("WithExplicitObjectTypeAnd4Arguments", 42, "42", 24.0m, TestGuid, new Foo(42, "42", 24.0m, TestGuid), typeof(Foo)) },
                new object[] { new NewWith5ArgumentsUnitTest<int, string, decimal, Guid, DateTime, IFoo>("WithExplicitObjectTypeAnd5Arguments", 42, "42", 24.0m, TestGuid, TestDateTime, new Foo(42, "42", 24.0m, TestGuid, TestDateTime), typeof(Foo)) },
            };

        public static readonly IEnumerable<object[]> PropertyGetterTestData = new[]
            {
                new object[] { new PropertyGetterUnitTest<Foo, int>("WithInstanceAndIntProperty", new Foo(42, "42"), "IntProperty", 42) },
                new object[] { new PropertyGetterUnitTest<Foo, string>("WithInstanceAndStringProperty", new Foo(42, "42"), "StringProperty", "42") },

                new object[] { new StaticPropertyGetterUnitTest<Foo, int>("WithStaticAndIntProperty", () => new Foo(42, "42"), "StaticIntProperty", 42) },
                new object[] { new StaticPropertyGetterUnitTest<Foo, string>("WithStaticAndStringProperty", () => new Foo(42, "42"), "StaticStringProperty", "42") },

                new object[] { new PropertyGetterUnitTest<Complex, int>("WithComplexInstanceAndIntProperty", new Complex(null, 1), "IntProperty", 1) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildA>("WithComplexInstanceAndChildAProperty", new Complex(new ComplexChildA(null, 2), 1), "A", new ComplexChildA(null, 2)) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildB>("WithComplexInstanceAndChildAPropertyDotChildBProperty", new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "A.B", new ComplexChildB(null, 3)) },
                new object[] { new PropertyGetterUnitTest<Complex, ComplexChildC>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C", new ComplexChildC(4)) },
                new object[] { new PropertyGetterUnitTest<Complex, int>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C.IntProperty", 4) },

                new object[] { new StaticPropertyGetterUnitTest<Complex, int>("WithComplexStaticAndIntProperty", () => new Complex(null, 1), "StaticIntProperty", 1) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildA>("WithComplexStaticAndChildAProperty", () => new Complex(new ComplexChildA(null, 2), 1), "StaticA", new ComplexChildA(null, 2)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildB>("WithComplexStaticAndChildAPropertyDotChildBProperty", () => new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "StaticA.StaticB", new ComplexChildB(null, 3)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, ComplexChildC>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC", new ComplexChildC(4)) },
                new object[] { new StaticPropertyGetterUnitTest<Complex, int>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC.StaticIntProperty", 4) },
            };

        public static readonly IEnumerable<object[]> PropertySetterTestData = new[]
            {
                new object[] { new PropertySetterUnitTest<Foo, int>("WithInstanceAndIntProperty", new Foo(), "IntProperty", 42) },
                new object[] { new PropertySetterUnitTest<Foo, string>("WithInstanceAndStringProperty", new Foo(), "StringProperty", "42") },

                new object[] { new StaticPropertySetterUnitTest<Foo, int>("WithStaticAndIntProperty", () => new Foo(0, null), "StaticIntProperty", 42) },
                new object[] { new StaticPropertySetterUnitTest<Foo, string>("WithStaticAndStringProperty", () => new Foo(0, null), "StaticStringProperty", "42") },

                new object[] { new PropertySetterUnitTest<Complex, int>("WithComplexInstanceAndIntProperty", new Complex(null, 1), "IntProperty", 10) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildA>("WithComplexInstanceAndChildAProperty", new Complex(new ComplexChildA(null, 2), 1), "A", new ComplexChildA(null, 20)) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildB>("WithComplexInstanceAndChildAPropertyDotChildBProperty", new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "A.B", new ComplexChildB(null, 30)) },
                new object[] { new PropertySetterUnitTest<Complex, ComplexChildC>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C", new ComplexChildC(40)) },
                new object[] { new PropertySetterUnitTest<Complex, int>("WithComplexInstanceAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "A.B.C.IntProperty", 40) },

                new object[] { new StaticPropertySetterUnitTest<Complex, int>("WithComplexStaticAndIntProperty", () => new Complex(null, 1), "StaticIntProperty", 10) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildA>("WithComplexStaticAndChildAProperty", () => new Complex(new ComplexChildA(null, 2), 1), "StaticA", new ComplexChildA(null, 20)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildB>("WithComplexStaticAndChildAPropertyDotChildBProperty", () => new Complex(new ComplexChildA(new ComplexChildB(null, 3), 2), 1), "StaticA.StaticB", new ComplexChildB(null, 30)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, ComplexChildC>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC", new ComplexChildC(40)) },
                new object[] { new StaticPropertySetterUnitTest<Complex, int>("WithComplexStaticAndChildAPropertyDotChildBPropertyDotChildCPropertyDotIntProperty", () => new Complex(new ComplexChildA(new ComplexChildB(new ComplexChildC(4), 3), 2), 1), "StaticA.StaticB.StaticC.StaticIntProperty", 40) },
            };
        // ReSharper restore MemberCanBePrivate.Global
        #endregion

        #region Test Types
        private interface IFoo
        {
            int IntProperty { get; set; }
            string StringProperty { get; set; }
            decimal DecimalProperty { get; set; }
            Guid GuidProperty { get; set; }
            DateTime DateTimeProperty { get; set; }

            void Bar();
        }

        private class Foo : IFoo
        {
            public Foo()
            { }

            public Foo(int intValue)
            {
                this.IntProperty = intValue;

                StaticIntProperty = intValue;
            }

            public Foo(int intValue, string stringValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;

                StaticIntProperty = intValue;
                StaticStringProperty = stringValue;
            }

            public Foo(int intValue, string stringValue, decimal decimalValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;

                StaticIntProperty = intValue;
                StaticStringProperty = stringValue;

                this.DecimalProperty = decimalValue;
            }

            public Foo(int intValue, string stringValue, decimal decimalValue, Guid guidValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;

                StaticIntProperty = intValue;
                StaticStringProperty = stringValue;

                this.DecimalProperty = decimalValue;
                this.GuidProperty = guidValue;
            }

            public Foo(int intValue, string stringValue, decimal decimalValue, Guid guidValue, DateTime dateTimeValue)
            {
                this.IntProperty = intValue;
                this.StringProperty = stringValue;

                StaticIntProperty = intValue;
                StaticStringProperty = stringValue;

                this.DecimalProperty = decimalValue;
                this.GuidProperty = guidValue;
                this.DateTimeProperty = dateTimeValue;
            }

            public int IntProperty { get; set; }
            public string StringProperty { get; set; }

            public static int StaticIntProperty { get; set; }
            public static string StaticStringProperty { get; set; }

            public decimal DecimalProperty { get; set; }
            public Guid GuidProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!TypeReflection.IsAssignableFrom(typeof(Foo), otherType))
                    return false;

                var otherAsFoo = (Foo)other;
                return this.IntProperty == otherAsFoo.IntProperty && this.StringProperty == otherAsFoo.StringProperty;
            }

            public override int GetHashCode()
            {
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                var stringPropertyHashCode = (this.StringProperty ?? String.Empty).GetHashCode();
                var decimalHasCode = this.DecimalProperty.GetHashCode();
                var guidHashCode = this.GuidProperty.GetHashCode();
                var dateTimeHashCode = this.DateTimeProperty.GetHashCode();
                return intPropertyHashCode ^ stringPropertyHashCode ^ decimalHasCode ^ guidHashCode ^ dateTimeHashCode;
            }

            public override string ToString()
            {
                var typeName = this.GetType().Name;
                var intPropertyAsString = Convert.ToString(this.IntProperty);
                var decimalPropertyAsString = Convert.ToString(this.DecimalProperty, CultureInfo.InvariantCulture);
                var guidPropertyAsString = Convert.ToString(this.GuidProperty);
                var dateTimePropertyAsString = Convert.ToString(this.DateTimeProperty, CultureInfo.InvariantCulture);

                return $"{typeName} [IntProperty={intPropertyAsString} StringProperty={this.StringProperty} DecimalProperty={decimalPropertyAsString} GuidProperty={guidPropertyAsString} DateTimeProperty={dateTimePropertyAsString}]";
            }

            public virtual void Bar() { }
        }

        private class FooExtended : Foo
        {
            public FooExtended()
            { }

            public FooExtended(int intValue)
                : base(intValue)
            { }

            public FooExtended(int intValue, string stringValue)
                : base(intValue, stringValue)
            { }

            public FooExtended(int intValue, string stringValue, decimal decimalValue)
                : base(intValue, stringValue, decimalValue)
            { }

            public FooExtended(int intValue, string stringValue, decimal decimalValue, Guid guidValue)
                : base(intValue, stringValue, decimalValue, guidValue)
            { }

            public FooExtended(int intValue, string stringValue, decimal decimalValue, Guid guidValue, DateTime dateTimeValue)
                : base(intValue, stringValue, decimalValue, guidValue, dateTimeValue)
            { }

            public override void Bar() { }
        }

        private class Complex
        {
            public Complex() { }

            public Complex(ComplexChildA a, int intValue)
            {
                this.A = a;
                this.IntProperty = intValue;

                StaticA = a;
                StaticIntProperty = intValue;
            }

            public ComplexChildA A { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildA StaticA { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!TypeReflection.IsAssignableFrom(typeof(Complex), otherType))
                    return false;

                var otherAsComplex = (Complex)other;

                var areChildrenEqual = this.A != null ? this.A.Equals(otherAsComplex.A) : otherAsComplex.A == null;
                return areChildrenEqual && this.IntProperty == otherAsComplex.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.A != null ? this.A.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            {
                var typeName = this.GetType().Name;
                var aPropertyAsString = this.A != null ? Convert.ToString(this.A) : "null";
                var intPropertyAsString = Convert.ToString(this.IntProperty);

                return $"{typeName} [A={aPropertyAsString} IntProperty={intPropertyAsString}]";
            }
        }

        private class ComplexChildA
        {
            public ComplexChildA() { }

            public ComplexChildA(ComplexChildB b, int intValue)
            {
                this.B = b;
                this.IntProperty = intValue;

                StaticB = b;
                StaticIntProperty = intValue;
            }

            public ComplexChildB B { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildB StaticB { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!TypeReflection.IsAssignableFrom(typeof(ComplexChildA), otherType))
                    return false;

                var otherAsComplexChildA = (ComplexChildA)other;

                var areChildrenEqual = this.B != null ? this.B.Equals(otherAsComplexChildA.B) : otherAsComplexChildA.B == null;
                return areChildrenEqual && this.IntProperty == otherAsComplexChildA.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.B != null ? this.B.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            {
                var typeName = this.GetType().Name;
                var bPropertyAsString = this.B != null ? Convert.ToString(this.B) : "null";
                var intPropertyAsString = Convert.ToString(this.IntProperty);

                return $"{typeName} [B={bPropertyAsString} IntProperty={intPropertyAsString}]";
            }
        }

        private class ComplexChildB
        {
            public ComplexChildB() { }

            public ComplexChildB(ComplexChildC c, int intValue)
            {
                this.C = c;
                this.IntProperty = intValue;

                StaticC = c;
                StaticIntProperty = intValue;
            }

            public ComplexChildC C { get; set; }
            public int IntProperty { get; set; }

            public static ComplexChildC StaticC { get; set; }
            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!TypeReflection.IsAssignableFrom(typeof(ComplexChildB), otherType))
                    return false;

                var otherAsComplexChildB = (ComplexChildB)other;

                var areChildrenEqual = this.C != null ? this.C.Equals(otherAsComplexChildB.C) : otherAsComplexChildB.C == null;
                return areChildrenEqual && this.IntProperty == otherAsComplexChildB.IntProperty;
            }

            public override int GetHashCode()
            {
                var childHashCode = this.C != null ? this.C.GetHashCode() : 0;
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return childHashCode ^ intPropertyHashCode;
            }

            public override string ToString()
            {
                var typeName = this.GetType().Name;
                var cPropertyAsString = this.C != null ? Convert.ToString(this.C) : "null";
                var intPropertyAsString = Convert.ToString(this.IntProperty);

                return $"{typeName} [C={cPropertyAsString} IntProperty={intPropertyAsString}]";
            }
        }

        private class ComplexChildC
        {
            public ComplexChildC() { }

            public ComplexChildC(int intValue)
            {
                this.IntProperty = intValue;

                StaticIntProperty = intValue;
            }

            public int IntProperty { get; set; }

            public static int StaticIntProperty { get; set; }

            public override bool Equals(object other)
            {
                if (Object.ReferenceEquals(this, other))
                    return true;

                if (other == null)
                    return false;

                var otherType = other.GetType();
                if (!TypeReflection.IsAssignableFrom(typeof(ComplexChildC), otherType))
                    return false;

                var otherAsComplexChildC = (ComplexChildC)other;
                return this.IntProperty == otherAsComplexChildC.IntProperty;
            }

            public override int GetHashCode()
            {
                var intPropertyHashCode = this.IntProperty.GetHashCode();
                return intPropertyHashCode;
            }

            public override string ToString()
            {
                var typeName = this.GetType().Name;
                var intPropertyAsString = Convert.ToString(this.IntProperty);

                return $"{typeName} [IntProperty={intPropertyAsString}]";
            }
        }

        private class Adder
        {
            public const string DefaultSum = "Default";

            public Adder() { this.Sum = DefaultSum; }
            static Adder() { StaticSum = DefaultSum; }

            public string Sum { get; set; }
            public static string StaticSum { get; set; }

            public string Add() { return DefaultSum; }
            public string Add(string x) { return x; }
            public string Add(string x, int y) { return AddImpl(x, y); }

            public void AddNoReturnValue() { this.Sum = DefaultSum; }
            public void AddNoReturnValue(string x) { this.Sum = x; }
            public void AddNoReturnValue(string x, int y) { this.Sum = AddImpl(x, y); }

            public static string StaticAdd() { return DefaultSum; }
            public static string StaticAdd(string x) { return x; }
            public static string StaticAdd(string x, int y) { return AddImpl(x, y); }

            public static void StaticAddNoReturnValue() { StaticSum = DefaultSum; }
            public static void StaticAddNoReturnValue(string x) { StaticSum = x; }
            public static void StaticAddNoReturnValue(string x, int y) { StaticSum = AddImpl(x, y); }

            private static string AddImpl(string x, int y) { return x + "+" + Convert.ToString(y) + "=" + Convert.ToString(Convert.ToInt32(x) + y); }
        }

        private class IsEquals<T>
            where T : IEquatable<T>
        {
            public IsEquals() { this.AreEqual = default(bool); }

            public bool AreEqual { get; set; }
            // ReSharper disable StaticMemberInGenericType
            public static bool StaticAreEqual0Arguments { get; set; }
            public static bool StaticAreEqual1Arguments { get; set; }
            public static bool StaticAreEqual2Arguments { get; set; }
            // ReSharper restore StaticMemberInGenericType

            public bool IsEqual() { return this.AreEqual; }
            public bool IsEqual(T x) { return x.Equals(x); }
            public bool IsEqual(T x, T y) { return x.Equals(y); }

            public void IsEqualNoReturnValue() { this.AreEqual = default(bool); }
            public void IsEqualNoReturnValue(T x) { this.AreEqual = x.Equals(x); }
            public void IsEqualNoReturnValue(T x, T y) { this.AreEqual = x.Equals(y); }

            public static bool StaticIsEqual() { return StaticAreEqual0Arguments; }
            public static bool StaticIsEqual(T x) { return x.Equals(x); }
            public static bool StaticIsEqual(T x, T y) { return x.Equals(y); }

            public static void StaticIsEqualNoReturnValue() { StaticAreEqual0Arguments = default(bool); }
            public static void StaticIsEqualNoReturnValue(T x) { StaticAreEqual1Arguments = x.Equals(x); }
            public static void StaticIsEqualNoReturnValue(T x, T y) { StaticAreEqual2Arguments = x.Equals(y); }
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region UnitTest Types
        private class MethodCallWith0ArgumentsUnitTest<TObject, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith0ArgumentsUnitTest(string name, TObject source, string methodName, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class MethodCallWith1ArgumentsUnitTest<TObject, TArgument, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith1ArgumentsUnitTest(string name, TObject source, string methodName, TArgument argument, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument = argument;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TArgument, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source, this.Argument);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class MethodCallWith2ArgumentsUnitTest<TObject, TArgument1, TArgument2, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public MethodCallWith2ArgumentsUnitTest(string name, TObject source, string methodName, TArgument1 argument1, TArgument2 argument2, TResult expectedResult)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var methodCallLambdaExpression = ExpressionBuilder.MethodCall<TObject, TArgument1, TArgument2, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", methodCallLambdaExpression);

                var methodCallLambda = methodCallLambdaExpression.Compile();

                var actualResult = methodCallLambda(this.Source, this.Argument1, this.Argument2);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class VoidMethodCallWith0ArgumentsUnitTest<TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith0ArgumentsUnitTest(string name, TObject source, string methodName, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        private class VoidMethodCallWith1ArgumentsUnitTest<TObject, TArgument> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith1ArgumentsUnitTest(string name, TObject source, string methodName, TArgument argument, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument = argument;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source, this.Argument);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        private class VoidMethodCallWith2ArgumentsUnitTest<TObject, TArgument1, TArgument2> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public VoidMethodCallWith2ArgumentsUnitTest(string name, TObject source, string methodName, TArgument1 argument1, TArgument2 argument2, Action<TObject> assertAction)
                : base(name)
            {
                this.Source = source;
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
            }

            protected override void Act()
            {
                var voidMethodCallLambdaExpression = ExpressionBuilder.VoidMethodCall<TObject, TArgument1, TArgument2>(this.MethodName);
                this.WriteLine("Lambda          = {0}", voidMethodCallLambdaExpression);

                var voidMethodCallLambda = voidMethodCallLambdaExpression.Compile();

                voidMethodCallLambda(this.Source, this.Argument1, this.Argument2);
            }

            protected override void Assert()
            {
                this.AssertAction(this.Source);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private TObject Source { get; set; }
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private Action<TObject> AssertAction { get; set; }
            #endregion
        }

        public class StaticMethodCallWith0ArgumentsUnitTest<TClass, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith0ArgumentsUnitTest(string name, string methodName, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda();
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class StaticMethodCallWith1ArgumentsUnitTest<TClass, TArgument, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith1ArgumentsUnitTest(string name, string methodName, TArgument argument, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument = argument;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TArgument, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda(this.Argument);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class StaticMethodCallWith2ArgumentsUnitTest<TClass, TArgument1, TArgument2, TResult> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticMethodCallWith2ArgumentsUnitTest(string name, string methodName, TArgument1 argument1, TArgument2 argument2, TResult expectedResult)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.ExpectedResult = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected Result = {0} ({1})", this.ExpectedResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Act()
            {
                var staticMethodCallLambdaExpression = ExpressionBuilder.StaticMethodCall<TClass, TArgument1, TArgument2, TResult>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticMethodCallLambdaExpression);

                var staticMethodCallLambda = staticMethodCallLambdaExpression.Compile();

                var actualResult = staticMethodCallLambda(this.Argument1, this.Argument2);
                this.ActualResult = actualResult;
                this.WriteLine("Actual Result   = {0} ({1})", this.ActualResult.SafeToString(), typeof(TResult).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().BeEquivalentTo(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TResult ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private TResult ExpectedResult { get; set; }
            #endregion
        }

        private class StaticVoidMethodCallWith0ArgumentsUnitTest<TClass> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith0ArgumentsUnitTest(string name, string methodName, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda();
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        private class StaticVoidMethodCallWith1ArgumentsUnitTest<TClass, TArgument> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith1ArgumentsUnitTest(string name, string methodName, TArgument argument, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument = argument;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument        = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass, TArgument>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda(this.Argument);
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument Argument { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        private class StaticVoidMethodCallWith2ArgumentsUnitTest<TClass, TArgument1, TArgument2> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticVoidMethodCallWith2ArgumentsUnitTest(string name, string methodName, TArgument1 argument1, TArgument2 argument2, Action assertAction)
                : base(name)
            {
                this.MethodName = methodName;
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.AssertAction = assertAction;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Class           = {0}", typeof(TClass).Name);
                this.WriteLine("Argument 1      = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2      = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
            }

            protected override void Act()
            {
                var staticVoidMethodCallLambdaExpression = ExpressionBuilder.StaticVoidMethodCall<TClass, TArgument1, TArgument2>(this.MethodName);
                this.WriteLine("Lambda          = {0}", staticVoidMethodCallLambdaExpression);

                var staticVoidMethodCallLambda = staticVoidMethodCallLambdaExpression.Compile();

                staticVoidMethodCallLambda(this.Argument1, this.Argument2);
            }

            protected override void Assert()
            {
                this.AssertAction();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region User Supplied Properties
            private string MethodName { get; set; }
            private TArgument1 Argument1 { get; set; }
            private TArgument2 Argument2 { get; set; }
            private Action AssertAction { get; set; }
            #endregion
        }

        private class CastUnitTest<TFrom, TTo> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public CastUnitTest(string name, TFrom source, bool expectedResult, TTo expectedValue = default(TTo))
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TFrom).Name);
                this.WriteLine("Expected Result = {0}", this.ExpectedResult);
                this.WriteLine("Expected Value  = {0} ({1})", this.ExpectedValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Act()
            {
                var castLambdaExpression = ExpressionBuilder.Cast<TFrom, TTo>();
                this.WriteLine("Lambda          = {0}", castLambdaExpression);

                var castLambda = castLambdaExpression.Compile();

                try
                {
                    this.ActualValue = castLambda(this.Source);
                    this.ActualResult = true;
                }
                catch (Exception)
                {
                    this.ActualResult = false;
                }

                this.WriteLine("Actual Result   = {0}", this.ActualResult);
                this.WriteLine("Actual Value    = {0} ({1})", this.ActualValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
                if (this.ActualResult)
                {
                    this.ActualValue.Should().BeEquivalentTo(this.ExpectedValue);
                }
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private bool ActualResult { get; set; }
            private TTo ActualValue { get; set; }
            #endregion

            #region User Supplied Properties
            private TFrom Source { get; set; }
            private bool ExpectedResult { get; set; }
            private TTo ExpectedValue { get; set; }
            #endregion
        }

        private class CastAsUnitTest<TFrom, TTo> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public CastAsUnitTest(string name, TFrom source, bool expectedResult, TTo expectedValue = default(TTo))
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source          = {0} ({1})", this.Source.SafeToString(), typeof(TFrom).Name);
                this.WriteLine("Expected Result = {0}", this.ExpectedResult);
                this.WriteLine("Expected Value  = {0} ({1})", this.ExpectedValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Act()
            {
                var castAsLambdaExpression = ExpressionBuilder.CastAs<TFrom, TTo>();
                this.WriteLine("Lambda          = {0}", castAsLambdaExpression);

                var castAsLambda = castAsLambdaExpression.Compile();

                this.ActualValue = castAsLambda(this.Source);
                this.ActualResult = Object.Equals(null, this.Source)
                    ? Object.Equals(this.ActualValue, null)
                    : !Object.Equals(this.ActualValue, null);

                this.WriteLine("Actual Result   = {0}", this.ActualResult);
                this.WriteLine("Actual Value    = {0} ({1})", this.ActualValue.SafeToString(), typeof(TTo).Name);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
                if (this.ActualResult)
                {
                    this.ActualValue.Should().BeEquivalentTo(this.ExpectedValue);
                }
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private bool ActualResult { get; set; }
            private TTo ActualValue { get; set; }
            #endregion

            #region User Supplied Properties
            private TFrom Source { get; set; }
            private bool ExpectedResult { get; set; }
            private TTo ExpectedValue { get; set; }
            #endregion
        }

        private class DefaultUnitTest<TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public DefaultUnitTest(string name, TObject expected)
                : base(name)
            {
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var defaultLambdaExpression = ExpressionBuilder.Default<TObject>();
                this.WriteLine("Lambda   = {0}", defaultLambdaExpression);

                var defaultLambda = defaultLambdaExpression.Compile();
                this.Actual = defaultLambda();
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().Be(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TObject Expected { get; set; }
            #endregion
        }

        private class NewWith0ArgumentsUnitTest<TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith0ArgumentsUnitTest(string name, TObject expected, Type objectType)
                : base(name)
            {
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith0ArgumentsLambdaExpression = ExpressionBuilder.New<TObject>(this.ObjectType);
                this.WriteLine("Lambda   = {0}", newWith0ArgumentsLambdaExpression);

                var newWith0ArgumentsLambda = newWith0ArgumentsLambdaExpression.Compile();
                this.Actual = newWith0ArgumentsLambda();
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class NewWith1ArgumentsUnitTest<TArgument, TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith1ArgumentsUnitTest(string name, TArgument argument, TObject expected, Type objectType)
                : base(name)
            {
                this.Argument = argument;
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument = {0} ({1})", this.Argument.SafeToString(), typeof(TArgument).Name);
                this.WriteLine("Expected = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith1ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument, TObject>(this.ObjectType);
                this.WriteLine("Lambda   = {0}", newWith1ArgumentsLambdaExpression);

                var newWith1ArgumentsLambda = newWith1ArgumentsLambdaExpression.Compile();
                this.Actual = newWith1ArgumentsLambda(this.Argument);
                this.WriteLine("Actual   = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument Argument { get; }
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class NewWith2ArgumentsUnitTest<TArgument1, TArgument2, TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith2ArgumentsUnitTest(string name, TArgument1 argument1, TArgument2 argument2, TObject expected, Type objectType)
                : base(name)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument 1 = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2 = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Expected   = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith2ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TObject>(this.ObjectType);
                this.WriteLine("Lambda     = {0}", newWith2ArgumentsLambdaExpression);

                var newWith2ArgumentsLambda = newWith2ArgumentsLambdaExpression.Compile();
                this.Actual = newWith2ArgumentsLambda(this.Argument1, this.Argument2);
                this.WriteLine("Actual     = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument1 Argument1 { get; }
            TArgument2 Argument2 { get; }
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class NewWith3ArgumentsUnitTest<TArgument1, TArgument2, TArgument3, TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith3ArgumentsUnitTest(string name, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TObject expected, Type objectType)
                : base(name)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.Argument3 = argument3;
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument 1 = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2 = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Argument 3 = {0} ({1})", this.Argument3.SafeToString(), typeof(TArgument3).Name);
                this.WriteLine("Expected   = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith3ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TObject>(this.ObjectType);
                this.WriteLine("Lambda     = {0}", newWith3ArgumentsLambdaExpression);

                var newWith3ArgumentsLambda = newWith3ArgumentsLambdaExpression.Compile();
                this.Actual = newWith3ArgumentsLambda(this.Argument1, this.Argument2, this.Argument3);
                this.WriteLine("Actual     = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument1 Argument1 { get; }
            TArgument2 Argument2 { get; }
            TArgument3 Argument3 { get; }
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class NewWith4ArgumentsUnitTest<TArgument1, TArgument2, TArgument3, TArgument4, TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith4ArgumentsUnitTest(string name, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TObject expected, Type objectType)
                : base(name)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.Argument3 = argument3;
                this.Argument4 = argument4;
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument 1 = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2 = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Argument 3 = {0} ({1})", this.Argument3.SafeToString(), typeof(TArgument3).Name);
                this.WriteLine("Argument 4 = {0} ({1})", this.Argument4.SafeToString(), typeof(TArgument4).Name);
                this.WriteLine("Expected   = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith4ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TArgument4, TObject>(this.ObjectType);
                this.WriteLine("Lambda     = {0}", newWith4ArgumentsLambdaExpression);

                var newWith4ArgumentsLambda = newWith4ArgumentsLambdaExpression.Compile();
                this.Actual = newWith4ArgumentsLambda(this.Argument1, this.Argument2, this.Argument3, this.Argument4);
                this.WriteLine("Actual     = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument1 Argument1 { get; }
            TArgument2 Argument2 { get; }
            TArgument3 Argument3 { get; }
            TArgument4 Argument4 { get; }
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class NewWith5ArgumentsUnitTest<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public NewWith5ArgumentsUnitTest(string name, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TObject expected, Type objectType)
                : base(name)
            {
                this.Argument1 = argument1;
                this.Argument2 = argument2;
                this.Argument3 = argument3;
                this.Argument4 = argument4;
                this.Argument5 = argument5;
                this.Expected = expected;
                this.ObjectType = objectType;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Argument 1 = {0} ({1})", this.Argument1.SafeToString(), typeof(TArgument1).Name);
                this.WriteLine("Argument 2 = {0} ({1})", this.Argument2.SafeToString(), typeof(TArgument2).Name);
                this.WriteLine("Argument 3 = {0} ({1})", this.Argument3.SafeToString(), typeof(TArgument3).Name);
                this.WriteLine("Argument 4 = {0} ({1})", this.Argument4.SafeToString(), typeof(TArgument4).Name);
                this.WriteLine("Argument 5 = {0} ({1})", this.Argument5.SafeToString(), typeof(TArgument5).Name);
                this.WriteLine("Expected   = {0} ({1})", this.Expected.SafeToString(), typeof(TObject).Name);
            }

            protected override void Act()
            {
                var newWith5ArgumentsLambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>(this.ObjectType);
                this.WriteLine("Lambda     = {0}", newWith5ArgumentsLambdaExpression);

                var newWith5ArgumentsLambda = newWith5ArgumentsLambdaExpression.Compile();
                this.Actual = newWith5ArgumentsLambda(this.Argument1, this.Argument2, this.Argument3, this.Argument4, this.Argument5);
                this.WriteLine("Actual     = {0} ({1})", this.Actual.SafeToString(), typeof(TObject).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            TObject Actual { get; set; }
            #endregion

            #region User Supplied Properties
            TArgument1 Argument1 { get; }
            TArgument2 Argument2 { get; }
            TArgument3 Argument3 { get; }
            TArgument4 Argument4 { get; }
            TArgument5 Argument5 { get; }
            TObject Expected { get; }
            Type ObjectType { get; }
            #endregion
        }

        private class PropertyGetterUnitTest<TObject, TProperty> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public PropertyGetterUnitTest(string name, TObject source, string propertyName, TProperty expected)
                : base(name)
            {
                this.Source = source;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source        = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Act()
            {
                var propertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertyGetterLambdaExpression);

                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                this.Actual = propertyGetterLambda(this.Source);
                this.WriteLine("Actual        = {0} ({1})", this.Actual.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        private class PropertySetterUnitTest<TObject, TProperty> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public PropertySetterUnitTest(string name, TObject source, string propertyName, TProperty expected)
                : base(name)
            {
                this.Source = source;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Source        = {0} ({1})", this.Source.SafeToString(), typeof(TObject).Name);
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Act()
            {
                var propertySetterLambdaExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertySetterLambdaExpression);

                var propertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(this.PropertyName);
                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                var actualBefore = propertyGetterLambda(this.Source);
                this.WriteLine("Actual Before = {0} ({1})", actualBefore.SafeToString(), typeof(TProperty).Name);
                this.ActualBefore = actualBefore;

                var propertySetterLambda = propertySetterLambdaExpression.Compile();
                propertySetterLambda(this.Source, this.Expected);

                var actualAfter = propertyGetterLambda(this.Source);
                this.WriteLine("Actual After  = {0} ({1})", actualAfter.SafeToString(), typeof(TProperty).Name);
                this.ActualAfter = actualAfter;
            }

            protected override void Assert()
            {
                this.ActualBefore.Should().NotBe(this.ActualAfter);
                this.ActualAfter.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty ActualBefore { get; set; }
            private TProperty ActualAfter { get; set; }
            #endregion

            #region User Supplied Properties
            private TObject Source { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        private class StaticPropertyGetterUnitTest<TClass, TProperty> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticPropertyGetterUnitTest(string name, Action staticInitializer, string propertyName, TProperty expected)
                : base(name)
            {
                this.StaticInitializer = staticInitializer;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);

                this.StaticInitializer();
            }

            protected override void Act()
            {
                var staticPropertyGetterLambdaExpression = ExpressionBuilder.StaticPropertyGetter<TClass, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", staticPropertyGetterLambdaExpression);

                var staticPropertyGetterLambda = staticPropertyGetterLambdaExpression.Compile();
                this.Actual = staticPropertyGetterLambda();
                this.WriteLine("Actual        = {0} ({1})", this.Actual.SafeToString(), typeof(TProperty).Name);
            }

            protected override void Assert()
            {
                this.Actual.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty Actual { get; set; }
            #endregion

            #region User Supplied Properties
            private Action StaticInitializer { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }

        private class StaticPropertySetterUnitTest<TClass, TProperty> : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public StaticPropertySetterUnitTest(string name, Action staticInitializer, string propertyName, TProperty expected)
                : base(name)
            {
                this.StaticInitializer = staticInitializer;
                this.PropertyName = propertyName;
                this.Expected = expected;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Property Name = {0}", this.PropertyName);
                this.WriteLine("Expected      = {0} ({1})", this.Expected.SafeToString(), typeof(TProperty).Name);

                this.StaticInitializer();
            }

            protected override void Act()
            {
                var propertySetterLambdaExpression = ExpressionBuilder.StaticPropertySetter<TClass, TProperty>(this.PropertyName);
                this.WriteLine("Lambda        = {0}", propertySetterLambdaExpression);

                var propertyGetterLambdaExpression = ExpressionBuilder.StaticPropertyGetter<TClass, TProperty>(this.PropertyName);
                var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
                var actualBefore = propertyGetterLambda();
                this.WriteLine("Actual Before = {0} ({1})", actualBefore.SafeToString(), typeof(TProperty).Name);
                this.ActualBefore = actualBefore;

                var propertySetterLambda = propertySetterLambdaExpression.Compile();
                propertySetterLambda(this.Expected);

                var actualAfter = propertyGetterLambda();
                this.WriteLine("Actual After  = {0} ({1})", actualAfter.SafeToString(), typeof(TProperty).Name);
                this.ActualAfter = actualAfter;
            }

            protected override void Assert()
            {
                this.ActualBefore.Should().NotBe(this.ActualAfter);
                this.ActualAfter.Should().BeEquivalentTo(this.Expected);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private TProperty ActualBefore { get; set; }
            private TProperty ActualAfter { get; set; }
            #endregion

            #region User Supplied Properties
            private Action StaticInitializer { get; set; }
            private string PropertyName { get; set; }
            private TProperty Expected { get; set; }
            #endregion
        }
        #endregion
    }
}
