﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

#pragma warning disable 169
#pragma warning disable 649

namespace ApiFramework.Reflection
{
    public class TypeReflectionTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeReflectionTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetBaseTypeTestData))]
        public void TestGetBaseType(string name, Type type, Type expectedBaseType)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type              = {0}", type != null ? type.Name : null);

            var expectedBaseTypeName = expectedBaseType != null ? expectedBaseType.Name : "null";
            this.WriteLine("Expected BaseType = {0}", expectedBaseTypeName);

            // Act
            var actualBaseType     = TypeReflection.GetBaseType(type);
            var actualBaseTypeName = actualBaseType != null ? actualBaseType.Name : "null";
            this.WriteLine("Actual BaseType   = {0}", actualBaseTypeName);

            // Assert
            actualBaseTypeName.Should().Be(expectedBaseTypeName);
        }

        [Theory]
        [MemberData(nameof(GetBaseTypesTestData))]
        public void TestGetBaseTypes(string name, Type type, IEnumerable<Type> expectedBaseTypes)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type               = {0}", type != null ? type.Name : null);

            var expectedBaseTypeNames = expectedBaseTypes.EmptyIfNull().Select(x => x.Name).SafeToDelimitedString(", ");
            this.WriteLine("Expected BaseTypes = {0}", expectedBaseTypeNames);

            // Act
            var actualBaseTypes     = TypeReflection.GetBaseTypes(type).ToList();
            var actualBaseTypeNames = actualBaseTypes.EmptyIfNull().Select(x => x.Name).SafeToDelimitedString(", ");
            this.WriteLine("Actual BaseTypes   = {0}", actualBaseTypeNames);

            // Assert
            actualBaseTypeNames.Should().Be(expectedBaseTypeNames);
        }

        [Theory]
        [MemberData(nameof(GetConstructorTestData))]
        public void TestGetConstructor(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes, string expectedConstructorName)
        {
            // Arrange
            var parameterTypesList = parameterTypes.SafeToList();

            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);
            this.WriteLine("Parameter Types = {0}", parameterTypesList.SafeToDelimitedString(", "));

            var expected = expectedConstructorName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualMethod = TypeReflection.GetConstructor(type, reflectionFlags, parameterTypesList);
            var actual       = actualMethod != null ? actualMethod.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetConstructorsTestData))]
        public void TestGetConstructors(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedConstructorNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            expectedConstructorNames = expectedConstructorNames.SafeToList();
            var expected = expectedConstructorNames;
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualConstructors = TypeReflection.GetConstructors(type, reflectionFlags);
            var actual = actualConstructors
                        .Select(x => x.Name)
                        .ToList();

            // Assert
            actual.Should().BeEquivalentTo(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(GetFieldTestData))]
        public void TestGetField(string name, Type type, string fieldName, ReflectionFlags reflectionFlags, string expectedFieldName)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type          = {0}", type);
            this.WriteLine("Field Name    = {0}", fieldName);
            this.WriteLine("Binding Flags = {0}", reflectionFlags);

            var expected = expectedFieldName;
            this.WriteLine("Expected      = {0}", expected);

            // Act
            var actualField = TypeReflection.GetField(type, fieldName, reflectionFlags);
            var actual      = actualField != null ? actualField.Name : null;
            this.WriteLine("Actual        = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetFieldsTestData))]
        public void TestGetFields(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedFieldNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type          = {0}", type);
            this.WriteLine("Binding Flags = {0}", reflectionFlags);

            var expected = expectedFieldNames.OrderBy(x => x)
                                             .ToList();
            this.WriteLine("Expected      = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualFields = TypeReflection.GetFields(type, reflectionFlags);
            var actual = actualFields
                        .OrderBy(x => x.Name)
                        .Select(x => x.Name)
                        .ToList();

            // Assert
            actual.Should().BeEquivalentTo(expected);
            this.WriteLine("Actual        = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(GetMethodTestData))]
        public void TestGetMethod(string name, Type type, string methodName, ReflectionFlags reflectionFlags, IEnumerable<Type> parameterTypes, string expectedMethodName)
        {
            // Arrange
            var parameterTypesList = parameterTypes.SafeToList();

            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Method Name     = {0}", methodName);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);
            this.WriteLine("Parameter Types = {0}", parameterTypesList.SafeToDelimitedString(", "));

            var expected = expectedMethodName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualMethod = TypeReflection.GetMethod(type, methodName, reflectionFlags, parameterTypesList);
            var actual       = actualMethod != null ? actualMethod.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetMethodsTestData))]
        public void TestGetMethods(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedMethodNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedMethodNames.SafeToList();
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualMethods = TypeReflection.GetMethods(type, reflectionFlags);
            var actual = actualMethods
                        .OrderBy(x => x.Name)
                        .Select(x => x.Name)
                        .ToList();

            // Assert
            // Ignore the inherited methods from Object.
            // So assert actual at least contains all of expected but may have more.
            actual.Should().Contain(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(GetPropertyTestData))]
        public void TestGetProperty(string name, Type type, string propertyName, ReflectionFlags reflectionFlags, string expectedPropertyName)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Property Name   = {0}", propertyName);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedPropertyName;
            this.WriteLine("Expected        = {0}", expected);

            // Act
            var actualProperty = TypeReflection.GetProperty(type, propertyName, reflectionFlags);
            var actual         = actualProperty != null ? actualProperty.Name : null;
            this.WriteLine("Actual          = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(GetPropertiesTestData))]
        public void TestGetProperties(string name, Type type, ReflectionFlags reflectionFlags, IEnumerable<string> expectedPropertyNames)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type            = {0}", type);
            this.WriteLine("Binding Flags   = {0}", reflectionFlags);

            var expected = expectedPropertyNames.OrderBy(x => x)
                                                .ToList();
            this.WriteLine("Expected        = {0}", expected.SafeToDelimitedString(", "));

            // Act
            var actualProperties = TypeReflection.GetProperties(type, reflectionFlags);
            var actual = actualProperties
                        .OrderBy(x => x.Name)
                        .Select(x => x.Name)
                        .ToList();

            // Assert
            actual.Should().BeEquivalentTo(expected);
            this.WriteLine("Actual          = {0}", actual.SafeToDelimitedString(", "));
        }

        [Theory]
        [MemberData(nameof(IsComplexTestData))]
        public void TestIsComplex(string name, Type type, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type     = {0}", type);
            this.WriteLine("Expected = {0}", expected);

            // Act
            var actual = TypeReflection.IsComplex(type);
            this.WriteLine("Actual   = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(IsEnumerableOfTTestData))]
        public void TestIsEnumerableOfT(string name, Type type, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type     = {0}", type);
            this.WriteLine("Expected = {0}", expected);

            // Act
            var actual = TypeReflection.IsEnumerableOfT(type);
            this.WriteLine("Actual   = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(IsImplementationOfTestData))]
        public void TestIsImplementationOf(string name, Type derivedType, Type baseType, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Derived Type = {0}", derivedType);
            this.WriteLine("Base Type    = {0}", baseType);
            this.WriteLine("Expected     = {0}", expected);

            // Act
            var actual = TypeReflection.IsImplementationOf(derivedType, baseType);
            this.WriteLine("Actual       = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(IsSimpleTestData))]
        public void TestIsSimple(string name, Type type, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Type     = {0}", type);
            this.WriteLine("Expected = {0}", expected);

            // Act
            var actual = TypeReflection.IsSimple(type);
            this.WriteLine("Actual   = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(IsSubclassOrImplementationOfTestData))]
        public void TestIsSubclassOrImplementationOf(string name, Type derivedType, Type baseOrInterfaceType, bool expected)
        {
            // Arrange
            this.WriteLine(name);
            this.WriteLine();
            this.WriteLine("Derived Type         = {0}", derivedType);
            this.WriteLine("BaseOrInterface Type = {0}", baseOrInterfaceType);
            this.WriteLine("Expected             = {0}", expected);

            // Act
            var actual = TypeReflection.IsSubclassOrImplementationOf(derivedType, baseOrInterfaceType);
            this.WriteLine("Actual               = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> GetBaseTypeTestData = new[]
                                                                           {
                                                                               new object[] {"WithInterfaceType", typeof(IShape), null},
                                                                               new object[] {"WithType", typeof(AbstractShape), typeof(object)},
                                                                               new object[] {"WithTypeAndBaseType", typeof(Rectangle), typeof(AbstractShape)},
                                                                               new object[] {"WithTypeAndBaseTypeAndBaseType", typeof(Square), typeof(Rectangle)},

                                                                               new object[] {"WithGenericInterfaceType", typeof(ICollection<>), null},
                                                                               new object[] {"WithGenericType", typeof(AbstractCollection<>), typeof(object)},
                                                                               new object[] {"WithGenericTypeAndGenericBaseType", typeof(Collection<>), typeof(AbstractCollection<>)},
                                                                               new object[] {"WithGenericTypeAndGenericBaseTypeAndGenericBaseType", typeof(DecoratedCollection<>), typeof(Collection<>)},
                                                                           };

        public static readonly IEnumerable<object[]> GetBaseTypesTestData = new[]
                                                                            {
                                                                                new object[] {"WithInterfaceType", typeof(IShape), Enumerable.Empty<Type>()},
                                                                                new object[] {"WithType", typeof(AbstractShape), new[] {typeof(object)}},
                                                                                new object[] {"WithTypeAndBaseType", typeof(Rectangle), new[] {typeof(AbstractShape), typeof(object)}},
                                                                                new object[] {"WithTypeAndBaseTypeAndBaseType", typeof(Square), new[] {typeof(Rectangle), typeof(AbstractShape), typeof(object)}},

                                                                                new object[] {"WithGenericInterfaceType", typeof(ICollection<>), Enumerable.Empty<Type>()},
                                                                                new object[] {"WithGenericType", typeof(AbstractCollection<>), new[] {typeof(object)}},
                                                                                new object[] {"WithGenericTypeAndGenericBaseType", typeof(Collection<>), new[] {typeof(AbstractCollection<>), typeof(object)}},
                                                                                new object[] {"WithGenericTypeAndGenericBaseTypeAndGenericBaseType", typeof(DecoratedCollection<>), new[] {typeof(Collection<>), typeof(AbstractCollection<>), typeof(object)}},
                                                                            };

        public static readonly IEnumerable<object[]> GetConstructorTestData = new[]
                                                                              {
                                                                                  new object[] {"WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] { }, ".ctor"},
                                                                                  new object[] {"WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int)}, ".ctor"},
                                                                                  new object[] {"WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                                                                                  new object[] {"WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                                  new object[] {"WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] { }, ".ctor"},
                                                                                  new object[] {"WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int)}, ".ctor"},
                                                                                  new object[] {"WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, ".ctor"},
                                                                                  new object[] {"WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},

                                                                                  new object[] {"WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] { }, null},
                                                                                  new object[] {"WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int)}, null},
                                                                                  new object[] {"WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string)}, null},
                                                                                  new object[] {"WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new Type[] {typeof(int), typeof(string), typeof(bool)}, ".ctor"},
                                                                              };

        public static readonly IEnumerable<object[]> GetConstructorsTestData = new[]
                                                                               {
                                                                                   new object[] {"WithPublic", typeof(ClassWithConstructors), ReflectionFlags.Public, new[] {".ctor", ".ctor", ".ctor"}},
                                                                                   new object[] {"WithPublicAndNonPublic", typeof(ClassWithConstructors), ReflectionFlags.Public | ReflectionFlags.NonPublic, new[] {".ctor", ".ctor", ".ctor", ".ctor"}},
                                                                                   new object[] {"WithNonPublic", typeof(ClassWithConstructors), ReflectionFlags.NonPublic, new[] {".ctor"}}
                                                                               };

        public static readonly IEnumerable<object[]> GetFieldTestData = new[]
                                                                        {
                                                                            new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithFields), "PublicField", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public            | ReflectionFlags.Instance, "PublicField"},
                                                                            new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithFields), "PublicStaticField", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public        | ReflectionFlags.Static, "PublicStaticField"},
                                                                            new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithFields), "PrivateField", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic     | ReflectionFlags.Instance, "PrivateField"},
                                                                            new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithFields), "PrivateStaticField", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static, "PrivateStaticField"},
                                                                            new object[] {"WithPublicAndInstance", typeof(ClassWithFields), "PublicFieldBase", ReflectionFlags.Public                                                      | ReflectionFlags.Instance, "PublicFieldBase"},
                                                                            new object[] {"WithPublicAndStatic", typeof(ClassWithFields), "PublicStaticFieldBase", ReflectionFlags.Public                                                  | ReflectionFlags.Static, "PublicStaticFieldBase"},
                                                                            new object[] {"WithNonPublicAndInstance", typeof(ClassWithFields), "PrivateFieldBase", ReflectionFlags.NonPublic                                               | ReflectionFlags.Instance, "PrivateFieldBase"},
                                                                            new object[] {"WithNonPublicAndStatic", typeof(ClassWithFields), "PrivateStaticFieldBase", ReflectionFlags.NonPublic                                           | ReflectionFlags.Static, "PrivateStaticFieldBase"},
                                                                        };

        public static readonly IEnumerable<object[]> GetFieldsTestData = new[]
                                                                         {
                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                 new[] {"PublicField"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                 new[] {"PublicStaticField"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[] {"PublicField", "PublicStaticField"}
                                                                             },


                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                 new[] {"ProtectedInternalField", "ProtectedField", "InternalField", "PrivateField"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                 new[] {"ProtectedInternalStaticField", "ProtectedStaticField", "InternalStaticField", "PrivateStaticField"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[] {"ProtectedInternalField", "ProtectedInternalStaticField", "ProtectedField", "ProtectedStaticField", "InternalField", "InternalStaticField", "PrivateField", "PrivateStaticField"}
                                                                             },


                                                                             new object[]
                                                                             {
                                                                                 "WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[] {"PublicField", "PublicStaticField", "ProtectedInternalField", "ProtectedInternalStaticField", "ProtectedField", "ProtectedStaticField", "InternalField", "InternalStaticField", "PrivateField", "PrivateStaticField"}
                                                                             },


                                                                             new object[]
                                                                             {
                                                                                 "WithPublicAndInstance", typeof(ClassWithFields), ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                 new[] {"PublicField", "PublicFieldBase"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithPublicAndStatic", typeof(ClassWithFields), ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                 new[] {"PublicStaticField", "PublicStaticFieldBase"}
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[] {"PublicField", "PublicFieldBase", "PublicStaticField", "PublicStaticFieldBase"}
                                                                             },


                                                                             new object[]
                                                                             {
                                                                                 "WithNonPublicAndInstance", typeof(ClassWithFields), ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                 new[]
                                                                                 {
                                                                                     "ProtectedInternalField", "ProtectedField", "InternalField", "PrivateField",
                                                                                     "ProtectedInternalFieldBase", "ProtectedFieldBase", "InternalFieldBase", "PrivateFieldBase"
                                                                                 }
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithNonPublicAndStatic", typeof(ClassWithFields), ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                 new[]
                                                                                 {
                                                                                     "ProtectedInternalStaticField", "ProtectedStaticField", "InternalStaticField", "PrivateStaticField",
                                                                                     "ProtectedInternalStaticFieldBase", "ProtectedStaticFieldBase", "InternalStaticFieldBase", "PrivateStaticFieldBase"
                                                                                 }
                                                                             },

                                                                             new object[]
                                                                             {
                                                                                 "WithNonPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[]
                                                                                 {
                                                                                     "ProtectedInternalField", "ProtectedInternalStaticField", "ProtectedField", "ProtectedStaticField", "InternalField", "InternalStaticField", "PrivateField", "PrivateStaticField",
                                                                                     "ProtectedInternalFieldBase", "ProtectedInternalStaticFieldBase", "ProtectedFieldBase", "ProtectedStaticFieldBase", "InternalFieldBase", "InternalStaticFieldBase", "PrivateFieldBase", "PrivateStaticFieldBase"
                                                                                 }
                                                                             },


                                                                             new object[]
                                                                             {
                                                                                 "WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithFields), ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                 new[]
                                                                                 {
                                                                                     "PublicField", "PublicStaticField", "ProtectedInternalField", "ProtectedInternalStaticField", "ProtectedField", "ProtectedStaticField", "InternalField", "InternalStaticField", "PrivateField", "PrivateStaticField",
                                                                                     "PublicFieldBase", "PublicStaticFieldBase", "ProtectedInternalFieldBase", "ProtectedInternalStaticFieldBase", "ProtectedFieldBase", "ProtectedStaticFieldBase", "InternalFieldBase", "InternalStaticFieldBase", "PrivateFieldBase", "PrivateStaticFieldBase"
                                                                                 }
                                                                             }
                                                                         };

        public static readonly IEnumerable<object[]> GetMethodTestData = new[]
                                                                         {
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] { }, "PublicMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PublicMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), "PublicMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] { }, "PublicStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] { }, "PrivateMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PrivateMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PrivateMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), "PrivateMethod", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] { }, "PrivateStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int)}, "PrivateStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PrivateStaticMethod"},
                                                                             new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), "PrivateStaticMethod", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] { }, "PublicMethodBase"},
                                                                             new object[] {"PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int)}, "PublicMethodBase"},
                                                                             new object[] {"PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "PublicMethodBase"},
                                                                             new object[] {"PublicAndInstance", typeof(ClassWithMethods), "PublicMethodBase", ReflectionFlags.Public | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] { }, "PublicStaticMethodBase"},
                                                                             new object[] {"PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int)}, "PublicStaticMethodBase"},
                                                                             new object[] {"PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "PublicStaticMethodBase"},
                                                                             new object[] {"PublicAndStatic", typeof(ClassWithMethods), "PublicStaticMethodBase", ReflectionFlags.Public | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] { }, "ProtectedMethodBase"},
                                                                             new object[] {"NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int)}, "ProtectedMethodBase"},
                                                                             new object[] {"NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string)}, "ProtectedMethodBase"},
                                                                             new object[] {"NonPublicAndInstance", typeof(ClassWithMethods), "ProtectedMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Instance, new Type[] {typeof(int), typeof(string), typeof(bool)}, null},

                                                                             new object[] {"NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] { }, "ProtectedStaticMethodBase"},
                                                                             new object[] {"NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int)}, "ProtectedStaticMethodBase"},
                                                                             new object[] {"NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string)}, "ProtectedStaticMethodBase"},
                                                                             new object[] {"NonPublicAndStatic", typeof(ClassWithMethods), "ProtectedStaticMethodBase", ReflectionFlags.NonPublic | ReflectionFlags.Static, new Type[] {typeof(int), typeof(string), typeof(bool)}, null}
                                                                         };

        public static readonly IEnumerable<object[]> GetMethodsTestData = new[]
                                                                          {
                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                  new[] {"PublicMethod", "PublicMethod", "PublicMethod"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                  new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod"}
                                                                              },


                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                  new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                  new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}
                                                                              },


                                                                              new object[]
                                                                              {
                                                                                  "WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PrivateMethod", "PrivateMethod", "PrivateMethod", "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod"}
                                                                              },


                                                                              new object[]
                                                                              {
                                                                                  "WithPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                  new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                  new[] {"PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[] {"PublicMethod", "PublicMethod", "PublicMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase"}
                                                                              },


                                                                              new object[]
                                                                              {
                                                                                  "WithNonPublicAndInstance", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                  new[] {"PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithNonPublicAndStatic", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                  new[] {"PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"}
                                                                              },

                                                                              new object[]
                                                                              {
                                                                                  "WithNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[]
                                                                                  {
                                                                                      "PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                                                                                      "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"
                                                                                  }
                                                                              },


                                                                              new object[]
                                                                              {
                                                                                  "WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithMethods), ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                  new[]
                                                                                  {
                                                                                      "PublicMethod", "PublicMethod", "PublicMethod", "PublicMethodBase", "PublicMethodBase", "PublicMethodBase",
                                                                                      "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethod", "PublicStaticMethodBase", "PublicStaticMethodBase", "PublicStaticMethodBase",
                                                                                      "PrivateMethod", "PrivateMethod", "PrivateMethod", "ProtectedMethodBase", "ProtectedMethodBase", "ProtectedMethodBase",
                                                                                      "PrivateStaticMethod", "PrivateStaticMethod", "PrivateStaticMethod", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase", "ProtectedStaticMethodBase"
                                                                                  }
                                                                              },
                                                                          };

        public static readonly IEnumerable<object[]> GetPropertyTestData = new[]
                                                                           {
                                                                               new object[] {"WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), "PublicProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public            | ReflectionFlags.Instance, "PublicProperty"},
                                                                               new object[] {"WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), "PublicStaticProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.Public        | ReflectionFlags.Static, "PublicStaticProperty"},
                                                                               new object[] {"WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), "PrivateProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic     | ReflectionFlags.Instance, "PrivateProperty"},
                                                                               new object[] {"WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticProperty", ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static, "PrivateStaticProperty"},
                                                                               new object[] {"WithPublicAndInstance", typeof(ClassWithProperties), "PublicPropertyBase", ReflectionFlags.Public                                                      | ReflectionFlags.Instance, "PublicPropertyBase"},
                                                                               new object[] {"WithPublicAndStatic", typeof(ClassWithProperties), "PublicStaticPropertyBase", ReflectionFlags.Public                                                  | ReflectionFlags.Static, "PublicStaticPropertyBase"},
                                                                               new object[] {"WithNonPublicAndInstance", typeof(ClassWithProperties), "PrivatePropertyBase", ReflectionFlags.NonPublic                                               | ReflectionFlags.Instance, "PrivatePropertyBase"},
                                                                               new object[] {"WithNonPublicAndStatic", typeof(ClassWithProperties), "PrivateStaticPropertyBase", ReflectionFlags.NonPublic                                           | ReflectionFlags.Static, "PrivateStaticPropertyBase"},
                                                                           };

        public static readonly IEnumerable<object[]> GetPropertiesTestData = new[]
                                                                             {
                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                     new[] {"PublicProperty"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                     new[] {"PublicStaticProperty"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[] {"PublicProperty", "PublicStaticProperty"}
                                                                                 },


                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndNonPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                     new[] {"ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndNonPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                     new[] {"ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[] {"ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty"}
                                                                                 },


                                                                                 new object[]
                                                                                 {
                                                                                     "WithDeclaredOnlyAndPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[] {"PublicProperty", "PublicStaticProperty", "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty"}
                                                                                 },


                                                                                 new object[]
                                                                                 {
                                                                                     "WithPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Instance,
                                                                                     new[] {"PublicProperty", "PublicPropertyBase"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Static,
                                                                                     new[] {"PublicStaticProperty", "PublicStaticPropertyBase"}
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[] {"PublicProperty", "PublicPropertyBase", "PublicStaticProperty", "PublicStaticPropertyBase"}
                                                                                 },


                                                                                 new object[]
                                                                                 {
                                                                                     "WithNonPublicAndInstance", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Instance,
                                                                                     new[]
                                                                                     {
                                                                                         "ProtectedInternalProperty", "ProtectedProperty", "InternalProperty", "PrivateProperty",
                                                                                         "ProtectedInternalPropertyBase", "ProtectedPropertyBase", "InternalPropertyBase", "PrivatePropertyBase"
                                                                                     }
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithNonPublicAndStatic", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Static,
                                                                                     new[]
                                                                                     {
                                                                                         "ProtectedInternalStaticProperty", "ProtectedStaticProperty", "InternalStaticProperty", "PrivateStaticProperty",
                                                                                         "ProtectedInternalStaticPropertyBase", "ProtectedStaticPropertyBase", "InternalStaticPropertyBase", "PrivateStaticPropertyBase"
                                                                                     }
                                                                                 },

                                                                                 new object[]
                                                                                 {
                                                                                     "WithNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[]
                                                                                     {
                                                                                         "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty",
                                                                                         "ProtectedInternalPropertyBase", "ProtectedInternalStaticPropertyBase", "ProtectedPropertyBase", "ProtectedStaticPropertyBase", "InternalPropertyBase", "InternalStaticPropertyBase", "PrivatePropertyBase", "PrivateStaticPropertyBase"
                                                                                     }
                                                                                 },


                                                                                 new object[]
                                                                                 {
                                                                                     "WithPublicAndNonPublicAndInstanceAndStatic", typeof(ClassWithProperties), ReflectionFlags.Public | ReflectionFlags.NonPublic | ReflectionFlags.Instance | ReflectionFlags.Static,
                                                                                     new[]
                                                                                     {
                                                                                         "PublicProperty", "PublicStaticProperty", "ProtectedInternalProperty", "ProtectedInternalStaticProperty", "ProtectedProperty", "ProtectedStaticProperty", "InternalProperty", "InternalStaticProperty", "PrivateProperty", "PrivateStaticProperty",
                                                                                         "PublicPropertyBase", "PublicStaticPropertyBase", "ProtectedInternalPropertyBase", "ProtectedInternalStaticPropertyBase", "ProtectedPropertyBase", "ProtectedStaticPropertyBase", "InternalPropertyBase", "InternalStaticPropertyBase", "PrivatePropertyBase", "PrivateStaticPropertyBase"
                                                                                     }
                                                                                 }
                                                                             };

        public static readonly IEnumerable<object[]> IsComplexTestData = new[]
                                                                         {
                                                                             new object[] {"Bool", typeof(bool), false},
                                                                             new object[] {"ByteArray", typeof(byte[]), false},
                                                                             new object[] {"Byte", typeof(byte), false},
                                                                             new object[] {"Char", typeof(char), false},
                                                                             new object[] {"DateTime", typeof(DateTime), false},
                                                                             new object[] {"DateTimeOffset", typeof(DateTimeOffset), false},
                                                                             new object[] {"Decimal", typeof(decimal), false},
                                                                             new object[] {"Double", typeof(double), false},
                                                                             new object[] {"Enum", typeof(PrimaryColor), false},
                                                                             new object[] {"Float", typeof(float), false},
                                                                             new object[] {"Guid", typeof(Guid), false},
                                                                             new object[] {"Int", typeof(int), false},
                                                                             new object[] {"Long", typeof(long), false},
                                                                             new object[] {"NullableBool", typeof(bool?), false},
                                                                             new object[] {"NullableByte", typeof(byte?), false},
                                                                             new object[] {"NullableChar", typeof(char?), false},
                                                                             new object[] {"NullableDateTime", typeof(DateTime?), false},
                                                                             new object[] {"NullableDateTimeOffset", typeof(DateTimeOffset?), false},
                                                                             new object[] {"NullableDecimal", typeof(decimal?), false},
                                                                             new object[] {"NullableDouble", typeof(double?), false},
                                                                             new object[] {"NullableEnum", typeof(PrimaryColor?), false},
                                                                             new object[] {"NullableFloat", typeof(float?), false},
                                                                             new object[] {"NullableGuid", typeof(Guid?), false},
                                                                             new object[] {"NullableInt", typeof(int?), false},
                                                                             new object[] {"NullableLong", typeof(long?), false},
                                                                             new object[] {"NullableSByte", typeof(sbyte?), false},
                                                                             new object[] {"NullableShort", typeof(short?), false},
                                                                             new object[] {"NullableTimeSpan", typeof(TimeSpan?), false},
                                                                             new object[] {"NullableUInt", typeof(uint?), false},
                                                                             new object[] {"NullableULong", typeof(ulong?), false},
                                                                             new object[] {"NullableUShort", typeof(ushort?), false},
                                                                             new object[] {"Object", typeof(object), true},
                                                                             new object[] {"SByte", typeof(sbyte), false},
                                                                             new object[] {"Short", typeof(short), false},
                                                                             new object[] {"String", typeof(string), false},
                                                                             new object[] {"TimeSpan", typeof(TimeSpan), false},
                                                                             new object[] {"Type", typeof(Type), false},
                                                                             new object[] {"UInt", typeof(uint), false},
                                                                             new object[] {"ULong", typeof(ulong), false},
                                                                             new object[] {"Uri", typeof(Uri), false},
                                                                             new object[] {"UShort", typeof(ushort), false},
                                                                             new object[] {"Class", typeof(Rectangle), true},
                                                                             new object[] {"Interface", typeof(IShape), true},
                                                                             new object[] {"OpenGeneric", typeof(ICollection<>), true},
                                                                             new object[] {"ClosedGeneric", typeof(ICollection<string>), true},
                                                                         };

        public static readonly IEnumerable<object[]> IsEnumerableOfTTestData = new[]
                                                                               {
                                                                                   new object[] {"WithSimpleType", typeof(bool), false},
                                                                                   new object[] {"WithComplexType", typeof(Rectangle), false},
                                                                                   new object[] {"WithClosedGenericList", typeof(List<string>), true},
                                                                                   new object[] {"WithOpenGenericList", typeof(List<>), true},
                                                                                   new object[] {"WithClosedEnumerableOfT", typeof(IEnumerable<string>), true},
                                                                                   new object[] {"WithOpenEnumerableOfT", typeof(IEnumerable<>), true},
                                                                                   new object[] {"WithArray", typeof(int[]), true},
                                                                               };

        public static readonly IEnumerable<object[]> IsImplementationOfTestData = new[]
                                                                                  {
                                                                                      new object[] {"NegativeCaseWithGenerics", typeof(Host<>), typeof(ICollection<>), false},
                                                                                      new object[] {"NegativeCaseWithNonGenerics", typeof(Rectangle), typeof(IAnimal), false},

                                                                                      new object[] {"PositiveCaseWithGenerics", typeof(Host<>), typeof(IHost<>), true},
                                                                                      new object[] {"PositiveCaseWithNonGenerics", typeof(Rectangle), typeof(IShape), true}
                                                                                  };

        public static readonly IEnumerable<object[]> IsSimpleTestData = new[]
                                                                        {
                                                                            new object[] {"Bool", typeof(bool), true},
                                                                            new object[] {"ByteArray", typeof(byte[]), true},
                                                                            new object[] {"Byte", typeof(byte), true},
                                                                            new object[] {"Char", typeof(char), true},
                                                                            new object[] {"DateTime", typeof(DateTime), true},
                                                                            new object[] {"DateTimeOffset", typeof(DateTimeOffset), true},
                                                                            new object[] {"Decimal", typeof(decimal), true},
                                                                            new object[] {"Double", typeof(double), true},
                                                                            new object[] {"Enum", typeof(PrimaryColor), true},
                                                                            new object[] {"Float", typeof(float), true},
                                                                            new object[] {"Guid", typeof(Guid), true},
                                                                            new object[] {"Int", typeof(int), true},
                                                                            new object[] {"Long", typeof(long), true},
                                                                            new object[] {"NullableBool", typeof(bool?), true},
                                                                            new object[] {"NullableByte", typeof(byte?), true},
                                                                            new object[] {"NullableChar", typeof(char?), true},
                                                                            new object[] {"NullableDateTime", typeof(DateTime?), true},
                                                                            new object[] {"NullableDateTimeOffset", typeof(DateTimeOffset?), true},
                                                                            new object[] {"NullableDecimal", typeof(decimal?), true},
                                                                            new object[] {"NullableDouble", typeof(double?), true},
                                                                            new object[] {"NullableEnum", typeof(PrimaryColor?), true},
                                                                            new object[] {"NullableFloat", typeof(float?), true},
                                                                            new object[] {"NullableGuid", typeof(Guid?), true},
                                                                            new object[] {"NullableInt", typeof(int?), true},
                                                                            new object[] {"NullableLong", typeof(long?), true},
                                                                            new object[] {"NullableSByte", typeof(sbyte?), true},
                                                                            new object[] {"NullableShort", typeof(short?), true},
                                                                            new object[] {"NullableTimeSpan", typeof(TimeSpan?), true},
                                                                            new object[] {"NullableUInt", typeof(uint?), true},
                                                                            new object[] {"NullableULong", typeof(ulong?), true},
                                                                            new object[] {"NullableUShort", typeof(ushort?), true},
                                                                            new object[] {"SByte", typeof(sbyte), true},
                                                                            new object[] {"Short", typeof(short), true},
                                                                            new object[] {"String", typeof(string), true},
                                                                            new object[] {"TimeSpan", typeof(TimeSpan), true},
                                                                            new object[] {"Type", typeof(Type), true},
                                                                            new object[] {"UInt", typeof(uint), true},
                                                                            new object[] {"ULong", typeof(ulong), true},
                                                                            new object[] {"Uri", typeof(Uri), true},
                                                                            new object[] {"UShort", typeof(ushort), true},
                                                                            new object[] {"Class", typeof(Rectangle), false},
                                                                            new object[] {"Interface", typeof(IShape), false},
                                                                            new object[] {"OpenGeneric", typeof(ICollection<>), false},
                                                                            new object[] {"ClosedGeneric", typeof(ICollection<string>), false},
                                                                            new object[] {"Object", typeof(object), false},
                                                                        };

        public static readonly IEnumerable<object[]> IsSubclassOrImplementationOfTestData = new[]
                                                                                            {
                                                                                                new object[] {"NegativeCaseWithGenericsWithDerivedAndInterface", typeof(Host<>), typeof(ICollection<>), false},
                                                                                                new object[] {"NegativeCaseWithGenericsWithDerivedAndAbstract", typeof(Host<>), typeof(AbstractCollection<>), false},
                                                                                                new object[] {"NegativeCaseWithGenericsWithAbstractAndInterface", typeof(AbstractHost<>), typeof(ICollection<>), false},

                                                                                                new object[] {"NegativeCaseWithNonGenericsWithDerivedAndInterface", typeof(Rectangle), typeof(IAnimal), false},
                                                                                                new object[] {"NegativeCaseWithNonGenericsWithDerivedAndAbstract", typeof(Rectangle), typeof(AbstractAnimal), false},
                                                                                                new object[] {"NegativeCaseWithNonGenericsWithAbstractAndInterface", typeof(AbstractShape), typeof(IAnimal), false},

                                                                                                new object[] {"PositiveCaseWithNonGenericsWithDerivedAndInterface", typeof(Rectangle), typeof(IShape), true},
                                                                                                new object[] {"PositiveCaseWithNonGenericsWithDerivedAndAbstract", typeof(Rectangle), typeof(AbstractShape), true},
                                                                                                new object[] {"PositiveCaseWithNonGenericsWithAbstractAndInterface", typeof(AbstractShape), typeof(IShape), true},

                                                                                                new object[] {"PositiveCaseWithGenericsWithDerivedAndInterface", typeof(Host<>), typeof(IHost<>), true},
                                                                                                new object[] {"PositiveCaseWithGenericsWithDerivedAndAbstract", typeof(Host<>), typeof(AbstractHost<>), true},
                                                                                                new object[] {"PositiveCaseWithGenericsWithAbstractAndInterface", typeof(AbstractHost<>), typeof(IHost<>), true}
                                                                                            };
        // ReSharper restore MemberCanBePrivate.Global
        #endregion

        #region Test Types
        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedMember.Global
        private enum PrimaryColor
        {
            Red,
            Green,
            Blue
        };

        private interface IAnimal
        {
            void Move();
        }

        private interface IShape
        {
            void Draw();
        }

        private abstract class AbstractAnimal : IAnimal
        {
            public abstract void Move();
        }

        private abstract class AbstractShape : IShape
        {
            public abstract void Draw();
        }

        private class Rectangle : AbstractShape
        {
            public override void Draw()
            {
            }
        }

        private class Square : Rectangle
        {
            public override void Draw()
            {
            }
        }

        private interface ICollection<out T>
        {
            T[] Items { get; }
        }

        private interface IHost<out T>
        {
            T HostedObject { get; }
        }

        private abstract class AbstractCollection<T> : ICollection<T>
        {
            public abstract T[] Items { get; }
        }

        private class Collection<T> : AbstractCollection<T>
        {
            public override T[] Items { get; }
        }

        private class DecoratedCollection<T> : Collection<T>
        {
            public int Count { get; }
        }

        private abstract class AbstractHost<T> : IHost<T>
        {
            public abstract T HostedObject { get; }
        }

        private class Host<T> : AbstractHost<T>
        {
            public Host(T hostedObject)
            {
                this._hostedObject = hostedObject;
            }

            private readonly T _hostedObject;

            public override T HostedObject
            {
                get { return this._hostedObject; }
            }
        }

        private class ClassWithMethodsBase
        {
            public string PublicMethodBase()
            {
                return String.Empty;
            }

            public string PublicMethodBase(int x)
            {
                return String.Empty;
            }

            public string PublicMethodBase(int x, string y)
            {
                return String.Empty;
            }

            public static string PublicStaticMethodBase()
            {
                return String.Empty;
            }

            public static string PublicStaticMethodBase(int x)
            {
                return String.Empty;
            }

            public static string PublicStaticMethodBase(int x, string y)
            {
                return String.Empty;
            }

            protected string ProtectedMethodBase()
            {
                return String.Empty;
            }

            protected string ProtectedMethodBase(int x)
            {
                return String.Empty;
            }

            protected string ProtectedMethodBase(int x, string y)
            {
                return String.Empty;
            }

            protected static string ProtectedStaticMethodBase()
            {
                return String.Empty;
            }

            protected static string ProtectedStaticMethodBase(int x)
            {
                return String.Empty;
            }

            protected static string ProtectedStaticMethodBase(int x, string y)
            {
                return String.Empty;
            }
        }

        private class ClassWithConstructors
        {
            public ClassWithConstructors()
            {
            }

            public ClassWithConstructors(int x)
            {
            }

            public ClassWithConstructors(int x, string y)
            {
            }

            protected ClassWithConstructors(int x, string y, bool z)
            {
            }
        }

        private class ClassWithMethods : ClassWithMethodsBase
        {
            public string PublicMethod()
            {
                return String.Empty;
            }

            public string PublicMethod(int x)
            {
                return String.Empty;
            }

            public string PublicMethod(int x, string y)
            {
                return String.Empty;
            }

            public static string PublicStaticMethod()
            {
                return String.Empty;
            }

            public static string PublicStaticMethod(int x)
            {
                return String.Empty;
            }

            public static string PublicStaticMethod(int x, string y)
            {
                return String.Empty;
            }

            private string PrivateMethod()
            {
                return String.Empty;
            }

            private string PrivateMethod(int x)
            {
                return String.Empty;
            }

            private string PrivateMethod(int x, string y)
            {
                return String.Empty;
            }

            private static string PrivateStaticMethod()
            {
                return String.Empty;
            }

            private static string PrivateStaticMethod(int x)
            {
                return String.Empty;
            }

            private static string PrivateStaticMethod(int x, string y)
            {
                return String.Empty;
            }
        }

        private class ClassWithPropertiesBase
        {
            public        string PublicPropertyBase       { get; set; }
            public static string PublicStaticPropertyBase { get; set; }

            protected internal        string ProtectedInternalPropertyBase       { get; set; }
            protected internal static string ProtectedInternalStaticPropertyBase { get; set; }

            protected        string ProtectedPropertyBase       { get; set; }
            protected static string ProtectedStaticPropertyBase { get; set; }

            internal        string InternalPropertyBase       { get; set; }
            internal static string InternalStaticPropertyBase { get; set; }

            private        string PrivatePropertyBase       { get; set; }
            private static string PrivateStaticPropertyBase { get; set; }
        }

        private class ClassWithProperties : ClassWithPropertiesBase
        {
            public        string PublicProperty       { get; set; }
            public static string PublicStaticProperty { get; set; }

            protected internal        string ProtectedInternalProperty       { get; set; }
            protected internal static string ProtectedInternalStaticProperty { get; set; }

            protected        string ProtectedProperty       { get; set; }
            protected static string ProtectedStaticProperty { get; set; }

            internal        string InternalProperty       { get; set; }
            internal static string InternalStaticProperty { get; set; }

            private        string PrivateProperty       { get; set; }
            private static string PrivateStaticProperty { get; set; }
        }

        private class ClassWithFieldsBase
        {
            public        string PublicFieldBase;
            public static string PublicStaticFieldBase;

            protected internal        string ProtectedInternalFieldBase;
            protected internal static string ProtectedInternalStaticFieldBase;

            protected        string ProtectedFieldBase;
            protected static string ProtectedStaticFieldBase;

            internal        string InternalFieldBase;
            internal static string InternalStaticFieldBase;

            // ReSharper disable InconsistentNaming
            private        string PrivateFieldBase;
            private static string PrivateStaticFieldBase;
            // ReSharper restore InconsistentNaming
        }

        private class ClassWithFields : ClassWithFieldsBase
        {
            public        string PublicField;
            public static string PublicStaticField;

            protected internal        string ProtectedInternalField;
            protected internal static string ProtectedInternalStaticField;

            protected        string ProtectedField;
            protected static string ProtectedStaticField;

            internal        string InternalField;
            internal static string InternalStaticField;

            // ReSharper disable InconsistentNaming
            private        string PrivateField;
            private static string PrivateStaticField;
            // ReSharper restore InconsistentNaming
        }
        // ReSharper restore UnusedMember.Global
        // ReSharper restore UnusedMember.Local
        #endregion
    }
}
