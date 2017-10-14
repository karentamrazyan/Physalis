using System;
using Physalis.Framework;
using Physalis.Utils;


namespace Start
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Start
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
            TracesOutputProvider.TracesOutput = new TraceOutput("127.0.0.1", 6666);

            Starter.Instance.Start();

            TracesOutputProvider.TracesOutput.OutputTrace("End");
        }
	}
}
