using System;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceReference : IServiceReference
	{
        #region --- Fields ---
        private IServiceRegistration registration;
        #endregion
        
        #region --- Properties ---
        public string[] Keys
        {
            get
            {
                // TODO:  Add ServiceReference.Keys getter implementation
                return null;
            }
        }

        public IBundle Bundle
        {
            get
            {
                // TODO:  Add ServiceReference.Bundle getter implementation
                return null;
            }
        }

        public object this[string index]
        {
            get
            {
                // TODO:  Add ServiceReference.this getter implementation
                return null;
            }
        }
        #endregion
        
        public ServiceReference(IServiceRegistration registration)
		{
            this.registration = registration;
        }

        public IBundle[] GetUsingBundles()
        {
            // TODO:  Add ServiceReference.GetUsingBundles implementation
            return null;
        }
    }
}
