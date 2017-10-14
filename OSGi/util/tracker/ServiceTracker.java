/*
 * Copyright (c) 2003, KNOPFLERFISH project
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials
 *   provided with the distribution.
 *
 * - Neither the name of the KNOPFLERFISH project nor the names of its
 *   contributors may be used to endorse or promote products derived
 *   from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 * COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

package org.osgi.util.tracker;

import org.osgi.framework.*;
import java.util.*;

/**
 * Utility class for grabbing services matching certain criteria.
 *
 * <p>
 * Typical use would be:
 *
 * <pre>
 *  // Create and open a tracker for the log service
 *  ServiceTracker logTracker = 
 *         new ServiceTracker(bc, LogService.class.getname(), null);
 * 
 *  logTracker.open(); 
 * 
 *  ...
 *
 *  // use the service
 *  ((LogService)logTracker.getService()).log(LogService.LOG_INFO, "message"));
 *
 * </pre>
 * Note that the above code might fail with <tt>NullPointerException</tt>
 * if no <tt>LogService</tt> is available.
 */
public class ServiceTracker implements ServiceTrackerCustomizer {

  // Context used for getting services
  protected BundleContext            bc;

  // Either a user-specified filter, or one generated by the
  // convenience constructors.
  protected Filter                   filter;

  // possibly /this/ if no customizer is specified
  protected ServiceTrackerCustomizer customizer;

  // ServiceReference - Service Object
  protected Hashtable tracked = new Hashtable();

  // Listener doing all the work - only non-null when tracker is open
  protected ServiceListener srListener = null;

  // Cache best service reference and it's rank
  protected ServiceReference bestSR         = null;
  protected long             bestSRRank     = -1;
  protected long             bestSRID       = -1;

  // increment at every add/remove
  protected int              trackingCount  = 0;

  /**
   * Create a <tt>ServiceTracker</tt> object on the 
   * specified <tt>ServiceReference</tt> object.
   *
   * <p>The service referenced by the specified 
   * <tt>ServiceReference</tt> object
   * will be tracked by this <tt>ServiceTracker</tt> object.
   *
   * @param context   <tt>BundleContext</tt> object against which 
   *                  the tracking is done.
   * @param reference <tt>ServiceReference</tt> object for the 
   *                  service to be tracked.
   * @param customizer The customizer object to call when services are
   *                   added, modified, or removed in this
   *                   <tt>ServiceTracker</tt> object.
   *                   If customizer is <tt>null</tt>, then this 
   *                   <tt>ServiceTracker</tt> object will be used
   *                   as the <tt>ServiceTrackerCustomizer</tt> object 
   *                   and the <tt>ServiceTracker</tt> object will call 
   *                   the <tt>ServiceTrackerCustomizer</tt> methods on 
   *                   itself.
   */
  public ServiceTracker(BundleContext            context, 
			ServiceReference         reference,
			ServiceTrackerCustomizer customizer)
  {
    try {
      Filter filter = context
	.createFilter("(" + Constants.SERVICE_ID + "=" +
		      reference.getProperty(Constants.SERVICE_ID) + ")");
      
      init(context, filter, customizer);
    } catch (InvalidSyntaxException e) {
      throw new RuntimeException("Filter syntax error: " + e.getMessage());
    }
  }

  public String toString() {
    return "ServiceTracker[" + 
      "filter=" + filter + 
      ", size=" + size() + 
      ", bestId=" + bestSRID + 
      "]";
  }

  
  /**
   * Create a <tt>ServiceTracker</tt> object on the specified class name.
   *
   * <p>Services registered under the specified class name will be tracked
   * by this <tt>ServiceTracker</tt> object.
   *
   * @param context   <tt>BundleContext</tt> object against which the 
   *                  tracking is done.
   * @param clazz     Class name of the services to be tracked.
   * @param customizer The customizer object to call when services are
   *                   added, modified, or removed in 
   *                   this <tt>ServiceTracker</tt> object.
   *                   If customizer is <tt>null</tt>, then this 
   *                   <tt>ServiceTracker</tt> object will be used
   *                   as the <tt>ServiceTrackerCustomizer</tt> object
   *                   and the <tt>ServiceTracker</tt> object
   *                   will call the <tt>ServiceTrackerCustomizer</tt> 
   *                   methods on itself.
   *
   * @throws IllegalArgumentException if the class name is <tt>null</tt>,
   *                                  empty string or causes a 
   *                                  <tt>InvalidSyntaxException</tt> 
   */
  public ServiceTracker(BundleContext            context, 
			String                   clazz,
			ServiceTrackerCustomizer customizer)
  {

    if(clazz == null) {
      throw new IllegalArgumentException("Class name can't be null");
    }
    if("".equals(clazz)) {
      throw new IllegalArgumentException("Class name can't be empty string");
    }
    
    try {
      Filter filter = context
	.createFilter("(" + Constants.OBJECTCLASS + "=" +  clazz + ")");
      
      init(context, filter, customizer);
    } catch (InvalidSyntaxException e) {
      throw new IllegalArgumentException("Filter syntax error: " + e.getMessage() + " - possibly junk in the class name=\"" + clazz + "\"");
    }
  }

  /**
   * Create a <tt>ServiceTracker</tt> object on the 
   * specified <tt>Filter</tt> object.
   *
   * <p>Services which match the specified <tt>Filter</tt> object 
   * will be tracked by this <tt>ServiceTracker</tt> object.
   *
   * @param context   <tt>BundleContext</tt> object against which the 
   *                  tracking is done.
   * @param filter    <tt>Filter</tt> object to select the 
   *                  services to be tracked.
   * @param customizer The customizer object to call when services are
   *                   added, modified, or removed in this 
   *                   <tt>ServiceTracker</tt> object.
   *                   If customizer is null, then this
   *                   <tt>ServiceTracker</tt> object will be used
   *                   as the <tt>ServiceTrackerCustomizer</tt> object 
   *                   and the <tt>ServiceTracker</tt>
   *                   object will call the <tt>ServiceTrackerCustomizer</tt>
   *                   methods on itself.
   * @since 1.1
   */
  public ServiceTracker(BundleContext            context, 
			Filter                   filter,
			ServiceTrackerCustomizer customizer)
  {
    init(context, filter, customizer);
  }

  /**
   * Used by all constrcutors.
   *
   * <p>
   * If <i>customizer</i> is <tt>null</tt>, use the 
   * <tt>ServicerTracker.this</tt> as customizer.
   *
   * @throws IllegalArgumentException if filter is <tt>null</tt>
   */
  private void init(BundleContext            context, 
		    Filter                   filter,
		    ServiceTrackerCustomizer customizer)
    
  {
    if(filter == null) {
      throw new IllegalArgumentException("Filter can't be null");
    }

    this.bc         = context;
    this.filter     = filter;
    this.customizer = customizer != null ? customizer : this;
  }
  


  /**
   * Open this <tt>ServiceTracker</tt> and begin tracking services.
   *
   * <p>Tracking is done by adding a <tt>ServiceListener</tt> to the 
   * bundlecontext used when creating the tracker.
   *
   * <p>If the tracker is already opened, do nothing.
   *
   * @throws java.lang.IllegalStateException if the <tt>BundleContext</tt>
   *         object with which this <tt>ServiceTracker</tt> object was 
   *         created is no longer valid.
   */
  public void open() {
    
    if(srListener != null) {
      return; // already  open
    }
    
    trackingCount = 0;

    // create listener and add it. Also call REGISTERED for
    // all services already installed
    srListener = new ServiceListener() {
	public void serviceChanged(ServiceEvent ev) {
	  ServiceReference ref = ev.getServiceReference();
	  
	  switch(ev.getType()) {
	  case ServiceEvent.REGISTERED:
	    {
	      if(filter.match(ref)) {
		Object obj = null;
		
		// ask customizer for actual service object
		try {
		  obj = customizer.addingService(ref);
		  add(ref, obj);  	
		} catch (Exception verybadstyle) {
		  verybadstyle.printStackTrace();
		}
	      }
	    }
	    break;
	  case ServiceEvent.MODIFIED:  
	    {
	      if(!filter.match(ref)) {
		remove(ref);
		
	      } else {
		
		try {
		  // We have possibly regained an object after a property 
		  // change
		  Object obj = tracked.get(ref);

		  if(obj == null) {
		    // if we don't have it, ask customizer for actual
		    // service object
		    obj = customizer.addingService(ref);
		    
		    add(ref, obj);
		  }
		  
		  // ...and check again and most likely call
		  // modifiedService()
		  if(obj != null) {
		    customizer.modifiedService(ref, obj);
		    
		    updateBest(ref);
		  }
		} catch (Exception verybadstyle) {
		  verybadstyle.printStackTrace();
		}
	      }	      
	    }
	    break;
	  case ServiceEvent.UNREGISTERING:
	    {
	      if(filter.match(ref)) {
		remove(ref);
	      }
	      
	      
	    }
	    break;
	  }
	}
      };
    
    // Add the listener and initialize all services already registered
    // Note that we listen for ALL services and filter them later
    // in the listener. This is because the tracker spec says we 
    // should be able to catch property modifications outside of the
    // current filter.
    try {
      String anyFilter = null;
      bc.addServiceListener(srListener, anyFilter);
      
      // we have to do this manually the first time
      ServiceReference[] srl = 
	bc.getServiceReferences(null, anyFilter);
      
      for(int i = 0; srl != null && i < srl.length; i++) {
	srListener.serviceChanged(new ServiceEvent(ServiceEvent.REGISTERED,
						   srl[i]));
      }
    } catch (Exception verybadstyle) {
      verybadstyle.printStackTrace();
    }
    
    //    System.out.println("***** created tracker for #" + bc.getBundle().getBundleId() +  ", filter=" + filter);
  }
  
  /**
   * Close this <tt>ServiceTracker</tt> object.
   *
   * <p>This method should be called when this <tt>ServiceTracker</tt> object
   * should end the tracking of services.
   *
   * <p>If the tracker is not opened, do nothing.
   */  
  public void close() {
    
    if(srListener != null) {
      
      // off we go
      try {
	bc.removeServiceListener(srListener);
      } catch (IllegalStateException ignored) {
	//	System.err.println("ServiceTracker.close called on invalid bundle " + bc);
	// Simply ignore bad bundles
      }
    
      // Cleanup on a clone to avoid remove/next problems in hashtable
      Hashtable tr = (Hashtable)tracked.clone();
      
      // and do all the cleanup in the same way we did the initialization
      for(Enumeration e = tr.keys(); e.hasMoreElements(); ) {
	ServiceReference ref = (ServiceReference)e.nextElement();
	remove(ref);
      }

      srListener = null;
    }
    trackingCount = 0;
  }
  
  /**
   * Calls the <tt>close</tt> method.
   */
  protected void finalize() throws Throwable {
    close();
  }
  
  /**
   * Default implementation of the 
   * <tt>ServiceTrackerCustomizer.addingService</tt> method.
   *
   * <p>This method is only called when this <tt>ServiceTracker</tt> object
   * has been constructed with a <tt>null</tt> 
   * <tt>ServiceTrackerCustomizer</tt> argument.
   *
   * The default implementation returns the result of
   * calling <tt>getService(reference)</tt>.
   *
   * <p>This method can be overridden to customize
   * the service object to be tracked for the service
   * being added.
   *
   * @param reference Reference to service being added to this
   *                  <tt>ServiceTracker</tt> object.
   * @return The service object to be tracked for the service
   *         added to this <tt>ServiceTracker</tt> object.
   */
  public Object addingService(ServiceReference reference) {
    Object obj = bc.getService(reference);

    return obj;
  }
  
  /**
   * Default implementation of the 
   * <tt>ServiceTrackerCustomizer.modifiedService</tt> method.
   *
   * <p>This method is only called when this <tt>ServiceTracker</tt> object
   * has been constructed with a <tt>null</tt> 
   * <tt>ServiceTrackerCustomizer</tt> argument.
   *
   * The default implementation does nothing.
   *
   * @param reference Reference to modified service.
   * @param service The modified service.
   */
  public void modifiedService(ServiceReference reference, Object service) {
    //     System.out.println("---- modifiedService " + reference.getProperty(Constants.SERVICE_ID));
  }
  
  /**
   * Default implementation of the 
   * <tt>ServiceTrackerCustomizer.removedService</tt> method.
   *
   * <p>This method is only called when this <tt>ServiceTracker</tt> object
   * has been constructed with a <tt>null</tt> 
   * <tt>ServiceTrackerCustomizer</tt> argument.
   *
   * The default implementation calls <tt>ungetService</tt>, on the
   * <tt>BundleContext</tt> object with which this 
   * <tt>ServiceTracker</tt> object was created,
   * passing the specified <tt>ServiceReference</tt> object.
   *
   * @param reference Reference to removed service.
   * @param service The service object for the removed service.
   */
  public void removedService(ServiceReference reference, Object service) {
    try {
      bc.ungetService(reference);
    } catch (IllegalStateException ignored) {
    }
  }
  
  /**
   * Wait for at least one service to be tracked by this 
   * <tt>ServiceTracker</tt> object.
   *
   * <p>
   * <b>Don't</b> use this method. You'll get bad karma for a long time.
   * If you really need the functionity, you're probably <b>much</b> 
   * better off using a <tt>ServiceListener</tt> directly.
   * </p>
   *
   * @param timeout time interval in milliseconds to wait.  If zero,
   *                 the method will wait indefinately.
   * @return Returns the result of <tt>getService()</tt>.
   */
  public Object waitForService(long timeout) throws InterruptedException  {
    if(srListener == null) {
      return null;
    }

    synchronized(tracked) {
      if (tracked.size() == 0) {
	tracked.wait(timeout);
      }
    }
    
    return getService();
  }
  
  /**
   * Return an array of <tt>ServiceReference</tt> objects for all services
   * being tracked.
   *
   * @return Array of <tt>ServiceReference</tt> objects or <tt>null</tt> 
   *         if no service are being tracked.
   */
  public ServiceReference[] getServiceReferences() {
    // This is just plain stupid. Should return an 
    // array of size zero instead /EW
    if(size() == 0) {
      return null;
    }

    ServiceReference[] srl = new ServiceReference[size()]; 
    int                n   = 0;

    for(Enumeration e = tracked.keys(); e.hasMoreElements(); ) {
      srl[n++] = (ServiceReference)e.nextElement();
    }
    return srl;
  }
  
  /**
   * Return an array of service objects for all services
   * being tracked.
   *
   * @return Array of service objects or <tt>null</tt> if no service
   *         are being tracked.
   */
  public Object[] getServices() {
    // This is just plain stupid. Should return an 
    // array of size zero instead /EW
    if(size() == 0) {
      return null;
    }

    Object[] srl = new Object[size()]; 
    int      n   = 0;

    for(Enumeration e = tracked.keys(); e.hasMoreElements(); ) {
      ServiceReference ref = (ServiceReference)e.nextElement();
      srl[n++]             = tracked.get(ref);
    }
    return srl;
  }

  /*
   * Update the bestSR and bestSR rank according to spec in 
   * <tt>getServiceReference</tt>.
   */
  protected void updateBest(ServiceReference sr) {
    synchronized(tracked) {
      
      long id   = ((Long)sr.getProperty(Constants.SERVICE_ID)).longValue();
      int  rank = 0;

      //      System.out.println("**** updateBest " + id);
      
      Object r = sr.getProperty(Constants.SERVICE_RANKING);
      if(r != null && (r instanceof Integer)) {
	rank = ((Integer)r).intValue();
      }
      
      if(bestSR == null) {
	bestSR     = sr;
	bestSRRank = rank;
	bestSRID   = id;
      } else {
	if(rank > bestSRRank) {
	  bestSR     = sr;
	  bestSRRank = rank;
	  bestSRID   = id;
	} else if(rank == bestSRRank) {
	  if(id < ((Long)bestSR.getProperty(Constants.SERVICE_ID)).longValue()) {
	    bestSR     = sr;
	    bestSRID   = id;
	  }
	}
      }
    }
  }


  /**
   * Returns a <tt>ServiceReference</tt> object for one of the services
   * being tracked by this <tt>ServiceTracker</tt> object.
   *
   * <p>If multiple services are being tracked, the service
   * with the highest ranking (as specified in its 
   * <tt>service.ranking</tt> property) is returned.
   * </p>
   *
   * <p>If there is a tie in ranking, the service with the lowest
   * service ID (as specified in its <tt>service.id</tt> property); that is,
   * the service that was registered first is returned.
   * </p>
   *
   * <p>This is the same algorithm used by 
   * <tt>BundleContext.getServiceReference</tt>.
   * </p>
   *
   * <p>
   * Implementation note: This code uses a cached best service reference for
   * quick retrieval. The cache is rebuilt at service 
   * registration/unregistation.
   * </p>
   *
   *
   * @return <tt>ServiceReference</tt> object or <tt>null</tt> 
   *         if no service is being tracked.
   * @since 1.1
   */
  public ServiceReference getServiceReference() {
    // avoid expensive loop of all references
    
    return bestSR;
  }
  
  /**
   * Returns the service object for the specified 
   * <tt>ServiceReference</tt> object
   * if the referenced service is being tracked.
   *
   * @param reference Reference to a service.
   * @return Service object or <tt>null</tt> if the service referenced by the
   * specified <tt>ServiceReference</tt> object is not being tracked.
   */
  public Object getService(ServiceReference reference) {
    Object obj = reference == null ? null : tracked.get(reference);

    return obj;
  }
  
  /**
   * Returns a service object for one of the services
   * being tracked by this <tt>ServiceTracker</tt> object.
   *
   * <p>If any services are being tracked, this method returns the result
   * of calling <tt>getService(getServiceReference())</tt>.
   *
   * @return Service object or <tt>null</tt> if no service is being tracked.
   */
  public Object getService() {
    return getService(getServiceReference());
  }
  
  /**
   * Remove a service from this <tt>ServiceTracker</tt> object.
   *
   * The specified service will be removed from this
   * <tt>ServiceTracker</tt> object.
   * If the specified service was being tracked then the
   * <tt>ServiceTrackerCustomizer.removedService</tt> method will be
   * called for that service.
   *
   * @param reference Reference to the service to be removed.
   */
  public void remove(ServiceReference reference)  {
    synchronized(tracked) {

      trackingCount++;
      Object obj = tracked.get(reference);

      if(obj != null) {

	// No longer tracked internally
	tracked.remove(reference);
	
	// Notify customizer
	try {
	  customizer.removedService(reference, obj);
	} catch (Exception verybadstyle) {
	  // Very bad style of customizer to throw exceptions, but
	  // catch them to be nice.
	  verybadstyle.printStackTrace();
	}

	// possibly rebuild cached best reference
	if(reference.equals(bestSR)) 
	{
	  bestSR = null;
	  
	  // OK - this is a bit expensive, but my guess is that
	  // getServiceReference() is called much more often than
	  // services gets uninstalled.
	  for(Enumeration e = tracked.keys(); e.hasMoreElements(); ) {
	    ServiceReference sr = (ServiceReference)e.nextElement();
	    updateBest(sr);
	  }
	}
      }
    }
  }
  
  /**
   * Number of services being tracked.
   */  
  public int size() {
    return tracked.size();
  }  

  public int getTrackingCount() {
    return trackingCount;
  }
  
  private void add(ServiceReference ref, Object obj) {
    if(obj != null) {
      synchronized(tracked) {
	tracked.put(ref, obj);
	trackingCount++;
	

	// update cached service reference
	updateBest(ref);
	
	// if someone is waiting in waitForService()
	tracked.notifyAll(); 			
      }
    }
  }
  boolean doLog() {
    return (bc.getBundle().getBundleId() == 0)
      && -1 != filter.toString().indexOf("osgitest2");
  }
}

