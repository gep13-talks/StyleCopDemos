using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// This class act as the ServiceLocator pattern.
    /// </summary>
    public sealed class ServiceLocator
    {
        #region Fields

        private readonly IDictionary<Type, object> services;

        private static volatile ServiceLocator instance;

        private static readonly object syncRoot = new Object();

        #endregion

        #region Constructor

        private ServiceLocator()
        {
            this.services = new Dictionary<Type, object>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the service of the given type.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>The service.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "API designed for type reference")]
        public static T GetService<T>()
        {
            return ServiceLocator.GetInstance().InternalGetService<T>();
        }

        /// <summary>
        /// Registers the service.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="service">The service.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "API designed for type reference")]
        public static void RegisterService<T>(object service)
        {
            ServiceLocator.GetInstance().InternalRegisterService<T>(service);
        }

        private T InternalGetService<T>()
        {
            object result;

            this.services.TryGetValue(typeof(T), out result);

            return (T)result;
        }

        private void InternalRegisterService<T>(object service)
        {
            this.services.Add(typeof(T), service);
        }

        private static ServiceLocator GetInstance()
        {
            if (ServiceLocator.instance == null)
            {
                lock (ServiceLocator.syncRoot)
                {
                    if (ServiceLocator.instance == null)
                        ServiceLocator.instance = new ServiceLocator();
                }
            }

            return ServiceLocator.instance;
        }

        #endregion
    }
}