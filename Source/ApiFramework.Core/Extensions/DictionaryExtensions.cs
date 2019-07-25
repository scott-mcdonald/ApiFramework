// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace ApiFramework
{
    /// <summary>
    /// Extension methods for the .NET <see cref="IDictionary{TKey,TValue}"/> interface.
    /// </summary>
    public static class DictionaryExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>
        /// Get the value by key from the dictionary, throws an exception if the value does not exist in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary object to extend with this extension method.</param>
        /// <param name="key">Key to get the value by in the dictionary.</param>
        /// <returns>The indexed value by key, throws a <see cref="KeyNotFoundException"/> if the value does not exist in the dictionary.</returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var value))
                return value;

            var message = $"Unable to get value for given key '{key}' from dictionary, key does not exist in dictionary.";
            throw new KeyNotFoundException(message);
        }
        #endregion
    }
}
