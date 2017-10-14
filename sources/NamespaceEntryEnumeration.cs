using System;

namespace Physalis.Framework
{
	/// <summary>
	/// Enumeration to be used in the <see cref="NamespaceEntry.ToString"/> method.
	/// </summary>
	public enum NamespaceEntryEnumeration : int
	{
	    Simple,     // Just the bundle name is returned
        Standard,   // Simple + version if defined
        Complete    // Standard + Bundle reference
    }
}
