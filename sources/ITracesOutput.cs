using System;

namespace Physalis.Utils
{
	/// <summary>
	/// As there is no real console on a Pocket PC, this interface is used to
	/// output traces on any support.
	/// </summary>
	public interface ITracesOutput
	{
        /// <summary>
        /// Output a trace on the chosen support.
        /// </summary>
        /// <param name="trace">The trace to be ouput.</param>
        void OutputTrace(string trace);
    }
}
