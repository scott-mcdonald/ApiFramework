// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal abstract class ApiConventionsConfiguration<TConvention>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public IEnumerable<TConvention> Create()
        {
            return this.ConventionsCollection;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected ICollection<TConvention> ConventionsCollection { get; } = new List<TConvention>();

        protected ICollection<Type> ConventionsByTypeCollection { get; } = new HashSet<Type>();
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected void HasConventionImpl(TConvention convention)
        {
            if (convention == null)
                return;

            var conventionType = convention.GetType();
            this.ConventionsCollection.Add(convention);
            this.ConventionsByTypeCollection.Add(conventionType);
        }

        protected bool WasConventionAlreadyAdded(TConvention convention)
        {
            Contract.Requires(convention != null);

            var conventionType = convention.GetType();
            return this.ConventionsByTypeCollection.Contains(conventionType);
        }
        #endregion
    }
}