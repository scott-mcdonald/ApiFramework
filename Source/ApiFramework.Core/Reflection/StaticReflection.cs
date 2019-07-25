// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace ApiFramework.Reflection
{
    /// <summary>Static reflection class to get property or method names at compile time.</summary>
    public static class StaticReflection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T, TProperty>(this T instance, Expression<Func<T, TProperty>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            Contract.Requires(expression != null);

            return GetMemberName(expression.Body);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string GetMemberName(Expression expression)
        {
            Contract.Requires(expression != null);

            switch (expression)
            {
                case MemberExpression memberExpression:
                    // Reference type property or field
                    return memberExpression.Member.Name;
                case MethodCallExpression methodCallExpression:
                    // Reference type method
                    return methodCallExpression.Method.Name;
                case UnaryExpression unaryExpression:
                    // Property, field of method returning value type
                    return GetMemberName(unaryExpression);
                default:
                    throw new ArgumentException("Invalid expression, must be either a MemberExpression, MethodCallExpression, or UnaryExpression.");
            }
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            Contract.Requires(unaryExpression != null);

            return unaryExpression.Operand is MethodCallExpression operand
                ? operand.Method.Name
                : ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
        #endregion
    }
}