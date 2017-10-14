using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// 
	/// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class BundleActivatorAttribute : System.Attribute
	{
		private string activator;
        
        public string Activator
        {
            get
            {
                return activator;
            }
        }

        public BundleActivatorAttribute(string activator)
		{
			this.activator = activator;
		}
	}
}
