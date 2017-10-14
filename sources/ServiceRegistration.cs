using System;
using System.Collections;
using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceRegistration : IServiceRegistration
	{
        #region --- Fields ---
        private object service;
        private IBundle bundle;
        private IDictionary properties;
        private IServiceReference reference;
        private bool available;
        #endregion

        #region --- Properties ---
        public IServiceReference Reference
        {
            get
            {
                return reference;
            }
        }

        public IDictionary Properties
        {
            set
            {
                this.properties = value;
            }
        }
        #endregion
        
        public ServiceRegistration(IBundle b, object s, IDictionary props)
		{
            bundle = b;
            service = s;
            properties = props;
            reference = new ServiceReference(this);
            available = true;
        }

        public void Unregister()
        {
            // TODO:  Add ServiceRegistration.Unregister implementation
        }
    }
}
