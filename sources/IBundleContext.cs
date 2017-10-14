using System;
using System.Collections;
using System.IO;

namespace Physalis.Specs.Framework
{
	public delegate void BundleEventHandler(object sender, BundleEventArgs e);
	public delegate void FrameworkEventHandler(object sender, FrameworkEventArgs e);
	public delegate void ServiceEventHandler(object sender, ServiceEventArgs e);
    
	/// <summary>
	/// A bundle's execution context within the Framework. 
	/// The context is used to grant access to other methods 
	/// so that this bundle can interact with the Framework.
	/// </summary>
	public interface IBundleContext
	{

		/// <summary>
		/// Returns the value of the specified property. 
		/// If the key is not found in the Framework properties, 
		/// the system properties are then searched. The method 
		/// returns <tt>null</tt> if the property is not found.
		/// </summary>

		object this[string index]
		{
			get;
		}

		/// <summary>
		/// Returns the <tt>Bundle</tt> object for this context bundle.
		/// 
		/// The context bundle is defined as the bundle that was assigned this 
		/// <tt>BundleContext</tt> in its <tt>BundleActivator</tt>.
		/// </summary>
		
		IBundle Bundle
		{
			get;
		}

		/// <summary>
		/// 
		/// </summary>

		IFilter CreateFilter(string filter);

		/// <summary>
		/// Returns the bundle with the specified identifier.
		/// </summary>
		
		IBundle GetBundle(Int32 id);
        
		/// <summary>
		/// Returns a list of all installed bundles. 
		/// 
		/// <p>This method returns 
		/// a list of all bundles installed in the OSGi environment at the 
		/// time of the call to this method. However, as the Framework is a 
		/// very dynamic environment, bundles can be installed or uninstalled 
		/// at anytime.
		/// </summary>
		
		IBundle[] GetBundles();
        
		/// <summary>
		/// 
		/// </summary>
		
		File GetDataFile(string name);
        
		/// <summary>
		/// 
		/// </summary>
		
		object GetService(IServiceReference reference);
        
		/// <summary>
		/// 
		/// </summary>
		
		IServiceReference GetServiceReference(string theClass);
        
		/// <summary>
		/// 
		/// </summary>
		
		IServiceReference[] GetServiceReferences(string theClass, string filter);
        
		/// <summary>
		/// Installs the bundle from the specified <tt>InputStream</tt> object.
		/// 
		/// <p>This method performs all of the steps listed in 
		/// <tt>BundleContext.installBundle(String location)</tt>, except that 
		/// the bundle's content will be read from the <tt>InputStream</tt> object.
		/// The location identifier string specified will be used as the identity 
		/// of the bundle.
		/// </summary>
		IBundle InstallBundle(string location, Stream source);

        IBundle InstallBundle(string location);
        
		/// <summary>
		/// 
		/// </summary>
		
		IServiceRegistration RegisterService(string theClass, object service, IDictionary properties);
        
		/// <summary>
		/// 
		/// </summary>
		
		IServiceRegistration RegisterService(string[] classes, object service, IDictionary properties);
        
		/// <summary>
		/// 
		/// </summary>

		bool UngetService(IServiceReference reference);

		/// <summary>
		/// 
		/// </summary>

		event BundleEventHandler BundleEvent;

		/// <summary>
		/// 
		/// </summary>
		
		event FrameworkEventHandler FrameworkEvent;
        
		/// <summary>
		/// 
		/// </summary>
		
		event ServiceEventHandler ServiceEvent;
	}
}
