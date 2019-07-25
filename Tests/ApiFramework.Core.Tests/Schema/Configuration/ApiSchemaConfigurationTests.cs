// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using ApiFramework.Schema.Annotations;
using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Conventions.Internal;
using ApiFramework.Schema.Internal;
using ApiFramework.XUnit;

using FluentAssertions;

using LogFramework;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Schema.Configuration
{
    public class ApiSchemaConfigurationTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSchemaConfigurationTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateApiSchemaWithAnnotationsTestData))]
        public void TestCreateApiSchemaWithAnnotations(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(CreateApiSchemaWithTypeConfigurationsTestData))]
        public void TestCreateApiSchemaWithTypeConfigurations(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(CreateApiSchemaWithFluentApiTestData))]
        public void TestCreateApiSchemaWithFluentApi(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PUBLIC TYPES /////////////////////////////////////////////////////
        #region CLR Object Types
        [ApiEnumerationType(Name = "country")]
        public enum Country
        {
            [ApiEnumerationValue(Name = "united-states", Description = "The country of United States.")]
            UnitedStates = 24,

            [ApiEnumerationValue(Name = "england", Description = "The country of England.")]
            England = 42,

            [ApiEnumerationValue(Name = "ireland", Description = "The country of Ireland.")]
            Ireland = 68
        }

        [ApiObjectType(Name = "mailing-addresses", Description = "USA mailing address.")]
        public class MailingAddress
        {
            // ReSharper disable UnusedMember.Local
            [ApiProperty(Name = "street-address", Required = true)]
            public string StreetAddress { get; set; }

            [ApiProperty(Name = "apt-number")] public int? AptNumber { get; set; }

            [ApiProperty(Name = "city", Required = true)]
            public string City { get; set; }

            [ApiProperty(Name = "state", Required = true)]
            public string State { get; set; }

            [ApiProperty(Name = "zip-code", Required = true)]
            public string ZipCode { get; set; }

            [ApiProperty(Name = "country", Required = true)]
            public Country Country { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        [ApiObjectType(Name = "phone-numbers", Description = "USA phone number.")]
        public class PhoneNumber
        {
            // ReSharper disable UnusedMember.Local
            [ApiProperty(Name = "country")] public Country? Country { get; set; }

            [ApiProperty(Name = "prefix")] public string Prefix { get; set; }

            [ApiProperty(Name = "area-code", Required = true)]
            public string AreaCode { get; set; }

            [ApiProperty(Name = "number", Required = true)]
            public string Number { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        [ApiObjectType(Name = "articles", Description = "An article that is part of a publication.")]
        public class Article
        {
            // ReSharper disable UnusedMember.Local
            [ApiIdentity]
            [ApiProperty(Name = "id")]
            public Guid ArticleId { get; set; }

            [ApiProperty(Name = "title", Required = true)]
            public string Title { get; set; }

            [ApiProperty(Name = "body", Required = true)]
            public string Body { get; set; }

            [ApiRelationship]
            [ApiProperty(Name = "author", Required = true, Description = "Single author of the article.")]
            public Person Author { get; set; }

            [ApiRelationship]
            [ApiProperty(Name = "comments", Required = true, Description = "Many comments on the article.", ItemRequired = true)]
            public List<Comment> Comments { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        [ApiObjectType(Name = "people", Description = "A human being.")]
        public class Person
        {
            // ReSharper disable UnusedMember.Local
            [ApiIdentity]
            [ApiProperty(Name = "id")]
            public int PersonId { get; set; }

            [ApiProperty(Name = "first-name", Required = true)]
            public string FirstName { get; set; }

            [ApiProperty(Name = "last-name", Required = true)]
            public string LastName { get; set; }

            [ApiProperty(Name = "mailing-address")]
            public MailingAddress MailingAddress { get; set; }

            [ApiProperty(Name = "phone-numbers", Required = true, ItemRequired = true)]
            public List<PhoneNumber> PhoneNumbers { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        [ApiObjectType(Name = "comments", Description = "A comment authored by a person on a subject.")]
        public class Comment
        {
            // ReSharper disable UnusedMember.Local
            [ApiIdentity]
            [ApiProperty(Name = "id")]
            public int CommentId { get; set; }

            [ApiProperty(Name = "text")] public string Text { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        [ApiObjectType(Name = "value-collections", Description = "A collection of value and nullable value collections for testing purposes.")]
        public class ValueCollections
        {
            [ApiProperty(Name = "enums", Required = true, ItemRequired = true)]
            public List<Country> Enums { get; set; }

            [ApiProperty(Name = "nullable-enums")] public List<Country?> NullableEnums { get; set; }

            [ApiProperty(Name = "nullable-scalars")]
            public List<int?> NullableScalars { get; set; }

            [ApiProperty(Name = "scalars", Required = true, ItemRequired = true)]
            public List<int> Scalars { get; set; }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data

        #region Expected Schema
        private static readonly IApiEnumerationType CountryApiEnumerationType =
            ApiTypeFactory<Country>.CreateApiEnumerationType(
                "country",
                typeof(Country).CreateDefaultApiEnumerationTypeDescription(),
                new[]
                {
                    new ApiEnumerationValue("united-states", "The country of United States.", nameof(Country.UnitedStates), (int)Country.UnitedStates),
                    new ApiEnumerationValue("england",       "The country of England.",       nameof(Country.England),      (int)Country.England),
                    new ApiEnumerationValue("ireland",       "The country of Ireland.",       nameof(Country.Ireland),      (int)Country.Ireland),
                });

        private static readonly IApiScalarType GuidApiScalarType =
            ApiTypeFactory<Guid>.CreateApiScalarType("guid", typeof(Guid).CreateDefaultApiScalarTypeDescription());

        private static readonly IApiScalarType IntApiScalarType =
            ApiTypeFactory<int>.CreateApiScalarType("int", typeof(int).CreateDefaultApiScalarTypeDescription());

        private static readonly IApiScalarType StringApiScalarType =
            ApiTypeFactory<string>.CreateApiScalarType("string", typeof(string).CreateDefaultApiScalarTypeDescription());

        private static readonly IApiObjectType MailingAddressApiObjectType =
            ApiTypeFactory<MailingAddress>.CreateApiObjectType(
                "mailing-addresses",
                "USA mailing address.",
                new[]
                {
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("street-address", StringApiScalarType,       ApiTypeModifiers.Required, x => x.StreetAddress),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("apt-number",     IntApiScalarType,          ApiTypeModifiers.None,     x => x.AptNumber),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("city",           StringApiScalarType,       ApiTypeModifiers.Required, x => x.City),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("state",          StringApiScalarType,       ApiTypeModifiers.Required, x => x.State),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("zip-code",       StringApiScalarType,       ApiTypeModifiers.Required, x => x.ZipCode),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("country",        CountryApiEnumerationType, ApiTypeModifiers.Required, x => x.Country)
                },
                null,
                null);

        private static readonly IApiObjectType MailingAddressExcludingCountryApiObjectType =
            ApiTypeFactory<MailingAddress>.CreateApiObjectType(
                "mailing-addresses",
                "USA mailing address.",
                new[]
                {
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("street-address", StringApiScalarType, ApiTypeModifiers.Required, x => x.StreetAddress),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("apt-number",     IntApiScalarType,    ApiTypeModifiers.None,     x => x.AptNumber),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("city",           StringApiScalarType, ApiTypeModifiers.Required, x => x.City),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("state",          StringApiScalarType, ApiTypeModifiers.Required, x => x.State),
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("zip-code",       StringApiScalarType, ApiTypeModifiers.Required, x => x.ZipCode)
                },
                null,
                null);

        private static readonly IApiObjectType MailingAddressExcludingCountryAndStringsApiObjectType =
            ApiTypeFactory<MailingAddress>.CreateApiObjectType(
                "mailing-addresses",
                "USA mailing address.",
                new[]
                {
                    ApiTypeFactory<MailingAddress>.CreateApiProperty("apt-number", IntApiScalarType, ApiTypeModifiers.None, x => x.AptNumber),
                },
                null,
                null);

        private static readonly IApiObjectType PhoneNumberApiObjectType =
            ApiTypeFactory<PhoneNumber>.CreateApiObjectType(
                "phone-numbers",
                "USA phone number.",
                new[]
                {
                    ApiTypeFactory<PhoneNumber>.CreateApiProperty("country",   CountryApiEnumerationType, ApiTypeModifiers.None,     x => x.Country),
                    ApiTypeFactory<PhoneNumber>.CreateApiProperty("prefix",    StringApiScalarType,       ApiTypeModifiers.None,     x => x.Prefix),
                    ApiTypeFactory<PhoneNumber>.CreateApiProperty("area-code", StringApiScalarType,       ApiTypeModifiers.Required, x => x.AreaCode),
                    ApiTypeFactory<PhoneNumber>.CreateApiProperty("number",    StringApiScalarType,       ApiTypeModifiers.Required, x => x.Number)
                },
                null,
                null);

        private static readonly IApiProperty CommentCommentIdApiProperty =
            ApiTypeFactory<Comment>.CreateApiProperty("id", IntApiScalarType, ApiTypeModifiers.Required, x => x.CommentId);

        private static readonly IApiIdentity CommentApiIdentity =
            ApiTypeFactory.CreateApiIdentity(CommentCommentIdApiProperty);

        private static readonly IApiObjectType CommentApiObjectType =
            ApiTypeFactory<Comment>.CreateApiObjectType(
                "comments",
                "A comment authored by a person on a subject.",
                new[]
                {
                    CommentCommentIdApiProperty,
                    ApiTypeFactory<Comment>.CreateApiProperty("text", StringApiScalarType, ApiTypeModifiers.None, x => x.Text)
                },
                CommentApiIdentity,
                null);

        private static readonly IApiProperty PersonPersonIdApiProperty =
            ApiTypeFactory<Person>.CreateApiProperty("id", IntApiScalarType, ApiTypeModifiers.Required, x => x.PersonId);

        private static readonly IApiProperty PersonMailingAddressApiProperty =
            ApiTypeFactory<Person>.CreateApiProperty("mailing-address", MailingAddressApiObjectType, ApiTypeModifiers.None, x => x.MailingAddress);

        private static readonly IApiCollectionType PersonPhoneNumbersApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(PhoneNumberApiObjectType, ApiTypeModifiers.Required);

        private static readonly IApiProperty PersonPhoneNumbersApiProperty =
            ApiTypeFactory<Person>.CreateApiProperty("phone-numbers", PersonPhoneNumbersApiCollectionType, ApiTypeModifiers.Required, x => x.PhoneNumbers);

        private static readonly IApiIdentity PersonApiIdentity =
            ApiTypeFactory.CreateApiIdentity(PersonPersonIdApiProperty);

        private static readonly IApiObjectType PersonApiObjectType =
            ApiTypeFactory<Person>.CreateApiObjectType(
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

        private static readonly IApiObjectType PersonExcludingMailingAddressAndPhoneNumbersApiObjectType =
            ApiTypeFactory<Person>.CreateApiObjectType(
                "people",
                "A human being.",
                new[]
                {
                    PersonPersonIdApiProperty,
                    ApiTypeFactory<Person>.CreateApiProperty("first-name", StringApiScalarType, ApiTypeModifiers.Required, x => x.FirstName),
                    ApiTypeFactory<Person>.CreateApiProperty("last-name",  StringApiScalarType, ApiTypeModifiers.Required, x => x.LastName)
                },
                PersonApiIdentity,
                null);

        private static readonly IApiProperty ArticleArticleIdApiProperty =
            ApiTypeFactory<Article>.CreateApiProperty("id", GuidApiScalarType, ApiTypeModifiers.Required, x => x.ArticleId);

        private static readonly IApiProperty ArticleAuthorApiProperty =
            ApiTypeFactory<Article>.CreateApiProperty("author", "Single author of the article.", PersonApiObjectType, ApiTypeModifiers.Required, x => x.Author);

        private static readonly IApiCollectionType ArticleCommentsApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(CommentApiObjectType, ApiTypeModifiers.Required);

        private static readonly IApiProperty ArticleCommentsApiProperty =
            ApiTypeFactory<Article>.CreateApiProperty("comments", "Many comments on the article.", ArticleCommentsApiCollectionType, ApiTypeModifiers.Required,
                                                      x => x.Comments);

        private static readonly IApiIdentity ArticleApiIdentity =
            ApiTypeFactory.CreateApiIdentity(ArticleArticleIdApiProperty);

        private static readonly IApiRelationship ArticleAuthorApiRelationship =
            ApiTypeFactory.CreateApiRelationship(ArticleAuthorApiProperty, ApiRelationshipCardinality.ToOne, PersonApiObjectType);

        private static readonly IApiRelationship ArticleCommentsApiRelationship =
            ApiTypeFactory.CreateApiRelationship(ArticleCommentsApiProperty, ApiRelationshipCardinality.ToMany, CommentApiObjectType);

        private static readonly IApiRelationship[] ArticleApiRelationships =
        {
            ArticleAuthorApiRelationship,
            ArticleCommentsApiRelationship
        };

        private static readonly IApiObjectType ArticleApiObjectType =
            ApiTypeFactory<Article>.CreateApiObjectType(
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

        private static readonly IApiCollectionType ValueCollectionsEnumsApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(CountryApiEnumerationType, ApiTypeModifiers.Required);

        private static readonly IApiCollectionType ValueCollectionsNullableEnumsApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(CountryApiEnumerationType, ApiTypeModifiers.None);

        private static readonly IApiCollectionType ValueCollectionsNullableScalarsApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(IntApiScalarType, ApiTypeModifiers.None);

        private static readonly IApiCollectionType ValueCollectionsScalarsApiCollectionType =
            ApiTypeFactory.CreateApiCollectionType(IntApiScalarType, ApiTypeModifiers.Required);

        private static readonly IApiObjectType ValueCollectionsApiObjectType =
            ApiTypeFactory<ValueCollections>.CreateApiObjectType(
                "value-collections",
                "A collection of value and nullable value collections for testing purposes.",
                new[]
                {
                    ApiTypeFactory<ValueCollections>.CreateApiProperty("enums",            ValueCollectionsEnumsApiCollectionType,           ApiTypeModifiers.Required, x => x.Enums),
                    ApiTypeFactory<ValueCollections>.CreateApiProperty("nullable-enums",   ValueCollectionsNullableEnumsApiCollectionType,   ApiTypeModifiers.None,     x => x.NullableEnums),
                    ApiTypeFactory<ValueCollections>.CreateApiProperty("nullable-scalars", ValueCollectionsNullableScalarsApiCollectionType, ApiTypeModifiers.None,     x => x.NullableScalars),
                    ApiTypeFactory<ValueCollections>.CreateApiProperty("scalars",          ValueCollectionsScalarsApiCollectionType,         ApiTypeModifiers.Required, x => x.Scalars)
                },
                null,
                null);

        public static readonly IApiSchema ApiSchemaWithMailingAddress =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddress),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    MailingAddressApiObjectType
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithMailingAddressAndPhoneNumber =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressAndPhoneNumber),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    MailingAddressApiObjectType,
                    PhoneNumberApiObjectType,
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithMailingAddressAndPhoneNumberAndComment =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressAndPhoneNumberAndComment),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    CommentApiObjectType,
                    MailingAddressApiObjectType,
                    PhoneNumberApiObjectType,
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        private static readonly IApiSchema ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    CommentApiObjectType,
                    MailingAddressApiObjectType,
                    PersonApiObjectType,
                    PhoneNumberApiObjectType
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        private static readonly IApiSchema ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    ArticleApiObjectType,
                    CommentApiObjectType,
                    MailingAddressApiObjectType,
                    PersonApiObjectType,
                    PhoneNumberApiObjectType
                },
                new[]
                {
                    GuidApiScalarType,
                    IntApiScalarType,
                    StringApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers),
                Enumerable.Empty<IApiEnumerationType>(),
                new[]
                {
                    PersonExcludingMailingAddressAndPhoneNumbersApiObjectType
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithMailingAddressExcludingCountry =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressExcludingCountry),
                Enumerable.Empty<IApiEnumerationType>(),
                new[]
                {
                    MailingAddressExcludingCountryApiObjectType
                },
                new[]
                {
                    IntApiScalarType,
                    StringApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithMailingAddressExcludingCountryAndStrings =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithMailingAddressExcludingCountryAndStrings),
                null,
                new[]
                {
                    MailingAddressExcludingCountryAndStringsApiObjectType
                },
                new[]
                {
                    IntApiScalarType
                });

        public static readonly IApiSchema ApiSchemaWithValueCollections =
            ApiTypeFactory.CreateApiSchema(
                nameof(ApiSchemaWithValueCollections),
                new[]
                {
                    CountryApiEnumerationType
                },
                new[]
                {
                    ValueCollectionsApiObjectType
                },
                new[]
                {
                    IntApiScalarType
                });
        #endregion

        #region Configurations
        public class CountryConfiguration : ApiEnumerationTypeConfiguration<Country>
        {
            public CountryConfiguration()
            {
                this.HasName("country")
                    .HasDescription(typeof(Country).CreateDefaultApiEnumerationTypeDescription())
                    .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                    .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                    .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland."));
            }
        }

        public class GuidConfiguration : ApiScalarTypeConfiguration<Guid>

        {
            public GuidConfiguration()
            {
                this.HasName("guid")
                    .HasDescription(typeof(Guid).CreateDefaultApiScalarTypeDescription());
            }
        }

        public class IntConfiguration : ApiScalarTypeConfiguration<int>
        {
            public IntConfiguration()
            {
                this.HasName("int")
                    .HasDescription(typeof(int).CreateDefaultApiScalarTypeDescription());
            }
        }

        public class StringConfiguration : ApiScalarTypeConfiguration<string>
        {
            public StringConfiguration()
            {
                this.HasName("string")
                    .HasDescription(typeof(string).CreateDefaultApiScalarTypeDescription());
            }
        }

        public class MailingAddressConfiguration : ApiObjectTypeConfiguration<MailingAddress>
        {
            public MailingAddressConfiguration()
            {
                this.HasName("mailing-addresses")
                    .HasDescription("USA mailing address.")
                    .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                    .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                    .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                    .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                    .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                    .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired());
            }
        }

        public class MailingAddressExcludingCountryPropertyConfiguration : ApiObjectTypeConfiguration<MailingAddress>
        {
            public MailingAddressExcludingCountryPropertyConfiguration()

            {
                this.HasName("mailing-addresses")
                    .HasDescription("USA mailing address.")
                    .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                    .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                    .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                    .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                    .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                    .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired());

                this.Exclude(x => x.Country);
            }
        }

        public class MailingAddressExcludingCountryAndStringPropertiesConfiguration : ApiObjectTypeConfiguration<MailingAddress>
        {
            public MailingAddressExcludingCountryAndStringPropertiesConfiguration()
            {
                this.HasName("mailing-addresses")
                    .HasDescription("USA mailing address.")
                    .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                    .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                    .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                    .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                    .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                    .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired());

                this.Exclude(x => x.StreetAddress)
                    .Exclude(x => x.City)
                    .Exclude(x => x.State)
                    .Exclude(x => x.ZipCode)
                    .Exclude(x => x.Country);
            }
        }

        public class PhoneNumberConfiguration : ApiObjectTypeConfiguration<PhoneNumber>
        {
            public PhoneNumberConfiguration()
            {
                this.HasName("phone-numbers")
                    .HasDescription("USA phone number.")
                    .ApiProperty(y => y.Country,  y => y.HasName("country"))
                    .ApiProperty(y => y.Prefix,   y => y.HasName("prefix"))
                    .ApiProperty(y => y.AreaCode, y => y.HasName("area-code").IsRequired())
                    .ApiProperty(y => y.Number,   y => y.HasName("number").IsRequired());
            }
        }

        public class CommentConfiguration : ApiObjectTypeConfiguration<Comment>
        {
            public CommentConfiguration()
            {
                this.HasName("comments")
                    .HasDescription("A comment authored by a person on a subject.")
                    .ApiProperty(y => y.CommentId, y => y.HasName("id"))
                    .ApiProperty(y => y.Text,      y => y.HasName("text"))
                    .ApiIdentity(y => y.CommentId);
            }
        }

        public class PersonConfiguration : ApiObjectTypeConfiguration<Person>
        {
            public PersonConfiguration()
            {
                this.HasName("people")
                    .HasDescription("A human being.")
                    .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                    .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                    .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                    .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                    .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                    .ApiIdentity(y => y.PersonId);
            }
        }

        public class PersonExcludingMailingAddressAndPhoneNumberConfiguration : ApiObjectTypeConfiguration<Person>

        {
            public PersonExcludingMailingAddressAndPhoneNumberConfiguration()
            {
                this.HasName("people")
                    .HasDescription("A human being.")
                    .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                    .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                    .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                    .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                    .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                    .ApiIdentity(y => y.PersonId);

                this.Exclude(x => x.MailingAddress)
                    .Exclude(x => x.PhoneNumbers);
            }
        }

        public class ArticleConfiguration : ApiObjectTypeConfiguration<Article>
        {
            public ArticleConfiguration()
            {
                this.HasName("articles")
                    .HasDescription("An article that is part of a publication.")
                    .ApiProperty(y => y.ArticleId, y => y.HasName("id"))
                    .ApiProperty(y => y.Title,     y => y.HasName("title").IsRequired())
                    .ApiProperty(y => y.Body,      y => y.HasName("body").IsRequired())
                    .ApiProperty(y => y.Author,    y => y.HasName("author").IsRequired().HasDescription("Single author of the article."))
                    .ApiProperty(y => y.Comments,  y => y.HasName("comments").IsRequired().HasDescription("Many comments on the article.").ApiCollectionType(z => z.ItemIsRequired()))
                    .ApiIdentity(y => y.ArticleId)
                    .ApiRelationship(y => y.Author)
                    .ApiRelationship(y => y.Comments);
            }
        }

        public class ValueCollectionsConfiguration : ApiObjectTypeConfiguration<ValueCollections>
        {
            public ValueCollectionsConfiguration()
            {
                this.HasName("value-collections")
                    .HasDescription("A collection of value and nullable value collections for testing purposes.")
                    .ApiProperty(y => y.Enums,           y => y.HasName("enums").IsRequired())
                    .ApiProperty(y => y.NullableEnums,   y => y.HasName("nullable-enums"))
                    .ApiProperty(y => y.NullableScalars, y => y.HasName("nullable-scalars"))
                    .ApiProperty(y => y.Scalars,         y => y.HasName("scalars").IsRequired());
            }
        }

        public class CountryWithApiConventionsConfiguration : ApiEnumerationTypeConfiguration<Country>
        {
            public CountryWithApiConventionsConfiguration()
            {
                this.HasDescription(typeof(Country).CreateDefaultApiEnumerationTypeDescription())
                    .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                    .ApiEnumerationValue(() => Country.England,      y => y.HasDescription("The country of England."))
                    .ApiEnumerationValue(() => Country.Ireland,      y => y.HasDescription("The country of Ireland."));
            }
        }

        public class IntWithApiConventionsConfiguration : ApiScalarTypeConfiguration<int>
        {
            public IntWithApiConventionsConfiguration()
            {
                this.HasName("int");
            }
        }

        public class MailingAddressWithApiConventionsConfiguration : ApiObjectTypeConfiguration<MailingAddress>
        {
            public MailingAddressWithApiConventionsConfiguration()
            {
                this.HasDescription("USA mailing address.")
                    .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                    .ApiProperty(y => y.City,          y => y.IsRequired())
                    .ApiProperty(y => y.State,         y => y.IsRequired())
                    .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                    .ApiProperty(y => y.Country,       y => y.IsRequired());
            }
        }

        public class PhoneNumberWithApiConventionsConfiguration : ApiObjectTypeConfiguration<PhoneNumber>
        {
            public PhoneNumberWithApiConventionsConfiguration()
            {
                this.HasDescription("USA phone number.")
                    .ApiProperty(y => y.AreaCode, y => y.IsRequired())
                    .ApiProperty(y => y.Number,   y => y.IsRequired());
            }
        }

        public class CommentWithApiConventionsConfiguration : ApiObjectTypeConfiguration<Comment>
        {
            public CommentWithApiConventionsConfiguration()
            {
                this.HasDescription("A comment authored by a person on a subject.")
                    .ApiProperty(y => y.CommentId, y => y.HasName("id"));
            }
        }

        public class PersonWithApiConventionsConfiguration : ApiObjectTypeConfiguration<Person>
        {
            public PersonWithApiConventionsConfiguration()
            {
                this.HasDescription("A human being.")
                    .ApiProperty(y => y.PersonId,     y => y.HasName("id"))
                    .ApiProperty(y => y.FirstName,    y => y.IsRequired())
                    .ApiProperty(y => y.LastName,     y => y.IsRequired())
                    .ApiProperty(y => y.PhoneNumbers, y => y.IsRequired().ApiCollectionType(z => z.ItemIsRequired()));
            }
        }

        public class ArticleWithApiConventionsConfiguration : ApiObjectTypeConfiguration<Article>
        {
            public ArticleWithApiConventionsConfiguration()
            {
                this.HasDescription("An article that is part of a publication.")
                    .ApiProperty(y => y.ArticleId, y => y.HasName("id"))
                    .ApiProperty(y => y.Title,     y => y.IsRequired())
                    .ApiProperty(y => y.Body,      y => y.IsRequired())
                    .ApiProperty(y => y.Author,    y => y.IsRequired().HasDescription("Single author of the article."))
                    .ApiProperty(y => y.Comments,  y => y.IsRequired().HasDescription("Many comments on the article.").ApiCollectionType(z => z.ItemIsRequired()));
            }
        }

        public class ValueCollectionsWithApiConventionsConfiguration : ApiObjectTypeConfiguration<ValueCollections>
        {
            public ValueCollectionsWithApiConventionsConfiguration()
            {
                this.HasDescription("A collection of value and nullable value collections for testing purposes.")
                    .ApiProperty(y => y.Enums,   y => y.IsRequired())
                    .ApiProperty(y => y.Scalars, y => y.IsRequired());
            }
        }
        #endregion

        #region Conventions
        public static readonly ApiDiscoverySettings ApiDiscoverySettings =
            new ApiDiscoverySettings(typeof(ApiSchemaConfigurationTests).Assembly)
            {
                ApiTypeDiscoveryPredicate = type =>
                {
                    var isNestedPublic = type.IsNestedPublic;
                    if (isNestedPublic == false)
                        return false;

                    var declaringType = type.DeclaringType;
                    if (declaringType != typeof(ApiSchemaConfigurationTests))
                        return false;

                    return true;
                },


                ApiConfigurationDiscoveryPredicate = type =>
                {
                    var typeName = type.Name;
                    return typeName.Contains("WithApiConventions");
                }
            };

        public static readonly ApiConventionSettings ApiConventionSettings = new ApiConventionSettings
        {
            ApiDiscoverySettings = ApiDiscoverySettings
        };

        public static readonly ILogger ApiSchemaLogger = new NullLogger();

        public static readonly Func<ApiSchemaFactorySettings> ApiTypeFactorySettingsWithFluentApiFactory = () =>
        {
            var configuration = new ApiConventionSetConfiguration();

            // API Enumeration Types
            configuration.ApiEnumerationTypeConventions()
                         .HasEnumValueDiscoveryConvention();

            configuration.ApiEnumerationTypeNameConventions()
                         .HasLowerCaseConvention();

            // API Enumeration Values
            configuration.ApiEnumerationValueNameConventions()
                         .HasKebabCaseConvention();

            // API Object Types
            configuration.ApiObjectTypeConventions()
                         .HasPropertyDiscoveryConvention()
                         .HasIdentityDiscoveryConvention()
                         .HasRelationshipDiscoveryConvention();

            configuration.ApiObjectTypeNameConventions()
                         .HasPluralizeConvention()
                         .HasKebabCaseConvention();

            // API Properties
            configuration.ApiPropertyNameConventions()
                         .HasKebabCaseConvention();

            // API Scalar Types
            configuration.ApiScalarTypeNameConventions()
                         .HasLowerCaseConvention();

            // API Schema
            configuration.ApiSchemaConventions()
                         .HasTypeDiscoveryConvention();

            var conventionSet = configuration.Create();
            var settings = new ApiSchemaFactorySettings
            {
                ApiConventionSet      = conventionSet,
                ApiConventionSettings = ApiConventionSettings
            };
            return settings;
        };

        public static readonly Func<ApiSchemaFactorySettings> ApiTypeFactorySettingsWithConfigurationsFactory = () =>
        {
            var configuration = new ApiConventionSetConfiguration();

            // API Enumeration Types
            configuration.ApiEnumerationTypeConventions()
                         .HasEnumValueDiscoveryConvention();

            configuration.ApiEnumerationTypeNameConventions()
                         .HasLowerCaseConvention();

            // API Enumeration Values
            configuration.ApiEnumerationValueNameConventions()
                         .HasKebabCaseConvention();

            // API Object Types
            configuration.ApiObjectTypeConventions()
                         .HasPropertyDiscoveryConvention()
                         .HasIdentityDiscoveryConvention()
                         .HasRelationshipDiscoveryConvention();

            configuration.ApiObjectTypeNameConventions()
                         .HasPluralizeConvention()
                         .HasKebabCaseConvention();

            // API Properties
            configuration.ApiPropertyNameConventions()
                         .HasKebabCaseConvention();

            // API Scalar Types
            configuration.ApiScalarTypeNameConventions()
                         .HasLowerCaseConvention();

            // API Schema
            configuration.ApiSchemaConventions()
                         .HasConfigurationDiscoveryConvention();

            var conventionSet = configuration.Create();
            var settings = new ApiSchemaFactorySettings
            {
                ApiConventionSet      = conventionSet,
                ApiConventionSettings = ApiConventionSettings
            };
            return settings;
        };

        public static readonly Func<ApiSchemaFactorySettings> ApiTypeFactorySettingsWithAnnotationsFactory = () =>
        {
            var configuration = new ApiConventionSetConfiguration();

            // API Enumeration Types
            configuration.ApiEnumerationTypeConventions()
                         .HasAnnotationDiscoveryConvention();

            // API Object Types
            configuration.ApiObjectTypeConventions()
                         .HasAnnotationDiscoveryConvention();

            // API Schema
            configuration.ApiSchemaConventions()
                         .HasAnnotationDiscoveryConvention();

            var conventionSet = configuration.Create();
            var settings = new ApiSchemaFactorySettings
            {
                ApiConventionSet      = conventionSet,
                ApiConventionSettings = ApiConventionSettings
            };
            return settings;
        };
        #endregion

        public static readonly IEnumerable<object[]> CreateApiSchemaWithAnnotationsTestData =
            new[]
            {
                // With API Conventions
                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddress");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddress)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumber");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumber)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndComment");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndComment)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticleAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle");
                            apiSchemaConfiguration.ApiScalarType<Guid>(x => x.HasName("guid"));
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<ValueCollections>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressPropertyAndPhoneNumbersPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.Exclude(y => y.MailingAddress)
                                                                               .Exclude(y => y.PhoneNumbers));
                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressTypeAndPhoneNumberTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.Country));
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryAndStringPropertiesAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.StreetAddress)
                                                                                       .Exclude(y => y.City)
                                                                                       .Exclude(y => y.State)
                                                                                       .Exclude(y => y.ZipCode)
                                                                                       .Exclude(y => y.Country));
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));
                            apiSchemaConfiguration.Exclude<Country>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndStringTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.Exclude<Country>()
                                                  .Exclude<string>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithValueCollectionsAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithValueCollections");
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithAnnotationsFactory,
                        ApiSchemaWithValueCollections)
                },
            };

        public static readonly IEnumerable<object[]> CreateApiSchemaWithTypeConfigurationsTestData =
            new[]
            {
                // Without API Conventions
                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddress",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddress");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddress)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumber",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumber");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PhoneNumberConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumber)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndComment",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndComment");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PhoneNumberConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new CommentConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndComment)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPerson",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PhoneNumberConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new CommentConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PersonConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new GuidConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PhoneNumberConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new CommentConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PersonConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new ArticleConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressPropertyAndPhoneNumbersProperty",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PersonExcludingMailingAddressAndPhoneNumberConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressTypeAndPhoneNumberType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new PersonConfiguration());

                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>();

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryProperty",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressExcludingCountryPropertyConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryAndStringProperties",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressExcludingCountryAndStringPropertiesConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.Exclude<Country>();
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndStringType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new StringConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new MailingAddressConfiguration());
                            apiSchemaConfiguration.Exclude<Country>();
                            apiSchemaConfiguration.Exclude<string>();
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithValueCollections",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithValueCollections");
                            apiSchemaConfiguration.HasConfiguration(new CountryConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new IntConfiguration());
                            apiSchemaConfiguration.HasConfiguration(new ValueCollectionsConfiguration());
                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithValueCollections)
                },

                // With API Conventions
                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddress");
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddress)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumber");
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumber)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndComment");
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndComment)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson");
                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticleAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle");
                            apiSchemaConfiguration.Exclude<ValueCollections>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressPropertyAndPhoneNumbersPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.Exclude(y => y.MailingAddress)
                                                                               .Exclude(y => y.PhoneNumbers));
                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressTypeAndPhoneNumberTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");
                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.Country));
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryAndStringPropertiesAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.StreetAddress)
                                                                                       .Exclude(y => y.City)
                                                                                       .Exclude(y => y.State)
                                                                                       .Exclude(y => y.ZipCode)
                                                                                       .Exclude(y => y.Country));
                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");
                            apiSchemaConfiguration.Exclude<Country>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndStringTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");
                            apiSchemaConfiguration.Exclude<Country>()
                                                  .Exclude<string>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();
                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithValueCollectionsAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithValueCollections");

                            apiSchemaConfiguration.Exclude<string>()
                                                  .Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithConfigurationsFactory,
                        ApiSchemaWithValueCollections)
                },
            };

        public static readonly IEnumerable<object[]> CreateApiSchemaWithFluentApiTestData =
            new[]
            {
                // Without API Conventions
                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddress",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddress");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddress)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumber",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumber");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasName("phone-numbers")
                                                                                    .HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.Country,  y => y.HasName("country"))
                                                                                    .ApiProperty(y => y.Prefix,   y => y.HasName("prefix"))
                                                                                    .ApiProperty(y => y.AreaCode, y => y.HasName("area-code").IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.HasName("number").IsRequired()));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumber)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndComment",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndComment");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasName("phone-numbers")
                                                                                    .HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.Country,  y => y.HasName("country"))
                                                                                    .ApiProperty(y => y.Prefix,   y => y.HasName("prefix"))
                                                                                    .ApiProperty(y => y.AreaCode, y => y.HasName("area-code").IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.HasName("number").IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasName("comments")
                                                                                .HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id"))
                                                                                .ApiProperty(y => y.Text,      y => y.HasName("text"))
                                                                                .ApiIdentity(y => y.CommentId));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndComment)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPerson",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasName("phone-numbers")
                                                                                    .HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.Country,  y => y.HasName("country"))
                                                                                    .ApiProperty(y => y.Prefix,   y => y.HasName("prefix"))
                                                                                    .ApiProperty(y => y.AreaCode, y => y.HasName("area-code").IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.HasName("number").IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasName("comments")
                                                                                .HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id"))
                                                                                .ApiProperty(y => y.Text,      y => y.HasName("text"))
                                                                                .ApiIdentity(y => y.CommentId));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasName("people")
                                                                               .HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                                                                               .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                                                                               .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                                                                               .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                                                                               .ApiIdentity(y => y.PersonId));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<Guid>(x => x.HasName("guid"));
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasName("phone-numbers")
                                                                                    .HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.Country,  y => y.HasName("country"))
                                                                                    .ApiProperty(y => y.Prefix,   y => y.HasName("prefix"))
                                                                                    .ApiProperty(y => y.AreaCode, y => y.HasName("area-code").IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.HasName("number").IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasName("comments")
                                                                                .HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id"))
                                                                                .ApiProperty(y => y.Text,      y => y.HasName("text"))
                                                                                .ApiIdentity(y => y.CommentId));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasName("people")
                                                                               .HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                                                                               .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                                                                               .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                                                                               .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                                                                               .ApiIdentity(y => y.PersonId));

                            // Article
                            apiSchemaConfiguration.ApiObjectType<Article>(x => x.HasName("articles")
                                                                                .HasDescription("An article that is part of a publication.")
                                                                                .ApiProperty(y => y.ArticleId, y => y.HasName("id"))
                                                                                .ApiProperty(y => y.Title,     y => y.HasName("title").IsRequired())
                                                                                .ApiProperty(y => y.Body,      y => y.HasName("body").IsRequired())
                                                                                .ApiProperty(y => y.Author,    y => y.HasName("author").IsRequired().HasDescription("Single author of the article."))
                                                                                .ApiProperty(y => y.Comments,  y => y.HasName("comments").IsRequired().HasDescription("Many comments on the article.").ApiCollectionType(z => z.ItemIsRequired()))
                                                                                .ApiIdentity(y => y.ArticleId)
                                                                                .ApiRelationship(y => y.Author)
                                                                                .ApiRelationship(y => y.Comments));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressPropertyAndPhoneNumbersProperty",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasName("people")
                                                                               .HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                                                                               .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                                                                               .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                                                                               .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                                                                               .ApiIdentity(y => y.PersonId));

                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.Exclude(y => y.MailingAddress)
                                                                               .Exclude(y => y.PhoneNumbers));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressTypeAndPhoneNumberType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasName("people")
                                                                               .HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,       y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,      y => y.HasName("first-name").IsRequired())
                                                                               .ApiProperty(y => y.LastName,       y => y.HasName("last-name").IsRequired())
                                                                               .ApiProperty(y => y.MailingAddress, y => y.HasName("mailing-address"))
                                                                               .ApiProperty(y => y.PhoneNumbers,   y => y.HasName("phone-numbers").IsRequired().ApiCollectionType(z => z.ItemIsRequired()))
                                                                               .ApiIdentity(y => y.PersonId));

                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>();

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryProperty",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.Country));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryAndStringProperties",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.StreetAddress)
                                                                                       .Exclude(y => y.City)
                                                                                       .Exclude(y => y.State)
                                                                                       .Exclude(y => y.ZipCode)
                                                                                       .Exclude(y => y.Country));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            apiSchemaConfiguration.Exclude<Country>();

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndStringType",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.HasName("street-address").IsRequired())
                                                                                       .ApiProperty(y => y.AptNumber,     y => y.HasName("apt-number"))
                                                                                       .ApiProperty(y => y.City,          y => y.HasName("city").IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.HasName("state").IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.HasName("zip-code").IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.HasName("country").IsRequired()));

                            apiSchemaConfiguration.Exclude<Country>();
                            apiSchemaConfiguration.Exclude<string>();

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithValueCollections",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithValueCollections");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasName("united-states").HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasName("england").HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasName("ireland").HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Value Collections
                            apiSchemaConfiguration.ApiObjectType<ValueCollections>(x => x.HasName("value-collections")
                                                                                         .HasDescription("A collection of value and nullable value collections for testing purposes.")
                                                                                         .ApiProperty(y => y.Enums,           y => y.HasName("enums").IsRequired())
                                                                                         .ApiProperty(y => y.NullableEnums,   y => y.HasName("nullable-enums"))
                                                                                         .ApiProperty(y => y.NullableScalars, y => y.HasName("nullable-scalars"))
                                                                                         .ApiProperty(y => y.Scalars,         y => y.HasName("scalars").IsRequired()));

                            return apiSchemaConfiguration;
                        },
                        null,
                        ApiSchemaWithValueCollections)
                },

                // With API Conventions
                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddress");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England, y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland, y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.IsRequired()));

                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddress)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumber");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England, y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland, y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.AreaCode, y => y.IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.IsRequired()));

                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumber)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndComment");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England, y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland, y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.AreaCode, y => y.IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id")));

                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();


                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndComment)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England, y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland, y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.AreaCode, y => y.IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id")));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,     y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,    y => y.IsRequired())
                                                                               .ApiProperty(y => y.LastName,     y => y.IsRequired())
                                                                               .ApiProperty(y => y.PhoneNumbers, y => y.IsRequired().ApiCollectionType(z => z.ItemIsRequired())));

                            apiSchemaConfiguration.Exclude<ValueCollections>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPerson)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticleAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England, y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland, y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired())
                                                                                       .ApiProperty(y => y.Country,       y => y.IsRequired()));

                            // Phone Number
                            apiSchemaConfiguration.ApiObjectType<PhoneNumber>(x => x.HasDescription("USA phone number.")
                                                                                    .ApiProperty(y => y.AreaCode, y => y.IsRequired())
                                                                                    .ApiProperty(y => y.Number,   y => y.IsRequired()));

                            // Comment
                            apiSchemaConfiguration.ApiObjectType<Comment>(x => x.HasDescription("A comment authored by a person on a subject.")
                                                                                .ApiProperty(y => y.CommentId, y => y.HasName("id")));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,     y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName,    y => y.IsRequired())
                                                                               .ApiProperty(y => y.LastName,     y => y.IsRequired())
                                                                               .ApiProperty(y => y.PhoneNumbers, y => y.IsRequired().ApiCollectionType(z => z.ItemIsRequired())));

                            // Article
                            apiSchemaConfiguration.ApiObjectType<Article>(x => x.HasDescription("An article that is part of a publication.")
                                                                                .ApiProperty(y => y.ArticleId, y => y.HasName("id"))
                                                                                .ApiProperty(y => y.Title,     y => y.IsRequired())
                                                                                .ApiProperty(y => y.Body,      y => y.IsRequired())
                                                                                .ApiProperty(y => y.Author,    y => y.IsRequired().HasDescription("Single author of the article."))
                                                                                .ApiProperty(y => y.Comments,  y => y.IsRequired().HasDescription("Many comments on the article.").ApiCollectionType(z => z.ItemIsRequired())));

                            apiSchemaConfiguration.Exclude<ValueCollections>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressAndPhoneNumberAndCommentAndPersonAndArticle)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressPropertyAndPhoneNumbersPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,  y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName, y => y.IsRequired())
                                                                               .ApiProperty(y => y.LastName,  y => y.IsRequired()));

                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.Exclude(y => y.MailingAddress)
                                                                               .Exclude(y => y.PhoneNumbers));

                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithPersonExcludingMailingAddressTypeAndPhoneNumberTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));
                            apiSchemaConfiguration.ApiScalarType<string>(x => x.HasName("string"));

                            // Person
                            apiSchemaConfiguration.ApiObjectType<Person>(x => x.HasDescription("A human being.")
                                                                               .ApiProperty(y => y.PersonId,  y => y.HasName("id"))
                                                                               .ApiProperty(y => y.FirstName, y => y.IsRequired())
                                                                               .ApiProperty(y => y.LastName,  y => y.IsRequired()));

                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithPersonExcludingMailingAddressAndPhoneNumbers)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryPropertyAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired()));

                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.Country));

                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryAndStringPropertiesAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasName("mailing-addresses")
                                                                                       .HasDescription("USA mailing address."));

                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.Exclude(y => y.StreetAddress)
                                                                                       .Exclude(y => y.City)
                                                                                       .Exclude(y => y.State)
                                                                                       .Exclude(y => y.ZipCode)
                                                                                       .Exclude(y => y.Country));

                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountry");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address.")
                                                                                       .ApiProperty(y => y.StreetAddress, y => y.IsRequired())
                                                                                       .ApiProperty(y => y.City,          y => y.IsRequired())
                                                                                       .ApiProperty(y => y.State,         y => y.IsRequired())
                                                                                       .ApiProperty(y => y.ZipCode,       y => y.IsRequired()));

                            apiSchemaConfiguration.Exclude<Country>();

                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressExcludingCountry)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithMailingAddressExcludingCountryTypeAndStringTypeAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithMailingAddressExcludingCountryAndStrings");

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Mailing Address
                            apiSchemaConfiguration.ApiObjectType<MailingAddress>(x => x.HasDescription("USA mailing address."));

                            apiSchemaConfiguration.Exclude<Country>();
                            apiSchemaConfiguration.Exclude<string>();

                            apiSchemaConfiguration.Exclude<PhoneNumber>()
                                                  .Exclude<ValueCollections>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithMailingAddressExcludingCountryAndStrings)
                },

                new object[]
                {
                    new CreateApiSchemaUnitTest(
                        "WithValueCollectionsAndApiConventions",
                        () =>
                        {
                            var apiSchemaConfiguration = new ApiSchemaConfiguration();
                            apiSchemaConfiguration.HasName("ApiSchemaWithValueCollections");

                            // Enums
                            apiSchemaConfiguration.ApiEnumerationType<Country>(x => x.HasName("country")
                                                                                     .ApiEnumerationValue(() => Country.UnitedStates, y => y.HasDescription("The country of United States."))
                                                                                     .ApiEnumerationValue(() => Country.England,      y => y.HasDescription("The country of England."))
                                                                                     .ApiEnumerationValue(() => Country.Ireland,      y => y.HasDescription("The country of Ireland.")));

                            // Scalars
                            apiSchemaConfiguration.ApiScalarType<int>(x => x.HasName("int"));

                            // Value Collections
                            apiSchemaConfiguration.ApiObjectType<ValueCollections>(x => x.HasName("value-collections")
                                                                                         .HasDescription("A collection of value and nullable value collections for testing purposes.")
                                                                                         .ApiProperty(y => y.Enums,   y => y.IsRequired())
                                                                                         .ApiProperty(y => y.Scalars, y => y.IsRequired()));

                            apiSchemaConfiguration.Exclude<MailingAddress>()
                                                  .Exclude<PhoneNumber>()
                                                  .Exclude<Comment>()
                                                  .Exclude<Person>()
                                                  .Exclude<Article>();

                            return apiSchemaConfiguration;
                        },
                        ApiTypeFactorySettingsWithFluentApiFactory,
                        ApiSchemaWithValueCollections)
                },
            };

        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Unit Tests
        private class CreateApiSchemaUnitTest : XUnitTest
        {
            #region Constructors
            public CreateApiSchemaUnitTest(string                         name,
                                           Func<ApiSchemaConfiguration>   apiSchemaConfigurationFactory,
                                           Func<ApiSchemaFactorySettings> apiSchemaFactorySettingsFactory,
                                           IApiSchema                     expectedApiSchema)
                : base(name)
            {
                this.ApiSchemaConfigurationFactory = apiSchemaConfigurationFactory;
                this.ApiTypeFactorySettingsFactory = apiSchemaFactorySettingsFactory;
                this.ExpectedApiSchema             = expectedApiSchema;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected API Schema");
                this.WriteLine(this.ExpectedApiSchema.ToTreeString());
                this.WriteLine();

                var apiSchemaConfiguration = this.ApiSchemaConfigurationFactory();
                this.ApiSchemaConfiguration = apiSchemaConfiguration;

                var apiSchemaFactorySettings = this.ApiTypeFactorySettingsFactory?.Invoke();
                this.ApiSchemaFactorySettings = apiSchemaFactorySettings;
            }

            protected override void Act()
            {
                var apiSchemaConfiguration   = this.ApiSchemaConfiguration;
                var apiSchemaFactorySettings = this.ApiSchemaFactorySettings;
                var actualApiSchema          = apiSchemaConfiguration.Create(apiSchemaFactorySettings);
                this.ActualApiSchema = actualApiSchema;

                this.WriteLine("Actual API Schema");
                this.WriteLine(this.ActualApiSchema.ToTreeString());
            }

            protected override void Assert()
            {
                var actualApiSchema   = this.ActualApiSchema;
                var expectedApiSchema = this.ExpectedApiSchema;

                actualApiSchema.Should().BeEquivalentTo(expectedApiSchema, options => options.AllowingInfiniteRecursion()
                                                                                             .IgnoringCyclicReferences());
            }
            #endregion

            #region User Supplied Properties
            private Func<ApiSchemaConfiguration>   ApiSchemaConfigurationFactory { get; }
            private Func<ApiSchemaFactorySettings> ApiTypeFactorySettingsFactory { get; }
            private IApiSchema                     ExpectedApiSchema             { get; }
            #endregion

            #region Calculated Properties
            private ApiSchemaConfiguration   ApiSchemaConfiguration   { get; set; }
            private ApiSchemaFactorySettings ApiSchemaFactorySettings { get; set; }
            private IApiSchema               ActualApiSchema          { get; set; }
            #endregion
        }
        #endregion
    }
}