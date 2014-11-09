namespace PolarisResources.Core.Components
{
    #region Usings ...

    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// Component Model composition container class
    /// </summary>
    public class MefContainer : IContainer
    {
        #region Fields

        /// <summary>
        /// Lazy container
        /// </summary>
        private readonly Lazy<CompositionContainer> lazyContainer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        public MefContainer()
        {
            this.lazyContainer = new Lazy<CompositionContainer>(this.Initialize);
        }

        #endregion

        #region Properties

        /// <summary>
        /// </summary>
        private CompositionContainer Container
        {
            get
            {
                return this.lazyContainer.Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="serviceType">
        /// </param>
        /// <returns>
        /// </returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        /// <summary>
        /// </summary>
        /// <param name="serviceType">
        /// </param>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public object GetInstance(Type serviceType, string key = null)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            IEnumerable<object> exports = this.Container.GetExportedValues<object>(contract);

            object instance = exports.FirstOrDefault();

            if (instance != null)
            {
                return instance;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public T GetInstance<T>(string key = null)
        {
            return (T)this.GetInstance(typeof(T), key);
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private CompositionContainer Initialize()
        {
            var catalog = new AggregateCatalog();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var assemblyCatalog = new AssemblyCatalog(assembly);
                    ComposablePartDefinition[] assemblyParts = assemblyCatalog.Parts.ToArray();
                    catalog.Catalogs.Add(assemblyCatalog);
                }
                catch (Exception)
                {
                    // Dont catch not importable assemblies errors
                }
            }

            var container = new CompositionContainer(catalog);
            var batch = new CompositionBatch();
            batch.AddExportedValue<IContainer>(this);
            container.Compose(batch);
            return container;
        }

        #endregion
    }
}
