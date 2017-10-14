using System;

namespace Physalis.Utils
{
	/// <summary>
	/// Used to access the ITraceOutput object. Useful to avoid
	/// cyclic dependencies.
	/// </summary>
    public class TracesOutputProvider
    {
        private static ITracesOutput output;
        
        public static ITracesOutput TracesOutput
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                if(output == null)
                {
                    throw new ArgumentNullException("Forbidden");
                }
            }
        }
    }
}
