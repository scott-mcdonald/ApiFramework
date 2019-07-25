// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents a fluent-style builder of API naming conventions.</summary>
    public interface IApiNamingConventionsBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds the built-in camel case API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasCamelCaseConvention();

        /// <summary>Adds the built-in kebab case API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasKebabCaseConvention();

        /// <summary>Adds the built-in lower case API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasLowerCaseConvention();

        /// <summary>Adds the built-in pascal case API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasPascalCaseConvention();

        /// <summary>Adds the built-in pluralize API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasPluralizeConvention();

        /// <summary>Adds the built-in singularize API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasSingularizeConvention();

        /// <summary>Adds the built-in upper case API naming convention to the collection of API naming conventions.</summary>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasUpperCaseConvention();

        /// <summary>Adds a custom API naming convention to the collection of API naming conventions.</summary>
        /// <param name="convention">Convention to add to the collection of API naming conventions.</param>
        /// <returns>A fluent-style API naming conventions builder for the collection of API naming conventions.</returns>
        IApiNamingConventionsBuilder HasConvention(IApiNamingConvention convention);
        #endregion
    }
}
