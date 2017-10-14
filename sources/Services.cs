using System;
using System.Collections;

using Physalis.Utils;

using Physalis.Specs.Framework;

namespace Physalis.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class Services
	{
        #region --- Fields ---
        private ArrayList services;
        private Hashtable serviceClasses;
        #endregion
        
        public Services()
		{
            services = new ArrayList();
            serviceClasses = new Hashtable();
        }

        internal IServiceRegistration Register(IBundle bundle,
                                                string[] classes,
                                                object service,
                                                IDictionary properties)
        {
            if (service == null)
            {
                throw new ArgumentNullException("Can't register null as service");
            }

            IServiceRegistration res;
            lock(this)
            {
                for (int i = 0; i < classes.Length; i++)
                {
	                if (classes[i] == null)
                    {
    	                throw new ArgumentNullException("Can't register as null class");
	                }

#if DEBUG
                    TracesOutputProvider.TracesOutput.OutputTrace("#" + bundle.Id + " registred " + i + ": " + classes[i] + " " + properties);
#endif 

	                if (bundle.Id != 0 && classes[i].Equals(typeof(NamespacesAdmin).FullName))
                    {
	                    throw new ArgumentException("Registeration of a PackageAdmin service is not allowed");
	                }

                    Type c = Type.GetType(classes[i]);
                    if(c == null)
                    {
	                    throw new ArgumentException("Class does not exist: " + classes[i]);
	                }

	                if(!(service is IServiceFactory) && !c.IsInstanceOfType(service))
                    {
	                    throw new ArgumentException("Object " + service + " is not an instance of " + classes[i]);
	                }
                }
            
                res = new ServiceRegistration(bundle, service, properties);
                services.Add(res);
            
                for (int i = 0; i < classes.Length; i++) 
                {
	                ArrayList s = (ArrayList) serviceClasses[classes[i]];
	                if (s == null)
                    {
	                    s = new ArrayList(1);
	                    serviceClasses[classes[i]] = s;
	                }

	                s.Add(res);
                }
            }

//            ServiceEventArgs evt = new ServiceEventArgs(ServiceTransition.Registered, res.Reference);
            Framework.Instance.FireServiceEvent(new ServiceEventArgs(ServiceTransition.Registered, res.Reference));
            return res;
        }
	}
}
