// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableFactory
    {
    }

    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableFactory<T, TMutable> : ApiMutableFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiMutableFactory(Func<T, TMutable> apiFactoryFunction, int indentWidth)
        {
            Contract.Requires(apiFactoryFunction != null);

            this.ApiFactoryFunction = apiFactoryFunction;
            this.IndentWidth        = indentWidth;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void AddModifier(ApiPrecedenceLevel        apiPrecedenceLevel,
                                Action<TMutable>          apiModifierFunction,
                                string                    callerTypeName,
                                [CallerMemberName] string callerMemberName = default(string))
        {
            Contract.Requires(apiModifierFunction != null);

            var apiModifier = new ApiMutableObjectModifier(apiPrecedenceLevel,
                                                           apiModifierFunction,
                                                           callerTypeName,
                                                           callerMemberName);

            this.ApiModifiers.Add(apiModifier);
        }

        public TMutable Create(T arg)
        {
            Contract.Requires(arg != null);

            // Create API mutable object.
            var apiMutable = this.ApiFactoryFunction(arg);

            ApiFrameworkLog.Trace($"Configuring {apiMutable}".Indent(this.IndentWidth));

            // Modify API object by the following priority:
            // 1. Convention
            // 2. Annotation
            // 3. Configuration
            // 4. FluentApi
            var apiModifiers = this.ApiModifiers;
            var apiModifiersGroupedAndOrdered = apiModifiers.GroupBy(x => x.ApiPrecedenceLevel)
                                                            .OrderBy(x => x.Key)
                                                            .SelectMany(x => x)
                                                            .ToList();
            var apiModifierFunctionCollection = apiModifiersGroupedAndOrdered.Select(x => x.ApiModifierFunction)
                                                                             .ToList();

            foreach (var apiModifierFunction in apiModifierFunctionCollection)
            {
                apiModifierFunction(apiMutable);
            }

            ApiFrameworkLog.Trace($"Configured {apiMutable}".Indent(this.IndentWidth));

            // Return the created and modified API mutable object.
            return apiMutable;
        }

        public void Merge(ApiMutableFactory<T, TMutable> other)
        {
            Contract.Requires(other != null);

            this.ApiModifiers.AddRange(other.ApiModifiers);
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<T, TMutable>              ApiFactoryFunction { get; }
        private List<ApiMutableObjectModifier> ApiModifiers       { get; } = new List<ApiMutableObjectModifier>();
        private int                            IndentWidth        { get; }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class ApiMutableObjectModifier
        {
            #region Public Constructors
            public ApiMutableObjectModifier(ApiPrecedenceLevel apiPrecedenceLevel,
                                            Action<TMutable>   apiModifierFunction,
                                            string             callerTypeName,
                                            string             callerMemberName)
            {
                Contract.Requires(apiModifierFunction != null);
                Contract.Requires(callerTypeName.SafeHasContent());
                Contract.Requires(callerMemberName.SafeHasContent());

                this.ApiPrecedenceLevel  = apiPrecedenceLevel;
                this.ApiModifierFunction = apiModifierFunction;
                this.CallerTypeName      = callerTypeName;
                this.CallerMemberName    = callerMemberName;
            }
            #endregion

            #region Public Properties
            public ApiPrecedenceLevel ApiPrecedenceLevel  { get; }
            public Action<TMutable>   ApiModifierFunction { get; }
            #endregion

            #region Public Methods
            public override string ToString()
            {
                const string typeName = nameof(ApiMutableObjectModifier);

                var level       = this.ApiPrecedenceLevel.ToString();
                var target      = this.CallerTypeName;
                var method      = this.CallerMemberName;
                var description = $"{typeName} [level={level} target={target} method={method}]";
                return description;
            }
            #endregion

            #region Private Properties
            private string CallerTypeName   { get; }
            private string CallerMemberName { get; }
            #endregion
        }
        #endregion
    }
}