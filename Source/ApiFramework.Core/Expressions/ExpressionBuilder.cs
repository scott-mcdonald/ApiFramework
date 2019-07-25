﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using ApiFramework.Reflection;

namespace ApiFramework.Expressions
{
    /// <summary>
    /// Represents a utility like class to build useful expressions that can
    /// be compiled into lambdas for execution at runtime.
    /// </summary>
    /// <remarks>
    /// The compiled lambdas only need to be compiled once and thus can be
    /// cached for future use, this technique is much faster then reflection
    /// at runtime.
    /// </remarks>
    public static class ExpressionBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Call Methods
        // Instance - Method Calls
        public static Expression<Func<TObject, TResult>> MethodCall<TObject, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject>(methodName, out var instanceExpression);

            var lambdaExpression = Expression.Lambda<Func<TObject, TResult>>(callExpression, instanceExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TArgument, TResult>> MethodCall<TObject, TArgument, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject, TArgument>(methodName, out var instanceExpression, out var argumentExpression);

            var lambdaExpression = Expression.Lambda<Func<TObject, TArgument, TResult>>(callExpression, instanceExpression, argumentExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TObject, TArgument1, TArgument2, TResult>> MethodCall<TObject, TArgument1, TArgument2, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject, TArgument1, TArgument2>(methodName, out var instanceExpression, out var argument1Expression, out var argument2Expression);

            var lambdaExpression = Expression.Lambda<Func<TObject, TArgument1, TArgument2, TResult>>(callExpression, instanceExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }

        // Instance - Void Method Calls
        public static Expression<Action<TObject>> VoidMethodCall<TObject>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject>(methodName, out var instanceExpression);

            var lambdaExpression = Expression.Lambda<Action<TObject>>(callExpression, instanceExpression);
            return lambdaExpression;
        }

        public static Expression<Action<TObject, TArgument>> VoidMethodCall<TObject, TArgument>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject, TArgument>(methodName, out var instanceExpression, out var argumentExpression);

            var lambdaExpression = Expression.Lambda<Action<TObject, TArgument>>(callExpression, instanceExpression, argumentExpression);
            return lambdaExpression;
        }

        public static Expression<Action<TObject, TArgument1, TArgument2>> VoidMethodCall<TObject, TArgument1, TArgument2>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateMethodCallExpression<TObject, TArgument1, TArgument2>(methodName, out var instanceExpression, out var argument1Expression, out var argument2Expression);

            var lambdaExpression = Expression.Lambda<Action<TObject, TArgument1, TArgument2>>(callExpression, instanceExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }

        // Static - Method Calls
        public static Expression<Func<TResult>> StaticMethodCall<TClass, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass>(methodName);

            var lambdaExpression = Expression.Lambda<Func<TResult>>(callExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument, TResult>> StaticMethodCall<TClass, TArgument, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass, TArgument>(methodName, out var argumentExpression);

            var lambdaExpression = Expression.Lambda<Func<TArgument, TResult>>(callExpression, argumentExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TResult>> StaticMethodCall<TClass, TArgument1, TArgument2, TResult>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass, TArgument1, TArgument2>(methodName, out var argument1Expression, out var argument2Expression);

            var lambdaExpression = Expression.Lambda<Func<TArgument1, TArgument2, TResult>>(callExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }

        // Static - Void Method Calls
        public static Expression<Action> StaticVoidMethodCall<TClass>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass>(methodName);

            var lambdaExpression = Expression.Lambda<Action>(callExpression);
            return lambdaExpression;
        }

        public static Expression<Action<TArgument>> StaticVoidMethodCall<TClass, TArgument>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass, TArgument>(methodName, out var argumentExpression);

            var lambdaExpression = Expression.Lambda<Action<TArgument>>(callExpression, argumentExpression);
            return lambdaExpression;
        }

        public static Expression<Action<TArgument1, TArgument2>> StaticVoidMethodCall<TClass, TArgument1, TArgument2>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var callExpression = CreateStaticMethodCallExpression<TClass, TArgument1, TArgument2>(methodName, out var argument1Expression, out var argument2Expression);

            var lambdaExpression = Expression.Lambda<Action<TArgument1, TArgument2>>(callExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }
        #endregion

        #region Cast Methods
        public static Expression<Func<TFrom, TTo>> Cast<TFrom, TTo>()
        {
            var fromType                   = typeof(TFrom);
            var toType                     = typeof(TTo);
            var parameterExpression        = Expression.Parameter(fromType, "a");
            var parameterConvertExpression = Expression.ConvertChecked(parameterExpression, toType);
            var lambdaExpression           = Expression.Lambda<Func<TFrom, TTo>>(parameterConvertExpression, parameterExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TFrom, TTo>> CastAs<TFrom, TTo>()
        {
            var fromType                  = typeof(TFrom);
            var toType                    = typeof(TTo);
            var parameterExpression       = Expression.Parameter(fromType, "a");
            var parameterTypeAsExpression = Expression.TypeAs(parameterExpression, toType);
            var lambdaExpression          = Expression.Lambda<Func<TFrom, TTo>>(parameterTypeAsExpression, parameterExpression);
            return lambdaExpression;
        }
        #endregion

        #region Default Methods
        public static Expression<Func<TObject>> Default<TObject>()
        {
            var objectType        = typeof(TObject);
            var defaultExpression = Expression.Default(objectType);
            var lambdaExpression  = Expression.Lambda<Func<TObject>>(defaultExpression);
            return lambdaExpression;
        }
        #endregion

        #region New Methods
        public static Expression<Func<TObject>> New<TObject>(Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var newExpression    = Expression.New(objectType);
            var lambdaExpression = Expression.Lambda<Func<TObject>>(newExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument, TObject>> New<TArgument, TObject>(Type objectType = null)
        {
            var lambdaExpression = ExpressionBuilder.New<TArgument, TObject>(ReflectionFlags.Public, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument, TObject>> New<TArgument, TObject>(ReflectionFlags reflectionFlags, Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var argumentType       = typeof(TArgument);
            var argumentExpression = Expression.Parameter(argumentType, "a");
            var constructorInfo    = TypeReflection.GetConstructor(objectType, reflectionFlags, argumentType);
            var newExpression      = Expression.New(constructorInfo, argumentExpression);
            var lambdaExpression   = Expression.Lambda<Func<TArgument, TObject>>(newExpression, argumentExpression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TObject>> New<TArgument1, TArgument2, TObject>(Type objectType = null)
        {
            var lambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TObject>(ReflectionFlags.Public, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TObject>> New<TArgument1, TArgument2, TObject>(ReflectionFlags reflectionFlags, Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var argument1Type       = typeof(TArgument1);
            var argument2Type       = typeof(TArgument2);
            var argument1Expression = Expression.Parameter(argument1Type, "a");
            var argument2Expression = Expression.Parameter(argument2Type, "b");
            var constructorInfo     = TypeReflection.GetConstructor(objectType, reflectionFlags, argument1Type, argument2Type);
            var newExpression       = Expression.New(constructorInfo, argument1Expression, argument2Expression);
            var lambdaExpression    = Expression.Lambda<Func<TArgument1, TArgument2, TObject>>(newExpression, argument1Expression, argument2Expression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TObject>> New<TArgument1, TArgument2, TArgument3, TObject>(Type objectType = null)
        {
            var lambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TObject>(ReflectionFlags.Public, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TObject>> New<TArgument1, TArgument2, TArgument3, TObject>(ReflectionFlags reflectionFlags, Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var argument1Type       = typeof(TArgument1);
            var argument2Type       = typeof(TArgument2);
            var argument3Type       = typeof(TArgument3);
            var argument1Expression = Expression.Parameter(argument1Type, "a");
            var argument2Expression = Expression.Parameter(argument2Type, "b");
            var argument3Expression = Expression.Parameter(argument3Type, "c");
            var constructorInfo     = TypeReflection.GetConstructor(objectType, reflectionFlags, argument1Type, argument2Type, argument3Type);
            var newExpression       = Expression.New(constructorInfo, argument1Expression, argument2Expression, argument3Expression);
            var lambdaExpression    = Expression.Lambda<Func<TArgument1, TArgument2, TArgument3, TObject>>(newExpression, argument1Expression, argument2Expression, argument3Expression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TArgument4, TObject>> New<TArgument1, TArgument2, TArgument3, TArgument4, TObject>(Type objectType = null)
        {
            var lambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TArgument4, TObject>(ReflectionFlags.Public, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TArgument4, TObject>> New<TArgument1, TArgument2, TArgument3, TArgument4, TObject>(ReflectionFlags reflectionFlags, Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var argument1Type       = typeof(TArgument1);
            var argument2Type       = typeof(TArgument2);
            var argument3Type       = typeof(TArgument3);
            var argument4Type       = typeof(TArgument4);
            var argument1Expression = Expression.Parameter(argument1Type, "a");
            var argument2Expression = Expression.Parameter(argument2Type, "b");
            var argument3Expression = Expression.Parameter(argument3Type, "c");
            var argument4Expression = Expression.Parameter(argument4Type, "d");
            var constructorInfo     = TypeReflection.GetConstructor(objectType, reflectionFlags, argument1Type, argument2Type, argument3Type, argument4Type);
            var newExpression       = Expression.New(constructorInfo, argument1Expression, argument2Expression, argument3Expression, argument4Expression);
            var lambdaExpression    = Expression.Lambda<Func<TArgument1, TArgument2, TArgument3, TArgument4, TObject>>(newExpression, argument1Expression, argument2Expression, argument3Expression, argument4Expression);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>> New<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>(Type objectType = null)
        {
            var lambdaExpression = ExpressionBuilder.New<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>(ReflectionFlags.Public, objectType);
            return lambdaExpression;
        }

        public static Expression<Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>> New<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>(ReflectionFlags reflectionFlags, Type objectType = null)
        {
            objectType = objectType ?? typeof(TObject);
            var argument1Type       = typeof(TArgument1);
            var argument2Type       = typeof(TArgument2);
            var argument3Type       = typeof(TArgument3);
            var argument4Type       = typeof(TArgument4);
            var argument5Type       = typeof(TArgument5);
            var argument1Expression = Expression.Parameter(argument1Type, "a");
            var argument2Expression = Expression.Parameter(argument2Type, "b");
            var argument3Expression = Expression.Parameter(argument3Type, "c");
            var argument4Expression = Expression.Parameter(argument4Type, "d");
            var argument5Expression = Expression.Parameter(argument5Type, "e");
            var constructorInfo     = TypeReflection.GetConstructor(objectType, reflectionFlags, argument1Type, argument2Type, argument3Type, argument4Type, argument5Type);
            var newExpression       = Expression.New(constructorInfo, argument1Expression, argument2Expression, argument3Expression, argument4Expression, argument5Expression);
            var lambdaExpression    = Expression.Lambda<Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TObject>>(newExpression, argument1Expression, argument2Expression, argument3Expression, argument4Expression, argument5Expression);
            return lambdaExpression;
        }
        #endregion

        #region Property Methods
        // Instance - Property Getter
        public static Expression<Func<TObject, TProperty>> PropertyGetter<TObject, TProperty>(string propertyName)
        {
            Contract.Requires(propertyName.SafeHasContent());

            var type               = typeof(TObject);
            var instanceExpression = Expression.Parameter(type, "x");
            var propertyExpression = (Expression)instanceExpression;
            var propertyNameSplit  = propertyName.Split('.');
            foreach (var name in propertyNameSplit)
            {
                var propertyInfo     = TypeReflection.GetProperty(type, name, ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static);
                var isPropertyStatic = PropertyReflection.IsStatic(propertyInfo);
                propertyExpression = Expression.Property(!isPropertyStatic ? propertyExpression : null, propertyInfo);
                type               = propertyInfo.PropertyType;
            }

            var lambdaExpression = Expression.Lambda<Func<TObject, TProperty>>(propertyExpression, instanceExpression);
            return lambdaExpression;
        }

        // Instance - Property Setter
        public static Expression<Action<TObject, TProperty>> PropertySetter<TObject, TProperty>(string propertyName)
        {
            Contract.Requires(propertyName.SafeHasContent());

            var type               = typeof(TObject);
            var propertyType       = typeof(TProperty);
            var instanceExpression = Expression.Parameter(type,         "a");
            var valueExpression    = Expression.Parameter(propertyType, "b");
            var propertyExpression = (Expression)instanceExpression;
            var propertyNameSplit  = propertyName.Split('.');
            foreach (var name in propertyNameSplit)
            {
                var propertyInfo     = TypeReflection.GetProperty(type, name, ReflectionFlags.Public | ReflectionFlags.Instance | ReflectionFlags.Static);
                var isPropertyStatic = PropertyReflection.IsStatic(propertyInfo);
                propertyExpression = Expression.Property(!isPropertyStatic ? propertyExpression : null, propertyInfo);
                type               = propertyInfo.PropertyType;
            }

            var propertyAssignExpression = Expression.Assign(propertyExpression, valueExpression);
            var lambdaExpression         = Expression.Lambda<Action<TObject, TProperty>>(propertyAssignExpression, instanceExpression, valueExpression);
            return lambdaExpression;
        }

        // Static - Property Getter
        public static Expression<Func<TProperty>> StaticPropertyGetter<TClass, TProperty>(string propertyName)
        {
            Contract.Requires(propertyName.SafeHasContent());

            var type               = typeof(TClass);
            var propertyExpression = default(Expression);
            var propertyNameSplit  = propertyName.Split('.');
            foreach (var name in propertyNameSplit)
            {
                var propertyInfo     = TypeReflection.GetProperty(type, name, ReflectionFlags.Public | ReflectionFlags.Static | ReflectionFlags.Instance);
                var isPropertyStatic = PropertyReflection.IsStatic(propertyInfo);
                propertyExpression = Expression.Property(isPropertyStatic ? null : propertyExpression, propertyInfo);
                type               = propertyInfo.PropertyType;
            }

            var lambdaExpression = Expression.Lambda<Func<TProperty>>(propertyExpression);
            return lambdaExpression;
        }

        // Static - Property Setter
        public static Expression<Action<TProperty>> StaticPropertySetter<TClass, TProperty>(string propertyName)
        {
            Contract.Requires(propertyName.SafeHasContent());

            var type               = typeof(TClass);
            var propertyType       = typeof(TProperty);
            var valueExpression    = Expression.Parameter(propertyType, "a");
            var propertyExpression = default(Expression);
            var propertyNameSplit  = propertyName.Split('.');
            foreach (var name in propertyNameSplit)
            {
                var propertyInfo     = TypeReflection.GetProperty(type, name, ReflectionFlags.Public | ReflectionFlags.Static | ReflectionFlags.Instance);
                var isPropertyStatic = PropertyReflection.IsStatic(propertyInfo);
                propertyExpression = Expression.Property(isPropertyStatic ? null : propertyExpression, propertyInfo);
                type               = propertyInfo.PropertyType;
            }

            var propertyAssignExpression = Expression.Assign(propertyExpression, valueExpression);
            var lambdaExpression         = Expression.Lambda<Action<TProperty>>(propertyAssignExpression, valueExpression);
            return lambdaExpression;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Call Methods
        private static MethodCallExpression CreateMethodCallExpression<TObject>(string                  methodName,
                                                                                out ParameterExpression instanceExpression)
        {
            Contract.Requires(methodName.SafeHasContent());

            var objectType = typeof(TObject);

            var methodInfo = TypeReflection.GetMethod(objectType, methodName);

            instanceExpression = Expression.Parameter(objectType, "a");
            var callExpression = Expression.Call(instanceExpression, methodInfo);
            return callExpression;
        }

        private static MethodCallExpression CreateMethodCallExpression<TObject, TArgument>(string                  methodName,
                                                                                           out ParameterExpression instanceExpression,
                                                                                           out ParameterExpression argumentExpression)
        {
            Contract.Requires(methodName.SafeHasContent());

            var objectType   = typeof(TObject);
            var argumentType = typeof(TArgument);

            var methodInfo = TypeReflection.GetMethod(objectType, methodName, argumentType);

            instanceExpression = Expression.Parameter(objectType,   "a");
            argumentExpression = Expression.Parameter(argumentType, "b");
            var callExpression = Expression.Call(instanceExpression, methodInfo, argumentExpression);
            return callExpression;
        }

        private static MethodCallExpression CreateMethodCallExpression<TObject, TArgument1, TArgument2>(string                  methodName,
                                                                                                        out ParameterExpression instanceExpression,
                                                                                                        out ParameterExpression argument1Expression,
                                                                                                        out ParameterExpression argument2Expression)
        {
            Contract.Requires(methodName.SafeHasContent());

            var objectType    = typeof(TObject);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);

            var methodInfo = TypeReflection.GetMethod(objectType, methodName, argument1Type, argument2Type);

            instanceExpression  = Expression.Parameter(objectType,    "a");
            argument1Expression = Expression.Parameter(argument1Type, "b");
            argument2Expression = Expression.Parameter(argument2Type, "c");
            var callExpression = Expression.Call(instanceExpression, methodInfo, argument1Expression, argument2Expression);
            return callExpression;
        }

        private static MethodCallExpression CreateStaticMethodCallExpression<TClass>(string methodName)
        {
            Contract.Requires(methodName.SafeHasContent());

            var classType = typeof(TClass);

            var methodInfo = TypeReflection.GetMethod(classType, methodName);

            var callExpression = Expression.Call(methodInfo);
            return callExpression;
        }

        private static MethodCallExpression CreateStaticMethodCallExpression<TClass, TArgument>(string                  methodName,
                                                                                                out ParameterExpression argumentExpression)
        {
            Contract.Requires(methodName.SafeHasContent());

            var classType    = typeof(TClass);
            var argumentType = typeof(TArgument);

            var methodInfo = TypeReflection.GetMethod(classType, methodName, argumentType);

            argumentExpression = Expression.Parameter(argumentType, "a");
            var callExpression = Expression.Call(methodInfo, argumentExpression);
            return callExpression;
        }

        private static MethodCallExpression CreateStaticMethodCallExpression<TClass, TArgument1, TArgument2>(string                  methodName,
                                                                                                             out ParameterExpression argument1Expression,
                                                                                                             out ParameterExpression argument2Expression)
        {
            Contract.Requires(methodName.SafeHasContent());

            var classType     = typeof(TClass);
            var argument1Type = typeof(TArgument1);
            var argument2Type = typeof(TArgument2);

            var methodInfo = TypeReflection.GetMethod(classType, methodName, argument1Type, argument2Type);

            argument1Expression = Expression.Parameter(argument1Type, "a");
            argument2Expression = Expression.Parameter(argument2Type, "b");
            var callExpression = Expression.Call(methodInfo, argument1Expression, argument2Expression);
            return callExpression;
        }
        #endregion
    }
}