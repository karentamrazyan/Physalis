using System;
using System.Collections;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// An RFC 1960-based Filter.
	/// <p><tt>Filter</tt> objects can be created by calling
	/// {@link BundleContext#createFilter} with the chosen 
	/// filter string.
	/// <p>A <tt>Filter</tt> object can be used numerous times 
	/// to determine if the match argument matches the filter 
	/// string that was used to create the <tt>Filter</tt> object.
	/// </summary>

	public interface IFilter
	{
		bool Match(IServiceReference reference);

		bool Match(IDictionary dictionary);

		string ToString();

		bool Equals(object obj);

		Int32 GetHashCode();
	}
}
