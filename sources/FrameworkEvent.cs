using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// A general Framework event.
	/// 
	/// <p><tt>FrameworkEvent</tt> is the event class used 
	/// when notifying listeners of general events occuring
	/// within the OSGI environment. A type code is used to 
	/// identify the event type for future extendability.
	/// </summary>
	public enum FrameworkEvent
	{
        Started,
        Error,
        PackagesRefreshed,
        StartlevelChanged
    }
}
