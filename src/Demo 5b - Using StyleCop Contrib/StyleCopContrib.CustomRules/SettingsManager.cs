using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using StyleCop;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// Manage and abstract the settings file properties.
    /// </summary>
    public sealed class SettingsManager
    {
        #region Fields

        private readonly IDictionary<string, object> settings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsManager"/> class.
        /// </summary>
        public SettingsManager()
        {
            this.settings = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the setting's property.
        /// </summary>
        /// <typeparam name="T">The type of the setting.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The property value.</param>
        public void SetSetting<T>(string propertyName, T value)
        {
            this.settings[propertyName] = value;
        }

        /// <summary>
        /// Gets the setting's property.
        /// </summary>
        /// <typeparam name="T">The type of the setting.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The property value.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "API designed for type reference")]
        public T GetSetting<T>(string propertyName, StyleCopAddIn analyzer, Settings settings)
        {
            T result;

            if (this.settings.ContainsKey(propertyName))
            {
                result = (T)this.settings[propertyName];
            }
            else
            {
                // StringProperty stringProperty = (StringProperty)analyzer.GetSetting(settings, propertyName);
                PropertyValue<T> property = (PropertyValue<T>)analyzer.GetSetting(settings, propertyName);

                if (property != null)
                {
                    result = property.Value;
                }
                else
                {
                    PropertyDescriptor<T> propertyDescriptor =
                        (PropertyDescriptor<T>)analyzer.PropertyDescriptors[propertyName];

                    if (propertyDescriptor != null)
                    {
                        result = propertyDescriptor.DefaultValue;
                    }
                    else
                    {
                        throw new InvalidOperationException(propertyName + " not found");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Resets all the settings.
        /// </summary>
        public void ResetSettings()
        {
            this.settings.Clear();
        }

        #endregion
    }
}