using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// Customizes the starting and stopping of this bundle.
	/// </summary>
	public interface IBundleActivator
	{
		/// <summary>
		/// Called when this bundle is started so the Framework can perform 
		/// the bundle-specific activities necessary to start this bundle. 
		/// </summary>
		void Start(IBundleContext context);
		/// <summary>
		/// Called when this bundle is stopped so the Framework can perform 
		/// the bundle-specific activities necessary to stop the bundle.
		/// </summary>
		void Stop(IBundleContext context);
	}
}
