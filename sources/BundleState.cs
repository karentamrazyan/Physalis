using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// Defines bundle's states.
	/// </summary>
	public enum BundleState : int
	{
		/// <summary>
		/// This bundle is uninstalled and may not be used.
		/// </summary>

		Uninstalled = 0x00000001,
        
		/// <summary>
		/// This bundle is installed but not yet resolved.
		/// </summary>
		
		Installed = 0x00000002,

		/// <summary>
		/// This bundle is resolved and is able to be started.
		/// </summary>

		Resolved = 0x00000004,
        
		/// <summary>
		/// This bundle is in the process of starting.
		/// </summary>

		Starting = 0x00000008,
        
		/// <summary>
		/// This bundle is in the process of stopping.
		/// </summary>

		Stopping = 0x00000010,
        
		/// <summary>
		/// This bundle is now running.
		/// </summary>

		Active = 0x00000020
	}
}
