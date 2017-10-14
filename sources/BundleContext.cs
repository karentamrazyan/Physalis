using System;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// Context of the bundle
	/// </summary>
	public class BundleContext : IBundleContext
	{
        #region --- Fields ---
        private IBundle bundle;
        #endregion

        #region --- Properties ---
        public object this[string index]
        {
            get
            {
                return Framework.Instance[index];
            }
        }

        public IBundle Bundle
        {
            get
            {
                return bundle;
            }
        }
        #endregion
        
        public BundleContext(IBundle bundle)
		{
            this.bundle = bundle;
        }

        public IFilter CreateFilter(string filter)
        {
            // TODO:  Add BundleContext.CreateFilter implementation
            return null;
        }

        public IBundle GetBundle(Int32 id)
        {
            // TODO:  Add BundleContext.GetBundle implementation
            return null;
        }

        public IBundle[] GetBundles()
        {
            // TODO:  Add BundleContext.GetBundles implementation
            return null;
        }

        public System.IO.File GetDataFile(string name)
        {
            // TODO:  Add BundleContext.GetDataFile implementation
            return null;
        }

        public object GetService(IServiceReference reference)
        {
            // TODO:  Add BundleContext.GetService implementation
            return null;
        }

        public IServiceReference GetServiceReference(string theClass)
        {
            // TODO:  Add BundleContext.GetServiceReference implementation
            return null;
        }

        public IServiceReference[] GetServiceReferences(string theClass, string filter)
        {
            // TODO:  Add BundleContext.GetServiceReferences implementation
            return null;
        }

        public IBundle InstallBundle(string location)
        {
            // TODO:  Add BundleContext.InstallBundle implementation
            return null;
        }

        public IBundle InstallBundle(string location, System.IO.Stream source)
        {
            // TODO:  Add BundleContext.InstallBundle implementation
            return null;
        }

        public IServiceRegistration RegisterService(string theClass, object service, System.Collections.IDictionary properties)
        {
            // TODO:  Add BundleContext.RegisterService implementation
            return null;
        }

        IServiceRegistration Physalis.Specs.Framework.IBundleContext.RegisterService(string[] classes, object service, System.Collections.IDictionary properties)
        {
            // TODO:  Add BundleContext.Physalis.Specs.Framework.IBundleContext.RegisterService implementation
            return null;
        }

        public bool UngetService(IServiceReference reference)
        {
            // TODO:  Add BundleContext.UngetService implementation
            return false;
        }

        public event Physalis.Specs.Framework.BundleEventHandler BundleEvent;

        public event Physalis.Specs.Framework.FrameworkEventHandler FrameworkEvent;

        public event Physalis.Specs.Framework.ServiceEventHandler ServiceEvent;
    }
}
