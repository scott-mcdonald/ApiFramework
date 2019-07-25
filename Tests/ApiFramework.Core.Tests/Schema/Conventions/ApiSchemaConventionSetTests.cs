// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Conventions.Internal;
using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Schema.Conventions
{
    public class ApiConventionSetTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiConventionSetTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateApiConventionSetTestData))]
        public void TestCreateApiConventionSet(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> CreateApiConventionSetTestData = new[]
        {
            new object[]
            {
                new CreateApiConventionSetUnitTest("WithEmptyApiConventions", () =>
                                                   {
                                                       var apiConventionSetConfiguration = new ApiConventionSetConfiguration();
                                                       return apiConventionSetConfiguration;
                                                   },
                                                   ApiConventionSet.Empty)
            },

            new object[]
            {
                new CreateApiConventionSetUnitTest("WithAllApiConventions", () =>
                                                   {
                                                       var apiConventionSetConfiguration = new ApiConventionSetConfiguration();

                                                       var apiEnumerationTypeConventions = apiConventionSetConfiguration.ApiEnumerationTypeConventions();
                                                       apiEnumerationTypeConventions.HasAnnotationDiscoveryConvention()
                                                                                    .HasEnumValueDiscoveryConvention()
                                                                                    .HasConvention(new SetDescriptionNullApiEnumerationTypeConvention());

                                                       var apiEnumerationTypeNameConventions = apiConventionSetConfiguration.ApiEnumerationTypeNameConventions();
                                                       apiEnumerationTypeNameConventions.HasCamelCaseConvention()
                                                                                        .HasKebabCaseConvention()
                                                                                        .HasLowerCaseConvention()
                                                                                        .HasPascalCaseConvention()
                                                                                        .HasPluralizeConvention()
                                                                                        .HasSingularizeConvention()
                                                                                        .HasUpperCaseConvention()
                                                                                        .HasConvention(new HashTagApiNamingConvention());

                                                       var apiEnumerationValueNameConventions = apiConventionSetConfiguration.ApiEnumerationValueNameConventions();
                                                       apiEnumerationValueNameConventions.HasCamelCaseConvention()
                                                                                         .HasKebabCaseConvention()
                                                                                         .HasLowerCaseConvention()
                                                                                         .HasPascalCaseConvention()
                                                                                         .HasPluralizeConvention()
                                                                                         .HasSingularizeConvention()
                                                                                         .HasUpperCaseConvention()
                                                                                         .HasConvention(new HashTagApiNamingConvention());

                                                       var apiObjectTypeConventions = apiConventionSetConfiguration.ApiObjectTypeConventions();
                                                       apiObjectTypeConventions.HasAnnotationDiscoveryConvention()
                                                                               .HasIdentityDiscoveryConvention()
                                                                               .HasPropertyDiscoveryConvention()
                                                                               .HasRelationshipDiscoveryConvention()
                                                                               .HasConvention(new SetDescriptionNullApiObjectTypeConvention());

                                                       var apiObjectTypeNameConventions = apiConventionSetConfiguration.ApiObjectTypeNameConventions();
                                                       apiObjectTypeNameConventions.HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention());

                                                       var apiPropertyNameConventions = apiConventionSetConfiguration.ApiPropertyNameConventions();
                                                       apiPropertyNameConventions.HasCamelCaseConvention()
                                                                                 .HasKebabCaseConvention()
                                                                                 .HasLowerCaseConvention()
                                                                                 .HasPascalCaseConvention()
                                                                                 .HasPluralizeConvention()
                                                                                 .HasSingularizeConvention()
                                                                                 .HasUpperCaseConvention()
                                                                                 .HasConvention(new HashTagApiNamingConvention());

                                                       var apiScalarTypeNameConventions = apiConventionSetConfiguration.ApiScalarTypeNameConventions();
                                                       apiScalarTypeNameConventions.HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention());

                                                       var apiSchemaConventions = apiConventionSetConfiguration.ApiSchemaConventions();
                                                       apiSchemaConventions.HasAnnotationDiscoveryConvention()
                                                                           .HasConfigurationDiscoveryConvention()
                                                                           .HasTypeDiscoveryConvention()
                                                                           .HasConvention(new SetNameNullApiSchemaConvention());

                                                       return apiConventionSetConfiguration;
                                                   },
                                                   new ApiConventionSet(
                                                       new List<IApiEnumerationTypeConvention>
                                                       {
                                                           new ApiAnnotationDiscoveryEnumTypeConvention(),
                                                           new ApiEnumerationValueDiscoveryEnumTypeConvention(),
                                                           new SetDescriptionNullApiEnumerationTypeConvention()
                                                       },
                                                       new List<IApiNamingConvention>
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiObjectTypeConvention>
                                                       {
                                                           new ApiAnnotationDiscoveryObjectTypeConvention(),
                                                           new ApiIdentityDiscoveryObjectTypeConvention(),
                                                           new ApiPropertyDiscoveryObjectTypeConvention(),
                                                           new ApiRelationshipDiscoveryObjectTypeConvention(),
                                                           new SetDescriptionNullApiObjectTypeConvention()
                                                       },
                                                       new List<IApiNamingConvention>
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiSchemaConvention>
                                                       {
                                                           new ApiAnnotationDiscoverySchemaConvention(),
                                                           new ApiConfigurationDiscoverySchemaConvention(),
                                                           new ApiTypeDiscoverySchemaConvention(),
                                                           new SetNameNullApiSchemaConvention()
                                                       }))
            },

            new object[]
            {
                new CreateApiConventionSetUnitTest("WithAllApiConventionsAddedTwice", () =>
                                                   {
                                                       var apiConventionSetConfiguration = new ApiConventionSetConfiguration();

                                                       var apiEnumerationTypeConventions = apiConventionSetConfiguration.ApiEnumerationTypeConventions();
                                                       apiEnumerationTypeConventions.HasAnnotationDiscoveryConvention()
                                                                                    .HasEnumValueDiscoveryConvention()
                                                                                    .HasConvention(new SetDescriptionNullApiEnumerationTypeConvention())
                                                                                    .HasAnnotationDiscoveryConvention()
                                                                                    .HasEnumValueDiscoveryConvention()
                                                                                    .HasConvention(new SetDescriptionNullApiEnumerationTypeConvention());

                                                       var apiEnumerationTypeNameConventions = apiConventionSetConfiguration.ApiEnumerationTypeNameConventions();
                                                       apiEnumerationTypeNameConventions.HasCamelCaseConvention()
                                                                                        .HasKebabCaseConvention()
                                                                                        .HasLowerCaseConvention()
                                                                                        .HasPascalCaseConvention()
                                                                                        .HasPluralizeConvention()
                                                                                        .HasSingularizeConvention()
                                                                                        .HasUpperCaseConvention()
                                                                                        .HasConvention(new HashTagApiNamingConvention())
                                                                                        .HasCamelCaseConvention()
                                                                                        .HasKebabCaseConvention()
                                                                                        .HasLowerCaseConvention()
                                                                                        .HasPascalCaseConvention()
                                                                                        .HasPluralizeConvention()
                                                                                        .HasSingularizeConvention()
                                                                                        .HasUpperCaseConvention()
                                                                                        .HasConvention(new HashTagApiNamingConvention());

                                                       var apiEnumerationValueNameConventions = apiConventionSetConfiguration.ApiEnumerationValueNameConventions();
                                                       apiEnumerationValueNameConventions.HasCamelCaseConvention()
                                                                                         .HasKebabCaseConvention()
                                                                                         .HasLowerCaseConvention()
                                                                                         .HasPascalCaseConvention()
                                                                                         .HasPluralizeConvention()
                                                                                         .HasSingularizeConvention()
                                                                                         .HasUpperCaseConvention()
                                                                                         .HasConvention(new HashTagApiNamingConvention())
                                                                                         .HasCamelCaseConvention()
                                                                                         .HasKebabCaseConvention()
                                                                                         .HasLowerCaseConvention()
                                                                                         .HasPascalCaseConvention()
                                                                                         .HasPluralizeConvention()
                                                                                         .HasSingularizeConvention()
                                                                                         .HasUpperCaseConvention()
                                                                                         .HasConvention(new HashTagApiNamingConvention());

                                                       var apiObjectTypeConventions = apiConventionSetConfiguration.ApiObjectTypeConventions();
                                                       apiObjectTypeConventions.HasAnnotationDiscoveryConvention()
                                                                               .HasIdentityDiscoveryConvention()
                                                                               .HasPropertyDiscoveryConvention()
                                                                               .HasRelationshipDiscoveryConvention()
                                                                               .HasConvention(new SetDescriptionNullApiObjectTypeConvention())
                                                                               .HasAnnotationDiscoveryConvention()
                                                                               .HasIdentityDiscoveryConvention()
                                                                               .HasPropertyDiscoveryConvention()
                                                                               .HasRelationshipDiscoveryConvention()
                                                                               .HasConvention(new SetDescriptionNullApiObjectTypeConvention());

                                                       var apiObjectTypeNameConventions = apiConventionSetConfiguration.ApiObjectTypeNameConventions();
                                                       apiObjectTypeNameConventions.HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention())
                                                                                   .HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention());

                                                       var apiPropertyNameConventions = apiConventionSetConfiguration.ApiPropertyNameConventions();
                                                       apiPropertyNameConventions.HasCamelCaseConvention()
                                                                                 .HasKebabCaseConvention()
                                                                                 .HasLowerCaseConvention()
                                                                                 .HasPascalCaseConvention()
                                                                                 .HasPluralizeConvention()
                                                                                 .HasSingularizeConvention()
                                                                                 .HasUpperCaseConvention()
                                                                                 .HasConvention(new HashTagApiNamingConvention())
                                                                                 .HasCamelCaseConvention()
                                                                                 .HasKebabCaseConvention()
                                                                                 .HasLowerCaseConvention()
                                                                                 .HasPascalCaseConvention()
                                                                                 .HasPluralizeConvention()
                                                                                 .HasSingularizeConvention()
                                                                                 .HasUpperCaseConvention()
                                                                                 .HasConvention(new HashTagApiNamingConvention());

                                                       var apiScalarTypeNameConventions = apiConventionSetConfiguration.ApiScalarTypeNameConventions();
                                                       apiScalarTypeNameConventions.HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention())
                                                                                   .HasCamelCaseConvention()
                                                                                   .HasKebabCaseConvention()
                                                                                   .HasLowerCaseConvention()
                                                                                   .HasPascalCaseConvention()
                                                                                   .HasPluralizeConvention()
                                                                                   .HasSingularizeConvention()
                                                                                   .HasUpperCaseConvention()
                                                                                   .HasConvention(new HashTagApiNamingConvention());

                                                       var apiSchemaConventions = apiConventionSetConfiguration.ApiSchemaConventions();
                                                       apiSchemaConventions.HasAnnotationDiscoveryConvention()
                                                                           .HasConfigurationDiscoveryConvention()
                                                                           .HasTypeDiscoveryConvention()
                                                                           .HasConvention(new SetNameNullApiSchemaConvention())
                                                                           .HasAnnotationDiscoveryConvention()
                                                                           .HasConfigurationDiscoveryConvention()
                                                                           .HasTypeDiscoveryConvention()
                                                                           .HasConvention(new SetNameNullApiSchemaConvention());


                                                       return apiConventionSetConfiguration;
                                                   },
                                                   new ApiConventionSet(
                                                       new List<IApiEnumerationTypeConvention>
                                                       {
                                                           new ApiAnnotationDiscoveryEnumTypeConvention(),
                                                           new ApiEnumerationValueDiscoveryEnumTypeConvention(),
                                                           new SetDescriptionNullApiEnumerationTypeConvention(),
                                                           new SetDescriptionNullApiEnumerationTypeConvention(),
                                                       },
                                                       new List<IApiNamingConvention>()
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>()
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiObjectTypeConvention>
                                                       {
                                                           new ApiAnnotationDiscoveryObjectTypeConvention(),
                                                           new ApiIdentityDiscoveryObjectTypeConvention(),
                                                           new ApiPropertyDiscoveryObjectTypeConvention(),
                                                           new ApiRelationshipDiscoveryObjectTypeConvention(),
                                                           new SetDescriptionNullApiObjectTypeConvention(),
                                                           new SetDescriptionNullApiObjectTypeConvention()
                                                       },
                                                       new List<IApiNamingConvention>()
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>()
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiNamingConvention>()
                                                       {
                                                           new ApiCamelCaseNamingConvention(),
                                                           new ApiKebabCaseNamingConvention(),
                                                           new ApiLowerCaseNamingConvention(),
                                                           new ApiPascalCaseNamingConvention(),
                                                           new ApiPluralizeNamingConvention(),
                                                           new ApiSingularizeNamingConvention(),
                                                           new ApiUpperCaseNamingConvention(),
                                                           new HashTagApiNamingConvention(),
                                                           new HashTagApiNamingConvention()
                                                       },
                                                       new List<IApiSchemaConvention>
                                                       {
                                                           new ApiAnnotationDiscoverySchemaConvention(),
                                                           new ApiConfigurationDiscoverySchemaConvention(),
                                                           new ApiTypeDiscoverySchemaConvention(),
                                                           new SetNameNullApiSchemaConvention(),
                                                           new SetNameNullApiSchemaConvention()
                                                       }))
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Custom Conventions
        private class HashTagApiNamingConvention : IApiNamingConvention
        {
            public string Apply(string oldName, ApiConventionSettings apiConventionSettings)
            {
                var newName = $"#{oldName ?? String.Empty}";
                return newName;
            }
        }

        private class SetDescriptionNullApiEnumerationTypeConvention : IApiEnumerationTypeConvention
        {
            public void Apply(IApiEnumerationTypeBuilder apiEnumerationTypeBuilder, ApiConventionSettings apiConventionSettings)
            {
                apiEnumerationTypeBuilder.HasDescription(null);
            }
        }

        private class SetDescriptionNullApiObjectTypeConvention : IApiObjectTypeConvention
        {
            public void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings)
            {
                apiObjectTypeBuilder.HasDescription(null);
            }
        }

        private class SetNameNullApiSchemaConvention : IApiSchemaConvention
        {
            public void Apply(IApiSchemaBuilder apiSchemaBuilder, ApiConventionSettings apiConventionSettings)
            {
                apiSchemaBuilder.HasName(null);
            }
        }
        #endregion

        #region Unit Tests
        private class CreateApiConventionSetUnitTest : XUnitTest
        {
            #region Constructors
            public CreateApiConventionSetUnitTest(string name, Func<ApiConventionSetConfiguration> apiConventionSetFactory, IApiConventionSet expectedApiConventionSet)
                : base(name)
            {
                this.ApiConventionSetFactory  = apiConventionSetFactory;
                this.ExpectedApiConventionSet = expectedApiConventionSet;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Expected API Convention Set");
                this.WriteLine(this.ExpectedApiConventionSet.ToString());
            }

            protected override void Act()
            {
                var actualApiConventionSet = this.ApiConventionSetFactory().Create();
                this.ActualApiConventionSet = actualApiConventionSet;

                this.WriteLine();

                this.WriteLine("Actual API Convention Set");
                this.WriteLine(this.ActualApiConventionSet.ToString());
            }

            protected override void Assert()
            {
                var actualApiConventionSet   = this.ActualApiConventionSet;
                var expectedApiConventionSet = this.ExpectedApiConventionSet;
                actualApiConventionSet.Should().BeEquivalentTo(expectedApiConventionSet, options => options.IncludingAllRuntimeProperties());
            }
            #endregion

            #region User Supplied Properties
            private Func<ApiConventionSetConfiguration> ApiConventionSetFactory  { get; }
            private IApiConventionSet                   ExpectedApiConventionSet { get; }
            #endregion

            #region Calculated Properties
            private IApiConventionSet ActualApiConventionSet { get; set; }
            #endregion
        }
        #endregion
    }
}