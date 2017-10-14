//package inHausLamp;
package de.inhaus.inHausLamp;

import org.osgi.framework.BundleActivator;
import org.osgi.framework.BundleContext;
import org.osgi.framework.ServiceRegistration;
import org.osgi.framework.BundleException;
import org.osgi.framework.ServiceReference;
import org.osgi.service.device.Device;
import java.util.Hashtable;
import de.inhaus.inHausBaseDriver.InHausBaseDriver;
import org.osgi.framework.ServiceListener;
import org.osgi.framework.ServiceEvent;

/**
 * 
 * 
 * @author 
 * @version 
 * @since 
 */
public class LampActivator implements BundleActivator , ServiceListener{

	public static void o(String str)  {
			System.out.println("[LampActivator] "+str);
	}


	/*Filter for ServiceListener.*/
	private static final String FILTER = "(objectClass=de.inhaus.inHausBaseDriver.InHausBaseDriver)";


	/*Will be initialized when starting activator, in order to be used in serviceChanged() method.*/
	private BundleContext bc = null;

	/*The ServiceReference variable for the gotten service.*/
	private ServiceReference refInHausBaseDriver = null;

	/*Variable used for holding service object.*/
	private InHausBaseDriver inHausBaseDriver = null;

	/*String representation of the service that will be gotten.*/
	final static String SERVICE_INHAUSBASEDRIVER = "de.inhaus.inHausBaseDriver.InHausBaseDriver";

	/*Variable used for the ServiceRegistration of registered service.*/
	private ServiceRegistration regLamp = null;
// private ServiceRegistration regLamp_osgi = null;

	/*Variable holding service object that is going to be registered.*/
	private de.inhaus.inHausLamp.LampImpl lamp = new LampImpl(this);
  private org.osgi.service.device.Device osgi_dev = new de.inhaus.inHausMain.DeviceImpl();

  private Hashtable props = null;

  public void preStart()
    {
			props = new Hashtable();
      props.put("THRC.Identifier", "inhaus.Lamp");
	    props.put("THRC.Interface", "de.inhaus.inHausLamp.Lamp");
	  //  props.put("THRC.Credential", "1000");
    }
  public void postStart()
    {
      if (inHausBaseDriver != null)
        lamp.setDriver(inHausBaseDriver);
    }


  // Methods inherited from interface org.osgi.framework.BundleActivator

  /**
   * 
   * 
   * @param bc 
   * @exception Exception
   */
  public void start(BundleContext bc) throws Exception {

		preStart();
		
		/*Assigning value to variable bc in order to be useful in serviceChanged() method.*/
		this.bc = bc;
		try {

		/*!--------------PART FOR REGISTERING SERVICES--------------*/
			/*Registering service with name "de.inhaus.inHausLamp.Lamp".*/
			/*INITIALIZE SERVICE OBJECT lamp FOR PROPER REGISTRATION OF THE UNDERLYING SERVICE!*/
			regLamp = bc.registerService("de.inhaus.inHausLamp.Lamp", lamp, props);
			bc.registerService( "org.osgi.service.device.Device", osgi_dev, props);
			/*!--------------END OF PART FOR REGISTERING SERVICES--------------*/
			/*!--------------PART FOR GETTING SERVICES--------------*/
			/*Getting ServiceReference for service with name "InHausBasedriver.InHausBasedriver" through BundleContext passed to start method.*/
			refInHausBaseDriver = bc.getServiceReference(SERVICE_INHAUSBASEDRIVER);
			if (refInHausBaseDriver != null)
			  { /*If a service with such name is already registered in the framework.*/
				/*Getting service object.*/
				  inHausBaseDriver = (InHausBaseDriver)bc.getService(refInHausBaseDriver);
			}
			/*!--------------END OF PART FOR GETTING SERVICES--------------*/
			
			/*!--------------PART FOR ADDING CLASS AS LISTENER--------------*/
			/*Adding this class as listener for ServiceEvents for services which names 
			  agree with the value of LDAP filter 'FILTER'.*/
			bc.addServiceListener(this, FILTER);
			/*!--------------END OF PART FOR ADDING LISTENERS--------------*/
		

		} catch (Exception e) {
			/*Logging error via org.osgi.service.log.LogService or com.prosyst.util.ref.Log 
			and printing the stack trace of the thrown exception.*/
			dump("[Lamp]Error in start method: " + e.getMessage(), e);

			
			throw new BundleException("[Lamp]Error in start method: " + e.getMessage());
		}
    postStart();
  
  }

  /**
   * 
   * 
   * @param bc 
   * @exception Exception
   */
  public void stop(BundleContext bc) throws Exception {
		/*!--------------PART FOR UNGETTING SERVICES--------------*/
		if (refInHausBaseDriver != null) {
			/*Ungetting service if the ServiceReference is not null, i.e. the service has been gotten.*/
			bc.ungetService(refInHausBaseDriver);
		}
		
		
		/*!--------------END OF PART FOR UNGETTING SERVICES--------------*/
		/*!--------------PART FOR REMOVING SERVICE LISTENERS--------------*/
		/*Removing this class as listener for any ServiceEvents.*/
		bc.removeServiceListener(this);
		/*!--------------END OF PART FOR REMOVING SERVICE LISTENERS--------------*/
		try {/*!--------------PART FOR UNREGISTERING SERVICE--------------*/
			/*Unregistering the underlying service if the registration is successful, i.e. field "regLamp" is not null.*/
			if (regLamp != null) {
				regLamp.unregister();
				regLamp = null;
			}
			lamp = null;
					
			/*!--------------END PART FOR UNREGISTERING SERVICES--------------*/
			

		} catch (Exception e) {
			/*Logging error via org.osgi.service.log.LogService or com.prosyst.util.ref.Log 
			and printing the stack trace of the thrown exception.*/
			dump("[Lamp]Error in stop method: " + e.getMessage(), e);
			
throw new BundleException("[Lamp]Error in stop method: " + e.getMessage());
		}

    
  }

	/** Method inherited from interface org.osgi.framework.ServiceListener.
	 *  Called when some ServiceEvent is processed - a service is registered,
	 *  unregistered or modified.
	 */
	public void serviceChanged(ServiceEvent event)  {
		/*DO NOT CHANGE THIS CODE!!!IT'S AUTOMATICALLY GENERATED*/
		switch (event.getType()) {
			case ServiceEvent.REGISTERED : {/*Service has been registered.*/
				/*Constructing an array to hold all names, that the registered service has been registered with.*/
				o("ServiceEvent.REGISTERED");
				String[] classNames = (String[])event.getServiceReference().getProperty("objectClass");
				for (	int k = 0; k < classNames.length; k++) {
					String objectClass = classNames[k];
					/*Searching for service with name "InHausBasedriver.InHausBasedriver".*/
					if (objectClass.equals(SERVICE_INHAUSBASEDRIVER)) {
						/*!--------------PART FOR GETTING SERVICES--------------*/
						/*Getting ServiceReference for service with name "InHausBasedriver.InHausBasedriver" through BundleContext passed to start method.*/
						refInHausBaseDriver = bc.getServiceReference(SERVICE_INHAUSBASEDRIVER);
						if (refInHausBaseDriver != null) { /*If a service with such name is already registered in the framework.*/
							/*Getting service object.*/
							inHausBaseDriver = (InHausBaseDriver)bc.getService(refInHausBaseDriver);
				      o("inHausBaseDriver = (InHausBaseDriver)bc.getService(refInHausBaseDriver);");
				      lamp.setDriver(inHausBaseDriver);
						}
						/*!--------------END OF PART FOR GETTING SERVICES--------------*/

						break;
					}
				}
				break;
			}
			case ServiceEvent.UNREGISTERING : {/*Service has been unregistered*/
				o("ServiceEvent.UNREGISTERING");
				/*Constructing an array to hold all names, that the registered service has been registered with.*/
				String[] classNames = (String[])event.getServiceReference().getProperty("objectClass");
				for (	int k_1 = 0; k_1 < classNames.length; k_1++) {
					String objectClass = classNames[k_1];
					/*Searching for service with name "InHausBasedriver.InHausBasedriver".*/
					if (objectClass.equals(SERVICE_INHAUSBASEDRIVER)) {
						/*!--------------PART FOR UNGETTING SERVICES--------------*/
						if (refInHausBaseDriver != null) {
							/*Ungetting service if the ServiceReference is not null, i.e. the service has been gotten.*/
							bc.ungetService(refInHausBaseDriver);
							lamp.setDriver(null);
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


	/** Common method for printing the stack  
	 *  traces of exceptions and logging errors.
	 *  by com.prosyst.util.ref.Log.
	 */
	public static void dump(String str, Throwable t)  {
		if (t != null) {
			t.printStackTrace();
		}
	}




}
