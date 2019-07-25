// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents the settings to apply in the API schema convention of auto discovery for API annotations, API configurations, or API types.</summary>
    public class ApiDiscoverySettings
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiDiscoverySettings(IEnumerable<Assembly> assemblies)
        {
            Contract.Requires(assemblies != null);

            this.Assemblies = assemblies.EmptyIfNull();
        }

        public ApiDiscoverySettings(params Assembly[] assemblies)
            : this(assemblies.AsEnumerable())
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the collection of CLR assemblies for auto discovery of API types.</summary>
        public IEnumerable<Assembly> Assemblies { get; }

        /// <summary>Get/Sets an optional predicate to apply during auto discovery of API annotations from a CLR type.</summary>
        public Func<Type, bool> ApiAnnotationDiscoveryPredicate { get; set; }

        /// <summary>Get/Sets an optional predicate to apply during auto discovery of API configurations from a CLR type.</summary>
        public Func<Type, bool> ApiConfigurationDiscoveryPredicate { get; set; }

        /// <summary>Get/Sets an optional predicate to apply during auto discovery of API types from a CLR type.</summary>
        public Func<Type, bool> ApiTypeDiscoveryPredicate { get; set; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly ApiDiscoverySettings Empty = new ApiDiscoverySettings();
        #endregion
    }
}