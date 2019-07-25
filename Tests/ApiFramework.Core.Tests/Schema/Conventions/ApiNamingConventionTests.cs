// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using ApiFramework.Schema.Conventions.Internal;
using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Schema.Conventions
{
    public class ApiNamingConventionTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiNamingConventionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ApplyApiNamingConventionTestData))]
        public void TestApplyApiNamingConvention(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IApiNamingConvention ApiCamelCaseNamingConvention = new ApiCamelCaseNamingConvention();
        private static readonly IApiNamingConvention ApiKebabCaseNamingConvention = new ApiKebabCaseNamingConvention();
        private static readonly IApiNamingConvention ApiLowerCaseNamingConvention = new ApiLowerCaseNamingConvention();
        private static readonly IApiNamingConvention ApiPascalCaseNamingConvention = new ApiPascalCaseNamingConvention();
        private static readonly IApiNamingConvention ApiPluralizeNamingConvention = new ApiPluralizeNamingConvention();
        private static readonly IApiNamingConvention ApiSingularizeNamingConvention = new ApiSingularizeNamingConvention();
        private static readonly IApiNamingConvention ApiUpperCaseNamingConvention = new ApiUpperCaseNamingConvention();
        private static readonly IApiNamingConvention ApiCustomNamingConvention = new HashTagApiNamingConvention();

        public static readonly IEnumerable<object[]> ApplyApiNamingConventionTestData = new[]
        {
            // CamelCaseNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameNull", ApiCamelCaseNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameEmpty", ApiCamelCaseNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameAsName", ApiCamelCaseNamingConvention, "Name", "name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameAsUserName", ApiCamelCaseNamingConvention, "UserName", "userName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameAsUserNameWithUnderscore", ApiCamelCaseNamingConvention, "User_Name", "userName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameAsPreviousUserName", ApiCamelCaseNamingConvention, "PreviousUserName", "previousUserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCamelCaseAndOldNameAsPreviousUserNameWithUnderscore", ApiCamelCaseNamingConvention, "Previous_User_Name", "previousUserName") },

            // KebabCaseNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameNull", ApiKebabCaseNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameEmpty", ApiKebabCaseNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameAsName", ApiKebabCaseNamingConvention, "Name", "name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameAsUserName", ApiKebabCaseNamingConvention, "UserName", "user-name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameAsUserNameWithUnderscore", ApiKebabCaseNamingConvention, "UserName", "user-name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameAsPreviousUserName", ApiKebabCaseNamingConvention, "PreviousUserName", "previous-user-name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithKebabCaseAndOldNameAsPreviousUserNameWithUnderscore", ApiKebabCaseNamingConvention, "Previous_User_Name", "previous-user-name") },

            // LowerCaseNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameNull", ApiLowerCaseNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameEmpty", ApiLowerCaseNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameAsName", ApiLowerCaseNamingConvention, "Name", "name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameAsUserName", ApiLowerCaseNamingConvention, "UserName", "username") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameAsUserNameWithUnderscore", ApiLowerCaseNamingConvention, "User_Name", "user_name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameAsPreviousUserName", ApiLowerCaseNamingConvention, "PreviousUserName", "previoususername") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithLowerCaseAndOldNameAsPreviousUserNameWithUnderscore", ApiLowerCaseNamingConvention, "Previous_User_Name", "previous_user_name") },

            // PascalCaseNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameNull", ApiPascalCaseNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameEmpty", ApiPascalCaseNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameAsName", ApiPascalCaseNamingConvention, "name", "Name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameAsUserName", ApiPascalCaseNamingConvention, "userName", "UserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameAsUserNameWithUnderscore", ApiPascalCaseNamingConvention, "user_name", "UserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameAsPreviousUserName", ApiPascalCaseNamingConvention, "previousUserName", "PreviousUserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPascalCaseAndOldNameAsPreviousUserNameWithUnderscore", ApiPascalCaseNamingConvention, "previous_user_name", "PreviousUserName") },

            // PluralNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameNull", ApiPluralizeNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameEmpty", ApiPluralizeNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsArticle", ApiPluralizeNamingConvention, "Article", "Articles") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsArticles", ApiPluralizeNamingConvention, "Articles", "Articles") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsMan", ApiPluralizeNamingConvention, "Man", "Men") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsMen", ApiPluralizeNamingConvention, "Men", "Men") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsPerson", ApiPluralizeNamingConvention, "Person", "People") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsPeople", ApiPluralizeNamingConvention, "People", "People") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsStatus", ApiPluralizeNamingConvention, "Status", "Statuses") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsStatuses", ApiPluralizeNamingConvention, "Statuses", "Statuses") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyArticle", ApiPluralizeNamingConvention, "TheOneAndOnlyArticle", "TheOneAndOnlyArticles") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyArticles", ApiPluralizeNamingConvention, "TheOneAndOnlyArticles", "TheOneAndOnlyArticles") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyMan", ApiPluralizeNamingConvention, "TheOneAndOnlyMan", "TheOneAndOnlyMen") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyMen", ApiPluralizeNamingConvention, "TheOneAndOnlyMen", "TheOneAndOnlyMen") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyPerson", ApiPluralizeNamingConvention, "TheOneAndOnlyPerson", "TheOneAndOnlyPeople") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyPeople", ApiPluralizeNamingConvention, "TheOneAndOnlyPeople", "TheOneAndOnlyPeople") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyStatus", ApiPluralizeNamingConvention, "TheOneAndOnlyStatus", "TheOneAndOnlyStatuses") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithPluralizeAndOldNameAsTheOneAndOnlyStatuses", ApiPluralizeNamingConvention, "TheOneAndOnlyStatuses", "TheOneAndOnlyStatuses") },

            // SingularNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameNull", ApiSingularizeNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameEmpty", ApiSingularizeNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsArticle", ApiSingularizeNamingConvention, "Article", "Article") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsArticles", ApiSingularizeNamingConvention, "Articles", "Article") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsMan", ApiSingularizeNamingConvention, "Man", "Man") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsMen", ApiSingularizeNamingConvention, "Men", "Man") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsPerson", ApiSingularizeNamingConvention, "Person", "Person") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsPeople", ApiSingularizeNamingConvention, "People", "Person") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsStatus", ApiSingularizeNamingConvention, "Status", "Status") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsStatuses", ApiSingularizeNamingConvention, "Statuses", "Status") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyArticle", ApiSingularizeNamingConvention, "TheOneAndOnlyArticle", "TheOneAndOnlyArticle") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyArticles", ApiSingularizeNamingConvention, "TheOneAndOnlyArticles", "TheOneAndOnlyArticle") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyMan", ApiSingularizeNamingConvention, "TheOneAndOnlyMan", "TheOneAndOnlyMan") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyMen", ApiSingularizeNamingConvention, "TheOneAndOnlyMen", "TheOneAndOnlyMan") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyPerson", ApiSingularizeNamingConvention, "TheOneAndOnlyPerson", "TheOneAndOnlyPerson") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyPeople", ApiSingularizeNamingConvention, "TheOneAndOnlyPeople", "TheOneAndOnlyPerson") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyStatus", ApiSingularizeNamingConvention, "TheOneAndOnlyStatus", "TheOneAndOnlyStatus") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithSingularizeAndOldNameAsTheOneAndOnlyStatuses", ApiSingularizeNamingConvention, "TheOneAndOnlyStatuses", "TheOneAndOnlyStatus") },

            // UpperCaseNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameNull", ApiUpperCaseNamingConvention, null, null) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameEmpty", ApiUpperCaseNamingConvention, String.Empty, String.Empty) },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameAsName", ApiUpperCaseNamingConvention, "Name", "NAME") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameAsUserName", ApiUpperCaseNamingConvention, "UserName", "USERNAME") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameAsUserNameWithUnderscore", ApiUpperCaseNamingConvention, "User_Name", "USER_NAME") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameAsPreviousUserName", ApiUpperCaseNamingConvention, "PreviousUserName", "PREVIOUSUSERNAME") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithUpperCaseAndOldNameAsPreviousUserNameWithUnderscore", ApiUpperCaseNamingConvention, "Previous_User_Name", "PREVIOUS_USER_NAME") },

            // CustomNamingConvention
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameNull", ApiCustomNamingConvention, null, "#") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameEmpty", ApiCustomNamingConvention, String.Empty, "#") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameAsName", ApiCustomNamingConvention, "Name", "#Name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameAsUserName", ApiCustomNamingConvention, "UserName", "#UserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameAsUserNameWithUnderscore", ApiCustomNamingConvention, "User_Name", "#User_Name") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameAsPreviousUserName", ApiCustomNamingConvention, "PreviousUserName", "#PreviousUserName") },
            new object[] { new ApplyApiNamingConventionUnitTest("WithCustomAndOldNameAsPreviousUserNameWithUnderscore", ApiCustomNamingConvention, "Previous_User_Name", "#Previous_User_Name") },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Custom Naming Conventions
        private class HashTagApiNamingConvention : IApiNamingConvention
        {
            public string Apply(string oldName, ApiConventionSettings apiConventionSettings)
            {
                var newName = $"#{oldName ?? String.Empty}";
                return newName;
            }
        }
        #endregion

        #region Unit Tests
        private class ApplyApiNamingConventionUnitTest : XUnitTest
        {
            #region Constructors
            public ApplyApiNamingConventionUnitTest(string name, IApiNamingConvention apiNamingConvention, string oldName, string expectedNewName)
                : base(name)
            {
                this.ApiNamingConvention = apiNamingConvention;
                this.OldName = oldName;
                this.ExpectedNewName = expectedNewName;
            }
            #endregion

            #region XUnitTest Overrides
            protected override void Arrange()
            {
                this.WriteLine("Old Name");
                this.WriteLine(this.OldName ?? NullStringDescription);

                this.WriteLine();

                this.WriteLine("Expected New Name");
                this.WriteLine(this.ExpectedNewName ?? NullStringDescription);
            }

            protected override void Act()
            {
                var actualNewName = this.ApiNamingConvention.Apply(this.OldName, null);
                this.ActualNewName = actualNewName;

                this.WriteLine();

                this.WriteLine("Actual New Name");
                this.WriteLine(actualNewName ?? NullStringDescription);
            }

            protected override void Assert()
            {
                this.ActualNewName.Should().Be(this.ExpectedNewName);
            }
            #endregion

            #region User Supplied Properties
            private IApiNamingConvention ApiNamingConvention { get; }
            private string OldName { get; }
            private string ExpectedNewName { get; }
            #endregion

            #region Calculated Properties
            private string ActualNewName { get; set; }
            private static readonly string NullStringDescription = "<null>";
            #endregion
        }
        #endregion
    }
}
