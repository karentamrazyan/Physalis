using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class BundleException : Exception
	{
		public BundleException() : base()
		{
		}

        public BundleException(string message) : base(message)
        {
        }

        public BundleException(string message, Exception nested) : base(message, nested)
        {
        }
	}
}
