using System;
using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ClrIdentityProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrIdentityProperty(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);

            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string ClrPropertyName { get; }

        public Type ClrPropertyType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ClrIdentityProperty)} [{nameof(this.ClrPropertyName)}={this.ClrPropertyName}]";
        }
        #endregion
    }
}