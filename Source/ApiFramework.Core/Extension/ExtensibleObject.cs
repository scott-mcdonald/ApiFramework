// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

using ApiFramework.Extension.Internal;

namespace ApiFramework.Extension
{
    /// <inheritdoc cref="IExtensibleObject{T}"/>
    /// <summary>Boilerplate implementation of <see cref="IExtensibleObject{T}"/> using <see cref="ExtensionDictionary"/> as an implementation detail.</summary>
    /// <typeparam name="T">Type of extensible object being extended.</typeparam>
    public abstract class ExtensibleObject<T> : IExtensibleObject<T>
        where T : IExtensibleObject<T>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public IEnumerable<IExtension<T>> Extensions => this.ExtensionDictionary.Value.Extensions;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IExtensibleObject<T> Implementation
        public void AddExtension(IExtension<T> extension)
        {
            Contract.Requires(extension != null);

            this.ExtensionDictionary.Value.AddExtension(extension);
        }

        public void RemoveExtension(Type extensionType)
        {
            Contract.Requires(extensionType != null);

            this.ExtensionDictionary.Value.RemoveExtension(extensionType);
        }

        public bool TryGetExtension(Type extensionType, out IExtension<T> extension)
        {
            Contract.Requires(extensionType != null);

            return this.ExtensionDictionary.Value.TryGetExtension(extensionType, out extension);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ExtensibleObject()
        {
            this.ExtensionDictionary = new Lazy<ExtensionDictionary<T>>(() =>
            {
                var extensionOwner      = this.ExtensionOwner;
                var extensionDictionary = new ExtensionDictionary<T>(extensionOwner);
                return extensionDictionary;
            }, LazyThreadSafetyMode.PublicationOnly);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected abstract T ExtensionOwner { get; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Lazy<ExtensionDictionary<T>> ExtensionDictionary { get; }
        #endregion
    }
}