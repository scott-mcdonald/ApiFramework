// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ApiFramework.XUnit;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace ApiFramework.Document
{
    public class ApiResourceIdentifierTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(EqualsMethodTestData))]
        public void TestEqualsMethod(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(EqualsOperatorTestData))]
        public void TestEqualsOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(NotEqualsOperatorTestData))]
        public void TestNotEqualsOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(CompareToMethodTestData))]
        public void TestCompareToMethod(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LessThanOperatorTestData))]
        public void TestLessThanOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LessThanOrEqualToOperatorTestData))]
        public void TestLessThanOrEqualToOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GreaterThanOperatorTestData))]
        public void TestGreaterThanOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(GreaterThanOrEqualToOperatorTestData))]
        public void TestGreaterThanOrEqualToOperator(IXUnitTest unitTest)
        {
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> EqualsMethodExpression      = (lhs, rhs) => lhs.Equals(rhs);
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> EqualsOperatorExpression    = (lhs, rhs) => lhs == rhs;
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> NotEqualsOperatorExpression = (lhs, rhs) => lhs != rhs;

        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, int>>  CompareToMethodExpression              = (lhs, rhs) => lhs.CompareTo(rhs);
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> LessThanOperatorExpression             = (lhs, rhs) => lhs < rhs;
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> LessThanOrEqualToOperatorExpression    = (lhs, rhs) => lhs <= rhs;
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> GreaterThanOperatorExpression          = (lhs, rhs) => lhs > rhs;
        private static readonly Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> GreaterThanOrEqualToOperatorExpression = (lhs, rhs) => lhs >= rhs;

        public static readonly IEnumerable<object[]> EqualsMethodTestData = new[]
                                                                            {
                                                                                new object[] {new PredicateOperationUnitTest("WithRhsNull",                            () => new ApiResourceIdentifier<int>("people", 42), () => default(ApiResourceIdentifier),                 EqualsMethodExpression, false)},
                                                                                new object[] {new PredicateOperationUnitTest("WithRhsEqualApiTypeAndEqualApiId",       () => new ApiResourceIdentifier<int>("people", 42), () => new ApiResourceIdentifier<int>("people",   42), EqualsMethodExpression, true)},
                                                                                new object[] {new PredicateOperationUnitTest("WithRhsEqualApiTypeAndNotEqualApiId",    () => new ApiResourceIdentifier<int>("people", 42), () => new ApiResourceIdentifier<int>("people",   24), EqualsMethodExpression, false)},
                                                                                new object[] {new PredicateOperationUnitTest("WithRhsNotEqualApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("people", 42), () => new ApiResourceIdentifier<int>("comments", 42), EqualsMethodExpression, false)},
                                                                                new object[] {new PredicateOperationUnitTest("WithRhsNotEqualApiTypeAndNotEqualApiId", () => new ApiResourceIdentifier<int>("people", 42), () => new ApiResourceIdentifier<int>("comments", 24), EqualsMethodExpression, false)},
                                                                            };

        public static readonly IEnumerable<object[]> EqualsOperatorTestData = new[]
                                                                              {
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                               () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               EqualsOperatorExpression,                             true)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                            () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), EqualsOperatorExpression,                             false)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                            () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 EqualsOperatorExpression, false)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",       () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("people",   42), EqualsOperatorExpression, true)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndNotEqualApiId",    () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("people",   24), EqualsOperatorExpression, false)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNotEqualApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("comments", 42), EqualsOperatorExpression, false)},
                                                                                  new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNotEqualApiTypeAndNotEqualApiId", () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("comments", 24), EqualsOperatorExpression, false)},
                                                                              };

        public static readonly IEnumerable<object[]> NotEqualsOperatorTestData = new[]
                                                                                 {
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                               () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               NotEqualsOperatorExpression,                          false)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                            () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), NotEqualsOperatorExpression,                          true)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                            () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 NotEqualsOperatorExpression, true)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",       () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("people",   42), NotEqualsOperatorExpression, false)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndNotEqualApiId",    () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("people",   24), NotEqualsOperatorExpression, true)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNotEqualApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("comments", 42), NotEqualsOperatorExpression, true)},
                                                                                     new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNotEqualApiTypeAndNotEqualApiId", () => new ApiResourceIdentifier<int>("people",                                       42), () => new ApiResourceIdentifier<int>("comments", 24), NotEqualsOperatorExpression, true)},
                                                                                 };

        public static readonly IEnumerable<object[]> CompareToMethodTestData = new[]
                                                                               {
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsNull",                          () => new ApiResourceIdentifier<int>("comments", 42), () => default(ApiResourceIdentifier),                 CompareToMethodExpression, +1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsLesserApiTypeAndLesserApiId",   () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("articles", 24), CompareToMethodExpression, +1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsLesserApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("articles", 42), CompareToMethodExpression, +1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsLesserApiTypeAndGreaterApiId",  () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("articles", 68), CompareToMethodExpression, +1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsEqualApiTypeAndLesserApiId",    () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("comments", 24), CompareToMethodExpression, +1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsEqualApiTypeAndEqualApiId",     () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("comments", 42), CompareToMethodExpression, 0)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsEqualApiTypeAndGreaterApiId",   () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("comments", 68), CompareToMethodExpression, -1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsGreaterApiTypeAndLesserApiId",  () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("people",   24), CompareToMethodExpression, -1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsGreaterApiTypeAndEqualApiId",   () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("people",   42), CompareToMethodExpression, -1)},
                                                                                   new object[] {new CompareOperationUnitTest("WithRhsGreaterApiTypeAndGreaterApiId", () => new ApiResourceIdentifier<int>("comments", 42), () => new ApiResourceIdentifier<int>("people",   68), CompareToMethodExpression, -1)},
                                                                               };

        public static readonly IEnumerable<object[]> LessThanOperatorTestData = new[]
                                                                                {
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                             () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               LessThanOperatorExpression,                           false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                          () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), LessThanOperatorExpression,                           true)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                          () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndLesserApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 24), LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 42), LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndGreaterApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 68), LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndLesserApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 24), LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",     () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 42), LessThanOperatorExpression, false)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndGreaterApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 68), LessThanOperatorExpression, true)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndLesserApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   24), LessThanOperatorExpression, true)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndEqualApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   42), LessThanOperatorExpression, true)},
                                                                                    new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndGreaterApiId", () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   68), LessThanOperatorExpression, true)},
                                                                                };

        public static readonly IEnumerable<object[]> LessThanOrEqualToOperatorTestData = new[]
                                                                                         {
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                             () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               LessThanOrEqualToOperatorExpression,                  true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                          () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), LessThanOrEqualToOperatorExpression,                  true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                          () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 LessThanOrEqualToOperatorExpression, false)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndLesserApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 24), LessThanOrEqualToOperatorExpression, false)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 42), LessThanOrEqualToOperatorExpression, false)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndGreaterApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 68), LessThanOrEqualToOperatorExpression, false)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndLesserApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 24), LessThanOrEqualToOperatorExpression, false)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",     () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 42), LessThanOrEqualToOperatorExpression, true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndGreaterApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 68), LessThanOrEqualToOperatorExpression, true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndLesserApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   24), LessThanOrEqualToOperatorExpression, true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndEqualApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   42), LessThanOrEqualToOperatorExpression, true)},
                                                                                             new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndGreaterApiId", () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   68), LessThanOrEqualToOperatorExpression, true)},
                                                                                         };

        public static readonly IEnumerable<object[]> GreaterThanOperatorTestData = new[]
                                                                                   {
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                             () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               GreaterThanOperatorExpression,                        false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                          () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), GreaterThanOperatorExpression,                        false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                          () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 GreaterThanOperatorExpression, true)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndLesserApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 24), GreaterThanOperatorExpression, true)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 42), GreaterThanOperatorExpression, true)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndGreaterApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 68), GreaterThanOperatorExpression, true)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndLesserApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 24), GreaterThanOperatorExpression, true)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",     () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 42), GreaterThanOperatorExpression, false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndGreaterApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 68), GreaterThanOperatorExpression, false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndLesserApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   24), GreaterThanOperatorExpression, false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndEqualApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   42), GreaterThanOperatorExpression, false)},
                                                                                       new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndGreaterApiId", () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   68), GreaterThanOperatorExpression, false)},
                                                                                   };

        public static readonly IEnumerable<object[]> GreaterThanOrEqualToOperatorTestData = new[]
                                                                                            {
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNull",                             () => default(ApiResourceIdentifier), () => default(ApiResourceIdentifier),               GreaterThanOrEqualToOperatorExpression,               true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNullAndRhsNotNull",                          () => default(ApiResourceIdentifier), () => new ApiResourceIdentifier<int>("people", 42), GreaterThanOrEqualToOperatorExpression,               false)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsNull",                          () => new ApiResourceIdentifier<int>("people",                                       42), () => default(ApiResourceIdentifier),                 GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndLesserApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 24), GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndEqualApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 42), GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsLesserApiTypeAndGreaterApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("articles", 68), GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndLesserApiId",    () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 24), GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndEqualApiId",     () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 42), GreaterThanOrEqualToOperatorExpression, true)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsEqualApiTypeAndGreaterApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("comments", 68), GreaterThanOrEqualToOperatorExpression, false)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndLesserApiId",  () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   24), GreaterThanOrEqualToOperatorExpression, false)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndEqualApiId",   () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   42), GreaterThanOrEqualToOperatorExpression, false)},
                                                                                                new object[] {new PredicateOperationUnitTest("WithLhsNotNullAndRhsGreaterApiTypeAndGreaterApiId", () => new ApiResourceIdentifier<int>("comments",                                     42), () => new ApiResourceIdentifier<int>("people",   68), GreaterThanOrEqualToOperatorExpression, false)},
                                                                                            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Test Types
        private class CompareOperationUnitTest : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public CompareOperationUnitTest(string                                                              name,
                                            Func<ApiResourceIdentifier>                                         lhsFactory,
                                            Func<ApiResourceIdentifier>                                         rhsFactory,
                                            Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, int>> operationExpression,
                                            int                                                                 expectedResult)
                : base(name)
            {
                this.LhsFactory          = lhsFactory;
                this.RhsFactory          = rhsFactory;
                this.OperationExpression = operationExpression;
                this.ExpectedResult      = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var lhs       = this.LhsFactory();
                var rhs       = this.RhsFactory();
                var operation = this.OperationExpression.Compile();

                this.Lhs       = lhs;
                this.Rhs       = rhs;
                this.Operation = operation;

                var lhsString = this.Lhs != null ? this.Lhs.ToString() : "null";
                var rhsString = this.Rhs != null ? this.Rhs.ToString() : "null";

                this.WriteLine($"LHS = {lhsString}");
                this.WriteLine($"RHS = {rhsString}");
                this.WriteLine();

                this.WriteLine($"Operation = {this.OperationExpression}");
                this.WriteLine();


                this.WriteLine($"Expected Result = {this.ExpectedResult}");
                this.WriteLine();
            }

            protected override void Act()
            {
                this.ActualResult = this.Operation(this.Lhs, this.Rhs);

                this.WriteLine($"Actual   Result = {this.ActualResult}");
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ApiResourceIdentifier                                   Lhs          { get; set; }
            private ApiResourceIdentifier                                   Rhs          { get; set; }
            private Func<ApiResourceIdentifier, ApiResourceIdentifier, int> Operation    { get; set; }
            private int                                                     ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private Func<ApiResourceIdentifier>                                         LhsFactory          { get; }
            private Func<ApiResourceIdentifier>                                         RhsFactory          { get; }
            private Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, int>> OperationExpression { get; }
            private int                                                                 ExpectedResult      { get; }
            #endregion
        }

        private class PredicateOperationUnitTest : XUnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public PredicateOperationUnitTest(string                                                               name,
                                              Func<ApiResourceIdentifier>                                          lhsFactory,
                                              Func<ApiResourceIdentifier>                                          rhsFactory,
                                              Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> operationExpression,
                                              bool                                                                 expectedResult)
                : base(name)
            {
                this.LhsFactory          = lhsFactory;
                this.RhsFactory          = rhsFactory;
                this.OperationExpression = operationExpression;
                this.ExpectedResult      = expectedResult;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region XUnitTest Overrides
            protected override void Arrange()
            {
                var lhs       = this.LhsFactory();
                var rhs       = this.RhsFactory();
                var operation = this.OperationExpression.Compile();

                this.Lhs       = lhs;
                this.Rhs       = rhs;
                this.Operation = operation;

                var lhsString = this.Lhs != null ? this.Lhs.ToString() : "null";
                var rhsString = this.Rhs != null ? this.Rhs.ToString() : "null";

                this.WriteLine($"LHS = {lhsString}");
                this.WriteLine($"RHS = {rhsString}");
                this.WriteLine();

                this.WriteLine($"Operation = {this.OperationExpression}");
                this.WriteLine();


                this.WriteLine($"Expected Result = {this.ExpectedResult}");
                this.WriteLine();
            }

            protected override void Act()
            {
                this.ActualResult = this.Operation(this.Lhs, this.Rhs);

                this.WriteLine($"Actual   Result = {this.ActualResult}");
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ApiResourceIdentifier                                    Lhs          { get; set; }
            private ApiResourceIdentifier                                    Rhs          { get; set; }
            private Func<ApiResourceIdentifier, ApiResourceIdentifier, bool> Operation    { get; set; }
            private bool                                                     ActualResult { get; set; }
            #endregion

            #region User Supplied Properties
            private Func<ApiResourceIdentifier>                                          LhsFactory          { get; }
            private Func<ApiResourceIdentifier>                                          RhsFactory          { get; }
            private Expression<Func<ApiResourceIdentifier, ApiResourceIdentifier, bool>> OperationExpression { get; }
            private bool                                                                 ExpectedResult      { get; }
            #endregion
        }
        #endregion
    }
}
