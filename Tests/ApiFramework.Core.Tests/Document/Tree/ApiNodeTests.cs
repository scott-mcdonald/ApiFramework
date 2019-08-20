// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Document.Tree
{
    public class ApiNodeTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiNodeTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(GetDocumentPathTestData))]
        public void TestGetDocumentPath(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GetDocumentPathStringTestData))]
        public void TestGetDocumentPathString(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> GetDocumentPathTestData = new[]
                                                                               {
                                                                                   new object[] {new GetDocumentPathUnitTest("WithEmptyPath(s)", () => ApiDocumentNode.Create(), Enumerable.Empty<ApiPathMixin>)},

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiNullNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data")
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "MailingAddresses",
                                                                                                                                                                     ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street"))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("StreetAddress")
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndPropertyAndPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "People",
                                                                                                                                                                     ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("MailingAddress"), "MailingAddresses",
                                                                                                                                                                                          ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street")))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("MailingAddress"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("StreetAddress")
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndPropertyAndPropertyAndPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "Articles",
                                                                                                                                                                     ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Author"), "People",
                                                                                                                                                                                          ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("MailingAddress"), "MailingAddresses",
                                                                                                                                                                                                               ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street"))))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Author"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("MailingAddress"),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("StreetAddress")
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndCollectionItemPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                         ApiScalarNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "John"))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0)
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndCollectionItemAndPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                         ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                              ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"))))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndCollectionItemAndPropertyAndCollectionItemPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                         ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                              ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                                                                                                                       ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Comments"))))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0),
                                                                                                                         })
                                                                                   },

                                                                                   new object[]
                                                                                   {
                                                                                       new GetDocumentPathUnitTest("WithPropertyAndCollectionItemAndPropertyAndCollectionItemAndPropertyPath(s)",
                                                                                                                   () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                         ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                              ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                                                                                                                       ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Comments",
                                                                                                                                                                                                                                            ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("Body"), "Article sucked.")))))),
                                                                                                                   () => new List<ApiPathMixin>
                                                                                                                         {
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                             ApiPathMixin.CreateCollectionItemPathMixin(0),
                                                                                                                             ApiPathMixin.CreatePropertyPathMixin("Body")
                                                                                                                         })
                                                                                   },
                                                                               };

        public static readonly IEnumerable<object[]> GetDocumentPathStringTestData = new[]
                                                                                     {
                                                                                         new object[] {new GetDocumentPathStringUnitTest("WithEmptyPath(s)", () => ApiDocumentNode.Create(), () => String.Empty)},

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiNullNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"))),
                                                                                                                               () => "Data")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "MailingAddresses",
                                                                                                                                                                                 ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street"))),
                                                                                                                               () => "Data.StreetAddress")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndPropertyAndPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "People",
                                                                                                                                                                                 ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("MailingAddress"), "MailingAddresses",
                                                                                                                                                                                                      ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street")))),
                                                                                                                               () => "Data.MailingAddress.StreetAddress")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndPropertyAndPropertyAndPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"), "Articles",
                                                                                                                                                                                 ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("Author"), "People",
                                                                                                                                                                                                      ApiObjectNode.Create(ApiPathMixin.CreatePropertyPathMixin("MailingAddress"), "MailingAddresses",
                                                                                                                                                                                                                           ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("StreetAddress"), "123 Main Street"))))),
                                                                                                                               () => "Data.Author.MailingAddress.StreetAddress")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndCollectionItemPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                                     ApiScalarNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "John"))),
                                                                                                                               () => "Data[0]")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndCollectionItemAndPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                                     ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                                          ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"))))),
                                                                                                                               () => "Data[0].Comments")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndCollectionItemAndPropertyAndCollectionItemPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                                     ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                                          ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                                                                                                                                   ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Comments"))))),
                                                                                                                               () => "Data[0].Comments[0]")
                                                                                         },

                                                                                         new object[]
                                                                                         {
                                                                                             new GetDocumentPathStringUnitTest("WithPropertyAndCollectionItemAndPropertyAndCollectionItemAndPropertyPath(s)",
                                                                                                                               () => ApiDocumentNode.Create(ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Data"),
                                                                                                                                                                                     ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Articles",
                                                                                                                                                                                                          ApiCollectionNode.Create(ApiPathMixin.CreatePropertyPathMixin("Comments"),
                                                                                                                                                                                                                                   ApiObjectNode.Create(ApiPathMixin.CreateCollectionItemPathMixin(0), "Comments",
                                                                                                                                                                                                                                                        ApiScalarNode.Create(ApiPathMixin.CreatePropertyPathMixin("Body"), "Article sucked.")))))),
                                                                                                                               () => "Data[0].Comments[0].Body")
                                                                                         },
                                                                                     };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class GetDocumentPathUnitTest : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetDocumentPathUnitTest(string                          name,
                                           Func<ApiDocumentNode>           apiDocumentNodeFactory,
                                           Func<IEnumerable<ApiPathMixin>> expectedApiDocumentPathFactory)
                : base(name)
            {
                this.ApiDocumentNodeFactory         = apiDocumentNodeFactory;
                this.ExpectedApiDocumentPathFactory = expectedApiDocumentPathFactory;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var apiDocumentNode         = this.ApiDocumentNodeFactory();
                var expectedApiDocumentPath = this.ExpectedApiDocumentPathFactory();

                var apiNode = (ApiNode)apiDocumentNode;
                while (apiNode.FirstNode != null)
                {
                    apiNode = apiNode.FirstNode;
                }

                this.ApiNode                 = apiNode;
                this.ExpectedApiDocumentPath = expectedApiDocumentPath;

                this.WriteLine("API Document Tree");
                this.WriteLine($"{apiDocumentNode.ToTreeString()}");
                this.WriteLine();

                this.WriteLine("API Node");
                this.WriteLine($"{apiNode.Name}");
                this.WriteLine();

                this.WriteLine("Expected API Document Path");
                this.WriteApiDocumentPath(this.ExpectedApiDocumentPath);
                this.WriteLine();
            }

            protected override void Act()
            {
                var actualApiDocumentPath = this.ApiNode.GetDocumentPath();
                this.ActualApiDocumentPath = actualApiDocumentPath;

                this.WriteLine("Actual   API Document Path");
                this.WriteApiDocumentPath(this.ActualApiDocumentPath);
            }

            protected override void Assert()
            {
                this.ActualApiDocumentPath.Should().BeEquivalentTo(this.ExpectedApiDocumentPath);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ApiNode                   ApiNode                 { get; set; }
            private IEnumerable<ApiPathMixin> ExpectedApiDocumentPath { get; set; }
            private IEnumerable<ApiPathMixin> ActualApiDocumentPath   { get; set; }
            #endregion

            #region User Supplied Properties
            private Func<ApiDocumentNode>           ApiDocumentNodeFactory         { get; }
            private Func<IEnumerable<ApiPathMixin>> ExpectedApiDocumentPathFactory { get; }
            #endregion

            // PRIVATE METHODS //////////////////////////////////////////////
            #region Methods
            private void WriteApiDocumentPath(IEnumerable<ApiPathMixin> apiDocumentPath)
            {
                var index = 0;
                foreach (var apiNodeMixin in apiDocumentPath)
                {
                    this.WriteLine($"[{index++}] {apiNodeMixin}");
                }
            }
            #endregion
        }

        private class GetDocumentPathStringUnitTest : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public GetDocumentPathStringUnitTest(string                name,
                                                 Func<ApiDocumentNode> apiDocumentNodeFactory,
                                                 Func<string>          expectedApiDocumentPathStringFactory)
                : base(name)
            {
                this.ApiDocumentNodeFactory               = apiDocumentNodeFactory;
                this.ExpectedApiDocumentPathStringFactory = expectedApiDocumentPathStringFactory;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var apiDocumentNode               = this.ApiDocumentNodeFactory();
                var expectedApiDocumentPathString = this.ExpectedApiDocumentPathStringFactory();

                var apiNode = (ApiNode)apiDocumentNode;
                while (apiNode.FirstNode != null)
                {
                    apiNode = apiNode.FirstNode;
                }

                this.ApiNode                       = apiNode;
                this.ExpectedApiDocumentPathString = expectedApiDocumentPathString;

                this.WriteLine("API Document Tree");
                this.WriteLine($"{apiDocumentNode.ToTreeString()}");
                this.WriteLine();

                this.WriteLine("API Node");
                this.WriteLine($"{apiNode.Name}");
                this.WriteLine();

                this.WriteLine("Expected API Document Path String");
                this.WriteLine(this.ExpectedApiDocumentPathString);
                this.WriteLine();
            }

            protected override void Act()
            {
                var actualApiDocumentPathString = this.ApiNode.GetDocumentPathString();
                this.ActualApiDocumentPathString = actualApiDocumentPathString;

                this.WriteLine("Actual   API Document Path String");
                this.WriteLine(this.ActualApiDocumentPathString);
            }

            protected override void Assert()
            {
                this.ActualApiDocumentPathString.Should().Be(this.ExpectedApiDocumentPathString);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ApiNode ApiNode                       { get; set; }
            private string  ExpectedApiDocumentPathString { get; set; }
            private string  ActualApiDocumentPathString   { get; set; }
            #endregion

            #region User Supplied Properties
            private Func<ApiDocumentNode> ApiDocumentNodeFactory               { get; }
            private Func<string>          ExpectedApiDocumentPathStringFactory { get; }
            #endregion
        }
        #endregion
    }
}
