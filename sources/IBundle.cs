using System;
using System.Collections;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// An installed bundle in the Framework.
	/// 
	/// A Bundle object is the access point to define the life cycle of
	/// an installed bundle. Each bundle installed in the OSGi environment
	/// will have an associated Bundle object.
	/// 
	/// A bundle will have a unique identity, a Int32, chosen by the
	/// Framework. This identity will not change during the life cycle 
	/// of a bundle, even when the bundle is updated. Uninstalling and
	/// then reinstalling the bundle will create a new unique identity.
	/// </summary>
	public interface IBundle
	{
		/// <summary>
		/// Returns this bundle's current state. 
		/// </summary>
	 
		BundleState State
		{
			get;
		}

		/// <summary>
		/// Returns this bundle's identifier. The bundle is assigned a unique
		/// identifier by the Framework when it is installed in the OSGi environment.
		/// </summary>

		Int32 Id
		{
			get;
		}

		/// <summary>
		/// Returns this bundle's location identifier.
		/// </summary>

		string Location
		{
			get;
		}

		/// <summary>
		/// Returns this bundle's Manifest headers and values.
		/// </summary>
		
		IDictionary Headers
		{
			get;
		}

		/// <summary>
		/// Returns this bundle's <tt>ServiceReference</tt> list for all
		/// services it has registered or <tt>null</tt> if this bundle
		/// has no registered services.
		/// </summary>
		
		IServiceReference[] RegisteredServices
		{
			get;
		}

		/// <summary>
		/// Returns this bundle's <tt>ServiceReference</tt> list for all
		/// services it is using or returns <tt>null</tt> if this bundle 
		/// is not using any services.
		/// 
		/// A bundle is considered to be using a service if its use count 
		/// for that service is greater than zero.
		/// </summary>

		IServiceReference[] ServicesInUse
		{
			get;
		}

		/// <summary>
		/// Starts this bundle.
		/// </summary>

		void Start();

		/// <summary>
		/// Stops this bundle.
		/// </summary>

		void Stop();

		/// <summary>
		/// Updates this bundle.
		/// </summary>

		void Update();

		/// <summary> 
		/// Updates this bundle from an <tt>InputStream</tt>.
		/// </summary>

		void Update(System.IO.Stream inputStream);

		/// <summary>
		/// Uninstalls this bundle.
		/// </summary>		
		
		void Uninstall();
		
		/// <summary>
		/// Find the specified resource in this bundle.
		/// </summary>
		
		string GetResource(string name);

		/// <summary> 
		/// Determines if this bundle has the specified permissions.
		/// </summary>
		
		bool HasPermission(object permission);
	}
}
