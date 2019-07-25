// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using ApiFramework.Schema.Internal;
using ApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Schema
{
    public class ApiSchemaTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSchemaTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(TryGetApiNamedTypeWithApiTypeTestData))]
        public void TestTryGetApiNamedTypeWithApiType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiNamedTypeWithClrTypeTestData))]
        public void TestTryGetApiNamedTypeWithClrType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiEnumerationTypeWithApiTypeTestData))]
        public void TestTryGetApiEnumerationTypeWithApiType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiEnumerationTypeWithClrTypeTestData))]
        public void TestTryGetApiEnumerationTypeWithClrType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiObjectTypeWithApiTypeTestData))]
        public void TestTryGetApiObjectTypeWithApiType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiObjectTypeWithClrTypeTestData))]
        public void TestTryGetApiObjectTypeWithClrType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiScalarTypeWithApiTypeTestData))]
        public void TestTryGetApiScalarTypeWithApiType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(TryGetApiScalarTypeWithClrTypeTestData))]
        public void TestTryGetApiScalarTypeWithClrType(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IApiEnumerationType CountryApiEnumerationType = ApiTypeFactory<Country>.CreateApiEnumerationType(
            "country",
            null,
            new[]
            {
                new ApiEnumerationValue("us", "The country of United States.", nameof(Country.UnitedStates), (int)Country.UnitedStates),
                new ApiEnumerationValue("gb", "The country of England.",       nameof(Country.England),      (int)Country.England),
                new ApiEnumerationValue("ie", "The country of Ireland.",       nameof(Country.Ireland),      (int)Country.Ireland),
            });

        private static readonly IApiScalarType GuidApiScalarType   = ApiTypeFactory<Guid>.CreateApiScalarType("guid", "Standard .NET GUID.");
        private static readonly IApiScalarType IntApiScalarType    = ApiTypeFactory<int>.CreateApiScalarType("int", "Standard .NET 32-bit integer.");
        private static readonly IApiScalarType StringApiScalarType = ApiTypeFactory<string>.CreateApiScalarType("string", "Standard .NET string.");

        private static readonly IApiObjectType MailingAddressApiObjectType = ApiTypeFactory<MailingAddress>.CreateApiObjectType(
            "mailing-addresses",
            "USA mailing address.",
            new[]
            {
                ApiTypeFactory<MailingAddress>.CreateApiProperty("street-address", StringApiScalarType,       ApiTypeModifiers.Required, x => x.StreetAddress),
                ApiTypeFactory<MailingAddress>.CreateApiProperty("apt-number",     StringApiScalarType,       ApiTypeModifiers.None,     x => x.AptNumber),
                ApiTypeFactory<MailingAddress>.CreateApiProperty("city",           StringApiScalarType,       ApiTypeModifiers.Required, x => x.City),
                ApiTypeFactory<MailingAddress>.CreateApiProperty("state",          StringApiScalarType,       ApiTypeModifiers.Required, x => x.State),
                ApiTypeFactory<MailingAddress>.CreateApiProperty("zip-code",       StringApiScalarType,       ApiTypeModifiers.Required, x => x.ZipCode),
                ApiTypeFactory<MailingAddress>.CreateApiProperty("country",        CountryApiEnumerationType, ApiTypeModifiers.Required, x => x.Country)
            },
            null,
            null);

        private static readonly IApiObjectType PhoneNumberApiObjectType = ApiTypeFactory<PhoneNumber>.CreateApiObjectType(
            "phone-numbers",
            "USA phone number.",
            new[]
            {
                ApiTypeFactory<PhoneNumber>.CreateApiProperty("prefix",    StringApiScalarType, ApiTypeModifiers.None,     x => x.Prefix),
                ApiTypeFactory<PhoneNumber>.CreateApiProperty("area-code", StringApiScalarType, ApiTypeModifiers.Required, x => x.AreaCode),
                ApiTypeFactory<PhoneNumber>.CreateApiProperty("number",    StringApiScalarType, ApiTypeModifiers.Required, x => x.Number)
            },
            null,
            null);

        private static readonly IApiProperty CommentCommentIdApiProperty = ApiTypeFactory<Comment>.CreateApiProperty("id", IntApiScalarType, ApiTypeModifiers.Required, x => x.CommentId);
        private static readonly IApiIdentity CommentApiIdentity          = ApiTypeFactory.CreateApiIdentity(CommentCommentIdApiProperty);

        private static readonly IApiObjectType CommentApiObjectType = ApiTypeFactory<Comment>.CreateApiObjectType(
            "comments",
            "A comment authored by a person on a subject.",
            new[]
            {
                CommentCommentIdApiProperty,
                ApiTypeFactory<Comment>.CreateApiProperty("text", StringApiScalarType, ApiTypeModifiers.None, x => x.Text)
            },
            CommentApiIdentity,
            null);

        private static readonly IApiProperty       PersonPersonIdApiProperty           = ApiTypeFactory<Person>.CreateApiProperty("id",              IntApiScalarType,            ApiTypeModifiers.Required, x => x.PersonId);
        private static readonly IApiProperty       PersonMailingAddressApiProperty     = ApiTypeFactory<Person>.CreateApiProperty("mailing-address", MailingAddressApiObjectType, ApiTypeModifiers.None,     x => x.MailingAddress);
        private static readonly IApiCollectionType PersonPhoneNumbersApiCollectionType = ApiTypeFactory.CreateApiCollectionType(PhoneNumberApiObjectType, ApiTypeModifiers.Required);
        private static readonly IApiProperty       PersonPhoneNumbersApiProperty       = ApiTypeFactory<Person>.CreateApiProperty("phone-numbers", PersonPhoneNumbersApiCollectionType, ApiTypeModifiers.Required, x => x.PhoneNumbers);
        private static readonly IApiIdentity       PersonApiIdentity                   = ApiTypeFactory.CreateApiIdentity(PersonPersonIdApiProperty);

        private static readonly IApiObjectType PersonApiObjectType = ApiTypeFactory<Person>.CreateApiObjectType(
            "people",
            "A human being.",
            new[]
            {
                PersonPersonIdApiProperty,
                ApiTypeFactory<Person>.CreateApiProperty("first-name", StringApiScalarType, ApiTypeModifiers.Required, x => x.FirstName),
                ApiTypeFactory<Person>.CreateApiProperty("last-name",  StringApiScalarType, ApiTypeModifiers.Required, x => x.LastName),
                PersonMailingAddressApiProperty,
                PersonPhoneNumbersApiProperty
            },
            PersonApiIdentity,
            null);

        private static readonly IApiProperty       ArticleArticleIdApiProperty      = ApiTypeFactory<Article>.CreateApiProperty("id",     GuidApiScalarType,   ApiTypeModifiers.Required, x => x.ArticleId);
        private static readonly IApiProperty       ArticleAuthorApiProperty         = ApiTypeFactory<Article>.CreateApiProperty("author", PersonApiObjectType, ApiTypeModifiers.Required, x => x.Author);
        private static readonly IApiCollectionType ArticleCommentsApiCollectionType = ApiTypeFactory.CreateApiCollectionType(CommentApiObjectType, ApiTypeModifiers.Required);
        private static readonly IApiProperty       ArticleCommentsApiProperty       = ApiTypeFactory<Article>.CreateApiProperty("comments", ArticleCommentsApiCollectionType, ApiTypeModifiers.Required, x => x.Comments);
        private static readonly IApiIdentity       ArticleApiIdentity               = ApiTypeFactory.CreateApiIdentity(ArticleArticleIdApiProperty);
        private static readonly IApiRelationship   ArticleAuthorApiRelationship     = ApiTypeFactory.CreateApiRelationship(ArticleAuthorApiProperty,   ApiRelationshipCardinality.ToOne,  PersonApiObjectType);
        private static readonly IApiRelationship   ArticleCommentsApiRelationship   = ApiTypeFactory.CreateApiRelationship(ArticleCommentsApiProperty, ApiRelationshipCardinality.ToMany, CommentApiObjectType);

        private static readonly IApiRelationship[] ArticleApiRelationships =
        {
            ArticleAuthorApiRelationship,
            ArticleCommentsApiRelationship
        };

        private static readonly IApiObjectType ArticleApiObjectType = ApiTypeFactory<Article>.CreateApiObjectType(
            "articles",
            "An article that is part of a publication.",
            new[]
            {
                ArticleArticleIdApiProperty,
                ApiTypeFactory<Article>.CreateApiProperty("title", StringApiScalarType, ApiTypeModifiers.Required, x => x.Title),
                ApiTypeFactory<Article>.CreateApiProperty("body",  StringApiScalarType, ApiTypeModifiers.Required, x => x.Body),
                ArticleAuthorApiProperty,
                ArticleCommentsApiProperty
            },
            ArticleApiIdentity,
            ArticleApiRelationships);

        private static readonly IApiSchema ApiSchema = ApiTypeFactory.CreateApiSchema(
            "Test API Schema",
            new[]
            {
                CountryApiEnumerationType
            },
            new[]
            {
                MailingAddressApiObjectType,
                PhoneNumberApiObjectType,
                ArticleApiObjectType,
                PersonApiObjectType,
                CommentApiObjectType
            },
            new[]
            {
                GuidApiScalarType,
                IntApiScalarType,
                StringApiScalarType
            });

        public static readonly IEnumerable<object[]> TryGetApiNamedTypeWithApiTypeTestData =
            new[]
            {
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeCountry",        ApiSchema, "country",           CountryApiEnumerationType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithUnknownTypeColors",       ApiSchema, "colors",            null)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeMailingAddress", ApiSchema, "mailing-addresses", MailingAddressApiObjectType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypePhoneNumber",    ApiSchema, "phone-numbers",     PhoneNumberApiObjectType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeArticle",        ApiSchema, "articles",          ArticleApiObjectType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypePerson",         ApiSchema, "people",            PersonApiObjectType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeComment",        ApiSchema, "comments",          CommentApiObjectType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithUnknownTypePoint",        ApiSchema, "points",            null)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeGuid",           ApiSchema, "guid",              GuidApiScalarType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeInt",            ApiSchema, "int",               IntApiScalarType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithKnownTypeString",         ApiSchema, "string",            StringApiScalarType)},
                new object[] {new TryGetApiNamedTypeWithApiTypeUnitTest("WithUnknownTypeLong",         ApiSchema, "long",              null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiNamedTypeWithClrTypeTestData =
            new[]
            {
                new object[] {new TryGetApiNamedTypeWithClrTypeUnitTest<Country>("WithKnownTypeCountry", ApiSchema, CountryApiEnumerationType)},
                new object[] {new TryGetApiNamedTypeWithClrTypeUnitTest<Colors>("WithUnknownTypeColors", ApiSchema, null)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<MailingAddress>("WithKnownTypeMailingAddress", ApiSchema, MailingAddressApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<PhoneNumber>("WithKnownTypePhoneNumber", ApiSchema, PhoneNumberApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Article>("WithKnownTypeArticle", ApiSchema, ArticleApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Person>("WithKnownTypePerson", ApiSchema, PersonApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Comment>("WithKnownTypeComment", ApiSchema, CommentApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Point>("WithUnknownTypePoint", ApiSchema, null)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<Guid>("WithKnownTypeGuid", ApiSchema, GuidApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<int>("WithKnownTypeInt", ApiSchema, IntApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<string>("WithKnownTypeString", ApiSchema, StringApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<long>("WithUnknownTypeLong", ApiSchema, null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiEnumerationTypeWithApiTypeTestData =
            new[]
            {
                new object[] {new TryGetApiEnumerationTypeWithApiTypeUnitTest("WithKnownTypeCountry",  ApiSchema, "country", CountryApiEnumerationType)},
                new object[] {new TryGetApiEnumerationTypeWithApiTypeUnitTest("WithUnknownTypeColors", ApiSchema, "colors",  null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiEnumerationTypeWithClrTypeTestData =
            new[]
            {
                new object[] {new TryGetApiEnumerationTypeWithClrTypeUnitTest<Country>("WithKnownTypeCountry", ApiSchema, CountryApiEnumerationType)},
                new object[] {new TryGetApiEnumerationTypeWithClrTypeUnitTest<Colors>("WithUnknownTypeColors", ApiSchema, null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiObjectTypeWithApiTypeTestData =
            new[]
            {
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithKnownTypeMailingAddress", ApiSchema, "mailing-addresses", MailingAddressApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithKnownTypePhoneNumber",    ApiSchema, "phone-numbers",     PhoneNumberApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithKnownTypeArticle",        ApiSchema, "articles",          ArticleApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithKnownTypePerson",         ApiSchema, "people",            PersonApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithKnownTypeComment",        ApiSchema, "comments",          CommentApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithApiTypeUnitTest("WithUnknownTypePoint",        ApiSchema, "points",            null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiObjectTypeWithClrTypeTestData =
            new[]
            {
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<MailingAddress>("WithKnownTypeMailingAddress", ApiSchema, MailingAddressApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<PhoneNumber>("WithKnownTypePhoneNumber", ApiSchema, PhoneNumberApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Article>("WithKnownTypeArticle", ApiSchema, ArticleApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Person>("WithKnownTypePerson", ApiSchema, PersonApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Comment>("WithKnownTypeComment", ApiSchema, CommentApiObjectType)},
                new object[] {new TryGetApiObjectTypeWithClrTypeUnitTest<Point>("WithUnknownTypePoint", ApiSchema, null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiScalarTypeWithApiTypeTestData =
            new[]
            {
                new object[] {new TryGetApiScalarTypeWithApiTypeUnitTest("WithKnownTypeGuid",   ApiSchema, "guid",   GuidApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithApiTypeUnitTest("WithKnownTypeInt",    ApiSchema, "int",    IntApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithApiTypeUnitTest("WithKnownTypeString", ApiSchema, "string", StringApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithApiTypeUnitTest("WithUnknownTypeLong", ApiSchema, "long",   null)},
            };

        public static readonly IEnumerable<object[]> TryGetApiScalarTypeWithClrTypeTestData =
            new[]
            {
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<Guid>("WithKnownTypeGuid", ApiSchema, GuidApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<int>("WithKnownTypeInt", ApiSchema, IntApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<string>("WithKnownTypeString", ApiSchema, StringApiScalarType)},
                new object[] {new TryGetApiScalarTypeWithClrTypeUnitTest<long>("WithUnknownTypeLong", ApiSchema, null)},
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region CLR Object Types
        private enum Country
        {
            UnitedStates,
            England,
            Ireland
        }

        private enum Colors
        {
            Green,
            Yellow,
            Red
        }

        private class MailingAddress
        {
            // ReSharper disable UnusedMember.Local
            public string StreetAddress { get; set; }
            public string AptNumber     { get; set; }
            public string City          { get; set; }
            public string State         { get; set; }
            public string ZipCode       { get; set; }

            public Country Country { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class PhoneNumber
        {
            // ReSharper disable UnusedMember.Local
            public string Prefix   { get; set; }
            public string AreaCode { get; set; }

            public string Number { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Point
        {
            // ReSharper disable UnusedMember.Local
            public int X { get; set; }

            public int Y { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Article
        {
            // ReSharper disable UnusedMember.Local
            public Guid   ArticleId { get; set; }
            public string Title     { get; set; }
            public string Body      { get; set; }

            public Person Author { get; set; }

            public List<Comment> Comments { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Person
        {
            // ReSharper disable UnusedMember.Local
            public int    PersonId  { get; set; }
            public string FirstName { get; set; }
            public string LastName  { get; set; }

            public MailingAddress MailingAddress { get; set; }

            public List<PhoneNumber> PhoneNumbers { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Comment
        {
            // ReSharper disable UnusedMember.Local
            public int CommentId { get; set; }

            public string Text { get; set; }
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region Unit Tests
        private class TryGetApiNamedTypeWithApiTypeUnitTest : XUnitTest
        {
            #region Constructors
            public TryGetApiNamedTypeWithApiTypeUnitTest(string name, IApiSchema apiSchema, string apiType, IApiNamedType expectedApiNamedType)
                : base(name)
            {
                this.ApiSchema            = apiSchema;
                this.ApiType              = apiType;
                this.ExpectedApiNamedType = expectedApiNamedType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiNamedType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var apiTypeName = !String.IsNullOrWhiteSpace(this.ApiType) ? this.ApiType : "null";
                this.WriteLine("API Type Name    : {0}", apiTypeName);
                this.WriteLine();

                var expectedApiNamedTypeDescription = this.ExpectedApiNamedType != null ? this.ExpectedApiNamedType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success        : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Named Type : {0}", expectedApiNamedTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiNamedType(this.ApiType, out var actualApiNamedType);

                this.ActualSuccess      = actualSuccess;
                this.ActualApiNamedType = actualApiNamedType;

                var actualApiNamedTypeDescription = this.ActualApiNamedType != null ? this.ActualApiNamedType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success        : {0}", this.ActualSuccess);
                this.WriteLine("  API Named Type : {0}", actualApiNamedTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiNamedType.Should().BeEquivalentTo(this.ExpectedApiNamedType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema    ApiSchema            { get; }
            private string        ApiType              { get; }
            private IApiNamedType ExpectedApiNamedType { get; }
            #endregion

            #region Calculated Properties
            private bool          ExpectedSuccess    { get; set; }
            private bool          ActualSuccess      { get; set; }
            private IApiNamedType ActualApiNamedType { get; set; }
            #endregion
        }

        private class TryGetApiNamedTypeWithClrTypeUnitTest<T> : XUnitTest
        {
            #region Constructors
            public TryGetApiNamedTypeWithClrTypeUnitTest(string name, IApiSchema apiSchema, IApiNamedType expectedApiNamedType)
                : base(name)
            {
                this.ApiSchema            = apiSchema;
                this.ExpectedApiNamedType = expectedApiNamedType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiNamedType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var clrTypeName = typeof(T).Name;
                this.WriteLine("CLR Type         : {0}", clrTypeName);
                this.WriteLine();

                var expectedApiNamedTypeDescription = this.ExpectedApiNamedType != null ? this.ExpectedApiNamedType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success        : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Named Type : {0}", expectedApiNamedTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiNamedType<T>(out var actualApiNamedType);

                this.ActualSuccess      = actualSuccess;
                this.ActualApiNamedType = actualApiNamedType;

                var actualApiNamedTypeDescription = this.ActualApiNamedType != null ? this.ActualApiNamedType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success        : {0}", this.ActualSuccess);
                this.WriteLine("  API Named Type : {0}", actualApiNamedTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiNamedType.Should().BeEquivalentTo(this.ExpectedApiNamedType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema    ApiSchema            { get; }
            private IApiNamedType ExpectedApiNamedType { get; }
            #endregion

            #region Calculated Properties
            private bool          ExpectedSuccess    { get; set; }
            private bool          ActualSuccess      { get; set; }
            private IApiNamedType ActualApiNamedType { get; set; }
            #endregion
        }

        private class TryGetApiEnumerationTypeWithApiTypeUnitTest : XUnitTest
        {
            #region Constructors
            public TryGetApiEnumerationTypeWithApiTypeUnitTest(string name, IApiSchema apiSchema, string apiType, IApiEnumerationType expectedApiEnumerationType)
                : base(name)
            {
                this.ApiSchema                  = apiSchema;
                this.ApiType                    = apiType;
                this.ExpectedApiEnumerationType = expectedApiEnumerationType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiEnumerationType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var apiTypeName = !String.IsNullOrWhiteSpace(this.ApiType) ? this.ApiType : "null";
                this.WriteLine("API Type Name   : {0}", apiTypeName);
                this.WriteLine();

                var expectedApiEnumerationTypeDescription = this.ExpectedApiEnumerationType != null ? this.ExpectedApiEnumerationType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success       : {0}",        this.ExpectedSuccess);
                this.WriteLine("  API Enumeration Type : {0}", expectedApiEnumerationTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiEnumerationType(this.ApiType, out var actualApiEnumerationType);

                this.ActualSuccess            = actualSuccess;
                this.ActualApiEnumerationType = actualApiEnumerationType;

                var actualApiEnumerationTypeDescription = this.ActualApiEnumerationType != null ? this.ActualApiEnumerationType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success       : {0}",        this.ActualSuccess);
                this.WriteLine("  API Enumeration Type : {0}", actualApiEnumerationTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiEnumerationType.Should().BeEquivalentTo(this.ExpectedApiEnumerationType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema          ApiSchema                  { get; }
            private string              ApiType                    { get; }
            private IApiEnumerationType ExpectedApiEnumerationType { get; }
            #endregion

            #region Calculated Properties
            private bool                ExpectedSuccess          { get; set; }
            private bool                ActualSuccess            { get; set; }
            private IApiEnumerationType ActualApiEnumerationType { get; set; }
            #endregion
        }

        private class TryGetApiEnumerationTypeWithClrTypeUnitTest<TEnumeration> : XUnitTest
        {
            #region Constructors
            public TryGetApiEnumerationTypeWithClrTypeUnitTest(string name, IApiSchema apiSchema, IApiEnumerationType expectedApiEnumerationType)
                : base(name)
            {
                this.ApiSchema                  = apiSchema;
                this.ExpectedApiEnumerationType = expectedApiEnumerationType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiEnumerationType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var clrTypeName = typeof(TEnumeration).Name;
                this.WriteLine("CLR Type        : {0}", clrTypeName);
                this.WriteLine();

                var expectedApiEnumerationTypeDescription = this.ExpectedApiEnumerationType != null ? this.ExpectedApiEnumerationType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success       : {0}",        this.ExpectedSuccess);
                this.WriteLine("  API Enumeration Type : {0}", expectedApiEnumerationTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiEnumerationType<TEnumeration>(out var actualApiEnumerationType);

                this.ActualSuccess            = actualSuccess;
                this.ActualApiEnumerationType = actualApiEnumerationType;

                var actualApiEnumerationTypeDescription = this.ActualApiEnumerationType != null ? this.ActualApiEnumerationType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success       : {0}",        this.ActualSuccess);
                this.WriteLine("  API Enumeration Type : {0}", actualApiEnumerationTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiEnumerationType.Should().BeEquivalentTo(this.ExpectedApiEnumerationType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema          ApiSchema                  { get; }
            private IApiEnumerationType ExpectedApiEnumerationType { get; }
            #endregion

            #region Calculated Properties
            private bool                ExpectedSuccess          { get; set; }
            private bool                ActualSuccess            { get; set; }
            private IApiEnumerationType ActualApiEnumerationType { get; set; }
            #endregion
        }

        private class TryGetApiObjectTypeWithApiTypeUnitTest : XUnitTest
        {
            #region Constructors
            public TryGetApiObjectTypeWithApiTypeUnitTest(string name, IApiSchema apiSchema, string apiType, IApiObjectType expectedApiObjectType)
                : base(name)
            {
                this.ApiSchema             = apiSchema;
                this.ApiType               = apiType;
                this.ExpectedApiObjectType = expectedApiObjectType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiObjectType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var apiTypeName = !String.IsNullOrWhiteSpace(this.ApiType) ? this.ApiType : "null";
                this.WriteLine("API Type Name     : {0}", apiTypeName);
                this.WriteLine();

                var expectedApiObjectTypeDescription = this.ExpectedApiObjectType != null ? this.ExpectedApiObjectType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success         : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Object Type : {0}", expectedApiObjectTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiObjectType(this.ApiType, out var actualApiObjectType);

                this.ActualSuccess       = actualSuccess;
                this.ActualApiObjectType = actualApiObjectType;

                var actualApiObjectTypeDescription = this.ActualApiObjectType != null ? this.ActualApiObjectType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success         : {0}", this.ActualSuccess);
                this.WriteLine("  API Object Type : {0}", actualApiObjectTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiObjectType.Should().BeEquivalentTo(this.ExpectedApiObjectType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema     ApiSchema             { get; }
            private string         ApiType               { get; }
            private IApiObjectType ExpectedApiObjectType { get; }
            #endregion

            #region Calculated Properties
            private bool           ExpectedSuccess     { get; set; }
            private bool           ActualSuccess       { get; set; }
            private IApiObjectType ActualApiObjectType { get; set; }
            #endregion
        }

        private class TryGetApiObjectTypeWithClrTypeUnitTest<TObject> : XUnitTest
        {
            #region Constructors
            public TryGetApiObjectTypeWithClrTypeUnitTest(string name, IApiSchema apiSchema, IApiObjectType expectedApiObjectType)
                : base(name)
            {
                this.ApiSchema             = apiSchema;
                this.ExpectedApiObjectType = expectedApiObjectType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiObjectType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var clrTypeName = typeof(TObject).Name;
                this.WriteLine("CLR Type          : {0}", clrTypeName);
                this.WriteLine();

                var expectedApiObjectTypeDescription = this.ExpectedApiObjectType != null ? this.ExpectedApiObjectType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success         : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Object Type : {0}", expectedApiObjectTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiObjectType<TObject>(out var actualApiObjectType);

                this.ActualSuccess       = actualSuccess;
                this.ActualApiObjectType = actualApiObjectType;

                var actualApiObjectTypeDescription = this.ActualApiObjectType != null ? this.ActualApiObjectType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success         : {0}", this.ActualSuccess);
                this.WriteLine("  API Object Type : {0}", actualApiObjectTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiObjectType.Should().BeEquivalentTo(this.ExpectedApiObjectType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema     ApiSchema             { get; }
            private IApiObjectType ExpectedApiObjectType { get; }
            #endregion

            #region Calculated Properties
            private bool           ExpectedSuccess     { get; set; }
            private bool           ActualSuccess       { get; set; }
            private IApiObjectType ActualApiObjectType { get; set; }
            #endregion
        }

        private class TryGetApiScalarTypeWithApiTypeUnitTest : XUnitTest
        {
            #region Constructors
            public TryGetApiScalarTypeWithApiTypeUnitTest(string name, IApiSchema apiSchema, string apiType, IApiScalarType expectedApiScalarType)
                : base(name)
            {
                this.ApiSchema             = apiSchema;
                this.ApiType               = apiType;
                this.ExpectedApiScalarType = expectedApiScalarType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiScalarType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var apiTypeName = !String.IsNullOrWhiteSpace(this.ApiType) ? this.ApiType : "null";
                this.WriteLine("API Type Name     : {0}", apiTypeName);
                this.WriteLine();

                var expectedApiScalarTypeDescription = this.ExpectedApiScalarType != null ? this.ExpectedApiScalarType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success         : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Scalar Type : {0}", expectedApiScalarTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiScalarType(this.ApiType, out var actualApiScalarType);

                this.ActualSuccess       = actualSuccess;
                this.ActualApiScalarType = actualApiScalarType;

                var actualApiScalarTypeDescription = this.ActualApiScalarType != null ? this.ActualApiScalarType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success         : {0}", this.ActualSuccess);
                this.WriteLine("  API Scalar Type : {0}", actualApiScalarTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiScalarType.Should().BeEquivalentTo(this.ExpectedApiScalarType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema     ApiSchema             { get; }
            private string         ApiType               { get; }
            private IApiScalarType ExpectedApiScalarType { get; }
            #endregion

            #region Calculated Properties
            private bool           ExpectedSuccess     { get; set; }
            private bool           ActualSuccess       { get; set; }
            private IApiScalarType ActualApiScalarType { get; set; }
            #endregion
        }

        private class TryGetApiScalarTypeWithClrTypeUnitTest<TScalar> : XUnitTest
        {
            #region Constructors
            public TryGetApiScalarTypeWithClrTypeUnitTest(string name, IApiSchema apiSchema, IApiScalarType expectedApiScalarType)
                : base(name)
            {
                this.ApiSchema             = apiSchema;
                this.ExpectedApiScalarType = expectedApiScalarType;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.ExpectedSuccess = this.ExpectedApiScalarType != null;

                this.WriteLine(this.ApiSchema.ToTreeString());
                this.WriteLine();

                var clrTypeName = typeof(TScalar).Name;
                this.WriteLine("CLR Type          : {0}", clrTypeName);
                this.WriteLine();

                var expectedApiScalarTypeDescription = this.ExpectedApiScalarType != null ? this.ExpectedApiScalarType.ToString() : "null";
                this.WriteLine("Expected");
                this.WriteLine("  Success         : {0}", this.ExpectedSuccess);
                this.WriteLine("  API Scalar Type : {0}", expectedApiScalarTypeDescription);

                this.WriteLine();
            }

            protected override void Act()
            {
                var actualSuccess = this.ApiSchema.TryGetApiScalarType<TScalar>(out var actualApiScalarType);

                this.ActualSuccess       = actualSuccess;
                this.ActualApiScalarType = actualApiScalarType;

                var actualApiScalarTypeDescription = this.ActualApiScalarType != null ? this.ActualApiScalarType.ToString() : "null";
                this.WriteLine("Actual");
                this.WriteLine("  Success         : {0}", this.ActualSuccess);
                this.WriteLine("  API Scalar Type : {0}", actualApiScalarTypeDescription);
            }

            protected override void Assert()
            {
                this.ActualSuccess.Should().Be(this.ExpectedSuccess);
                this.ActualApiScalarType.Should().BeEquivalentTo(this.ExpectedApiScalarType, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private IApiSchema     ApiSchema             { get; }
            private IApiScalarType ExpectedApiScalarType { get; }
            #endregion

            #region Calculated Properties
            private bool           ExpectedSuccess     { get; set; }
            private bool           ActualSuccess       { get; set; }
            private IApiScalarType ActualApiScalarType { get; set; }
            #endregion
        }
        #endregion
    }
}