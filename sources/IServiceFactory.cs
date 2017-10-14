using System;

namespace Physalis.Specs.Framework
{
	/// <summary>
	/// See <see cref="org.osgi.framework.ServiceFactory"/>
	/// </summary>
	public interface IServiceFactory
	{
        object GetService(IBundle bundle, IServiceRegistration registration);
        void UngetService(IBundle bundle, IServiceRegistration registration, object service);
    }
}
