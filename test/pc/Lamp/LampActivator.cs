namespace de.inhaus.inHausLamp
{
	using System;
	using IBundleActivator = Physalis.Framework.IBundleActivator;
	using IBundleContext = Physalis.Framework.IBundleContext;
	using IServiceRegistration = Physalis.Framework.IServiceRegistration;
	using BundleException = Physalis.Framework.BundleException;
	using IServiceReference = Physalis.Framework.IServiceReference;
	//using Device = org.osgi.service.device.Device;
	//using InHausBaseDriver = de.inhaus.inHausBaseDriver.InHausBaseDriver;
	//using ServiceListener = org.osgi.framework.ServiceListener;
	//using ServiceEvent = org.osgi.framework.ServiceEvent;

	public class LampActivator : IBundleActivator //, ServiceListener
	{
		public LampActivator()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			lamp = new LampImpl(this);
			osgi_dev = new de.inhaus.inHausMain.DeviceImpl();
		}
		
		public static void  o(System.String str)
		{
			System.Console.Out.WriteLine("[LampActivator] " + str);
		}
		
		
		/*Filter for ServiceListener.*/
		private const System.String FILTER = "(objectClass=de.inhaus.inHausBaseDriver.InHausBaseDriver)";
		
		
		/*Will be initialized when starting activator, in order to be used in serviceChanged() method.*/
		private BundleContext bc = null;
		
		/*The ServiceReference variable for the gotten service.*/
		private ServiceReference refInHausBaseDriver = null;
		
		/*Variable used for holding service object.*/
		private InHausBaseDriver inHausBaseDriver = null;
		
		/*String representation of the service that will be gotten.*/
		internal const System.String SERVICE_INHAUSBASEDRIVER = "de.inhaus.inHausBaseDriver.InHausBaseDriver";
		
		/*Variable used for the ServiceRegistration of registered service.*/
		private ServiceRegistration regLamp = null;
		// private ServiceRegistration regLamp_osgi = null;
		
		/*Variable holding service object that is going to be registered.*/
		//UPGRADE_NOTE: The initialization of  'lamp' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
		private de.inhaus.inHausLamp.LampImpl lamp;
		//UPGRADE_NOTE: The initialization of  'osgi_dev' was moved to method 'InitBlock'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1005"'
		private org.osgi.service.device.Device osgi_dev;
		
		private System.Collections.Hashtable props = null;
		
		public virtual void  preStart()
		{
			props = new System.Collections.Hashtable();
			SupportClass.PutElement(props, "THRC.Identifier", "inhaus.Lamp");
			SupportClass.PutElement(props, "THRC.Interface", "de.inhaus.inHausLamp.Lamp");
			//  props.put("THRC.Credential", "1000");
		}
		public virtual void  postStart()
		{
			if (inHausBaseDriver != null)
				lamp.setDriver(inHausBaseDriver);
		}
		
		
		// Methods inherited from interface org.osgi.framework.BundleActivator
		
		/// <summary> 
		/// 
		/// </summary>
		public virtual void  start(BundleContext bc)
		{
			
			preStart();
			
			/*Assigning value to variable bc in order to be useful in serviceChanged() method.*/
			this.bc = bc;
			try
			{
				
				/*!--------------PART FOR REGISTERING SERVICES--------------*/
				/*Registering service with name "de.inhaus.inHausLamp.Lamp".*/
				/*INITIALIZE SERVICE OBJECT lamp FOR PROPER REGISTRATION OF THE UNDERLYING SERVICE!*/
				regLamp = bc.registerService("de.inhaus.inHausLamp.Lamp", lamp, props);
				bc.registerService("org.osgi.service.device.Device", osgi_dev, props);
				/*!--------------END OF PART FOR REGISTERING SERVICES--------------*/
				/*!--------------PART FOR GETTING SERVICES--------------*/
				/*Getting ServiceReference for service with name "InHausBasedriver.InHausBasedriver" through BundleContext passed to start method.*/
				refInHausBaseDriver = bc.getServiceReference(SERVICE_INHAUSBASEDRIVER);
				if (refInHausBaseDriver != null)
				{
					/*If a service with such name is already registered in the framework.*/
					/*Getting service object.*/
					inHausBaseDriver = (InHausBaseDriver) bc.getService(refInHausBaseDriver);
				}
				/*!--------------END OF PART FOR GETTING SERVICES--------------*/
				
				/*!--------------PART FOR ADDING CLASS AS LISTENER--------------*/
				/*Adding this class as listener for ServiceEvents for services which names 
				agree with the value of LDAP filter 'FILTER'.*/
				bc.addServiceListener(this, FILTER);
				/*!--------------END OF PART FOR ADDING LISTENERS--------------*/
			}
			catch (System.Exception e)
			{
				/*Logging error via org.osgi.service.log.LogService or com.prosyst.util.ref.Log 
				and printing the stack trace of the thrown exception.*/
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				dump("[Lamp]Error in start method: " + e.Message, e);
				
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				throw new BundleException("[Lamp]Error in start method: " + e.Message);
			}
			postStart();
		}
		
		/// <summary> 
		/// 
		/// </summary>
		public virtual void  stop(BundleContext bc)
		{
			/*!--------------PART FOR UNGETTING SERVICES--------------*/
			if (refInHausBaseDriver != null)
			{
				/*Ungetting service if the ServiceReference is not null, i.e. the service has been gotten.*/
				bc.ungetService(refInHausBaseDriver);
			}
			
			
			/*!--------------END OF PART FOR UNGETTING SERVICES--------------*/
			/*!--------------PART FOR REMOVING SERVICE LISTENERS--------------*/
			/*Removing this class as listener for any ServiceEvents.*/
			bc.removeServiceListener(this);
			/*!--------------END OF PART FOR REMOVING SERVICE LISTENERS--------------*/
			try
			{
				/*!--------------PART FOR UNREGISTERING SERVICE--------------*/
				/*Unregistering the underlying service if the registration is successful, i.e. field "regLamp" is not null.*/
				if (regLamp != null)
				{
					regLamp.unregister();
					regLamp = null;
				}
				lamp = null;
				
				/*!--------------END PART FOR UNREGISTERING SERVICES--------------*/
			}
			catch (System.Exception e)
			{
				/*Logging error via org.osgi.service.log.LogService or com.prosyst.util.ref.Log 
				and printing the stack trace of the thrown exception.*/
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				dump("[Lamp]Error in stop method: " + e.Message, e);
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1043"'
				throw new BundleException("[Lamp]Error in stop method: " + e.Message);
			}
		}
		
		/// <summary>Method inherited from interface org.osgi.framework.ServiceListener.
		/// Called when some ServiceEvent is processed - a service is registered,
		/// unregistered or modified.
		/// </summary>
		public virtual void  serviceChanged(ServiceEvent event_Renamed)
		{
			/*DO NOT CHANGE THIS CODE!!!IT'S AUTOMATICALLY GENERATED*/
			switch (event_Renamed.Type)
			{
				
				case ServiceEvent.REGISTERED:  {
						/*Service has been registered.*/
						/*Constructing an array to hold all names, that the registered service has been registered with.*/
						o("ServiceEvent.REGISTERED");
						System.String[] classNames = (System.String[]) event_Renamed.ServiceReference.getProperty("objectClass");
						for (int k = 0; k < classNames.Length; k++)
						{
							System.String objectClass = classNames[k];
							/*Searching for service with name "InHausBasedriver.InHausBasedriver".*/
							if (objectClass.Equals(SERVICE_INHAUSBASEDRIVER))
							{
								/*!--------------PART FOR GETTING SERVICES--------------*/
								/*Getting ServiceReference for service with name "InHausBasedriver.InHausBasedriver" through BundleContext passed to start method.*/
								refInHausBaseDriver = bc.getServiceReference(SERVICE_INHAUSBASEDRIVER);
								if (refInHausBaseDriver != null)
								{
									/*If a service with such name is already registered in the framework.*/
									/*Getting service object.*/
									inHausBaseDriver = (InHausBaseDriver) bc.getService(refInHausBaseDriver);
									o("inHausBaseDriver = (InHausBaseDriver)bc.getService(refInHausBaseDriver);");
									lamp.setDriver(inHausBaseDriver);
								}
								/*!--------------END OF PART FOR GETTING SERVICES--------------*/
								
								break;
							}
						}
						break;
					}
				
				case ServiceEvent.UNREGISTERING:  {
						/*Service has been unregistered*/
						o("ServiceEvent.UNREGISTERING");
						/*Constructing an array to hold all names, that the registered service has been registered with.*/
						System.String[] classNames = (System.String[]) event_Renamed.ServiceReference.getProperty("objectClass");
						for (int k_1 = 0; k_1 < classNames.Length; k_1++)
						{
							System.String objectClass = classNames[k_1];
							/*Searching for service with name "InHausBasedriver.InHausBasedriver".*/
							if (objectClass.Equals(SERVICE_INHAUSBASEDRIVER))
							{
								/*!--------------PART FOR UNGETTING SERVICES--------------*/
								if (refInHausBaseDriver != null)
								{
									/*Ungetting service if the ServiceReference is not null, i.e. the service has been gotten.*/
									bc.ungetService(refInHausBaseDriver);
									lamp.Driver = null;
								}
								/*!--------------END OF PART FOR UNGETTING SERVICES--------------*/
								
								break;
							}
						}
						break;
					}
				}
			/*END OF AUTOMATICALLY GENERATED CODE*/
		}
		
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1100"'
		/// <summary>Common method for printing the stack  
		/// traces of exceptions and logging errors.
		/// by com.prosyst.util.ref.Log.
		/// </summary>
		public static void  dump(System.String str, System.Exception t)
		{
			if (t != null)
			{
				SupportClass.WriteStackTrace(t, Console.Error);
			}
		}
	}
}